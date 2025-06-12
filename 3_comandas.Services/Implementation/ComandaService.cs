using Comandas.Services.Interface;
using Comandas.Data.Interface;
using Comandas.Shared.DTOs;
using Comandas.Shared.DTOs.Item;
using Comandas.Domain;
using Comandas.Shared.Enumeration;

namespace Comandas.Services.Implementation
{
    public class ComandaService : IComandaService
    {
        private readonly IComandaRepository _comandaRepository;
        private readonly IMesaRepository _mesaRepository;
        private readonly ICardapioItemService _cardapioItemService;
        private readonly ICardapioItemRepository _cardapioItemRepository;
        private readonly IComandaItemRepository _comandaItemRepository;
        public ComandaService(IComandaRepository comandaRepository,
                                IMesaRepository mesaRepository,
                                ICardapioItemService cardapioItemService,
                                IComandaItemRepository comandaItemRepository,
                                ICardapioItemRepository cardapioItemRepository)
        {
            _comandaRepository = comandaRepository;
            _mesaRepository = mesaRepository;
            _cardapioItemService = cardapioItemService;
            _comandaItemRepository = comandaItemRepository;
            _cardapioItemRepository = cardapioItemRepository;
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
        public async Task<ServiceResponseDTO<ComandaResponsePostDTO>> PostComanda(ComandaPostDTO comandaPostDTO)
        {
            
            if (!await _mesaRepository.MesaExiste(comandaPostDTO.NumeroMesa))
            {
                return ServiceResponseDTO<ComandaResponsePostDTO>.Fail("Mesa não existe.");
            }

            if (!await _mesaRepository.MesaDesocupada(comandaPostDTO.NumeroMesa))
            {           
                return ServiceResponseDTO<ComandaResponsePostDTO>.Fail("Mesa ocupada.");
            }

            foreach (int item in comandaPostDTO.CardapioItens)
            {
                if (await _cardapioItemService.GetCardapioItem(item) is null)
                {
                    return ServiceResponseDTO<ComandaResponsePostDTO>.Fail($"Id cardapio item: {item}, não encontado.");
                }
            }

            var comandaInsert = new Comanda
            {
                NumeroMesa = comandaPostDTO.NumeroMesa,
                NomeCliente = comandaPostDTO.NomeCliente,
                SituacaoComanda = (int)SituacaoComanda.Aberto
            };

            await _comandaRepository.PostComanda(comandaInsert);

            var itensInsert = new List<ComandaItem>();

            foreach (int cItem in comandaPostDTO.CardapioItens)
            {
                itensInsert.Add(new ComandaItem
                {
                    Comanda = comandaInsert,
                    CardapioItem = await _cardapioItemRepository.ReturnCardapioItem(cItem)
                });
            }

            await _comandaItemRepository.AdicionaComandasItems(itensInsert);

            await _comandaRepository.SaveChangesAsync();

            return ServiceResponseDTO<ComandaResponsePostDTO>
            .Ok(new ComandaResponsePostDTO
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
                    });
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