namespace ControleFinanceiro.Bussiness.Models;

public class Transacao : Entity
{
    public TipoTransacao Tipo { get; set; }
    public DateTime Data { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }

    public enum TipoTransacao
    {
        Despesa,
        Receita
    }
}
