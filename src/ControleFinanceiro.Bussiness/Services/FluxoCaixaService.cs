using ControleFinanceiro.Api.ViewModels;
using ControleFinanceiro.Bussiness.Interfaces;
using static ControleFinanceiro.Bussiness.Models.Transacao;

namespace ControleFinanceiro.Bussiness.Services;

public class FluxoCaixaService : BaseService, IFluxoCaixaService
{
    private readonly ITransacaoRepository _transacaoRepository;
    public FluxoCaixaService(INotificador notificador,
        ITransacaoRepository transacaoRepository) : base(notificador)
    {
        _transacaoRepository = transacaoRepository;
    }

    public async Task<IEnumerable<SaldoDiarioViewModel>> ObterSaldosDiarios(DateTime? dataInicio = null, DateTime? dataFim = null)
    {
        var transacoes = await _transacaoRepository.ObterPorPeriodo(dataInicio, dataFim);

        var saldosDiarios = transacoes
            .GroupBy(t => t.Data.Date)
            .OrderBy(g => g.Key)
            .Select(g => new SaldoDiarioViewModel
            {
                Data = g.Key,
                TotalReceitas = g.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor),
                TotalDespesas = g.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor),
            })
            .ToList();

        decimal saldoAcumulado = 0;
        foreach (var saldo in saldosDiarios)
        {
            saldo.SaldoDiario = saldo.TotalReceitas - saldo.TotalDespesas;
            saldoAcumulado += saldo.SaldoDiario;
            saldo.SaldoAcumulado = saldoAcumulado;
        }

        return saldosDiarios;
    }

    public async Task<IEnumerable<DiaSaldoNegativoViewModel>> ObterDiasComSaldoNegativo(DateTime? dataInicio = null, DateTime? dataFim = null)
    {
        var saldosDiarios = await ObterSaldosDiarios(dataInicio, dataFim);

        return saldosDiarios
            .Where(s => s.SaldoDiario < 0)
            .Select(s => new DiaSaldoNegativoViewModel
            {
                Data = s.Data,
                Saldo = s.SaldoDiario
            })
            .ToList();
    }

    public async Task<ResumoFinanceiroViewModel> ObterResumoFinanceiroAsync()
    {
        var transacoes = await _transacaoRepository.ObterTodos();
        var hoje = DateTime.Today;
        var primeiroDiaDoMes = new DateTime(hoje.Year, hoje.Month, 1);
        var ultimoDiaDoMes = primeiroDiaDoMes.AddMonths(1).AddDays(-1);

        var transacoesDoMes = transacoes
            .Where(t => t.Data >= primeiroDiaDoMes && t.Data <= ultimoDiaDoMes)
            .ToList();

        var totalReceitas = transacoes
            .Where(t => t.Tipo == TipoTransacao.Receita)
            .Sum(t => t.Valor);

        var totalDespesas = transacoes
            .Where(t => t.Tipo == TipoTransacao.Despesa)
            .Sum(t => t.Valor);

        var saldoAtual = totalReceitas - totalDespesas;

        var mediaMensalReceitas = transacoes
            .Where(t => t.Tipo == TipoTransacao.Receita)
            .GroupBy(t => new { t.Data.Year, t.Data.Month })
            .Average(g => g.Sum(t => t.Valor));

        var mediaMensalDespesas = transacoes
            .Where(t => t.Tipo == TipoTransacao.Despesa)
            .GroupBy(t => new { t.Data.Year, t.Data.Month })
            .Average(g => g.Sum(t => t.Valor));

        return new ResumoFinanceiroViewModel
        {
            TotalReceitas = totalReceitas,
            TotalDespesas = totalDespesas,
            SaldoAtual = saldoAtual,
            MediaMensalReceitas = mediaMensalReceitas,
            MediaMensalDespesas = mediaMensalDespesas
        };
    }

    public async Task<SaldoAtualViewModel> ObterSaldoAtualAsync()
    {
        var transacoes = await _transacaoRepository.ObterTodos();

        var totalReceitas = transacoes
            .Where(t => t.Tipo == TipoTransacao.Receita)
            .Sum(t => t.Valor);

        var totalDespesas = transacoes
            .Where(t => t.Tipo == TipoTransacao.Despesa)
            .Sum(t => t.Valor);

        return new SaldoAtualViewModel
        {
            Saldo = totalReceitas - totalDespesas,
            DataAtualizacao = DateTime.Now
        };
    }

    public void Dispose()
    {
        _transacaoRepository?.Dispose();
    }
}
