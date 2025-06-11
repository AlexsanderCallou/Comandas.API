using Comandas.Services.Interface;
using Comandas.Data.Interface;
using Comandas.Shared.DTOs;
using Comandas.Domain;
using Comandas.Shared.Enumeration;

namespace Comandas.Services.Implementation
{
    public class ComandaService : IComandaService
    {
        private readonly IComandaRepository _comandaRepository;
        private readonly IMesaRepository _mesaRepository;
        public ComandaService(IComandaRepository comandaRepository, IMesaRepository mesaRepository)
        {
            _comandaRepository = comandaRepository;
            _mesaRepository = mesaRepository;
        }
        public Task<bool> GetExisteComanda(int id)
        {
            return _comandaRepository.GetExisteComanda(id);
        }
        public Task<ComandaGetDTO?> GetComanda(int id)
        {
            return _comandaRepository.GetComanda(id);
        }
        public Task<IEnumerable<ComandaGetDTO?>> GetComandas(int idSituacaoComanda)
        {
            return _comandaRepository.GetComandas(idSituacaoComanda);
        }
        public async Task<ComandaResponsePostDTO?> PostComanda(ComandaPostDTO comandaPostDTO)
        {
            //TODO realizar as validaçoes do comandaitem, se existem no banco.

            if (!await _mesaRepository.MesaExiste(comandaPostDTO.NumeroMesa))
            {   
                //TODO criar objeto generico para retornar
                return null;
            }

            if (!await _mesaRepository.MesaDesocupada(comandaPostDTO.NumeroMesa))
            {
            
                return null;
            }


            var comandaInsert = new Comanda
            {
                NumeroMesa      = comandaPostDTO.NumeroMesa,
                NomeCliente     = comandaPostDTO.NomeCliente,
                SituacaoComanda = (int)SituacaoComanda.Aberto
            };

            await _comandaRepository.PostComanda(comandaInsert);


            var itensInsert = comandaPostDTO.CardapioItens
                .Select(id => new ComandaItem {
                    ComandaId      = comandaInsert.Id,
                    CardapioItemId = id
                })
                .ToList();

            await _comandasDBContext.ComandaItems.AddRangeAsync(itensInsert);

            await _comandasDBContext.SaveChangesAsync();

            return new ComandaResponsePostDTO
            {
                Id = comandaInsert.Id,
                NomeCliente = comandaInsert.NomeCliente,
                NumeroMesa = comandaInsert.NumeroMesa,
                SituacaoComanda = comandaInsert.SituacaoComanda,
                ComandaItems = itensInsert.Select(c => new ComandaItemGetDTO
                                            {
                                                Id = c.Id,
                                                CardapioItemId = c.CardapioItemId,
                                                ComandaId = c.ComandaId,
                                                Titulo = c.CardapioItem.Titulo
                                            }).ToList()
            };

            return null;
        }
        public Task<bool> PutComanda(ComandaPutDTO comandaPutDTO)
        {
            throw new NotImplementedException();
        }
        public Task<bool> PatchComanda(ComandaPatchDTO comandaPatchDTO)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteComanda(int id)
        {
            throw new NotImplementedException();
        }
    }
}