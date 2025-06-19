using Comandas.Domain;


namespace Comandas.Data.Interface
{
    public interface IComandaItemRepository
    {
        Task AdicionaComandasItems(List<ComandaItem> comandaItem);
        Task<IEnumerable<ComandaItem>> ReturnComandaItens(List<int> ids);
        Task<IEnumerable<ComandaItem>> ReturnComandaItensInComanda(int comandaId);
        void DeleteComandaItens(ComandaItem[] comandaItems);
    }
}

