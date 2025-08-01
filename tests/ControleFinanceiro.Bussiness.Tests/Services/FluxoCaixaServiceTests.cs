using ControleFinanceiro.Bussiness.Interfaces;
using ControleFinanceiro.Bussiness.Models;
using ControleFinanceiro.Bussiness.Services;
using FluentAssertions;
using Moq;
using static ControleFinanceiro.Bussiness.Models.Transacao;

namespace ControleFinanceiro.Bussiness.Tests.Services;

public class FluxoCaixaServiceTests
{
    private readonly Mock<ITransacaoRepository> _mockRepository;
    private readonly Mock<INotificador> _mockNotificador;
    private readonly FluxoCaixaService _service;

    public FluxoCaixaServiceTests()
    {
        _mockRepository = new Mock<ITransacaoRepository>();
        _mockNotificador = new Mock<INotificador>();
        _service = new FluxoCaixaService(_mockNotificador.Object, _mockRepository.Object);
    }

    [Fact]
    public async Task ObterSaldoAtualAsync_ComTransacoes_DeveRetornarSaldoCorreto()
    {
        // Arrange
        var transacoes = new List<Transacao>
        {
            new() { Tipo = TipoTransacao.Receita, Valor = 5000.00m, Data = DateTime.Today },
            new() { Tipo = TipoTransacao.Despesa, Valor = 2000.00m, Data = DateTime.Today }
        };

        _mockRepository.Setup(r => r.ObterTodos())
                      .ReturnsAsync(transacoes);

        // Act
        var resultado = await _service.ObterSaldoAtualAsync();

        // Assert
        resultado.Should().NotBeNull();
        resultado.Saldo.Should().Be(3000.00m);
    }

    [Fact]
    public void FluxoCaixaService_DeveImplementarIFluxoCaixaService()
    {
        // Assert
        _service.Should().BeAssignableTo<IFluxoCaixaService>();
    }
}
