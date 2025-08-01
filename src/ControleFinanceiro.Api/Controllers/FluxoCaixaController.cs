using ControleFinanceiro.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FluxoCaixaController : MainController
{
    private readonly IFluxoCaixaService _fluxoCaixaService;
    public FluxoCaixaController(IFluxoCaixaService fluxoCaixaService,
        INotificador notificador) : base(notificador)
    {
        _fluxoCaixaService = fluxoCaixaService;
    }

    /// <summary>
    /// Obtém o saldo diário do fluxo de caixa
    /// </summary>
    /// <param name="dataInicio">Data inicial (opcional)</param>
    /// <param name="dataFim">Data final (opcional)</param>
    [HttpGet("saldo-diario")]
    public async Task<IActionResult> ObterSaldosDiarios([FromQuery] DateTime? dataInicio = null, [FromQuery] DateTime? dataFim = null)
    {
        try
        {
            var resultado = await _fluxoCaixaService.ObterSaldosDiarios(dataInicio, dataFim);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Obtém os dias com saldo negativo
    /// </summary>
    /// <param name="dataInicio">Data inicial (opcional)</param>
    ///// <param name="dataFim">Data final (opcional)</param>
    [HttpGet("dias-negativos")]
    public async Task<IActionResult> ObterDiasComSaldoNegativo([FromQuery] DateTime? dataInicio = null, [FromQuery] DateTime? dataFim = null)
    {
        try
        {
            var resultado = await _fluxoCaixaService.ObterDiasComSaldoNegativo(dataInicio, dataFim);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Obtém o resumo financeiro
    /// </summary>
    [HttpGet("resumo")]
    public async Task<IActionResult> ObterResumoFinanceiro()
    {
        try
        {
            var resultado = await _fluxoCaixaService.ObterResumoFinanceiroAsync();
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Obtém o saldo atual
    /// </summary>
    [HttpGet("saldo-atual")]
    public async Task<IActionResult> ObterSaldoAtual()
    {
        try
        {
            var resultado = await _fluxoCaixaService.ObterSaldoAtualAsync();
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
