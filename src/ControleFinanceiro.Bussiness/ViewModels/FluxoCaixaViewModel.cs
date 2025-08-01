namespace ControleFinanceiro.Api.ViewModels;

public class SaldoDiarioViewModel
{
    public DateTime Data { get; set; }
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal SaldoDiario { get; set; }
    public decimal SaldoAcumulado { get; set; }
}

public class DiaSaldoNegativoViewModel
{
    public DateTime Data { get; set; }
    public decimal Saldo { get; set; }
}

public class ResumoFinanceiroViewModel
{
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal SaldoAtual { get; set; }
    public decimal MediaMensalReceitas { get; set; }
    public decimal MediaMensalDespesas { get; set; }
}

public class FluxoCaixaConsolidadoViewModel
{
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal SaldoPeriodo { get; set; }
    public IEnumerable<SaldoDiarioViewModel> DetalhesDiarios { get; set; }
}

public class SaldoAtualViewModel
{
    public decimal Saldo { get; set; }
    public DateTime DataAtualizacao { get; set; }
}

public class HistoricoSaldoViewModel
{
    public DateTime Data { get; set; }
    public decimal Saldo { get; set; }
}
