using System.ComponentModel.DataAnnotations;
using static ControleFinanceiro.Bussiness.Models.Transacao;

namespace ControleFinanceiro.Bussiness.ViewModels;

public class AddTransacaoViewModel
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public TipoTransacao Tipo { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public decimal Valor { get; set; }
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public DateTime Data { get; set; }

    public bool Ativo { get; set; } = true;
}
