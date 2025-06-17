using Comandas.Shared.DTOs;

namespace Comandas.Services.Interface
{
    public interface IComandaService
    {
        Task<ComandaGetDTO?> GetComanda(int id);
        Task<IEnumerable<ComandaGetDTO?>> GetComandas(int idSituacaoComanda);
        Task<ServiceResponseDTO<ComandaResponsePostDTO>> PostComanda(ComandaPostDTO comandaPostDTO);
        Task<ServiceResponseDTO<bool>> PutComanda(ComandaPutDTO comandaPutDTO);
        Task<ServiceResponseDTO<bool>> DeleteComanda(int id);
        Task<ServiceResponseDTO<bool>> PatchComanda(ComandaPatchDTO comandaPatchDTO);
        Task<bool> GetExisteComanda(int id);
    }
}