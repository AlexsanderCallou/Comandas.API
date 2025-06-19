using Comandas.Data.Interface;
using Comandas.Domain;
using Comandas.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Comandas.Data.Implementation
{
    public class PedidoCozinhaRepository : IPedidoCozinhaRepository
    {

        private readonly ComandasDBContext _comandasDBContext;

        public PedidoCozinhaRepository(ComandasDBContext comandasDBContext)
        {
            _comandasDBContext = comandasDBContext;
        }

        public async Task CreatePedidoCozinha(PedidoCozinha pedidoCozinha)
        {
            await _comandasDBContext.PedidosCozinha.AddAsync(pedidoCozinha);
        }

        public void DeletaPedidoCozinha(PedidoCozinha pedidoCozinha)
        {
            _comandasDBContext.PedidosCozinha.Remove(pedidoCozinha);
        }

        public async Task<IEnumerable<PedidoCozinhaGetDTO>> ReturnPedidosCozinha(int situacaoPedidoCozinha)
        {

            var pedidoCozinha = await _comandasDBContext.PedidosCozinha
                                            .Include(c => c.Comanda)
                                            .Include(c => c.PedidoCozinhaItems)
                                                .ThenInclude(c => c.ComandaItem)
                                                    .ThenInclude(c => c.CardapioItem)
                                            .Where(c => c.SituacaoId == situacaoPedidoCozinha)
                                            .Select(c => new PedidoCozinhaGetDTO
                                            {
                                                Id = c.Id,
                                                NumeroMesa = c.Comanda.NumeroMesa,
                                                NomeCliente = c.Comanda.NomeCliente,
                                                TituloItem = c.PedidoCozinhaItems.First().ComandaItem.CardapioItem.Titulo
                                            }).OrderBy(c => c.Id).ToListAsync();

            return pedidoCozinha;

        }

        public async Task<bool> AtualizaSituacaoPedidoCozinha(int id, PedidioCozinhaPatchDTO pedidioCozinhaPatchDTO)
        {
            var pedidoCozinha = await _comandasDBContext.PedidosCozinha.FirstOrDefaultAsync(c => c.Id == id);

            if (pedidoCozinha is null)
            {
                return false;
            }

            pedidoCozinha.SituacaoId = pedidioCozinhaPatchDTO.SituacaoPedidoCozinhaId;

            await _comandasDBContext.SaveChangesAsync();

            return true;

        }
    }
}