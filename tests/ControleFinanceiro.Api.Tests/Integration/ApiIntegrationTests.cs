using ControleFinanceiro.Api.Controllers;
using ControleFinanceiro.Bussiness.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControleFinanceiro.Api.Tests.Integration;

public class ApiIntegrationTests
{
    [Fact]
    public void TransacaoController_DeveSerInstanciado()
    {
        // Arrange
        var mockTransacaoService = new Mock<ITransacaoService>();
        var mockTransacaoRepository = new Mock<ITransacaoRepository>();
        var mockNotificador = new Mock<INotificador>();

        // Act
        var controller = new TransacaoController(
            mockTransacaoService.Object,
            mockTransacaoRepository.Object,
            mockNotificador.Object);

        // Assert
        controller.Should().NotBeNull();
        controller.Should().BeAssignableTo<ControllerBase>();
    }

    [Fact]
    public void FluxoCaixaController_DeveSerInstanciado()
    {
        // Arrange
        var mockFluxoCaixaService = new Mock<IFluxoCaixaService>();
        var mockNotificador = new Mock<INotificador>();

        // Act
        var controller = new FluxoCaixaController(
            mockFluxoCaixaService.Object,
            mockNotificador.Object);

        // Assert
        controller.Should().NotBeNull();
        controller.Should().BeAssignableTo<ControllerBase>();
    }

    [Fact]
    public void Controllers_DevemTerAtributoRoute()
    {
        // Arrange & Act
        var transacaoControllerType = typeof(TransacaoController);
        var fluxoCaixaControllerType = typeof(FluxoCaixaController);

        // Assert
        transacaoControllerType.GetCustomAttributes(typeof(RouteAttribute), false)
            .Should().NotBeEmpty();
        
        fluxoCaixaControllerType.GetCustomAttributes(typeof(RouteAttribute), false)
            .Should().NotBeEmpty();
    }
}
