using ControleFinanceiro.Bussiness.Models;
using ControleFinanceiro.Bussiness.ViewModels;

namespace ControleFinanceiro.Bussiness.Extensions;

public static class AddTransacaoExtension
{
    public static AddTransacaoViewModel ToViewModel(this Transacao transacao)
    {
        if (transacao == null) return null;
        return new AddTransacaoViewModel
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Tipo = transacao.Tipo,
            Ativo = transacao.Ativo,
        };
    }

    public static IEnumerable<AddTransacaoViewModel> ToViewModel(this IEnumerable<Transacao> transacoes)
    {
        return transacoes?.Select(s => s.ToViewModel());
    }

    public static Transacao ToEntity(this AddTransacaoViewModel transacao)
    {
        if (transacao == null) return null;
        return new Transacao
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Tipo = transacao.Tipo,
            Ativo = transacao.Ativo,
        };
    }

    public static IEnumerable<Transacao> ToViewModel(this IEnumerable<AddTransacaoViewModel> transacoes)
    {
        return transacoes?.Select(s => s.ToEntity());
    }
}
