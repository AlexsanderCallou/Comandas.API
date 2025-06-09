using Comandas.Services.Interface;
using Comandas.Data.Interface;
using Comandas.Shared.DTOs;

namespace Comandas.Services.Implementation
{
    public class ComandaService : IComandaService
    {
        private readonly IComandaRepository _comandaRepository;
        public ComandaService(IComandaRepository comandaRepository)
        {
            _comandaRepository = comandaRepository;
        }
        public Task<bool> GetExisteComanda(int id)
        {
            return _comandaRepository.GetExisteComanda(id);
        }
        public Task<ComandaGetDTO?> GetComanda(int id)
        {
            return _comandaRepository.GetComanda(id);
        }
        public Task<IEnumerable<ComandaGetDTO?>> GetComandas(int idSituacaoComanda)
        {
            return _comandaRepository.GetComandas(idSituacaoComanda);
        }
        public Task<ComandaResponsePostDTO?> PostComanda(ComandaPostDTO comandaPostDTO)
        {
            return _comandaRepository.PostComanda(comandaPostDTO);
        }
        public Task<bool> PutComanda(ComandaPutDTO comandaPutDTO)
        {
            throw new NotImplementedException();
        }
        public Task<bool> PatchComanda(ComandaPatchDTO comandaPatchDTO)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteComanda(int id)
        {
            throw new NotImplementedException();
        }
    }
}