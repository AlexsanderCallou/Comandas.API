using Comandas.Domain;
using Comandas.Shared.DTOs;

namespace Comandas.Data.Interface
{
    public interface IPedidoCozinhaRepository
    {
        Task<IEnumerable<PedidoCozinhaGetDTO>> GetPedidoCozinha(int situacaoPedidoCozinha);
        Task<bool> PatchPedidoCozinha(int Id, PedidioCozinhaPatchDTO pedidioCozinhaPatchDTO);
        Task CreatePedidoCozinha(PedidoCozinha pedidoCozinha);
    }
}