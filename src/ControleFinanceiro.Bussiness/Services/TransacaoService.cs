using ControleFinanceiro.Bussiness.Interfaces;
using ControleFinanceiro.Bussiness.Models;
using ControleFinanceiro.Bussiness.Models.Validations;

namespace ControleFinanceiro.Bussiness.Services;

public class TransacaoService : BaseService, ITransacaoService 
{
    private readonly ITransacaoRepository _transacaoRepository;
    public TransacaoService(INotificador notificador, 
        ITransacaoRepository transacaoRepository) : base(notificador)
    {
        _transacaoRepository = transacaoRepository;
    }

    public async Task Adicionar(Transacao transacao)
    {
        if (!ExecutarValidacao(new TransacaoValidation(), transacao)) return;

        await _transacaoRepository.Adicionar(transacao);
    }

    public async Task Atualizar(Transacao transacao)
    {
        if (!ExecutarValidacao(new TransacaoValidation(), transacao)) return;

        await _transacaoRepository.Atualizar(transacao);
    }

    public async Task Remover(Guid id)
    {
        await _transacaoRepository.Remover(id);
    }

    public void Dispose()
    {
       _transacaoRepository?.Dispose();  
    }
}
