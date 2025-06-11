using Comandas.Shared.DTOs;

namespace Comandas.Services.Interface
{

    public interface IMesaService
    {
        Task<MesaGetDTO?> GetMesa(int id);
        Task<IEnumerable<MesaGetDTO>> GetMesas();
        Task<MesaResponsePostDTO> PostMesa(MesaPostDTO mesaPostDTO);
        Task<bool> PutMesa(MesaPutDTO mesaPutDTO);
        Task<bool> DeleteMesa(int id);
        Task<bool> MesaDesocupada(int id);
        Task<bool> MesaExiste(int id);
    }
}