using Comandas.Domain;
using Comandas.Shared.DTOs;

namespace Comandas.Data.Interface
{

    public interface IMesaRepository
    {
        Task<MesaGetDTO?> ReturnMesa(int id);
        Task<IEnumerable<MesaGetDTO>> ReturnMesas();
        Task<MesaResponsePostDTO> CreateMesa(MesaPostDTO mesaPostDTO);
        Task<bool> AtualizaMesa(MesaPutDTO mesaPutDTO);
        Task<bool> DeleteMesa(int id);
        Task<bool> ReturnMesaDisponivel(int numeroMesa);
        Task<bool> ReturnMesaExiste(int id);
        Task<Mesa?> ReturnMesaByNumMesa(int numeroMesa);
    }    

}