using System.Reflection.Metadata;
using Comandas.Domain;
using Comandas.Shared.DTOs;

namespace Comandas.Data.Interface
{
    public interface IComandaRepository
    {
        Task<ComandaGetDTO?> ReturnComandaDTO(int id);
        Task<IEnumerable<ComandaGetDTO?>> ReturnListComandasDTO(int idSituacaoComanda);
        Task<bool> CreateComanda(Comanda comanda);
        void DeleteComanda(Comanda comanda);
        Task<bool> ExisteComanda(int id);
        Task SaveChangesAsync();
        Task<Comanda?> ReturnComanda(int id);
    }
}