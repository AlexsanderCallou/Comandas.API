using System;
using Comandas.Shared.DTOs;

namespace Comandas.Data.Interface;

public interface IUsuarioRepository
{
    Task<UsuarioGetDTO?> GetUsuario(int id);
    Task<UsuarioLoginRepositorytDTO?> GetUsuarioLogin(string email);
    Task<IEnumerable<UsuarioGetDTO>> GetUsuarios();
    Task<UsuarioPostResponseDTO> PostUsuario(UsuarioPostDTO usuarioPostDTO);
    Task<bool> PutUsuario(UsuarioPutDTO usuarioPutDTO);
    Task<bool> DeleteUsuario(int Id); 
}
