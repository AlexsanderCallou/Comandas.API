using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Comandas.Data;
using Comandas.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using Comandas.Services.Interface;

namespace Comandas.API.Controllers {


    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase{

        public readonly IUsuarioService _usuarioService;

        public UsuarioController(ComandasDBContext comandasDBContext, IUsuarioService usuarioService){

            _usuarioService = usuarioService;
        }


        [HttpGet("{id}")]  
        [SwaggerResponse(200,"Retorna um usuario.", typeof(UsuarioGetDTO))]
        [Authorize]
        public async Task<ActionResult<UsuarioGetDTO>> GetUsuario(int id)
        {
           
            var usuario = await _usuarioService.GetUsuario(id);

            if(usuario is null){
                return NotFound("Usuario não encontrado.");
            }

            return Ok(usuario);
        }

        [HttpGet]
        [SwaggerResponse(200,"Retorna Usuarios.", typeof(IEnumerable<UsuarioGetDTO>))]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UsuarioGetDTO>>> GetUsuarios()
        {
            return Ok(await _usuarioService.GetUsuarios());
        }

        [Authorize]
        [HttpPost]
        [SwaggerResponse(201,"Cria um usuario", typeof(UsuarioPostDTO))]
        [Authorize]
        public async Task<ActionResult<UsuarioPostDTO>> PostUsuario(UsuarioPostDTO usuarioPostDTO){

            var usuarioResponse = await _usuarioService.PostUsuario(usuarioPostDTO);

            return CreatedAtAction("GetUsuario", new { id = usuarioResponse.Id }, usuarioResponse);

        }

        [HttpPut("{Id}")]
        [Authorize]
        public async Task<ActionResult> PutUsuario([FromRoute] int Id, [FromBody] UsuarioPutDTO usuarioPutDTO){

            if(Id != usuarioPutDTO.Id){
                return BadRequest();
            }

            if (await _usuarioService.PutUsuario(usuarioPutDTO))
            {
                return NoContent();
            }

                return UnprocessableEntity();
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<ActionResult> DeleteUsuario(int Id){

            if (await _usuarioService.DeleteUsuario(Id))
            {
                return NoContent();
            }
                return UnprocessableEntity();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UsuarioLoginResponseDTO>> Login(UsuarioLoginResquestDTO usuarioLoginResquestDTO){

          
            var usuario = await _usuarioService.PostLogin(usuarioLoginResquestDTO);

            if (!usuario.RetornoSucesso) {
                return NotFound(usuario.Retorno);
            }
            
            return Ok(usuario.usuarioLoginResponseDTO);

        }
    }
}



