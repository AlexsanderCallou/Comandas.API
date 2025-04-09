

using Comandas.API.Models;
using Microsoft.AspNetCore.Mvc;
using Comandas.API.DTOs;
using Microsoft.VisualBasic;
using Swashbuckle.AspNetCore.Annotations;
using Comandas.API.DataBase;
using Microsoft.EntityFrameworkCore;


namespace Comandas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MesaController:ControllerBase{

        public readonly ComandasDBContext _banco;

        public MesaController(ComandasDBContext comandasDBContext)
        {
            _banco = comandasDBContext;
        }



        [HttpGet("{id}")]
        [SwaggerResponse(200,"Retorna uma mesa", typeof(MesaGetDTO))]
        public async Task<ActionResult<MesaGetDTO>> GetMesa(int id) {
            
            var mesa = await _banco.Mesas.FirstAsync(m => m.Id == id);

            return Ok(new MesaGetDTO(){Id = mesa.Id, NumeroMesa = mesa.NumeroMesa, SituacaoMesa = mesa.SituacaoMesa});    
        
        }
        
        [HttpGet]
        [SwaggerResponse(200,"Retorna uma lista de mesas.",typeof(IEnumerable<MesaGetDTO>))]
        public async Task<ActionResult<IEnumerable<MesaGetDTO>>> GetMesas(){
            
            var mesas = await _banco.Mesas.ToListAsync();


            return Ok( mesas.Select(m => new MesaGetDTO{
                                            Id = m.Id, 
                                            NumeroMesa = m.NumeroMesa, 
                                            SituacaoMesa = m.SituacaoMesa
                                            }
                                    )
                    );
        }

        [HttpPost]
        [SwaggerResponse(201,"Edita uma mesa", typeof(MesaPostDTO))]
        public ActionResult<MesaPostDTO> PostMesa(MesaPostDTO mesaPostDTO){
            
            var Mesa = new MesaPostDTO();

            return CreatedAtAction("GetMesa",new{id=1},Mesa);

        }

        [HttpPut("{id}")]
        [SwaggerResponse(204,"Altera a situação de uma mesa")]
        [SwaggerResponse(400,"Id da mesa informada não é o mesmo do corpo")]
        public ActionResult<MesaPutDTO> PutMesa(int id, MesaPutDTO mesaPutDTO){
            
            if(id != mesaPutDTO.Id){
                return BadRequest();
            }

            mesaPutDTO.SituacaoMesa = 2;

            return NoContent();

        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204,"Exclui uma mesa")]
        public ActionResult DeleteMesa(int id){
            return NoContent();
        }

    }

}



