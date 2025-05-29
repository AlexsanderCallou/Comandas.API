using System;
using Comandas.Data.Interface;
using Comandas.Services.Interfaces;
using Comandas.Shared.DTOs;

namespace Comandas.Services.Implementation;

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

    public async Task<bool> PutUsuario(UsuarioPutDTO usuarioPutDTO)
    {
        return await _usuarioRepository.PutUsuario(usuarioPutDTO);
    }

    public async Task<bool> DeleteUsuario(int Id)
    {
        return await _usuarioRepository.DeleteUsuario(Id);
    }

}
