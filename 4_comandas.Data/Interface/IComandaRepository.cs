using System.Reflection.Metadata;
using Comandas.Domain;
using Comandas.Shared.DTOs;

namespace Comandas.Data.Interface
{
    public interface IComandaRepository
    {
        Task<ComandaGetDTO?> GetComanda(int id);
        Task<IEnumerable<ComandaGetDTO?>> GetComandas(int idSituacaoComanda);
        Task<bool> PostComanda(Comanda comanda);
        Task<bool> PutComanda(ComandaPutDTO comandaPutDTO);
        Task<bool> DeleteComanda(int id);
        Task<bool> PatchComanda(ComandaPatchDTO comandaPatchDTO);
        Task<bool> GetExisteComanda(int id);
        Task SaveChangesAsync();
    }
}