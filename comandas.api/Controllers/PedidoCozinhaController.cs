using Comandas.API.DataBase;
using Comandas.Shared.DTOs;
using Comandas.Shared.Enumeration;
using Comandas.Data;
using Comandas.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Comandas.Services.Interface;

namespace Comandas.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PedidoCozinhaController : ControllerBase
    {

        private readonly IPedidoCozinhaService _pedidoCozinhaService;

        public PedidoCozinhaController(IPedidoCozinhaService pedidoCozinhaService)
        {
            _pedidoCozinhaService = pedidoCozinhaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoCozinhaGetDTO>>> GetPedidoCozinha([FromQuery] int situacaoPedidoCozinha)
        {

            return Ok(_pedidoCozinhaService.GetPedidoCozinha(situacaoPedidoCozinha));

        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchPedidoCozinha([FromRoute] int id, [FromBody] PedidioCozinhaPatchDTO pedidioCozinhaPatchDTO)
        {

            var pedidoCozinha = await _pedidoCozinhaService.PatchPedidoCozinha(id, pedidioCozinhaPatchDTO);

            if (pedidoCozinha == false)
            {
                return BadRequest("Pedido não encontrado.");
            }

            return NoContent();

        }
    }
}