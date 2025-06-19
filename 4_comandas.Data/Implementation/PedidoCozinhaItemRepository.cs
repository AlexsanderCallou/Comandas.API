using Comandas.Data.Interface;
using Comandas.Domain;
using Microsoft.EntityFrameworkCore;

namespace Comandas.Data.Implementation;

public class PedidoCozinhaItemRepository : IPedidoCozinhaItemRepository
{
    private readonly ComandasDBContext _comandasDBContext;
    public PedidoCozinhaItemRepository(ComandasDBContext comandasDBContext)
    {
        _comandasDBContext =  comandasDBContext;
    }


    public async Task<IEnumerable<PedidoCozinhaItem>> ReturnPedidoCozinhaItens()
    {
        throw new NotImplementedException();
    }

    public async Task<PedidoCozinhaItem> ReturnPedidoCozinhaItens(int Id)
    {
        throw new NotImplementedException();
    }

    public async Task<PedidoCozinhaItem> ReturnPedidoCozinhaItensByComandaItemId(int Id)
    {
        return await _comandasDBContext.PedidoCozinhaItems
            .Include(p => p.PedidoCozinha)
            .Where(p => p.ComanadaItemId == Id)
            .FirstOrDefaultAsync();
    }

    public async Task CreatePedidoCozinhaItem(PedidoCozinhaItem pedidoCozinhaItem)
    {
        throw new NotImplementedException();
    }
    
    public void DeletePedidoCozinhaItem(PedidoCozinhaItem pedidoCozinhaItem)
    {
        _comandasDBContext.PedidoCozinhaItems.Remove(pedidoCozinhaItem);
    }
}