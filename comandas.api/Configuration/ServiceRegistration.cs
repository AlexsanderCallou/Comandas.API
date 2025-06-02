using Comandas.Services.Implementation;
using Comandas.Services.Interface;
using Comandas.Data.Implementation;
using Comandas.Data.Interface;

namespace Comandas.API.Configuration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServiceRegistration(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUsuarioService, UsuarioService>();
            serviceCollection.AddScoped<IUsuarioRepository, UsuarioRepository>();
            serviceCollection.AddScoped<IMesaService, MesaService>();
            serviceCollection.AddScoped<ICardapioItemService, CardapioItemService>();
            serviceCollection.AddScoped<IMesaRepository, MesaRepository>();
            serviceCollection.AddScoped<ICardapioItemRepository, CardapioItemRepository>();
            serviceCollection.AddScoped<IPedidoCozinhaService, PedidoCozinhaService>();
            serviceCollection.AddScoped<IPedidoCozinhaRepository, PedidoCozinhaRepository>();

            return serviceCollection;
        }
    }
}