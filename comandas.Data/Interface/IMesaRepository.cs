using Comandas.Shared.DTOs;

namespace Comandas.Data.Interface
{

    public interface IMesaRepository
    {
        Task<MesaGetDTO?> GetMesa(int Id);
        Task<IEnumerable<MesaGetDTO>> GetMesas();
        Task<MesaResponsePostDTO> PostMesa(MesaPostDTO mesaPostDTO);
        Task<bool> PutMesa(MesaPutDTO mesaPutDTO);
        Task<bool> DeleteMesa(int Id); 
    }    

}