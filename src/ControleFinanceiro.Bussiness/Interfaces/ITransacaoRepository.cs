using ControleFinanceiro.Bussiness.Models;

namespace ControleFinanceiro.Bussiness.Interfaces;

public interface ITransacaoRepository : IRepository<Transacao>
{
    Task<IEnumerable<Transacao>> ObterPorPeriodo(DateTime? dataInicio, DateTime? dataFim); 
}

