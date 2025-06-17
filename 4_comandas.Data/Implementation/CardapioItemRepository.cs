using Comandas.Data.Interface;
using Comandas.Domain;
using Comandas.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Comandas.Data.Implementation
{
    public class CardapioItemRepository : ICardapioItemRepository
    {

        private readonly ComandasDBContext _banco;

        public CardapioItemRepository(ComandasDBContext comandasDBContext)
        {
            _banco = comandasDBContext;
        }

        public async Task<CardapioItemGetDTO?> ReturnCardapioItemDTO(int Id)
        {
            var response = await _banco.CardapioItems
                                        .Where(c => c.Id == Id)
                                        .Select(c => new CardapioItemGetDTO
                                        {
                                            Id = c.Id,
                                            Descricao = c.Descricao,
                                            Preco = c.Preco,
                                            PossuiPreparo = c.PossuiPreparo,
                                            Titulo = c.Titulo

                                        })
                                        .TagWith("GetCardapioItem")
                                        .FirstOrDefaultAsync();

            if (response is null)
            {
                return default;
            }
            return response;

        }

        public async Task<IEnumerable<CardapioItemGetDTO>> ReturnCardapioItens()
        {
            return await _banco.CardapioItems
                                .AsNoTracking()
                                .Select(c => new CardapioItemGetDTO
                                {
                                    Id = c.Id,
                                    Descricao = c.Descricao,
                                    PossuiPreparo = c.PossuiPreparo,
                                    Preco = c.Preco,
                                    Titulo = c.Titulo
                                })
                                .TagWith("GetCardaioItens")
                                .ToListAsync();
        }

        public async Task<CardapioItemResponsePostDTO> CreateCardapioItem(CardapioItemPostDTO cardapioItemPostDTO)
        {
            var cardapioItem = new CardapioItem
            {
                Titulo = cardapioItemPostDTO.Titulo,
                Descricao = cardapioItemPostDTO.Descricao,
                PossuiPreparo = cardapioItemPostDTO.PossuiPreparo,
                Preco = cardapioItemPostDTO.Preco
            };

            await _banco.CardapioItems.AddAsync(cardapioItem);

            await _banco.SaveChangesAsync();

            return new CardapioItemResponsePostDTO
            {
                Id = cardapioItem.Id,
                Descricao = cardapioItem.Descricao,
                PossuiPreparo = cardapioItem.PossuiPreparo,
                Preco = cardapioItem.Preco,
                Titulo = cardapioItem.Titulo
            };
        }

        public async Task<bool> AtualizaCardapioItem(CardapioItemPutDTO cardapioItemPutDTO)
        {
            var cardapioItem = await _banco.CardapioItems
                                            .Where(c => c.Id == cardapioItemPutDTO.Id)
                                            .ExecuteUpdateAsync(c => c
                                            .SetProperty(c => c.Descricao, cardapioItemPutDTO.Descricao)
                                            .SetProperty(c => c.PossuiPreparo, cardapioItemPutDTO.PossuiPreparo)
                                            .SetProperty(c => c.Preco, cardapioItemPutDTO.Preco)
                                            .SetProperty(c => c.Titulo, cardapioItemPutDTO.Titulo));
            if (cardapioItem > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteCardapioItem(int Id)
        {
            var cardapioItem = await _banco.CardapioItems.Where(c => c.Id == Id).ExecuteDeleteAsync();

            if (cardapioItem > 0)
            {
                return true;
            }
            return false;
        }

        public async Task AdicionaCardapiosItens(List<CardapioItem> cardapioItems)
        {
            await _banco.CardapioItems.AddRangeAsync(cardapioItems);
        }

        public async Task<CardapioItem> ReturnCardapioItem(int Id)
        {
            var response = await _banco.CardapioItems
                                        .Where(c => c.Id == Id)
                                        .TagWith("GetCardapioItem")
                                        .FirstAsync();
            return response;
        }
    }
}