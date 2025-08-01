using ControleFinanceiro.Bussiness.Interfaces;
using ControleFinanceiro.Bussiness.Notificacoes;
using ControleFinanceiro.Bussiness.Services;
using ControleFinanceiro.Data.Repositories;

namespace ControleFinanceiro.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<INotificador, Notificador>();
        services.AddScoped<ITransacaoService, TransacaoService>();
        services.AddScoped<IFluxoCaixaService, FluxoCaixaService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<ITransacaoRepository, TransacaoRepository>();
        services.AddHostedService<FluxoCaixaNotificationService>();
        
        return services;
    }
}