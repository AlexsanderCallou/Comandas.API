using System;
using Comandas.Shared.DTOs;

namespace comandas.Data.Interface;

public interface IUsuarioRepository
{

    Task<UsuarioGetDTO?> GetUsuario(int id);
    Task<IEnumerable<UsuarioGetDTO>> GetUsuarios();
    Task<UsuarioPostResponseDTO> PostUsuario(UsuarioPostDTO usuarioPostDTO);
}
