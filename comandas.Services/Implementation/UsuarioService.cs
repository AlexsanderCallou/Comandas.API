using System;
using comandas.Data.Interface;
using comandas.Services.Interfaces;
using Comandas.Shared.DTOs;

namespace comandas.Services.Implementation;

public class UsuarioService : IUsuarioService
{

    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
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

    
}
