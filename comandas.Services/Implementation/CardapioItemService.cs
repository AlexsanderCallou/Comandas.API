using Comandas.Data.Interfaces;
using Comandas.Services.Interfaces;
using Comandas.Shared.DTOs;

namespace Comandas.Services.Implementation
{
    public class CardapioItemService : ICardapioItemService
    {
        private readonly ICardapioItemRepository _cardapioItemRepository;

        public CardapioItemService(ICardapioItemRepository cardapioItemRepository)
        {
            _cardapioItemRepository = cardapioItemRepository;
        }

        public Task<CardapioItemGetDTO?> GetCardapioItem(int Id)
        {
            return _cardapioItemRepository.GetCardapioItem(Id);
        }

        public Task<IEnumerable<CardapioItemGetDTO>> GetCardapioItems()
        {
            return _cardapioItemRepository.GetCardapioItens();
        }

        public Task<CardapioItemResponsePostDTO> PostCardapioItem(CardapioItemPostDTO cardapioItemPostDTO)
        {
            return _cardapioItemRepository.PostCardapioItem(cardapioItemPostDTO);
        }

        public Task<bool> PutCardapioItem(CardapioItemPutDTO cardapioItemPutDTO)
        {
            try
            {
                return _cardapioItemRepository.PutCardapioItem(cardapioItemPutDTO);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<bool> DeleteCardapioItem(int Id)
        {
            try
            {
                return _cardapioItemRepository.DeleteCardapioItem(Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}