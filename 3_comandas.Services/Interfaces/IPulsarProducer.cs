using System;
using comandas.Shared.Event;

namespace comandas.Services.Interfaces;

public interface IPulsarProducerService
{

    public Task EnviarMenssagem(EmailEvent emailEvent);

}
