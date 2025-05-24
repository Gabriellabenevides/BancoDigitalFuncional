using BancoDigitalFuncional.GraphQL;
using BDFuncional.Domain.Interface;
using Domain.Entities;
using Moq;

namespace BDFuncional.Tests.API.GraphQL;

public class MutationTest : GeradorDados
{
    private readonly Mock<IContaService> _mockContaService;
    private readonly Mutation _mutation;

    public MutationTest()
    {
        _mockContaService = new Mock<IContaService>();
        _mutation = new Mutation();
    }

    [Fact]
    public async Task Sacar_DeveRetornarContaAtualizada()
    {
        // Arrange
        var contaEsperada = Criar<Conta>();
        ConfigurarSaque("123", 10m, contaEsperada);

        // Act
        var resultado = await _mutation.Sacar("123", 10m, _mockContaService.Object);

        // Assert
        Assert.Equal(contaEsperada, resultado);
    }

    [Fact]
    public async Task Depositar_DeveRetornarContaAtualizada()
    {
        // Arrange
        var contaEsperada = Criar<Conta>();
        ConfigurarDeposito("123", 10m, contaEsperada);

        // Act
        var resultado = await _mutation.Depositar("123", 10m, _mockContaService.Object);

        // Assert
        Assert.Equal(contaEsperada, resultado);
    }

    private void ConfigurarSaque(string contaId, decimal valor, Conta contaResultado)
    {
        _mockContaService.Setup(s => s.Sacar(contaId, valor))
                         .ReturnsAsync(contaResultado);
    }

    private void ConfigurarDeposito(string contaId, decimal valor, Conta contaResultado)
    {
        _mockContaService.Setup(s => s.Depositar(contaId, valor))
                         .ReturnsAsync(contaResultado);
    }
}
