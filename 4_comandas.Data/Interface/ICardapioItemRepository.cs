using Comandas.Domain;
using Comandas.Shared.DTOs;

namespace Comandas.Data.Interface
{
    public interface ICardapioItemRepository
    {
        Task<CardapioItemGetDTO?> ReturnCardapioItemDTO(int Id);
        Task<CardapioItem> ReturnCardapioItem(int Id);
        Task<IEnumerable<CardapioItemGetDTO>> ReturnCardapioItens();
        Task<CardapioItemResponsePostDTO> CreateCardapioItem(CardapioItemPostDTO cardapioItemPostDTO);
        Task<bool> AtualizaCardapioItem(CardapioItemPutDTO cardapioItemPutDTO);
        Task<bool> DeleteCardapioItem(int Id);
        Task AdicionaCardapiosItens(List<CardapioItem> cardapioItems); 
    }
}