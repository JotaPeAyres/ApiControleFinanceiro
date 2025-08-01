using ControleFinanceiro.Bussiness.Extensions;
using ControleFinanceiro.Bussiness.Interfaces;
using ControleFinanceiro.Bussiness.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Api.Controllers;

//[Authorize]
[Route("api/transacoes")]
public class TransacaoController : MainController
{
    private readonly ITransacaoService _transacaoService;
    private readonly ITransacaoRepository _transacaoRepository;
    public TransacaoController(ITransacaoService transacaoService,
        ITransacaoRepository transactionRepository,
        INotificador notificador) : base(notificador) 
    {
        _transacaoService = transacaoService;
        _transacaoRepository = transactionRepository;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<AddTransacaoViewModel>> ObterTodos()
    {
        return (await _transacaoRepository.ObterTodos()).ToViewModel();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AddTransacaoViewModel>> ObterPorId(Guid id)
    {
        var transacao = (await _transacaoRepository.ObterPorId(id)).ToViewModel();

        if (transacao == null) return NotFound();
        return transacao;
    }

    [HttpPost]
    public async Task<ActionResult<AddTransacaoViewModel>> Adicionar(AddTransacaoViewModel transacaoViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _transacaoService.Adicionar(transacaoViewModel.ToEntity());

        return CustomResponse(transacaoViewModel);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<AddTransacaoViewModel>> Excluir(Guid id)
    {
        var transacaoViewModel = await ObterPorId(id);

        if (transacaoViewModel == null) return NotFound();

        await _transacaoService.Remover(id);

        return CustomResponse(transacaoViewModel);
    }
}
