using FluentValidation;

namespace ControleFinanceiro.Bussiness.Models.Validations;

public class TransacaoValidation : AbstractValidator<Transacao>
{
    public TransacaoValidation()
    {
        RuleFor(f => f.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(c => c.Valor)
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");
    }
}
