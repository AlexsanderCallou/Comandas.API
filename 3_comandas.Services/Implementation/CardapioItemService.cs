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

        private async Task<bool> InvalidarCacheCardapioItemById(int Id)
        {
            string chave = $"CardapioItem:{Id}";

            return await _redisRepository.RemoveAsync(chave);
        }

        private async Task<bool> InvalidarCacheCardapioItems()
        {
            return await _redisRepository.RemoveAsync("CardapioItems");
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

        public async Task<CardapioItemResponsePostDTO> PostCardapioItem(CardapioItemPostDTO cardapioItemPostDTO)
        {
            try
            {
                var response = await _cardapioItemRepository.PostCardapioItem(cardapioItemPostDTO);

                await InvalidarCacheCardapioItems();

                return response;

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<bool> PutCardapioItem(CardapioItemPutDTO cardapioItemPutDTO)
        {
            try
            {
                bool response = await _cardapioItemRepository.PutCardapioItem(cardapioItemPutDTO);

                await InvalidarCacheCardapioItemById(cardapioItemPutDTO.Id);

                await InvalidarCacheCardapioItems();

                return response;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteCardapioItem(int Id)
        {
            try
            {

                bool response = await _cardapioItemRepository.DeleteCardapioItem(Id);

                await InvalidarCacheCardapioItemById(Id);

                await InvalidarCacheCardapioItems();

                return response;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}