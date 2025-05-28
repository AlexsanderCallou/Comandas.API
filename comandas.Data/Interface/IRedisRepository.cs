namespace Comandas.Data.Implementation
{

    public interface IRedisRepository
    {
        Task<T?> GetAsync<T>(string Chave);
        Task<bool> SaveAsync<T>(string Chave, T dados, TimeSpan? TempoVida);
        Task<bool> RemoveAsync(string Chave);        
    }

}