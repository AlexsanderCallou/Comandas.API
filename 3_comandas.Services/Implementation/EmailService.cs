using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using comandas.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace comandas.Services.Implementation;

public class EmailService : IEmailService
{

    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<bool> EnviarEmail(string EmailDestino, string Assunto, string Texto)
    {
        var emailOrigem = _configuration.GetSection("EmailConfig:EmailOrigem").Value;
        var servidor = _configuration.GetSection("EmailConfig:Servidor").Value;
        var porta = _configuration.GetSection("EmailConfig:Porta").Value;
        var senha = _configuration.GetSection("EmailConfig:Senha").Value;

        try
        {

            using (var mensagem = new MailMessage())
            {
                mensagem.From = new MailAddress(emailOrigem!);
                mensagem.To.Add(new MailAddress(EmailDestino));
                mensagem.Subject = Assunto;
                mensagem.Body = Texto;
                mensagem.BodyEncoding = Encoding.UTF8;
                mensagem.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
                mensagem.Priority = MailPriority.Normal;

                using (var cliente = new SmtpClient())
                {
                    cliente.Host = servidor!;
                    cliente.Port = int.Parse(porta!);
                    cliente.EnableSsl = true;
                    cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
                    cliente.Credentials = new NetworkCredential(emailOrigem, senha);
                    cliente.UseDefaultCredentials = false;
                    cliente.Send(mensagem);

                    return Task.FromResult(true);   
                }
            }

        }
        catch (SmtpFailedRecipientException smtpRecEx)
        {
            return Task.FromResult(false);
        }
        catch (SmtpException smtpEx)
        {
            return Task.FromResult(false);
        }
        catch (Exception ex)
        {
            return Task.FromResult(false);
        }

    }
}
