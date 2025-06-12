using Comandas.Domain;
using Comandas.Shared.DTOs;

namespace Comandas.Data.Interface
{
    public interface ICardapioItemRepository
    {
        Task<CardapioItemGetDTO?> GetCardapioItem(int Id);
        Task<CardapioItem?> ReturnCardapioItem(int Id);
        Task<IEnumerable<CardapioItemGetDTO>> GetCardapioItens();
        Task<CardapioItemResponsePostDTO> PostCardapioItem(CardapioItemPostDTO cardapioItemPostDTO);
        Task<bool> PutCardapioItem(CardapioItemPutDTO cardapioItemPutDTO);
        Task<bool> DeleteCardapioItem(int Id);
        Task AdicionaCardapiosItens(List<CardapioItem> cardapioItems); 
    }
}