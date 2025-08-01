using ControleFinanceiro.Bussiness.Models;
using ControleFinanceiro.Data.Repositories;
using ControleFinanceiro.Data.Tests.Helpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using static ControleFinanceiro.Bussiness.Models.Transacao;

namespace ControleFinanceiro.Data.Tests.Repositories;

public class TransacaoRepositoryDataTests : DataTestBase
{
    private readonly TransacaoRepository _repository;

    public TransacaoRepositoryDataTests()
    {
        _repository = new TransacaoRepository(Context);
    }

    #region CRUD Operations Tests

    [Fact]
    public async Task Adicionar_TransacaoValida_DeveInserirNoBanco()
    {
        // Arrange
        var transacao = DataTestFactory.CriarTransacao(
            descricao: "Teste Inserção",
            valor: 1000m,
            tipo: TipoTransacao.Receita
        );

        // Act
        _repository.Adicionar(transacao);
        await _repository.SaveChanges();

        // Assert
        var transacaoSalva = await Context.Transacoes.FirstOrDefaultAsync(t => t.Id == transacao.Id);
        transacaoSalva.Should().NotBeNull();
        transacaoSalva!.Descricao.Should().Be("Teste Inserção");
        transacaoSalva.Valor.Should().Be(1000m);
        transacaoSalva.Tipo.Should().Be(TipoTransacao.Receita);
    }

    [Fact]
    public async Task ObterPorId_TransacaoExistente_DeveRetornarTransacao()
    {
        // Arrange
        var transacao = DataTestFactory.CriarTransacao(descricao: "Busca por ID");
        await Context.Transacoes.AddAsync(transacao);
        await Context.SaveChangesAsync();

        // Act
        var resultado = await _repository.ObterPorId(transacao.Id);

        // Assert
        resultado.Should().NotBeNull();
        resultado!.Id.Should().Be(transacao.Id);
        resultado.Descricao.Should().Be("Busca por ID");
    }

    [Fact]
    public async Task ObterTodos_ComTransacoes_DeveRetornarTodas()
    {
        // Arrange
        var transacoes = DataTestFactory.CriarTransacoes(3);
        await Context.Transacoes.AddRangeAsync(transacoes);
        await Context.SaveChangesAsync();

        // Act
        var resultado = await _repository.ObterTodos();

        // Assert
        resultado.Should().HaveCount(3);
    }

    [Fact]
    public async Task Atualizar_TransacaoExistente_DeveModificarNoBanco()
    {
        // Arrange
        var transacao = DataTestFactory.CriarTransacao(descricao: "Original", valor: 500m);
        await Context.Transacoes.AddAsync(transacao);
        await Context.SaveChangesAsync();

        // Act
        transacao.Descricao = "Atualizada";
        transacao.Valor = 750m;
        transacao.SetUpdatedAt();
        
        _repository.Atualizar(transacao);
        await _repository.SaveChanges();

        // Assert
        var transacaoAtualizada = await Context.Transacoes.FirstOrDefaultAsync(t => t.Id == transacao.Id);
        transacaoAtualizada.Should().NotBeNull();
        transacaoAtualizada!.Descricao.Should().Be("Atualizada");
        transacaoAtualizada.Valor.Should().Be(750m);
        transacaoAtualizada.Alteracao.Should().NotBeNull();
    }

    [Fact]
    public async Task Remover_TransacaoExistente_DeveExcluirDoBanco()
    {
        // Arrange
        var transacao = DataTestFactory.CriarTransacao();
        await Context.Transacoes.AddAsync(transacao);
        await Context.SaveChangesAsync();

        // Act
        _repository.Remover(transacao.Id);
        await _repository.SaveChanges();

        // Assert
        var transacaoRemovida = await Context.Transacoes.FirstOrDefaultAsync(t => t.Id == transacao.Id);
        transacaoRemovida.Should().BeNull();
    }

    [Fact]
    public async Task ObterPorPeriodo_PeriodoEspecifico_DeveRetornarTransacoesDoPeriodo()
    {
        // Arrange
        var dataInicio = new DateTime(2024, 1, 1);
        var dataFim = new DateTime(2024, 1, 31);
        
        var transacoesDentro = DataTestFactory.CriarTransacoesPorPeriodo(dataInicio, dataFim, 2);
        var transacoesFora = DataTestFactory.CriarTransacoesPorPeriodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 28), 1);
        
        await Context.Transacoes.AddRangeAsync(transacoesDentro);
        await Context.Transacoes.AddRangeAsync(transacoesFora);
        await Context.SaveChangesAsync();

        // Act
        var resultado = await _repository.ObterPorPeriodo(dataInicio, dataFim);

        // Assert
        resultado.Should().HaveCount(2);
        resultado.Should().OnlyContain(t => t.Data >= dataInicio && t.Data <= dataFim);
    }

    [Fact]
    public async Task ObterPorPeriodo_SemParametros_DeveRetornarTodasTransacoes()
    {
        // Arrange
        var transacoes = DataTestFactory.CriarTransacoes(3);
        await Context.Transacoes.AddRangeAsync(transacoes);
        await Context.SaveChangesAsync();

        // Act
        var resultado = await _repository.ObterPorPeriodo();

        // Assert
        resultado.Should().HaveCount(3);
    }

    [Fact]
    public async Task ObterPorPeriodo_DeveRetornarOrdenadoPorDataDecrescente()
    {
        // Arrange
        var transacoes = new[]
        {
            DataTestFactory.CriarTransacao(data: new DateTime(2024, 1, 1)),
            DataTestFactory.CriarTransacao(data: new DateTime(2024, 3, 1)),
            DataTestFactory.CriarTransacao(data: new DateTime(2024, 2, 1))
        };
        
        await Context.Transacoes.AddRangeAsync(transacoes);
        await Context.SaveChangesAsync();

        // Act
        var resultado = await _repository.ObterPorPeriodo();

        // Assert
        resultado.Should().BeInDescendingOrder(t => t.Data);
    }

    [Fact]
    public async Task ObterPorId_IdInexistente_DeveRetornarNull()
    {
        // Arrange
        var idInexistente = Guid.NewGuid();

        // Act
        var resultado = await _repository.ObterPorId(idInexistente);

        // Assert
        resultado.Should().BeNull();
    }

    [Fact]
    public async Task Buscar_ComPredicado_DeveRetornarTransacoesFiltradas()
    {
        // Arrange
        var transacoes = new[]
        {
            DataTestFactory.CriarTransacao(tipo: TipoTransacao.Receita, valor: 1000m),
            DataTestFactory.CriarTransacao(tipo: TipoTransacao.Despesa, valor: 500m)
        };
        
        await Context.Transacoes.AddRangeAsync(transacoes);
        await Context.SaveChangesAsync();

        // Act
        var receitas = await _repository.Buscar(t => t.Tipo == TipoTransacao.Receita);

        // Assert
        receitas.Should().HaveCount(1);
        receitas.Should().OnlyContain(t => t.Tipo == TipoTransacao.Receita);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _repository?.Dispose();
        }
        base.Dispose(disposing);
    }
}
