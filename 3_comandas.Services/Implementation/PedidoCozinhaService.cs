using Comandas.Data.Interface;
using Comandas.Services.Interface;
using Comandas.Shared.DTOs;

namespace Comandas.Services.Implementation
{
    public class PedidoCozinhaService : IPedidoCozinhaService
    {

        private readonly IPedidoCozinhaRepository _pedidoCozinhaRepository;
        public PedidoCozinhaService(IPedidoCozinhaRepository pedidoCozinhaRepository)
        {
            _pedidoCozinhaRepository = pedidoCozinhaRepository;
        }

        public Task<IEnumerable<PedidoCozinhaGetDTO>> GetPedidoCozinha(int situacaoPedidoCozinha)
        {
            return _pedidoCozinhaRepository.ReturnPedidosCozinha(situacaoPedidoCozinha);
        }

        public Task<bool> PatchPedidoCozinha(int Id, PedidioCozinhaPatchDTO pedidioCozinhaPatchDTO)
        {
            return _pedidoCozinhaRepository.AtualizaSituacaoPedidoCozinha(Id, pedidioCozinhaPatchDTO);
        }
    }
}