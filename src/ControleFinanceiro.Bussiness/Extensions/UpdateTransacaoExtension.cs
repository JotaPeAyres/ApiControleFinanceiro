using ControleFinanceiro.Bussiness.Models;
using ControleFinanceiro.Bussiness.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Bussiness.Extensions;

public static class UpdateTransacaoExtension
{
    public static Transacao ToEntity(this UpdateTransacaoViewModel transacao)
    {
        if (transacao == null) return null;
        return new Transacao
        {
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Tipo = transacao.Tipo,
            Ativo = transacao.Ativo,
        };
    }
}
