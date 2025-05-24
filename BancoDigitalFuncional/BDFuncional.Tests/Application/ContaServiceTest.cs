using BDFuncional.Domain;
using BDFuncional.Domain.Interface;
using DBFuncional.Application.Service;
using Domain.Entities;
using Moq;

namespace BDFuncional.Tests.Application;

public class ContaServiceTest : GeradorDados
{
    private readonly Mock<IContaRepository> _mockRepository;
    private readonly ContaService _service;

    public ContaServiceTest()
    {
        _mockRepository = new Mock<IContaRepository>();
        _service = new ContaService(_mockRepository.Object);
    }

    [Fact]
    public async Task Depositar_DeveSomarSaldoERetornarConta()
    {
        // Arrange
        var conta = Criar<Conta>();
        setupObterPorNumero(conta);
        var valorNaoAtualizado = conta.Saldo;

        // Act
        var resultado = await _service.Depositar(conta.NumeroConta, 50m);

        // Assert
        Assert.Equal(valorNaoAtualizado + 50m, resultado.Saldo);
        verifyObterPorNumero(conta.NumeroConta, Times.Once());
        verifyAtualizar(conta, Times.Once());
    }

    [Fact]
    public async Task Sacar_DeveSubtrairSaldoERetornarConta()
    {
        // Arrange
        var conta = Criar<Conta>();
        setupObterPorNumero(conta);
        var valorNaoAtualizado = conta.Saldo;

        // Act
        var resultado = await _service.Sacar(conta.NumeroConta, 1);

        // Assert
        Assert.Equal(valorNaoAtualizado - 1m, resultado.Saldo);
        verifyObterPorNumero(conta.NumeroConta, Times.Once());
        verifyAtualizar(conta, Times.Once());
    }

    [Fact]
    public async Task Sacar_ComSaldoInsuficiente_DeveLancarExcecao()
    {
        // Arrange
        var conta = Criar<Conta>();
        conta.Saldo = 10m;
        setupObterPorNumero(conta);

        // Act & Assert
        await Assert.ThrowsAsync<SaldoInsuficienteException>(() => _service.Sacar(conta.NumeroConta, 20m));
        verifyAtualizar(conta, Times.Never());
    }

    [Fact]
    public async Task ObterSaldo_DeveRetornarSaldo()
    {
        // Arrange
        var conta = Criar<Conta>();
        setupObterPorNumero(conta);

        // Act
        var saldo = await _service.ObterSaldo(conta.NumeroConta);

        // Assert
        Assert.Equal(conta.Saldo, saldo);
        verifyAtualizar(conta, Times.Never());
    }

    [Fact]
    public async Task Operacao_ComContaInexistente_DeveLancarExcecao()
    {
        // Arrange
        _mockRepository.Setup(r => r.ObterPorNumero("inexistente")).ReturnsAsync((Conta?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ContaNaoEncontradaException>(() => _service.ObterSaldo("inexistente"));
    }

    [Fact]
    public async Task Depositar_ComNumeroContaNulo_DeveLancarExcecao()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentoNull>(() => _service.Depositar(null!, 10m));
    }

    [Fact]
    public async Task Depositar_ComValorNegativo_DeveLancarExcecao()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ValorNegativo>(() => _service.Depositar("123", -10m));
    }

    private void verifyObterPorNumero(string numeroConta, Times times)
    {
        _mockRepository.Verify(r => r.ObterPorNumero(numeroConta), times);
    }
    private void verifyAtualizar(Conta conta, Times times)
    {
        _mockRepository.Verify(r => r.Atualizar(conta), times);
    }

    private void setupObterPorNumero(Conta conta)
    {
        _mockRepository.Setup(r => r.ObterPorNumero(conta.NumeroConta)).ReturnsAsync(conta);
    }
}
