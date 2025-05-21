using System;
using Comandas.Shared.DTOs;

namespace comandas.Services.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioGetDTO?> GetUsuario(int id);
    Task<IEnumerable<UsuarioGetDTO>> GetUsuarios();
    Task<UsuarioPostResponseDTO> PostUsuario(UsuarioPostDTO usuarioPostDTO);
}
