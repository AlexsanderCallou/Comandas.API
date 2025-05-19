using System;
using comandas.Data.Interface;
using Comandas.Shared.DTOs;
using Comandas.Data;
using Microsoft.EntityFrameworkCore;

namespace comandas.Data.Implementation;

public class UsuarioRepository : IUsuarioRepository
{

    public readonly ComandasDBContext _banco;
    
    public UsuarioRepository(ComandasDBContext comandasDBContext)
    {
        _banco = comandasDBContext;
    }

    public async Task<UsuarioGetDTO> GetUsuario(int id)
    {
        var usuario = await _banco.Usuarios.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);

        return null;   
    }
}
