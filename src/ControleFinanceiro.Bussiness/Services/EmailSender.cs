using ControleFinanceiro.Bussiness.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Logging;

namespace ControleFinanceiro.Bussiness.Services;

public class EmailSender : IEmailSender
{
    private string host;
    private int port;
    private bool enableSSL;
    private string userName;
    private string password;
    private string fromEmail;
    private string fromName;
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;

        host = _configuration["EmailSettings:Host"];
        port = int.Parse(_configuration["EmailSettings:Port"]);
        enableSSL = bool.Parse(_configuration["EmailSettings:EnableSsl"]);
        userName = _configuration["EmailSettings:Username"];
        password = _configuration["EmailSettings:Password"];
        fromEmail = _configuration["EmailSettings:FromEmail"];
        fromName = _configuration["EmailSettings:FromName"];
    }

    public async Task SendEmailAsync(string email, string assunto, string corpo)
    {
        try
        {
            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = enableSSL
            };


            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = assunto,
                Body = corpo,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
        catch (Exception emailEx)
        {
            _logger.LogError(emailEx, "Erro ao enviar e-mail");
        }
    }
}
