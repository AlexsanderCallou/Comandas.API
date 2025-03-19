

using Comandas.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Comandas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MesaController:ControllerBase{
        
        [HttpGet("{id}")]
        public ActionResult GetMesa(int id) {
            return Ok(new Mesa(){Id = 1, NumeroMesa = 1, SituacaoMesa = 0}); 
        }

    }

}



