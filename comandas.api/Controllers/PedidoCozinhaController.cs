using Comandas.API.DataBase;
using Comandas.Shared.DTOs;
using Comandas.Shared.Enumeration;
using Comandas.Data;
using Comandas.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Comandas.API.Controllers{


    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PedidoCozinhaController : ControllerBase
    {

        private readonly ComandasDBContext _banco;

        public PedidoCozinhaController(ComandasDBContext comandasDBContext)
        {
            _banco = comandasDBContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoCozinhaGetDTO>>> GetPedidoCozinha([FromQuery]int situacaoPedidoCozinha)
        {

            var pedidoCozinha = await _banco.PedidosCozinha
                                            .Include(c => c.Comanda)
                                            .Include(c => c.PedidoCozinhaItems)
                                                .ThenInclude(c => c.ComandaItem)
                                                    .ThenInclude(c => c.CardapioItem)
                                            .Where(c => c.SituacaoId == situacaoPedidoCozinha)
                                            .Select(c => new PedidoCozinhaGetDTO{
                                                Id = c.Id,
                                                NumeroMesa = c.Comanda.NumeroMesa,
                                                NomeCliente = c.Comanda.NomeCliente,
                                                TituloItem = c.PedidoCozinhaItems.First().ComandaItem.CardapioItem.Titulo
                                            }).OrderBy(c => c.Id).ToListAsync();

            return Ok(pedidoCozinha);

        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchPedidoCozinha([FromRoute]int id, [FromBody]PedidioCozinhaPatchDTO pedidioCozinhaPatchDTO)
        {

            var pedidoCozinha = await _banco.PedidosCozinha.FirstOrDefaultAsync(c => c.Id == id);

            if(pedidoCozinha is null){
                return BadRequest("Pedido não encontrado.");
            }

            pedidoCozinha.SituacaoId = pedidioCozinhaPatchDTO.SituacaoPedidoCozinhaId;

            await _banco.SaveChangesAsync();

            return NoContent();

        }
    }
}