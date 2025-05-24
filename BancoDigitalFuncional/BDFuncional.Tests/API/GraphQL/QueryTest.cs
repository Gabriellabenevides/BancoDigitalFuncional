using BancoDigitalFuncional.GraphQL;
using BDFuncional.Domain.Interface;
using Domain.Entities;
using Moq;

namespace BDFuncional.Tests.API.GraphQL;

public class QueryTest : GeradorDados
{
    private readonly Mock<IContaService> _mockContaService;
    private readonly Query _query;

    public QueryTest()
    {
        _mockContaService = new Mock<IContaService>();
        _query = new Query();
    }

    [Fact]
    public async Task Saldo_DeveRetornarSaldo()
    {
        // Arrange
        var conta = Criar<Conta>();

        _mockContaService
            .Setup(service => service.ObterSaldo(conta.NumeroConta))
            .ReturnsAsync(conta.Saldo);

        // Act
        var resultado = await _query.Saldo(conta.NumeroConta, _mockContaService.Object);

        // Assert
        Assert.Equal(conta.Saldo, resultado);
    }
}
