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

        public async Task<ComandaGetDTO?> GetComanda(int id)
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

        public async Task<IEnumerable<ComandaGetDTO?>> GetComandas(int idSituacaoComanda)
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

        public async Task<bool> PostComanda(Comanda comanda)
        {
            await _comandasDBContext.Comandas.AddAsync(comanda);

            return true;
        }

        public Task<bool> PutComanda(ComandaPutDTO comandaPutDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PatchComanda(ComandaPatchDTO comandaPatchDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteComanda(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> GetExisteComanda(int id)
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
    }
}