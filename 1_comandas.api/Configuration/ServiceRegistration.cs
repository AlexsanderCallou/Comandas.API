using Comandas.Services.Implementation;
using Comandas.Services.Interface;
using Comandas.Data.Implementation;
using Comandas.Data.Interface;
using comandas.Services.Interfaces;
using comandas.Services.Implementation;

namespace Comandas.API.Configuration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServiceRegistration(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUsuarioService, UsuarioService>();
            serviceCollection.AddScoped<IUsuarioRepository, UsuarioRepository>();

            serviceCollection.AddScoped<IMesaService, MesaService>();
            serviceCollection.AddScoped<IMesaRepository, MesaRepository>();

            serviceCollection.AddScoped<ICardapioItemService, CardapioItemService>();
            serviceCollection.AddScoped<ICardapioItemRepository, CardapioItemRepository>();

            serviceCollection.AddScoped<IPedidoCozinhaService, PedidoCozinhaService>();
            serviceCollection.AddScoped<IPedidoCozinhaRepository, PedidoCozinhaRepository>();
            
            serviceCollection.AddScoped<IPedidoCozinhaItemRepository, PedidoCozinhaItemRepository>();

            serviceCollection.AddScoped<IComandaItemRepository, ComandaItemRepository>();

            serviceCollection.AddScoped<IComandaService, ComandaService>();
            serviceCollection.AddScoped<IComandaRepository, ComandaRepository>();
            

            serviceCollection.AddScoped<IRedisRepository, RedisRepository>();

            serviceCollection.AddScoped<IPulsarProducerService, PulsarProducerService>();
            serviceCollection.AddHostedService<PulsarConsumerService>();


            serviceCollection.AddSingleton<IEmailService, EmailService>();

            return serviceCollection;
        }
    }
}