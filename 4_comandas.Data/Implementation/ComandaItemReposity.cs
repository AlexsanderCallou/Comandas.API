using Comandas.Data.Interface;
using Comandas.Domain;
using Microsoft.EntityFrameworkCore;

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

        public void DeleteComandaItens(ComandaItem[] comandaItems)
        {
            _comandasDBContext.ComandaItems.RemoveRange(comandaItems);

        }

        public async Task<IEnumerable<ComandaItem>> ReturnComandaItens(List<int> ids)
        {
            return await _comandasDBContext.ComandaItems.Where(c => ids.Contains(c.Id)).ToListAsync();
        }
    }
}