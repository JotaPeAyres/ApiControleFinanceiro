using ControleFinanceiro.Bussiness.Interfaces;
using ControleFinanceiro.Bussiness.Models;
using ControleFinanceiro.Bussiness.Notificacoes;
using ControleFinanceiro.Bussiness.Services;
using FluentAssertions;
using Moq;
using static ControleFinanceiro.Bussiness.Models.Transacao;

namespace ControleFinanceiro.Bussiness.Tests.Integration;

public class TransacaoIntegrationTests
{
    [Fact]
    public async Task AdicionarTransacao_ComServicoERepository_DeveExecutarCorretamente()
    {
        // Arrange
        var mockRepository = new Mock<ITransacaoRepository>();
        var notificador = new Notificador();
        var service = new TransacaoService(notificador, mockRepository.Object);
        
        var transacao = new Transacao
        {
            Tipo = TipoTransacao.Receita,
            Data = DateTime.Today,
            Descricao = "Salário",
            Valor = 5000.00m
        };

        // Act
        await service.Adicionar(transacao);

        // Assert
        notificador.TemNotificacao().Should().BeFalse();
        mockRepository.Verify(r => r.Adicionar(It.IsAny<Transacao>()), Times.Once);
    }

    [Fact]
    public async Task FluxoCaixaService_ComRepository_DeveCalcularSaldo()
    {
        // Arrange
        var transacoes = new List<Transacao>
        {
            new() { Tipo = TipoTransacao.Receita, Valor = 5000.00m, Data = DateTime.Today },
            new() { Tipo = TipoTransacao.Despesa, Valor = 2000.00m, Data = DateTime.Today }
        };

        var mockRepository = new Mock<ITransacaoRepository>();
        mockRepository.Setup(r => r.ObterTodos()).ReturnsAsync(transacoes);
        
        var notificador = new Notificador();
        var service = new FluxoCaixaService(notificador, mockRepository.Object);

        // Act
        var saldo = await service.ObterSaldoAtualAsync();

        // Assert
        saldo.Should().NotBeNull();
        saldo.Saldo.Should().Be(3000.00m);
    }

    [Fact]
    public void ServicosIntegrados_DevemSerInstanciadosCorretamente()
    {
        // Arrange
        var mockRepository = new Mock<ITransacaoRepository>();
        var notificador = new Notificador();

        // Act
        var transacaoService = new TransacaoService(notificador, mockRepository.Object);
        var fluxoCaixaService = new FluxoCaixaService(notificador, mockRepository.Object);

        // Assert
        transacaoService.Should().NotBeNull();
        fluxoCaixaService.Should().NotBeNull();
        transacaoService.Should().BeAssignableTo<ITransacaoService>();
        fluxoCaixaService.Should().BeAssignableTo<IFluxoCaixaService>();
    }
}
