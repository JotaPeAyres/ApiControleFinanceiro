using ControleFinanceiro.Api.ViewModels;

namespace ControleFinanceiro.Bussiness.Interfaces;

public interface IFluxoCaixaService : IDisposable
{
    Task<IEnumerable<SaldoDiarioViewModel>> ObterSaldosDiarios(DateTime? dataInicio = null, DateTime? dataFim = null);
    Task<IEnumerable<DiaSaldoNegativoViewModel>> ObterDiasComSaldoNegativo(DateTime? dataInicio = null, DateTime? dataFim = null);
    Task<ResumoFinanceiroViewModel> ObterResumoFinanceiroAsync();
    Task<SaldoAtualViewModel> ObterSaldoAtualAsync();
}
