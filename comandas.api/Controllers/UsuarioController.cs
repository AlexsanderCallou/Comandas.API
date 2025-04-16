using Comandas.API.DataBase;
using Comandas.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Comandas.API.Controllers {


    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase{

        public readonly ComandasDBContext _banco;

        public UsuarioController(ComandasDBContext comandasDBContext){
            _banco = comandasDBContext;
        }


        [HttpGet("{id}")]
        [SwaggerResponse(200,"Retorna um ususario.", typeof(UsuarioGetDTO))]
        public async Task<ActionResult<UsuarioGetDTO>> GetUsuario(int id){
            return null
        }




    }

}

