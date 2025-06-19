using System.Data.Common;
using System.Runtime.CompilerServices;
using Comandas.Data.Interface;
using Comandas.Domain;
using Comandas.Shared.DTOs;
using Comandas.Shared.DTOs.Item;
using Comandas.Shared.Enumeration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Comandas.Data.Implementation
{
    public class ComandaRepository : IComandaRepository
    {
        private readonly ComandasDBContext _comandasDBContext;
        public ComandaRepository(ComandasDBContext comandasDBContext)
        {
            _comandasDBContext = comandasDBContext;
        }

        public async Task<ComandaGetDTO?> ReturnComandaDTO(int id)
        {
            return await _comandasDBContext.Comandas
                                        .Include(c => c.ComandaItems)
                                            .ThenInclude(c => c.CardapioItem)
                                        .Where(c => c.Id == id)
                                        .Select(c => new ComandaGetDTO
                                        {
                                            Id = c.Id,
                                            NomeCliente = c.NomeCliente,
                                            NumeroMesa = c.NumeroMesa,
                                            SituacaoComanda = c.SituacaoComanda,
                                            ComandaItems = c.ComandaItems
                                                .Select(c => new ComandaItemGetDTO
                                                {
                                                    Id = c.Id,
                                                    Titulo = c.CardapioItem.Titulo
                                                }).ToList()
                                        }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ComandaGetDTO?>> ReturnListComandasDTO(int idSituacaoComanda)
        {
            return await _comandasDBContext.Comandas
                            .Include(c => c.ComandaItems)
                                .ThenInclude(c => c.CardapioItem)
                            .Where(c => c.SituacaoComanda == idSituacaoComanda)
                            .Select(c => new ComandaGetDTO
                            {
                                Id = c.Id,
                                NomeCliente = c.NomeCliente,
                                NumeroMesa = c.NumeroMesa,
                                SituacaoComanda = c.SituacaoComanda,
                                ComandaItems = c.ComandaItems
                                    .Select(c => new ComandaItemGetDTO
                                    {
                                        Id = c.Id,
                                        Titulo = c.CardapioItem.Titulo
                                    }).ToList()
                            }).ToListAsync();
        }

        public async Task<bool> CreateComanda(Comanda comanda)
        {
            await _comandasDBContext.Comandas.AddAsync(comanda);

            return true;
        }
        public void DeleteComanda(Comanda comanda)
        { 
            _comandasDBContext.Comandas.Remove(comanda);
           
        }

        public async Task<bool> ExisteComanda(int id)
        {
            return await _comandasDBContext.Comandas
                                        .Include(c => c.ComandaItems)
                                            .ThenInclude(c => c.CardapioItem)
                                        .Where(c => c.Id == id)
                                        .Select(c => new ComandaGetDTO
                                        {
                                            Id = c.Id
                                        }).AnyAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _comandasDBContext.SaveChangesAsync();
        }

        public async Task<Comanda> ReturnComanda(int id)
        {
            return await _comandasDBContext.Comandas.Where(c => c.Id == id).FirstAsync();
        }
    }
}