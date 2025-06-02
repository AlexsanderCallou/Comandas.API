using Comandas.Data.Interface;
using Comandas.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Comandas.Data.Implementation
{
    public class PedidoCozinhaRepository : IPedidoCozinhaRepository
    {

        private readonly ComandasDBContext _banco;

        public PedidoCozinhaRepository(ComandasDBContext comandasDBContext)
        {
            _banco = comandasDBContext;
        }

        public async Task<IEnumerable<PedidoCozinhaGetDTO>> GetPedidoCozinha(int situacaoPedidoCozinha)
        {

            var pedidoCozinha = await _banco.PedidosCozinha
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

        public async Task<bool> PatchPedidoCozinha(int id, PedidioCozinhaPatchDTO pedidioCozinhaPatchDTO)
        {
            var pedidoCozinha = await _banco.PedidosCozinha.FirstOrDefaultAsync(c => c.Id == id);

            if (pedidoCozinha is null)
            {
                return false;
            }

            pedidoCozinha.SituacaoId = pedidioCozinhaPatchDTO.SituacaoPedidoCozinhaId;

            await _banco.SaveChangesAsync();

            return true;

        }
    }
}