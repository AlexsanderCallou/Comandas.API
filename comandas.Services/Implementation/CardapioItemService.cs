using Comandas.Data.Interface;
using Comandas.Services.Interface;
using Comandas.Shared.DTOs;

namespace Comandas.Services.Implementation
{
    public class CardapioItemService : ICardapioItemService
    {
        private readonly ICardapioItemRepository _cardapioItemRepository;
        private readonly IRedisRepository _redisRepository;

        public CardapioItemService(ICardapioItemRepository cardapioItemRepository, IRedisRepository redisRepository)
        {
            _cardapioItemRepository = cardapioItemRepository;
            _redisRepository = redisRepository;
        }

        public async Task<CardapioItemGetDTO?> GetCardapioItem(int Id)
        {

            string chave = $"CardapioItem:{Id}";

            var cardapioCache = await _redisRepository.GetAsync<CardapioItemGetDTO>(chave);

            if (cardapioCache is null)
            {
                var response = await _cardapioItemRepository.GetCardapioItem(Id);
                
                await _redisRepository.SaveAsync<CardapioItemGetDTO>(chave, response!, TimeSpan.FromMinutes(1));

                return response;
            }

            return cardapioCache;

        }

        public async Task<IEnumerable<CardapioItemGetDTO>> GetCardapioItems()
        {

            string chave = "CardapioItems";

            var cardaiosCache = await _redisRepository.GetAsync<IEnumerable<CardapioItemGetDTO>>(chave);

            if (cardaiosCache is not null)
            {
                return cardaiosCache;
            }

            var response = await _cardapioItemRepository.GetCardapioItens();

            await _redisRepository.SaveAsync<IEnumerable<CardapioItemGetDTO>>(chave, response, TimeSpan.FromMinutes(1));

            return response;

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