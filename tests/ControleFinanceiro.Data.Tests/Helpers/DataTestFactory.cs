using Bogus;
using ControleFinanceiro.Bussiness.Models;
using static ControleFinanceiro.Bussiness.Models.Transacao;

namespace ControleFinanceiro.Data.Tests.Helpers;

public static class DataTestFactory
{
    private static readonly Faker<Transacao> TransacaoFaker = new Faker<Transacao>("pt_BR")
        .RuleFor(t => t.Id, f => Guid.NewGuid())
        .RuleFor(t => t.Descricao, f => f.Commerce.ProductName())
        .RuleFor(t => t.Valor, f => f.Random.Decimal(1, 10000))
        .RuleFor(t => t.Data, f => f.Date.Between(DateTime.Now.AddYears(-2), DateTime.Now))
        .RuleFor(t => t.Tipo, f => f.PickRandom<TipoTransacao>())
        .RuleFor(t => t.Ativo, f => true)
        .RuleFor(t => t.Inclusao, f => DateTime.Now)
        .RuleFor(t => t.Alteracao, f => null);

    public static Transacao CriarTransacao(
        string? descricao = null,
        decimal? valor = null,
        DateTime? data = null,
        TipoTransacao? tipo = null,
        bool? ativo = null,
        Guid? id = null)
    {
        var transacao = TransacaoFaker.Generate();

        if (id.HasValue) transacao.Id = id.Value;
        if (descricao != null) transacao.Descricao = descricao;
        if (valor.HasValue) transacao.Valor = valor.Value;
        if (data.HasValue) transacao.Data = data.Value;
        if (tipo.HasValue) transacao.Tipo = tipo.Value;
        if (ativo.HasValue) transacao.Ativo = ativo.Value;

        return transacao;
    }

    public static List<Transacao> CriarTransacoes(int quantidade)
    {
        return TransacaoFaker.Generate(quantidade);
    }

    public static List<Transacao> CriarTransacoesPorPeriodo(DateTime inicio, DateTime fim, int quantidade)
    {
        return new Faker<Transacao>("pt_BR")
            .RuleFor(t => t.Id, f => Guid.NewGuid())
            .RuleFor(t => t.Descricao, f => f.Commerce.ProductName())
            .RuleFor(t => t.Valor, f => f.Random.Decimal(1, 10000))
            .RuleFor(t => t.Data, f => f.Date.Between(inicio, fim))
            .RuleFor(t => t.Tipo, f => f.PickRandom<TipoTransacao>())
            .RuleFor(t => t.Ativo, f => true)
            .RuleFor(t => t.Inclusao, f => DateTime.Now)
            .RuleFor(t => t.Alteracao, f => null)
            .Generate(quantidade);
    }

    public static List<Transacao> CriarTransacoesPorTipo(TipoTransacao tipo, int quantidade)
    {
        return new Faker<Transacao>("pt_BR")
            .RuleFor(t => t.Id, f => Guid.NewGuid())
            .RuleFor(t => t.Descricao, f => f.Commerce.ProductName())
            .RuleFor(t => t.Valor, f => f.Random.Decimal(1, 10000))
            .RuleFor(t => t.Data, f => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
            .RuleFor(t => t.Tipo, f => tipo)
            .RuleFor(t => t.Ativo, f => true)
            .RuleFor(t => t.Inclusao, f => DateTime.Now)
            .RuleFor(t => t.Alteracao, f => null)
            .Generate(quantidade);
    }

    public static List<Transacao> CriarTransacoesComValoresEspecificos(params decimal[] valores)
    {
        var transacoes = new List<Transacao>();
        
        foreach (var valor in valores)
        {
            transacoes.Add(CriarTransacao(valor: valor));
        }

        return transacoes;
    }
}
