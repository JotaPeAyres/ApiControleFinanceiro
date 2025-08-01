using ControleFinanceiro.Bussiness.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ControleFinanceiro.Bussiness.Services;

public class FluxoCaixaNotificationService : BackgroundService
{
    private readonly ILogger<FluxoCaixaNotificationService> _logger;
    private readonly IServiceProvider _provider;
    private readonly string emailEnvio;
    private readonly IConfiguration _configuration;
    public FluxoCaixaNotificationService(ILogger<FluxoCaixaNotificationService> logger,
        IServiceProvider provider,
        IConfiguration configuration
        )
    {
        _configuration = configuration;
        emailEnvio = _configuration["EmailEnvio"];
        _logger = logger;
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("BackgroundService iniciado.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _provider.CreateScope();

                var saldoAtual = await scope.ServiceProvider.GetRequiredService<IFluxoCaixaService>().ObterSaldoAtualAsync();

                if (saldoAtual.Saldo < 0)
                {
                    _logger.LogWarning("Saldo negativo detectado: Saldo");

                    var assunto = "Alerta: Saldo Negativo";
                    var dataFormatada = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                    var saldoFormatado = saldoAtual.Saldo.ToString("C");

                    var corpo = $@"
                    <h2>Alerta de Saldo Negativo</h2>
                    <p>O saldo atual do fluxo de caixa está negativo.</p>
                    <p><strong>Saldo Atual:</strong> {saldoFormatado}</p>
                    <p><strong>Data da Verificação:</strong> {dataFormatada}</p>
                    <p>Por favor, tome as providências necessárias para regularizar a situação.</p>";

                    try
                    {
                        await scope.ServiceProvider.GetRequiredService<IEmailSender>().SendEmailAsync(emailEnvio, assunto, corpo);

                        _logger.LogInformation("E-mail enviado com sucesso");
                    }
                    catch (Exception emailEx)
                    {
                        _logger.LogError(emailEx, "Erro ao enviar e-mail");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar saldo");
            }

            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("BackgroundService finalizado.");
        await base.StopAsync(stoppingToken);
    }
}
