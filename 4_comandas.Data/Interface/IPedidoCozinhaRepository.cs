using Comandas.Domain;
using Comandas.Shared.DTOs;

namespace Comandas.Data.Interface
{
    public interface IPedidoCozinhaRepository
    {
        Task<IEnumerable<PedidoCozinhaGetDTO>> ReturnPedidosCozinha(int situacaoPedidoCozinha);
        Task<bool> AtualizaSituacaoPedidoCozinha(int Id, PedidioCozinhaPatchDTO pedidioCozinhaPatchDTO);
        Task CreatePedidoCozinha(PedidoCozinha pedidoCozinha);
        void DeletaPedidoCozinha(PedidoCozinha pedidoCozinha);
        
    }
}