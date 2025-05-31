using Comandas.Shared.DTOs;

namespace Comandas.Services.Interfaces
{
    public interface ICardapioItemService
    {
        Task<CardapioItemGetDTO?> GetCardapioItem(int Id);
        Task<IEnumerable<CardapioItemGetDTO>> GetCardapioItems();
        Task<CardapioItemResponsePostDTO> PostCardapioItem(CardapioItemPostDTO cardapioItemPostDTO);
        Task<bool> PutCardapioItem(CardapioItemPutDTO cardapioItemPutDTO);
        Task<bool> DeleteCardapioItem(int Id);

    }
}