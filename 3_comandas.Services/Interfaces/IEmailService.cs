using System;

namespace comandas.Services.Interfaces;

public interface IEmailService
{
    Task<bool> EnviarEmail(string EmailDestino, string Assunto, string Texto);
    
}
