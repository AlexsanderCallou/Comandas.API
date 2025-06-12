using Comandas.Shared.DTOs;

namespace Comandas.Services.Interface
{
    public interface IComandaService
    {
        Task<ComandaGetDTO?> GetComanda(int id);
        Task<IEnumerable<ComandaGetDTO?>> GetComandas(int idSituacaoComanda);
        Task<ServiceResponseDTO<ComandaResponsePostDTO>> PostComanda(ComandaPostDTO comandaPostDTO);
        Task<bool> PutComanda(ComandaPutDTO comandaPutDTO);
        Task<bool> DeleteComanda(int id);
        Task<bool> PatchComanda(ComandaPatchDTO comandaPatchDTO);
        Task<bool> GetExisteComanda(int id);
    }
}