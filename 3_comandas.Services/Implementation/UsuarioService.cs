using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Comandas.Services.Interface;
using Comandas.Data.Interface;

using Comandas.Shared.DTOs;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using DotPulsar.Abstractions;
using comandas.Services.Interfaces;
using comandas.Shared.Event;

namespace Comandas.Services.Implementation;

public class UsuarioService : IUsuarioService
{

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _configuration;
    private readonly IPulsarProducerService _pusarProducerService;
    
    public UsuarioService(IUsuarioRepository usuarioRepository, IConfiguration configuration, IPulsarProducerService pulsarProducerService)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
        _pusarProducerService = pulsarProducerService;
    }

    public async Task<UsuarioGetDTO?> GetUsuario(int id)
    {
        return await _usuarioRepository.GetUsuario(id);
    }

    public async Task<IEnumerable<UsuarioGetDTO>> GetUsuarios()
    {
        return await _usuarioRepository.GetUsuarios();
    }

    public async Task<UsuarioPostResponseDTO> PostUsuario(UsuarioPostDTO usuarioPostDTO)
    {
        try
        {
            return await _usuarioRepository.PostUsuario(usuarioPostDTO);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<bool> PutUsuario(UsuarioPutDTO usuarioPutDTO)
    {
        return await _usuarioRepository.PutUsuario(usuarioPutDTO);
    }

    public async Task<bool> DeleteUsuario(int Id)
    {
        return await _usuarioRepository.DeleteUsuario(Id);
    }

    public async Task<UsuarioLoginServiceDTO> PostLogin(UsuarioLoginResquestDTO usuarioLoginResquestDTO)
    {

        var tokenHandler = new JwtSecurityTokenHandler();
// 
        var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("TokenLogin")??"");

        var usuario = await _usuarioRepository.GetUsuarioLogin(usuarioLoginResquestDTO.Email);

            if (usuario is null) {
                return new UsuarioLoginServiceDTO
                {
                    Retorno = "Usuario/Senha invalidos.",
                    RetornoSucesso = false
                };
            }

            if (!usuario.Senha.Equals(usuarioLoginResquestDTO.Senha)){
                return new UsuarioLoginServiceDTO
                {
                    Retorno = "Usuario/Senha invalidos.",
                    RetornoSucesso = false
                };
            }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddHours(1),
            Subject = new ClaimsIdentity(
                new Claim[]{
                        new Claim(ClaimTypes.Name,usuario.Nome),
                        new Claim(ClaimTypes.NameIdentifier,usuario.Id.ToString())
                }
            ),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var tokenString = tokenHandler.WriteToken(token);

        await _pusarProducerService.EnviarMenssagem(new EmailEvent
        {
            assunto = "Bem Vindo!",
            email = usuario.Email,
            menssagem = "Voce realizou login",
            nome = usuario.Nome
        });

        return new UsuarioLoginServiceDTO
        {
            Retorno = "Sucesso",
            RetornoSucesso = true,
            usuarioLoginResponseDTO = new UsuarioLoginResponseDTO
            {
                Id = usuario.Id,
                BearerToken = tokenString
            }
        };

    }
}
