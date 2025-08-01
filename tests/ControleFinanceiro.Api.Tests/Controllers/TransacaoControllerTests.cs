using ControleFinanceiro.Api.Controllers;
using ControleFinanceiro.Bussiness.Interfaces;
using ControleFinanceiro.Bussiness.Models;
using ControleFinanceiro.Bussiness.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static ControleFinanceiro.Bussiness.Models.Transacao;

namespace ControleFinanceiro.Api.Tests.Controllers;

public class TransacaoControllerTests
{
    private readonly Mock<ITransacaoService> _mockTransacaoService;
    private readonly Mock<ITransacaoRepository> _mockTransacaoRepository;
    private readonly Mock<INotificador> _mockNotificador;
    private readonly TransacaoController _controller;

    public TransacaoControllerTests()
    {
        _mockTransacaoService = new Mock<ITransacaoService>();
        _mockTransacaoRepository = new Mock<ITransacaoRepository>();
        _mockNotificador = new Mock<INotificador>();
        _controller = new TransacaoController(
            _mockTransacaoService.Object,
            _mockTransacaoRepository.Object,
            _mockNotificador.Object);
    }

    [Fact]
    public async Task ObterTodos_DeveRetornarListaDeTransacoes()
    {
        // Arrange
        var transacoes = new List<Transacao>
        {
            new() { Id = Guid.NewGuid(), Tipo = TipoTransacao.Receita, Descricao = "Salário", Valor = 5000.00m, Data = DateTime.Today },
            new() { Id = Guid.NewGuid(), Tipo = TipoTransacao.Despesa, Descricao = "Mercado", Valor = 300.00m, Data = DateTime.Today }
        };

        _mockTransacaoRepository.Setup(r => r.ObterTodos())
                                .ReturnsAsync(transacoes);

        // Act
        var resultado = await _controller.ObterTodos();

        // Assert
        resultado.Should().NotBeNull();
        resultado.Should().HaveCount(2);
    }

    [Fact]
    public async Task ObterPorId_ComIdValido_DeveRetornarTransacao()
    {
        // Arrange
        var id = Guid.NewGuid();
        var transacao = new Transacao
        {
            Id = id,
            Tipo = TipoTransacao.Receita,
            Descricao = "Salário",
            Valor = 5000.00m,
            Data = DateTime.Today
        };

        _mockTransacaoRepository.Setup(r => r.ObterPorId(id))
                                .ReturnsAsync(transacao);

        // Act
        var resultado = await _controller.ObterPorId(id);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task Adicionar_ComTransacaoValida_DeveRetornarOk()
    {
        // Arrange
        var viewModel = new AddTransacaoViewModel
        {
            Tipo = TipoTransacao.Receita,
            Descricao = "Salário",
            Valor = 5000.00m,
            Data = DateTime.Today
        };

        _mockNotificador.Setup(n => n.TemNotificacao()).Returns(false);

        // Act
        var resultado = await _controller.Adicionar(viewModel);

        // Assert
        resultado.Should().NotBeNull();
        _mockTransacaoService.Verify(s => s.Adicionar(It.IsAny<Transacao>()), Times.Once);
    }

    [Fact]
    public void TransacaoController_DeveHerdarDeMainController()
    {
        // Assert
        _controller.Should().BeAssignableTo<MainController>();
    }
}
