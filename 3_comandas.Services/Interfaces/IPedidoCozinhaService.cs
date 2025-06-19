using Comandas.Shared.DTOs;

namespace Comandas.Services.Interface
{
    public interface IPedidoCozinhaService
    {
        Task<IEnumerable<PedidoCozinhaGetDTO>> GetPedidoCozinha(int situacaoPedidoCozinha);
        Task<bool> PatchPedidoCozinha(int Id, PedidioCozinhaPatchDTO pedidioCozinhaPatchDTO);
        
    }
}