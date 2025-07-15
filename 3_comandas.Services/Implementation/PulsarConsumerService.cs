using comandas.Services.Interfaces;
using comandas.Shared.Event;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
namespace comandas.Services.Implementation
{ 
    public class PulsarConsumerService : BackgroundService
    {
        private readonly IPulsarClient _pulsarClient;
        private readonly IConsumer<string> _consumer;
        private readonly ILogger<PulsarConsumerService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public PulsarConsumerService(ILogger<PulsarConsumerService> logger,
                                    IServiceProvider serviceProvider)
        {
            _pulsarClient = PulsarClient.Builder().ServiceUrl(new Uri("pulsar://pulsar:6650")).Build();

            _consumer = _pulsarClient.NewConsumer(Schema.String)
                                     .Topic("MensagemEmail")
                                     .SubscriptionName("PulsarConsumerService-Subscription")
                                     .SubscriptionType(SubscriptionType.Shared)
                                     .Create();

            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var mensagem = await _consumer.Receive(stoppingToken);

                var evento = Encoding.UTF8.GetString(mensagem.Data);

                var eventoJson = JsonConvert.DeserializeObject<EmailEvent>(evento);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                    var disparo = await emailService.EnviarEmail(eventoJson!.email, eventoJson.assunto, eventoJson.menssagem);
                    if (disparo)
                    {
                        await _consumer.Acknowledge(mensagem);
                    }
                    _logger.LogInformation($"Email enviado: {disparo}");
                }

                _logger.LogInformation($"Evento Recebido: {evento}");

            }
        }
    }
}