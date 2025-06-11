using Comandas.API.DataBase;
using Comandas.API.Models;
using Comandas.Domain;
using Comandas.Data;
using Comandas.Shared.DTOs;
using Comandas.Shared.DTOs.Item;
using Comandas.Shared.Enumeration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comandas.Services.Interface;

namespace Comandas.API.Controllers
{


    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class ComandaController : ControllerBase
    {
        private readonly IComandaService _comandaService;
        private readonly IMesaService _mesaService;
        private readonly ILogger<ComandaController> _logger;
        private readonly ComandasDBContext _banco;

        public ComandaController(ComandasDBContext comandasDBContext,
                                ILogger<ComandaController> logger,
                                IComandaService comandaService,
                                IMesaService mesaService)
        {
            _banco = comandasDBContext;
            _logger = logger;
            _comandaService = comandaService;
            _mesaService = mesaService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComandaGetDTO>> GetComanda(int id)
        {
            return Ok(_comandaService.GetComanda(id));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComandaGetDTO>>> GetComandas([FromQuery] ComandaFilterDTO comandaFilterDTO)
        {
            if (comandaFilterDTO.SituacaoComanda is null)
            {
                return BadRequest("Deve-se informar a situação das comandas.");
            }
            return Ok(await _comandaService.GetComandas((int)comandaFilterDTO.SituacaoComanda));
        }

        [HttpPost]
        public async Task<ActionResult<ComandaGetDTO>> PostComanda(ComandaPostDTO comandaPostDTO)
        {

            if (!await _mesaService.MesaExiste(comandaPostDTO.NumeroMesa))
            {
                return BadRequest("Mesa não encontrada.");
            }

            if (!await _mesaService.MesaDesocupada(comandaPostDTO.NumeroMesa))
            {
                return BadRequest($"Mesa {comandaPostDTO.NumeroMesa} esta ocupada.");
            }

            //TODO validar se os ids dos itens do cardapio existem

            var comandaInsert = await _comandaService.PostComanda(comandaPostDTO);

            if (comandaInsert is null)
            {
                return BadRequest("Comanda não foi salva.");
            }

            return CreatedAtAction("GetComanda", new { id = comandaInsert.Id }, comandaInsert);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutComanda(int id, ComandaPutDTO comandaPutDTO)
        {

            if (id != comandaPutDTO.Id)
            {
                return BadRequest();
            }

            var comanda = await _banco.Comandas.FirstOrDefaultAsync(c => c.Id == id);

            if (comanda is null)
            {
                return NotFound("Comanda não encontrada.");
            }


            //verificar se foi informado uma nova mesa. 

            if (comandaPutDTO.NumeroMesa > 0 && comandaPutDTO.NumeroMesa != comanda.NumeroMesa)
            {

                var mesa = await _banco.Mesas.FirstOrDefaultAsync(c => c.NumeroMesa == comandaPutDTO.NumeroMesa);

                if (mesa is null)
                {
                    return NotFound("Mesa não encontrada");
                }

                if (mesa.SituacaoMesa == (int)SituacaoMesa.Ocupada)
                {
                    return BadRequest($"Mesa {mesa.NumeroMesa} está ocupada.");
                }

                mesa.SituacaoMesa = (int)SituacaoMesa.Ocupada;

                var mesaAtual = await _banco.Mesas.FirstOrDefaultAsync(c => c.NumeroMesa == comanda.NumeroMesa);

                mesaAtual!.SituacaoMesa = (int)SituacaoMesa.Disponivel;

                comanda.NumeroMesa = mesa.NumeroMesa;

            }

            //verificar se foi informado um novo nome p o cliente.

            if (!string.IsNullOrEmpty(comandaPutDTO.NomeCliente))
            {

                comanda.NomeCliente = comandaPutDTO.NomeCliente;

            }

            //percorrer os itens da comanda e verificar se eh uma exclusao.

            var itensExcluir = new List<int>();

            itensExcluir = comandaPutDTO.ComandaItems.Where(c => c.Excluir).Select(c => c.Id).ToList();

            if (itensExcluir.Any())
            {

                var comandaItensExcluir = await _banco.ComandaItems.Where(c => itensExcluir.Contains(c.Id)).ToListAsync();

                if (!comandaItensExcluir.Any())
                {
                    return BadRequest("Nenhum id de item informado.");
                }

                _banco.ComandaItems.RemoveRange(comandaItensExcluir);

            }


            //verificar se eh para adicionar um novo item. 

            var idsAdd = new List<int>();

            idsAdd = comandaPutDTO.ComandaItems.Where(c => c.Excluir == false)
                                                        .Select(c => c.CardapioItemId).ToList();

            if (idsAdd.Any())
            {

                List<ComandaItem> comandaItens = idsAdd.Select(c =>
                                                                new ComandaItem
                                                                {
                                                                    Comanda = comanda,
                                                                    CardapioItemId = c
                                                                }).ToList();

                _banco.ComandaItems.AddRange(comandaItens);

                foreach (ComandaItem comandaItem in comandaItens)
                {

                    var cardapioItem = await _banco.CardapioItems.FirstOrDefaultAsync(c => c.Id == comandaItem.CardapioItemId);

                    if (cardapioItem!.PossuiPreparo)
                    {

                        var pedidoCozinha = new PedidoCozinha
                        {
                            Comanda = comanda,
                            SituacaoId = (int)SituacaoPedidoCozinha.Pendente
                        };

                        await _banco.PedidosCozinha.AddAsync(pedidoCozinha);

                        var pedidoCozinhaItem = new PedidoCozinhaItem
                        {
                            PedidoCozinha = pedidoCozinha,
                            ComandaItem = comandaItem
                        };

                        await _banco.PedidoCozinhaItems.AddAsync(pedidoCozinhaItem);
                    }
                }
            }

            //fazer a persistencia dos dados.

            try
            {

                await _banco.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Erro interno no servidor.");

                return StatusCode(500, "Ouve um erro interno no sistema.");

            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteComanda(int id)
        {

            var comanda = await _banco.Comandas.FindAsync(id);

            if (comanda is null)
            {
                NotFound($"Comanda não encontrada {id}");
            }

            var comandaItems = await _banco.ComandaItems.Include(c => c.CardapioItem).Where(c => c.ComandaId == comanda.Id).ToListAsync();

            var comandaItemsPreparo = comandaItems.Where(c => c.CardapioItem.PossuiPreparo).ToList();

            foreach (var item in comandaItemsPreparo)
            {

                var pedidoCozinhaItem = await _banco.PedidoCozinhaItems.Include(c => c.PedidoCozinha).Where(c => c.ComanadaItemId == item.Id).FirstAsync();

                if (pedidoCozinhaItem is not null)
                {
                    _banco.PedidoCozinhaItems.Remove(pedidoCozinhaItem);
                    _banco.PedidosCozinha.Remove(pedidoCozinhaItem.PedidoCozinha);
                }

            }

            if (comandaItems.Any())
            {

                _banco.ComandaItems.RemoveRange(comandaItems);
            }

            _banco.Comandas.Remove(comanda);

            await _banco.SaveChangesAsync();

            return NoContent();

        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchComanda(int id, [FromBody] ComandaPatchDTO comandaPatchDTO)
        {

            var comanda = await _banco.Comandas.FindAsync(id);

            if (comanda is null)
            {
                return BadRequest("Comanda não encontrada.");
            }

            comanda.SituacaoComanda = comandaPatchDTO.SituacaoComanda;

            if (comandaPatchDTO.SituacaoComanda == (int)SituacaoComanda.Fechado)
            {

                var mesa = await _banco.Mesas.Where(c => c.NumeroMesa == comanda.NumeroMesa).FirstOrDefaultAsync();

                if (mesa is null)
                {
                    return BadRequest("Mesa não encontrada");
                }

                mesa.SituacaoMesa = (int)SituacaoMesa.Disponivel;

            }
            await _banco.SaveChangesAsync();

            return NoContent();

        }

    }

}
