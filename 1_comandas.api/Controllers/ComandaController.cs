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

        public ComandaController(ComandasDBContext comandasDbContext,
                                ILogger<ComandaController> logger,
                                IComandaService comandaService,
                                IMesaService mesaService)
        {
            _banco = comandasDbContext;
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

            if (!comanda.Success)
            {
                return StatusCode(comanda!.Errors!.First().ErrorCode, new { comanda!.Errors!.First().Message });
            }
 
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteComanda(int id)
        {
            var response = await _comandaService.DeleteComanda(id);
            
            if (!response.Success)
            {
                return StatusCode(response!.Errors!.First().ErrorCode, new { message = response!.Errors!.First().Message });
            }
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchComanda(int id, [FromBody] ComandaPatchDTO comandaPatchDto)
        {
            /*if (comandaPatchDTO.SituacaoComanda == (int)SituacaoComanda.Aberto)
            {
                return BadRequest("Serviço apenas para encerrar comanda.");
            }*/
            
            var response = await _comandaService.PatchComanda(id, comandaPatchDto);

            if (!response.Success)
            {
                return StatusCode(response!.Errors!.First().ErrorCode, new { message = response!.Errors!.First().Message });
            }
            
            return NoContent();

        }

    }

}
