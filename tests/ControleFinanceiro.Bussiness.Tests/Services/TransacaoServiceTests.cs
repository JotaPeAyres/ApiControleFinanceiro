using ControleFinanceiro.Bussiness.Interfaces;
using ControleFinanceiro.Bussiness.Models;
using ControleFinanceiro.Bussiness.Services;
using FluentAssertions;
using Moq;
using static ControleFinanceiro.Bussiness.Models.Transacao;

namespace ControleFinanceiro.Bussiness.Tests.Services;

public class TransacaoServiceTests
{
    private readonly Mock<ITransacaoRepository> _mockRepository;
    private readonly Mock<INotificador> _mockNotificador;
    private readonly TransacaoService _service;

    public TransacaoServiceTests()
    {
        _mockRepository = new Mock<ITransacaoRepository>();
        _mockNotificador = new Mock<INotificador>();
        _service = new TransacaoService(_mockNotificador.Object, _mockRepository.Object);
    }

    [Fact]
    public async Task Adicionar_ComTransacaoValida_DeveAdicionarTransacao()
    {
        // Arrange
        var transacao = new Transacao
        {
            Tipo = TipoTransacao.Receita,
            Data = DateTime.Now,
            Descricao = "Salário",
            Valor = 5000.00m
        };

        // Act
        await _service.Adicionar(transacao);

        // Assert
        _mockRepository.Verify(r => r.Adicionar(It.IsAny<Transacao>()), Times.Once);
    }

    [Fact]
    public async Task Remover_ComIdValido_DeveRemoverTransacao()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        await _service.Remover(id);

        // Assert
        _mockRepository.Verify(r => r.Remover(id), Times.Once);
    }

    [Fact]
    public void TransacaoService_DeveImplementarITransacaoService()
    {
        // Assert
        _service.Should().BeAssignableTo<ITransacaoService>();
    }
}
