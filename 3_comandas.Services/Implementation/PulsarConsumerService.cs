using comandas.Shared.Event;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comandas.Services.Implementation
{
    public class PulsarConsumerService : BackgroundService
    {
        private readonly IPulsarClient _pulsarClient;
        private readonly IConsumer<string> _consumer;
        private readonly ILogger<PulsarConsumerService> _logger;
        public PulsarConsumerService(ILogger<PulsarConsumerService> logger)
        {

            _pulsarClient = PulsarClient.Builder().ServiceUrl(new Uri("pulsar://pulsar:6650")).Build();

            _consumer = _pulsarClient.NewConsumer(Schema.String)
                                        .Topic("MensagemEmail")
                                        .SubscriptionName("PulsarConsumerService-Subscription")
                                        .SubscriptionType(SubscriptionType.Shared)
                                        .Create();
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var mensagem = await _consumer.Receive(stoppingToken);

                var evento = Encoding.UTF8.GetString(mensagem.Data);

                var eventoJson = JsonConvert.DeserializeObject<EmailEvent>(evento);

                _logger.LogInformation($"Evento Recebido: {evento}");

                await _consumer.Acknowledge(mensagem);
            }
        }
    }
}
