using ControleFinanceiro.Bussiness.Models;
using FluentAssertions;
using static ControleFinanceiro.Bussiness.Models.Transacao;

namespace ControleFinanceiro.Bussiness.Tests.Models;

public class TransacaoTests
{
    [Fact]
    public void Transacao_DeveCriarInstanciaComPropriedadesCorretas()
    {
        // Arrange
        var tipo = TipoTransacao.Receita;
        var data = new DateTime(2023, 1, 15);
        var descricao = "Salário";
        var valor = 5000.00m;

        // Act
        var transacao = new Transacao
        {
            Tipo = tipo,
            Data = data,
            Descricao = descricao,
            Valor = valor
        };

        // Assert
        transacao.Tipo.Should().Be(tipo);
        transacao.Data.Should().Be(data);
        transacao.Descricao.Should().Be(descricao);
        transacao.Valor.Should().Be(valor);
    }

    [Theory]
    [InlineData(TipoTransacao.Receita)]
    [InlineData(TipoTransacao.Despesa)]
    public void Transacao_DeveAceitarTiposValidos(TipoTransacao tipo)
    {
        // Arrange & Act
        var transacao = new Transacao
        {
            Tipo = tipo,
            Data = DateTime.Now,
            Descricao = "Teste",
            Valor = 100.00m
        };

        // Assert
        transacao.Tipo.Should().Be(tipo);
    }
}
