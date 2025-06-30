using System;

namespace comandas.Shared.Event;

public class EmailEvent
{
    public string nome { get; set; } = default!;
    public string email { get; set; } = default!;
    public string assunto { get; set; } = default!;
    public string menssagem { get; set; } = default!;

}
