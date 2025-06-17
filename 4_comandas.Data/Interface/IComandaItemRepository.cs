using Comandas.Domain;


namespace Comandas.Data.Interface
{
    public interface IComandaItemRepository
    {
        Task AdicionaComandasItems(List<ComandaItem> comandaItem);
        Task<IEnumerable<ComandaItem>> ReturnComandaItens(List<int> ids);
        void DeleteComandaItens(ComandaItem[] comandaItems);
    }
}

