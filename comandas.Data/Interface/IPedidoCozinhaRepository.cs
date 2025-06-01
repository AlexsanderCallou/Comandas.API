using Comandas.Shared.DTOs;

namespace Comandas.Data.Interface
{
    public interface IPedidoCozinhaRepository
    {
        Task<PedidoCozinhaGetDTO> GetPedidoCozinha(int Id);
        Task<IEnumerable<PedidoCozinhaGetDTO>> GetPedidosCozinha();
        Task<bool> PatchPedidoCozinha();
    }
}