using Microsoft.AspNetCore.Mvc;
using Comandas.Shared.DTOs;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Comandas.Services.Interfaces;


namespace Comandas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MesaController:ControllerBase
    {

        public readonly IMesaService _mesaServices;

        public MesaController(IMesaService mesaService){
            _mesaServices = mesaService;
        }



        [HttpGet("{id}")]
        [SwaggerResponse(200,"Retorna uma mesa", typeof(MesaGetDTO))]
        public async Task<ActionResult<MesaGetDTO>> GetMesa(int Id) {

            var mesa = await _mesaServices.GetMesa(Id);

            if (mesa is null)
            {
                return NotFound("Usuario não encontrado.");
            }

            return Ok(mesa);    
        
        }

        [HttpGet]
        [SwaggerResponse(200, "Retorna uma lista de mesas.", typeof(IEnumerable<MesaGetDTO>))]
        public async Task<ActionResult<IEnumerable<MesaGetDTO>>> GetMesas()
        {

            return Ok(await _mesaServices.GetMesas());

        }

        [HttpPost]
        [SwaggerResponse(201,"Cria uma mesa", typeof(MesaPostDTO))]
        public async Task<ActionResult<MesaPostDTO>> PostMesa(MesaPostDTO mesaPostDTO){

            Task<MesaResponsePostDTO> mesaResponse = _mesaServices.PostMesa(mesaPostDTO);

            return CreatedAtAction("GetMesa",new{id=mesaResponse.Id},mesaResponse);

        }

        [HttpPut("{id}")]
        [SwaggerResponse(204,"Altera uma mesa")]
        [SwaggerResponse(400,"Id da mesa informada não é o mesmo do corpo")]
        public async Task<ActionResult<MesaPutDTO>> PutMesa(int id, MesaPutDTO mesaPutDTO){
            
            if(id != mesaPutDTO.Id){
                return BadRequest();
            }

            if (await _mesaServices.PutMesa(mesaPutDTO))
            {
                return NoContent();
            }

                return UnprocessableEntity();

        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204,"Exclui uma mesa")]
        public async Task<ActionResult> DeleteMesa(int Id){

            if (await _mesaServices.DeleteMesa(Id))
            {
                return NoContent();
            }
            
                return UnprocessableEntity();
        }

    }

}



