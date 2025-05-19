using System;
using Comandas.Shared.DTOs;

namespace comandas.Data.Interface;

public interface IUsuarioRepository
{

    Task<UsuarioGetDTO> GetUsuario(int id);

}
