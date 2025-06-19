using Comandas.Domain;
using Comandas.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Comandas.Data.Interface;
using Comandas.Shared.Enumeration;

namespace Comandas.Data.Implementation
{

    public class MesaRepository : IMesaRepository
    {

        private readonly ComandasDBContext _banco;

        public MesaRepository(ComandasDBContext comandasDBContext)
        {
            _banco = comandasDBContext;
        }

        public async Task<MesaGetDTO?> ReturnMesa(int Id)
        {
            var mesa = await _banco.Mesas
                                .Where(m => m.Id == Id)
                                .Select(m => new MesaGetDTO
                                {
                                    Id = m.Id,
                                    NumeroMesa = m.NumeroMesa,
                                    SituacaoMesa = m.SituacaoMesa
                                })
                                .TagWith("GetMesa")
                                .FirstOrDefaultAsync();
            if (mesa is null)
            {
                return default;
            }
            return mesa;             
        }

        public async Task<IEnumerable<MesaGetDTO>> ReturnMesas()
        {
            return await _banco.Mesas
                                .AsNoTracking()
                                .Select(u => new MesaGetDTO
                                {
                                    Id = u.Id,
                                    NumeroMesa = u.NumeroMesa,
                                    SituacaoMesa = u.SituacaoMesa
                                })
                                .TagWith("GetUsuarios")
                                .ToListAsync();
        }

        public async Task<MesaResponsePostDTO> CreateMesa(MesaPostDTO mesaPostDTO)
        {
            var mesa = new Mesa
            {
                NumeroMesa = mesaPostDTO.NumeroMesa,
                SituacaoMesa = mesaPostDTO.SituacaoMesa
            };

            await _banco.Mesas.AddAsync(mesa);

            await _banco.SaveChangesAsync();

            return new MesaResponsePostDTO
            {
                Id = mesa.Id,
                NumeroMesa = mesa.NumeroMesa,
                SituacaoMesa = mesa.SituacaoMesa
            };
        }

        public async Task<bool> AtualizaMesa(MesaPutDTO mesaPutDTO)
        {
            var mesa = await _banco.Mesas
                                    .Where(m => m.Id == mesaPutDTO.Id)
                                    .ExecuteUpdateAsync(m => m
                                    .SetProperty(m => m.NumeroMesa, mesaPutDTO.NumeroMesa)
                                    .SetProperty(m => m.SituacaoMesa, mesaPutDTO.SituacaoMesa)
                                    );
            if (mesa > 0)
            {
                return true;
            }
                return false;
        }
        public async Task<bool> DeleteMesa(int Id)
        {

            if (await _banco.Mesas.Where(m => m.Id == Id).ExecuteDeleteAsync() > 0)
            {
                return true;
            }
                return false;
        }

        public async Task<bool> ReturnMesaDisponivel(int numeroMesa)
        {
            return await _banco.Mesas
                            .Where(m => m.SituacaoMesa == (int)SituacaoMesa.Disponivel 
                                        && m.NumeroMesa == numeroMesa)
                            .AnyAsync();
        }

        public async Task<bool> ReturnMesaExiste(int id)
        {
            return await _banco.Mesas
                .Where(m => m.Id == id)
                .AnyAsync();
        }

        public async Task<Mesa?> ReturnMesaByNumMesa(int numeroMesa)
        {
            return await _banco.Mesas
                                .Where(m => m.NumeroMesa == numeroMesa)
                                .FirstOrDefaultAsync();
        }
    }

}