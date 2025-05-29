using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Comandas.Data;
using Comandas.Shared.DTOs;
using Comandas.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using Comandas.Services.Interfaces;

namespace Comandas.API.Controllers {


    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase{

        public readonly ComandasDBContext _banco;

        public readonly IUsuarioService _usuarioService;

        public UsuarioController(ComandasDBContext comandasDBContext, IUsuarioService usuarioService){

            _banco = comandasDBContext;
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
            else
            {
                return UnprocessableEntity();
            }
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<ActionResult> DeleteUsuario(int Id){

            if (await _usuarioService.DeleteUsuario(Id))
            {
                return NoContent();
            }
            else
            {
                return UnprocessableEntity();
            }

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UsuarioLoginResponseDTO>> Login(UsuarioLoginResquestDTO usuarioLoginResquestDTO){

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes("3e8acfc238f45a314fd4b2bde272678ad30bd1774743a11dbc5c53ac71ca494b");

            var usuario = await _banco.Usuarios.FirstOrDefaultAsync(c => c.Email == usuarioLoginResquestDTO.Email);

            if (usuario is null) {
                return NotFound("Usuario/Senha invalidos.");
            }

            if (!usuario.Senha.Equals(usuarioLoginResquestDTO.Senha)){
                return NotFound("Usuario/Senha invalidos.");
            }

            
            var tokenDescriptor = new SecurityTokenDescriptor{
                Expires = DateTime.UtcNow.AddHours(1),
                Subject = new ClaimsIdentity(
                    new Claim[]{
                        new Claim(ClaimTypes.Name,usuario.Nome),
                        new Claim(ClaimTypes.NameIdentifier,usuario.Id.ToString())
                    }
                ),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new UsuarioLoginResponseDTO{Id = usuario.Id, BearerToken = tokenString});

        }
    }
}



