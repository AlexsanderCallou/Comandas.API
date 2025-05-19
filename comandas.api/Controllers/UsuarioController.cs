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
using comandas.Services.Interfaces;

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
        [SwaggerResponse(200,"Retorna um ususario.", typeof(UsuarioGetDTO))]
        [Authorize]
        public async Task<ActionResult<UsuarioGetDTO>> GetUsuario(int id){
            
            var usuario = await _banco.Usuarios.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);

            if(usuario is null){
                return NotFound("Usuario não encontrado.");
            }

            return Ok(
                new UsuarioGetDTO(){
                        Id = usuario.Id, //mas por que??
                        Nome = usuario.Nome,
                        Email = usuario.Email
                }
            );
        }

        [HttpGet]
        [SwaggerResponse(200,"Retorna Usuarios.", typeof(IEnumerable<UsuarioGetDTO>))]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UsuarioGetDTO>>> GetUsuarios(){

            var usuarios = await _banco.Usuarios.AsNoTracking().ToListAsync();

            return Ok(usuarios.Select(c => new UsuarioGetDTO{
                        Id = c.Id,
                        Nome = c.Nome,
                        Email = c.Email 
            }
            ));
        }

        [Authorize]
        [HttpPost]
        [SwaggerResponse(201,"Cria um usuario", typeof(UsusarioPostDTO))]
        [Authorize]
        public async Task<ActionResult<UsusarioPostDTO>> PostUsuario(UsusarioPostDTO ususarioPostDTO){

            var usuario = new Usuario(){
                    Nome = ususarioPostDTO.Nome,
                    Email = ususarioPostDTO.Email,
                    Senha = ususarioPostDTO.Senha
            };

            _banco.Usuarios.Add(usuario);

            await _banco.SaveChangesAsync();

            var usuarioResponse = new UsuarioPostResponseDTO(){
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };

            return CreatedAtAction("GetUsuario", new {id = usuarioResponse.Id}, usuarioResponse);

        }

        [HttpPut("{Id}")]
        [Authorize]
        public async Task<ActionResult> PutUsuario([FromRoute] int Id, [FromBody] UsuarioPutDTO usuarioPutDTO){

            if(Id != usuarioPutDTO.Id){
                return BadRequest();
            }

            var usuario = _banco.Usuarios.FirstOrDefault(c => c.Id == Id);
            
            if (usuario is null){
                return NotFound("Usuario não encontrado.");
            }

            usuario.Nome = usuarioPutDTO.Nome;
            usuario.Email = usuarioPutDTO.Email;
            usuario.Senha = usuarioPutDTO.Senha;

            await _banco.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<ActionResult> DeleteUsuario(int Id){

            var usuario = _banco.Usuarios.FirstOrDefault(c => c.Id == Id);

            if(usuario is null){
                return NotFound("Usuario não encontrado.");
            }
            
            _banco.Usuarios.Remove(usuario);

            await _banco.SaveChangesAsync();

            return NoContent();

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



