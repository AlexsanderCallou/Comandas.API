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
            var getComanda = await _comandaService.GetComanda(id);
            if (getComanda is null)
            {
                return NotFound("Comanda não encontrada.");
            }
            return Ok(getComanda);
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

            var comandaInsert = await _comandaService.PostComanda(comandaPostDTO);

            if (!comandaInsert.Success)
            {
                return BadRequest(comandaInsert.Message);
            }

            return CreatedAtAction("GetComanda", new { id = comandaInsert.Data!.Id}, comandaInsert);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutComanda(int id, ComandaPutDTO comandaPutDTO)
        {

            if (id != comandaPutDTO.Id)
            {
                return BadRequest();
            }

            var comanda = await _comandaService.PutComanda(comandaPutDTO);
            //TODO falta ver a questao do retorno code, pois temos uma lista agora. 
            if (!comanda.Success) 
            {
                return NotFound("Comanda não encontrada.");
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
