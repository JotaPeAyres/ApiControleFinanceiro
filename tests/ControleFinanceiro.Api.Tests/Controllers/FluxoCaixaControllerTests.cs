using ControleFinanceiro.Api.Controllers;
using ControleFinanceiro.Api.ViewModels;
using ControleFinanceiro.Bussiness.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControleFinanceiro.Api.Tests.Controllers;

public class FluxoCaixaControllerTests
{
    private readonly Mock<IFluxoCaixaService> _mockFluxoCaixaService;
    private readonly Mock<INotificador> _mockNotificador;
    private readonly FluxoCaixaController _controller;

    public FluxoCaixaControllerTests()
    {
        _mockFluxoCaixaService = new Mock<IFluxoCaixaService>();
        _mockNotificador = new Mock<INotificador>();
        _controller = new FluxoCaixaController(_mockFluxoCaixaService.Object, _mockNotificador.Object);
    }

    [Fact]
    public async Task ObterSaldoAtual_DeveRetornarOkComSaldo()
    {
        // Arrange
        var saldoMock = new SaldoAtualViewModel 
        { 
            Saldo = 1000.00m, 
            DataAtualizacao = DateTime.Now 
        };
        _mockFluxoCaixaService.Setup(s => s.ObterSaldoAtualAsync())
                              .ReturnsAsync(saldoMock);

        // Act
        var resultado = await _controller.ObterSaldoAtual();

        // Assert
        resultado.Should().BeOfType<OkObjectResult>();
        var okResult = resultado as OkObjectResult;
        okResult?.Value.Should().Be(saldoMock);
    }

    [Fact]
    public async Task ObterResumoFinanceiro_DeveRetornarOkComResumo()
    {
        // Arrange
        var resumoMock = new ResumoFinanceiroViewModel 
        { 
            TotalReceitas = 5000.00m, 
            TotalDespesas = 3000.00m, 
            SaldoAtual = 2000.00m,
            MediaMensalReceitas = 2500.00m,
            MediaMensalDespesas = 1500.00m
        };
        _mockFluxoCaixaService.Setup(s => s.ObterResumoFinanceiroAsync())
                              .ReturnsAsync(resumoMock);

        // Act
        var resultado = await _controller.ObterResumoFinanceiro();

        // Assert
        resultado.Should().BeOfType<OkObjectResult>();
        var okResult = resultado as OkObjectResult;
        okResult?.Value.Should().Be(resumoMock);
    }

    [Fact]
    public void FluxoCaixaController_DeveHerdarDeMainController()
    {
        // Assert
        _controller.Should().BeAssignableTo<MainController>();
    }
}
