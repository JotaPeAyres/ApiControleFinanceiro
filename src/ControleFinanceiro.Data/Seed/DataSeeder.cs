using ControleFinanceiro.Bussiness.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ControleFinanceiro.Data.Seed;

public static class DataSeeder
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        var culture = new CultureInfo("pt-BR");
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        var transacoes = new List<Transacao>
        {
            new Transacao()
            {
                Tipo = Transacao.TipoTransacao.Despesa,
                Data = new DateTime(2022, 08, 29),
                Descricao = "Cartão de Crédito",
                Valor = 825.82m,
            },
            new Transacao()
            {
                Tipo = Transacao.TipoTransacao.Despesa,
                Data = new DateTime(2022, 08, 29),
                Descricao = "Curso C#",
                Valor = 200.00m,
            },
            new Transacao()
            {
                Tipo = Transacao.TipoTransacao.Receita,
                Data = new DateTime(2022, 08, 31),
                Descricao = "Salário",
                Valor = 7000.00m,
            },
            new Transacao()
            {
                Tipo = Transacao.TipoTransacao.Despesa,
                Data = new DateTime(2022, 09, 01),
                Descricao = "Mercado",
                Valor = 3000.00m,
            },
            new Transacao()
            {
                Tipo = Transacao.TipoTransacao.Despesa,
                Data = new DateTime(2022, 09, 01),
                Descricao = "Farmácia",
                Valor = 300.00m,
            },
            new Transacao()
            {
                Tipo = Transacao.TipoTransacao.Despesa,
                Data = new DateTime(2022, 09, 01),
                Descricao = "Combustível",
                Valor = 800.25m,
            },
            new Transacao()
            {
                Tipo = Transacao.TipoTransacao.Despesa,
                Data = new DateTime(2022, 09, 15),
                Descricao = "Financiamento Carro",
                Valor = 900.00m,
            },
            new Transacao()
            {
                Tipo = Transacao.TipoTransacao.Despesa,
                Data = new DateTime(2022, 09, 22),
                Descricao = "Financiamento Casa",
                Valor = 1200.00m,
            },
            new Transacao()
            {
                Tipo = Transacao.TipoTransacao.Receita,
                Data = new DateTime(2022, 09, 25),
                Descricao = "Freelance Projeto XPTO",
                Valor = 2500.00m,
            }
        };

        modelBuilder.Entity<Transacao>().HasData(transacoes);
    }
}
