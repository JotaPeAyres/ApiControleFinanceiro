using ControleFinanceiro.Bussiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Bussiness.Interfaces;

public interface ITransacaoService : IDisposable
{
    Task Adicionar(Transacao transacao);
    Task Atualizar(Transacao transacao);
    Task Remover(Guid id);
}
