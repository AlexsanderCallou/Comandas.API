using Comandas.Domain;
namespace Comandas.Data.Interface;

public interface IPedidoCozinhaItemRepository
{
    Task<IEnumerable<PedidoCozinhaItem>> ReturnPedidoCozinhaItens();
    Task<PedidoCozinhaItem> ReturnPedidoCozinhaItens(int Id);
    Task<PedidoCozinhaItem> ReturnPedidoCozinhaItensByComandaItemId(int Id);
    Task CreatePedidoCozinhaItem(PedidoCozinhaItem pedidoCozinhaItem);
    void DeletePedidoCozinhaItem(PedidoCozinhaItem pedidoCozinhaItem);
    
}