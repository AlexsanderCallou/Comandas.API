using Comandas.API.DataBase;
using Comandas.API.DTOs;
using Comandas.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Comandas.API.Controllers{


    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class ComandaController : ControllerBase{

        private readonly ComandasDBContext _banco;

        public ComandaController(ComandasDBContext comandasDBContext)
        {
            _banco = comandasDBContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComandaGetDTO>> GetComanda(int id){

            var comanda = await _banco.Comandas
                                .Include(c => c.ComandaItems)
                                    .ThenInclude(c => c.CardapioItem)
                                .FirstOrDefaultAsync(c => c.Id == id);


            if (comanda is null){
                return NotFound("Comanda não encontrada.");
            }

            var comandaGetDTO = new ComandaGetDTO {
                Id = comanda.Id,
                NomeCliente = comanda.NomeCliente,
                NumeroMesa = comanda.NumeroMesa,
                SituacaoComanda = comanda.SituacaoComanda,
                ComandaItems = comanda.ComandaItems.Select(c => new ComandaItemGetDTO{
                        Id = c.Id,
                        Titulo = c.CardapioItem.Titulo
                }).ToList()
            };

            return Ok(comandaGetDTO);
       }

       [HttpGet]
       public async Task<ActionResult<IEnumerable<ComandaGetDTO>>> GetComandas(){

        var comandas = await _banco.Comandas
                .Where(c => c.SituacaoComanda == (int)SituacaoComanda.Aberto)
                .Select(c => new ComandaGetDTO{
                    Id = c.Id,
                    NumeroMesa = c.NumeroMesa,
                    NomeCliente = c.NomeCliente,
                    SituacaoComanda = c.SituacaoComanda,

                    ComandaItems = c.ComandaItems
                        .Select(c => new ComandaItemGetDTO{
                            Id = c.Id,
                            Titulo = c.CardapioItem.Titulo
                        }).ToList()
                }).ToListAsync();


        return Ok(comandas);


       }

        [HttpPost]
        public async Task<ActionResult<ComandaGetDTO>> PostComanda(ComandaPostDTO comandaPostDTO){

            //verificar se a mesa esta disponivel

            var mesa = await _banco.Mesas.FirstOrDefaultAsync(c => c.NumeroMesa == comandaPostDTO.NumeroMesa);

            if (mesa is null){
                return BadRequest("Mesa não encontrada.");
            }

            if (mesa.SituacaoMesa == (int)SituacaoMesa.Ocupada){
                return BadRequest($"Mesa {mesa.NumeroMesa} esta ocupada.");
            }

            mesa.SituacaoMesa = (int)SituacaoMesa.Ocupada;

            //criar nova comanda.

            var comanda = new Comanda{
                NumeroMesa = comandaPostDTO.NumeroMesa,
                NomeCliente = comandaPostDTO.NomeCliente,
            };

            //salvar no contexto.

            await _banco.Comandas.AddAsync(comanda);

            //adcionar os itens.

            foreach(var item in comandaPostDTO.CardapioItens){                
                var comandaItem = new ComandaItem{
                    CardapioItemId = item,
                    Comanda = comanda
                };
                await _banco.ComandaItems.AddAsync(comandaItem);

                var cardapioItem = await _banco.CardapioItems.FirstOrDefaultAsync(c => c.Id == item);

                if (cardapioItem is null){
                    return BadRequest("Item não existe.");
                }

                if (cardapioItem.PossuiPreparo){
                    var pedidoCozinha = new PedidoCozinha{
                        Comanda = comanda,
                        SituacaoId = (int)SituacaoPedidoCozinha.Pendente,         
                    };
                
                    var pedidoCozinhaItem = new PedidoCozinhaItem{
                        PedidoCozinha = pedidoCozinha,
                        ComandaItem = comandaItem
                    };

                    await _banco.PedidosCozinha.AddAsync(pedidoCozinha);
                    await _banco.PedidoCozinhaItems.AddAsync(pedidoCozinhaItem);

                }
            };

            //salvar no banco.

            await _banco.SaveChangesAsync();

            //criar um novo comanda dto

            return CreatedAtAction("GetComanda", new {id = comanda.Id}, comanda);

        }

    }
    enum SituacaoComanda{
        Aberto = 1,
        Fechado = 2
    }
    enum SituacaoMesa{
        Disponivel = 0,
        Ocupada = 1
    }
    enum SituacaoPedidoCozinha{
        Pendente = 1,
        Andamento = 2,
        Finalizado = 3,
        Entregue = 4,
    }
}