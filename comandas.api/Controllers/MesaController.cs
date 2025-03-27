

using Comandas.API.Models;
using Microsoft.AspNetCore.Mvc;
using Comandas.API.DTOs;
using Microsoft.VisualBasic;
using Swashbuckle.AspNetCore.Annotations;


namespace Comandas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MesaController:ControllerBase{
            
        [HttpGet("{id}")]
        [SwaggerResponse(200,"Retorna uma mesa", typeof(MesaGetDTO))]
        public ActionResult<MesaGetDTO> GetMesa(int id) {
            
            return Ok(new MesaGetDTO(){Id = 1, NumeroMesa = 1, SituacaoMesa = 1});    
        
        }
        
        [HttpGet]
        [SwaggerResponse(200,"Retorna uma lista de mesas.",typeof(IEnumerable<MesaGetDTO>))]
        public ActionResult<IEnumerable<MesaGetDTO>> GetMesas(){
            return Ok(new List<MesaGetDTO>(){new MesaGetDTO(){Id = 1, NumeroMesa = 1, SituacaoMesa = 2}
                                            ,new MesaGetDTO(){Id = 2, NumeroMesa = 2, SituacaoMesa = 2}
                                            });
        }

        [HttpPost]
        [SwaggerResponse(201,"Edita uma mesa", typeof(MesaPostDTO))]
        public ActionResult<MesaPostDTO> PostMesa(MesaPostDTO mesaPostDTO){
            
            var Mesa = new MesaPostDTO();

            return CreatedAtAction("GetMesa",new{id=1},Mesa);

        }

        [HttpPut]
        [SwaggerResponse(204,"Altera a situação de uma mesa")]
        [SwaggerResponse(400,"Id da mesa informada não é o mesmo do corpo")]
        public ActionResult<MesaPutDTO> PutMesa(int id, MesaPutDTO mesaPutDTO){
            
            if(id != mesaPutDTO.Id){
                return BadRequest();
            }

            mesaPutDTO.SituacaoMesa = 2;

            return NoContent();

        }

        [HttpDelete]
        [SwaggerResponse(204,"Exclui uma mesa")]
        public ActionResult DeleteMesa(int id){
            return NoContent();
        }

    }

}



