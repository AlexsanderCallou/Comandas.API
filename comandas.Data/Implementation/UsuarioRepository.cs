using System;
using comandas.Data.Interface;
using Comandas.Shared.DTOs;
using Comandas.Data;
using Microsoft.EntityFrameworkCore;
using Comandas.Domain;

namespace comandas.Data.Implementation;

public class UsuarioRepository : IUsuarioRepository
{

    public readonly ComandasDBContext _banco;
    
    public UsuarioRepository(ComandasDBContext comandasDBContext)
    {
        _banco = comandasDBContext;
    }

    public async Task<UsuarioGetDTO?> GetUsuario(int id)
    {
        var usuario = await _banco
                                .Usuarios
                                .AsNoTracking()
                                .Where(b => b.Id == id)
                                .Select(c => new UsuarioGetDTO
                                {
                                    Id = c.Id,
                                    Nome = c.Nome,
                                    Email = c.Email
                                })
                                .TagWith("GetUsuario")
                                .FirstOrDefaultAsync();

        if (usuario is null)
        {
            return default;
        }

        return usuario;
    }

    public async Task<IEnumerable<UsuarioGetDTO>> GetUsuarios()
    {
        return await _banco.Usuarios.AsNoTracking().Select(c => new UsuarioGetDTO
        {
            Id = c.Id,
            Nome = c.Nome,
            Email = c.Email
        }).TagWith("GetUsuarios").ToListAsync();

    }

    public async Task<UsuarioPostResponseDTO> PostUsuario(UsuarioPostDTO usuarioPostDTO)
    {
        var usuario = new Usuario
        {
            Nome = usuarioPostDTO.Nome,
            Email = usuarioPostDTO.Email,
            Senha = usuarioPostDTO.Senha
        };

        await _banco.Usuarios.AddAsync(usuario);

        await _banco.SaveChangesAsync();

        return new UsuarioPostResponseDTO
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email
        };
    }
}
