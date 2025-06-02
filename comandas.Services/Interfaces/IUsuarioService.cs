using System;
using Comandas.Shared.DTOs;

namespace Comandas.Services.Interface
{
    public interface IUsuarioService
    {
        Task<UsuarioGetDTO?> GetUsuario(int id);
        Task<IEnumerable<UsuarioGetDTO>> GetUsuarios();
        Task<UsuarioPostResponseDTO> PostUsuario(UsuarioPostDTO usuarioPostDTO);
        Task<bool> PutUsuario(UsuarioPutDTO usuarioPutDTO);
        Task<bool> DeleteUsuario(int Id);
    }
}
