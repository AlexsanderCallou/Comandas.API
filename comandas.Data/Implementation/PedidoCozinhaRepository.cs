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

        public async Task<PedidoCozinhaGetDTO> GetPedidoCozinha(int Id)
        {

            var pedidoCozinha = await _banco.PedidosCozinha
                                            .Include(c => c.Comanda)
                                            .Include(c => c.PedidoCozinhaItems)
                                                .ThenInclude(c => c.ComandaItem)
                                                    .ThenInclude(c => c.CardapioItem)
                                            .Where(c => c.SituacaoId == Id)
                                            .Select(c => new PedidoCozinhaGetDTO
                                            {
                                                Id = c.Id,
                                                NumeroMesa = c.Comanda.NumeroMesa,
                                                NomeCliente = c.Comanda.NomeCliente,
                                                TituloItem = c.PedidoCozinhaItems.First().ComandaItem.CardapioItem.Titulo
                                            }).OrderBy(c => c.Id).ToListAsync();

            return null;
            
        }

        public Task<IEnumerable<PedidoCozinhaGetDTO>> GetPedidosCozinha()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PatchPedidoCozinha()
        {
            throw new NotImplementedException();
        }
    } 
}