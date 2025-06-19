using Comandas.Services.Interface;
using Comandas.Data.Interface;
using Comandas.Shared.DTOs;
using Comandas.Shared.DTOs.Item;
using Comandas.Domain;
using Comandas.Shared.Enumeration;
using Microsoft.Extensions.Logging;

namespace Comandas.Services.Implementation
{
    public class ComandaService : IComandaService
    {
        private readonly IComandaRepository _comandaRepository;
        private readonly IMesaRepository _mesaRepository;
        private readonly ICardapioItemService _cardapioItemService;
        private readonly ICardapioItemRepository _cardapioItemRepository;
        private readonly IComandaItemRepository _comandaItemRepository;
        private readonly IPedidoCozinhaRepository _pedidoCozinhaRepository;
        private readonly IPedidoCozinhaItemRepository _pedidoCozinhaItemRepository;
        private readonly ILogger<ComandaService> _logger;
        public ComandaService(IComandaRepository comandaRepository,
                                IMesaRepository mesaRepository,
                                ICardapioItemService cardapioItemService,
                                IComandaItemRepository comandaItemRepository,
                                ICardapioItemRepository cardapioItemRepository,
                                IPedidoCozinhaRepository pedidoCozinhaRepository,
                                IPedidoCozinhaItemRepository pedidoCozinhaItemRepository,
                                ILogger<ComandaService> logger)
        {
            _comandaRepository = comandaRepository;
            _mesaRepository = mesaRepository;
            _cardapioItemService = cardapioItemService;
            _comandaItemRepository = comandaItemRepository;
            _cardapioItemRepository = cardapioItemRepository;
            _pedidoCozinhaRepository = pedidoCozinhaRepository;
            _pedidoCozinhaItemRepository = pedidoCozinhaItemRepository;
            _logger = logger;
        }
        public Task<bool> GetExisteComanda(int id)
        {
            return _comandaRepository.ExisteComanda(id);
        }
        public Task<ComandaGetDTO?> GetComanda(int id)
        {
            return _comandaRepository.ReturnComandaDTO(id);
        }
        public Task<IEnumerable<ComandaGetDTO?>> GetComandas(int idSituacaoComanda)
        {
            return _comandaRepository.ReturnListComandasDTO(idSituacaoComanda);
        }
        public async Task<ServiceResponseDTO<ComandaResponsePostDTO>> PostComanda(ComandaPostDTO comandaPostDTO)
        {
            var mesa = await _mesaRepository.ReturnMesaByNumMesa(comandaPostDTO.NumeroMesa);
            
            if (!await _mesaRepository.ReturnMesaExiste(comandaPostDTO.NumeroMesa))
            {
                return ServiceResponseDTO<ComandaResponsePostDTO>.Fail("Mesa não existe.");
            }

            if (!await _mesaRepository.ReturnMesaDisponivel(comandaPostDTO.NumeroMesa))
            {
                return ServiceResponseDTO<ComandaResponsePostDTO>.Fail("Mesa ocupada.");
            }
            
            mesa.SituacaoMesa = (int)SituacaoMesa.Ocupada;
            
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

            await _comandaRepository.CreateComanda(comandaInsert);

            var itensInsert = new List<ComandaItem>();

            foreach (int cItem in comandaPostDTO.CardapioItens)
            {
                var comandaItemInsert = new ComandaItem
                {
                    Comanda = comandaInsert,
                    CardapioItem = await _cardapioItemRepository.ReturnCardapioItem(cItem)
                }; 
                
                itensInsert.Add(comandaItemInsert);
                
                var cardapioItem = await _cardapioItemRepository.ReturnCardapioItem(cItem);
                if (cardapioItem.PossuiPreparo)
                {
                    var pedidoCozinhaInsert = new PedidoCozinha
                    {
                        Comanda = comandaInsert,
                        SituacaoId = (int)SituacaoPedidoCozinha.Pendente

                    };
                    var pedidoCozinhaItemInsert = new PedidoCozinhaItem
                    {
                        PedidoCozinha = pedidoCozinhaInsert,
                        ComandaItem = comandaItemInsert
                    };
                    _pedidoCozinhaRepository.CreatePedidoCozinha(pedidoCozinhaInsert);
                    _pedidoCozinhaItemRepository.CreatePedidoCozinhaItem(pedidoCozinhaItemInsert);
                }
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

        public async Task<ServiceResponseDTO<bool>> PutComanda(ComandaPutDTO comandaPutDTO)
        {

            List<ErroResult> errosList = [];
            
            var comanda = await _comandaRepository.ReturnComanda(comandaPutDTO.Id);

            if (comanda is null)
            {
                errosList.Add(new(404, "Comanda não encontrada"));

                return ServiceResponseDTO<bool>.Fail(errosList);    
            }

            if (comandaPutDTO.NumeroMesa > 0 && comandaPutDTO.NumeroMesa != comanda.NumeroMesa)
            {

                var mesa = await _mesaRepository.ReturnMesaByNumMesa(comandaPutDTO.NumeroMesa);

                if (mesa is null)
                {
                    errosList.Add(new(404,"Mesa não encontrada"));

                    return ServiceResponseDTO<bool>.Fail(errosList);
                }

                if (mesa.SituacaoMesa == (int)SituacaoMesa.Ocupada)
                {
                    errosList.Add(new(400,$"Mesa {mesa.NumeroMesa} está ocupada."));

                    return ServiceResponseDTO<bool>.Fail(errosList);
                }

                mesa.SituacaoMesa = (int)SituacaoMesa.Ocupada;

                var mesaAtual = await _mesaRepository.ReturnMesaByNumMesa(comanda.NumeroMesa);

                mesaAtual!.SituacaoMesa = (int)SituacaoMesa.Disponivel;

                comanda.NumeroMesa = mesa.NumeroMesa;
            }

            //verificar se foi informado um novo nome p o cliente.

            if (!string.IsNullOrEmpty(comandaPutDTO.NomeCliente))
            {
                comanda.NomeCliente = comandaPutDTO.NomeCliente;
            }

            //percorrer os itens da comanda e verificar se eh uma exclusao.

            var itensExcluir = new List<int>();

            itensExcluir = comandaPutDTO.ComandaItems
                                        .Where(c => c.Excluir)
                                        .Select(c => c.Id)
                                        .ToList();

            if (itensExcluir.Any())
            {
                var comandaItensExcluir = await _comandaItemRepository.ReturnComandaItens(itensExcluir);
                if (!comandaItensExcluir.Any())
                {
                    errosList.Add(new(400,"Nenhum id de item informado."));
                    return ServiceResponseDTO<bool>.Fail(errosList);
                }
                _comandaItemRepository.DeleteComandaItens(comandaItensExcluir.ToArray());
            }
            //verificar se eh para adicionar um novo item. 
            var idsAdd = new List<int>();

            idsAdd = comandaPutDTO.ComandaItems.Where(c => c.Excluir == false)
                                                        .Select(c => c.CardapioItemId).ToList();

            if (idsAdd.Any())
            {

                List<ComandaItem> comandaItens = idsAdd.Select(c =>
                                                                new ComandaItem
                                                                {
                                                                    Comanda = comanda,
                                                                    CardapioItemId = c
                                                                }).ToList();

                await _comandaItemRepository.AdicionaComandasItems(comandaItens);

                var pedidoCozinhaItens = new List<PedidoCozinhaItem>();

                foreach (ComandaItem comandaItem in comandaItens)
                {

                    var cardapioItem = await _cardapioItemRepository.ReturnCardapioItemDTO(comandaItem.CardapioItemId);

                    if (cardapioItem!.PossuiPreparo)
                    {

                        var pedidoCozinha = new PedidoCozinha
                        {
                            Comanda = comanda,
                            SituacaoId = (int)SituacaoPedidoCozinha.Pendente
                        };

                        await _pedidoCozinhaRepository.CreatePedidoCozinha(pedidoCozinha);

                        var pedidoCozinhaItem = new PedidoCozinhaItem
                        {
                            PedidoCozinha = pedidoCozinha,
                            ComandaItem = comandaItem
                        };

                        pedidoCozinhaItens.Add(pedidoCozinhaItem);

                        if (pedidoCozinhaItens.Any())
                        {
                            pedidoCozinha.PedidoCozinhaItems = pedidoCozinhaItens;
                        }
                    }
                }
            }

            try
            {

                await _comandaRepository.SaveChangesAsync();

                return ServiceResponseDTO<bool>.Ok(true);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno no servidor.");
                errosList.Add(new(500, "Ouve um erro interno no sistema."));
                return ServiceResponseDTO<bool>.Fail(errosList);
            }

        }

        public async Task<ServiceResponseDTO<bool>> DeleteComanda(int id)
        {
            List<ErroResult> errosList = [];
            
            if (!await _comandaRepository.ExisteComanda(id))
            {
                errosList.Add(new ErroResult(404, $"Comanda não encontrada: {id}"));
            }

            var comandaItems = await _comandaItemRepository.ReturnComandaItensInComanda(id);
            
            var comandaItemsPreparo = comandaItems.Where(c => c.CardapioItem.PossuiPreparo).ToList();

            foreach (var item in comandaItemsPreparo)
            {

                var pedidoCozinhaItem = await _pedidoCozinhaItemRepository.ReturnPedidoCozinhaItensByComandaItemId(item.Id);

                if (pedidoCozinhaItem is not null)
                {
                    _pedidoCozinhaItemRepository.DeletePedidoCozinhaItem(pedidoCozinhaItem);
                    _pedidoCozinhaRepository.DeletaPedidoCozinha(pedidoCozinhaItem.PedidoCozinha);
                }

            }

            if (comandaItems.Any())
            {
                _comandaItemRepository.DeleteComandaItens(comandaItems.ToArray());
            }

            _comandaRepository.DeleteComanda(await _comandaRepository.ReturnComanda(id));

            await _comandaRepository.SaveChangesAsync();
            
            return ServiceResponseDTO<bool>.Ok(true);
        }

        public Task<ServiceResponseDTO<bool>> PatchComanda(ComandaPatchDTO comandaPatchDTO)
        {
            throw new NotImplementedException();
        }

    }
}