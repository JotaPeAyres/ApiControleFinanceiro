using ControleFinanceiro.Bussiness.Models;
using ControleFinanceiro.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static ControleFinanceiro.Bussiness.Models.Transacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.Api.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();
        
        try
        {
            var context = services.GetRequiredService<ApiDbContext>();
            
            // Aplica as migrações pendentes
            await ApplyMigrationsAsync(context, logger);
            
            // Popula o banco de dados com dados iniciais, se necessário
            await SeedInitialDataAsync(context, logger);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ocorreu um erro ao inicializar o banco de dados");
            throw;
        }
    }

    private static async Task ApplyMigrationsAsync(ApiDbContext context, ILogger logger)
    {
        logger.LogInformation("Verificando migrações pendentes...");
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        var migrations = pendingMigrations.ToList();
        
        if (migrations.Any())
        {
            logger.LogInformation("Aplicando {Count} migração(ões) pendente(s)...", migrations.Count);
            await context.Database.MigrateAsync();
            logger.LogInformation("Migrações aplicadas com sucesso!");
        }
        else
        {
            logger.LogInformation("Nenhuma migração pendente para aplicar.");
        }
    }

    private static async Task SeedInitialDataAsync(ApiDbContext context, ILogger logger)
    {
        // Verifica se já existem transações no banco
        if (await context.Transacoes.AnyAsync())
        {
            logger.LogInformation("Banco de dados já contém dados. Pulando a inserção de dados iniciais.");
            return;
        }

        logger.LogInformation("Iniciando a inserção de dados iniciais...");

        var transacoes = new List<Transacao>
        {
            new Transacao { Tipo = TipoTransacao.Despesa, Data = new DateTime(2022, 8, 29), Descricao = "Cartão de Crédito", Valor = 825.82m },
            new Transacao { Tipo = TipoTransacao.Despesa, Data = new DateTime(2022, 8, 29), Descricao = "Curso C#", Valor = 200.00m },
            new Transacao { Tipo = TipoTransacao.Receita, Data = new DateTime(2022, 8, 31), Descricao = "Salário", Valor = 7000.00m },
            new Transacao { Tipo = TipoTransacao.Despesa, Data = new DateTime(2022, 9, 1), Descricao = "Mercado", Valor = 3000.00m },
            new Transacao { Tipo = TipoTransacao.Despesa, Data = new DateTime(2022, 9, 1), Descricao = "Farmácia", Valor = 300.00m },
            new Transacao { Tipo = TipoTransacao.Despesa, Data = new DateTime(2022, 9, 1), Descricao = "Combustível", Valor = 800.25m },
            new Transacao { Tipo = TipoTransacao.Despesa, Data = new DateTime(2022, 9, 15), Descricao = "Financiamento Carro", Valor = 900.00m },
            new Transacao { Tipo = TipoTransacao.Despesa, Data = new DateTime(2022, 9, 22), Descricao = "Financiamento Casa", Valor = 1200.00m },
            new Transacao { Tipo = TipoTransacao.Receita, Data = new DateTime(2022, 9, 25), Descricao = "Freelance Projeto XPTO", Valor = 2500.00m }
        };

        //new(TipoTransacao.Despesa, new DateTime(2022, 8, 29), "Curso C#", 200.00m),
        //new(TipoTransacao.Receita, new DateTime(2022, 8, 31), "Salário", 7000.00m),
        //new(TipoTransacao.Despesa, new DateTime(2022, 9, 1), "Mercado", 3000.00m),
        //new(TipoTransacao.Despesa, new DateTime(2022, 9, 1), "Farmácia", 300.00m),
        //new(TipoTransacao.Despesa, new DateTime(2022, 9, 1), "Combustível", 800.25m),
        //new(TipoTransacao.Despesa, new DateTime(2022, 9, 15), "Financiamento Carro", 900.00m),
        //new(TipoTransacao.Despesa, new DateTime(2022, 9, 22), "Financiamento Casa", 1200.00m),
        //new(TipoTransacao.Receita, new DateTime(2022, 9, 25), "Freelance Projeto XPTO", 2500.00m)

        await context.Transacoes.AddRangeAsync(transacoes);
        var result = await context.SaveChangesAsync();

        logger.LogInformation("Inseridas {Count} transações iniciais no banco de dados.", result);
    }
}
