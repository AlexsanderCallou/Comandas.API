using Comandas.API.DataBase;
using Comandas.API.DTOs;
using Microsoft.AspNetCore.Authorization;
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

    }
    enum SituacaoComanda{
        Aberto = 1,
        Fechado = 2
    }
}