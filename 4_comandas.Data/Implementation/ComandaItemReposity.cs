using Comandas.Data.Interface;
using Comandas.Domain;

namespace Comandas.Data.Implementation
{
    public class ComandaItemRepository : IComandaItemRepository
    {
        private readonly ComandasDBContext _comandasDBContext;
        public ComandaItemRepository(ComandasDBContext comandasDBContext)
        {
            _comandasDBContext = comandasDBContext;
        }

        public async Task AdicionaComandasItems(List<ComandaItem> comandaItem)
        {
            await _comandasDBContext.ComandaItems.AddRangeAsync(comandaItem);
        }
    }
}