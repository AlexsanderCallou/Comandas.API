using Comandas.Domain;

namespace Comandas.Data.Interface
{
    public interface IComandaItemRepository
    {
        Task AdicionaComandasItems(List<ComandaItem> comandaItem);
    }
}