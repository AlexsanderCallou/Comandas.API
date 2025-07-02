using System;
using comandas.Services.Interfaces;
using comandas.Shared.Event;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using Newtonsoft.Json;

namespace comandas.Services.Implementation;

public class PulsarProducerService : IPulsarProducerService, IAsyncDisposable
{

    private readonly IProducer<string> _producer;
    private readonly IPulsarClient _pulsarClient;
    public PulsarProducerService()
    {
        _pulsarClient = PulsarClient.Builder().ServiceUrl(new Uri("pulsar://pulsar:6650")).Build();
        _producer = _pulsarClient.NewProducer(Schema.String).Topic("MensagemEmail").Create();
    }

    public async ValueTask DisposeAsync()
    {
        if (_producer is not null)
        {
            await _producer.DisposeAsync();
        }
        if (_pulsarClient is not null)
        {
            await _pulsarClient.DisposeAsync();
        }
    }

    public async Task EnviarMenssagem(EmailEvent emailEvent)
    {
        var mensagemEmail = JsonConvert.SerializeObject(emailEvent);

        await _producer.Send(mensagemEmail);
    }
}
