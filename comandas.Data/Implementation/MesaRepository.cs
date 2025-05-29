using Comandas.Domain;
using Comandas.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Comandas.Data.Implementation
{

    public class MesaRepository : IMesaRepository
    {

        private readonly ComandasDBContext _banco;

        public MesaRepository(ComandasDBContext comandasDBContext)
        {
            _banco = comandasDBContext;
        }

        public async Task<MesaGetDTO?> GetMesa(int Id)
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

        public async Task<IEnumerable<MesaGetDTO>> GetMesas()
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

        public async Task<MesaResponsePostDTO> PostMesa(MesaPostDTO mesaPostDTO)
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

        public async Task<bool> PutMesa(int Id, MesaPutDTO mesaPutDTO)
        {
            var mesa = await _banco.Mesas
                                    .Where(m => m.Id == Id)
                                    .ExecuteUpdateAsync(m => m
                                    .SetProperty(m => m.NumeroMesa, mesaPutDTO.NumeroMesa)
                                    .SetProperty(m => m.SituacaoMesa, mesaPutDTO.SituacaoMesa)
                                    );
            if (mesa > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DeleteMesa(int Id)
        {

            if (await _banco.Mesas.Where(m => m.Id == Id).ExecuteDeleteAsync() > 0)
            {
                return true;
            } else {
                return false;
            }

        }

    }

}