using BDFuncional.Domain;
using HotChocolate;
using Moq;

namespace BDFuncional.Tests.API;
public class CustomErrorFilterTest
{
    private readonly CustomErrorFilter _filter;

    public CustomErrorFilterTest()
    {
        _filter = new CustomErrorFilter();
    }

    [Theory]
    [InlineData(typeof(SaldoInsuficienteException), Constantes.SaldoInsuficiente)]
    [InlineData(typeof(ContaNaoEncontradaException), Constantes.ContaNaoEncontrada)]
    [InlineData(typeof(ArgumentoNull), Constantes.ArgumentoNulo)]
    [InlineData(typeof(ValorNegativo), Constantes.ValorNegativo)]
    [InlineData(typeof(ArgumentException), Constantes.ErroInterno)]
    public void OnError_DeveRetornarMensagemCorretaParaExcecoesConhecidas(Type exceptionType, string expectedMessage)
    {
        // Arrange
        var exception = (Exception)Activator.CreateInstance(exceptionType)!;
        var mockError = new Mock<IError>();
        mockError.SetupGet(e => e.Exception).Returns(exception);
        mockError.Setup(e => e.WithMessage(It.IsAny<string>())).Returns(mockError.Object);
        mockError.Setup(e => e.RemoveExtensions()).Returns(mockError.Object);

        // Act
        var result = _filter.OnError(mockError.Object);

        // Assert
        mockError.Verify(e => e.WithMessage(expectedMessage), Times.Once);
        mockError.Verify(e => e.RemoveExtensions(), Times.Once);
        Assert.Equal(mockError.Object, result);
    }

    [Fact]
    public void OnError_DeveRetornarErrorOriginalParaExcecaoDesconhecida()
    {
        // Arrange
        var exception = new InvalidOperationException();
        var mockError = new Mock<IError>();
        mockError.SetupGet(e => e.Exception).Returns(exception);

        // Act
        var result = _filter.OnError(mockError.Object);

        // Assert
        Assert.Equal(mockError.Object, result);
    }
}
