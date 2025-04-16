using System.Runtime.CompilerServices;
using Comandas.API.DataBase;
using Comandas.API.DTOs;
using Comandas.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            
            var usuario = await _banco.Usuarios.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);

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
        public async Task<ActionResult<IEnumerable<UsuarioGetDTO>>> GetUsuarios(){

            var usuarios = await _banco.Usuarios.AsNoTracking().ToListAsync();

            return Ok(usuarios.Select(c => new UsuarioGetDTO{
                        Id = c.Id,
                        Nome = c.Nome,
                        Email = c.Email 
            }
            ));
        }

        [HttpPost]
        [SwaggerResponse(201,"Cria um usuario", typeof(UsusarioPostDTO))]
        public async Task<ActionResult<UsusarioPostDTO>> PostUsuario(UsusarioPostDTO ususarioPostDTO){

            var usuario = new Usuario(){
                    Nome = ususarioPostDTO.Nome,
                    Email = ususarioPostDTO.Email,
                    Senha = ususarioPostDTO.Senha
            };

            _banco.Usuarios.Add(usuario);

            await _banco.SaveChangesAsync();

            return CreatedAtAction("PostUsuario", new {id = usuario.Id}, usuario);

        }

    }

}

