

using Comandas.API.Models;
using Microsoft.AspNetCore.Mvc;
using Comandas.API.DTOs;
using Microsoft.VisualBasic;
using Swashbuckle.AspNetCore.Annotations;
using Comandas.API.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace Comandas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MesaController:ControllerBase{

        public readonly ComandasDBContext _banco;

        public MesaController(ComandasDBContext comandasDBContext){
            _banco = comandasDBContext;
        }



        [HttpGet("{id}")]
        [SwaggerResponse(200,"Retorna uma mesa", typeof(MesaGetDTO))]
        public async Task<ActionResult<MesaGetDTO>> GetMesa(int id) {
          
            var mesa = await _banco.Mesas.AsNoTracking().FirstAsync(m => m.Id == id);

            return Ok(new MesaGetDTO(){Id = mesa.Id, NumeroMesa = mesa.NumeroMesa, SituacaoMesa = mesa.SituacaoMesa});    
        
        }
        
        [HttpGet]
        [SwaggerResponse(200,"Retorna uma lista de mesas.",typeof(IEnumerable<MesaGetDTO>))]
        public async Task<ActionResult<IEnumerable<MesaGetDTO>>> GetMesas(){
            
            var mesas = await _banco.Mesas.AsNoTracking().ToListAsync();

            return Ok( mesas.Select(m => new MesaGetDTO{
                                            Id = m.Id, 
                                            NumeroMesa = m.NumeroMesa, 
                                            SituacaoMesa = m.SituacaoMesa
                                            }
                                    )
                    );
        }

        [HttpPost]
        [SwaggerResponse(201,"Cria uma mesa", typeof(MesaPostDTO))]
        public async Task<ActionResult<MesaPostDTO>> PostMesa(MesaPostDTO mesaPostDTO){
            
            var mesa = new Mesa(){
                NumeroMesa = mesaPostDTO.NumeroMesa,
                SituacaoMesa = mesaPostDTO.SituacaoMesa
            };

            _banco.Mesas.Add(mesa);

            await _banco.SaveChangesAsync();

            return CreatedAtAction("GetMesa",new{id=mesa.Id},mesa);

        }

        [HttpPut("{id}")]
        [SwaggerResponse(204,"Altera uma mesa")]
        [SwaggerResponse(400,"Id da mesa informada não é o mesmo do corpo")]
        public async Task<ActionResult<MesaPutDTO>> PutMesa(int id, MesaPutDTO mesaPutDTO){
            
            if(id != mesaPutDTO.Id){
                return BadRequest();
            }

            var mesa = await _banco.Mesas.FirstOrDefaultAsync(m => m.Id == id);

            if(mesa is null){
                return NotFound("Mesa não encontrada.");
            }

            mesa.NumeroMesa = mesaPutDTO.NumeroMesa;
            mesa.SituacaoMesa = mesaPutDTO.SituacaoMesa;

            await _banco.SaveChangesAsync();
            
            return NoContent();

        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204,"Exclui uma mesa")]
        public async Task<ActionResult> DeleteMesa(int id){

            var mesa = await _banco.Mesas.FirstOrDefaultAsync(m => m.Id == id);

            if (mesa is null){
                return NotFound("Mesa não encontrada");
            }

            _banco.Mesas.Remove(mesa);

            await _banco.SaveChangesAsync();

            return NoContent();
        }

    }

}



