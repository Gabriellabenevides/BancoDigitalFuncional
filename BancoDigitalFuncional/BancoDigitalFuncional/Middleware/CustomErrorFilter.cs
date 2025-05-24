using BDFuncional.Domain;
using Microsoft.VisualBasic;

public class CustomErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is SaldoInsuficienteException)
        {
            return error
                .WithMessage(Constantes.SaldoInsuficiente)
                .RemoveExtensions();
        }
        if (error.Exception is ContaNaoEncontradaException)
        {
            return error
                .WithMessage(Constantes.ContaNaoEncontrada)
                .RemoveExtensions();
        }

        if (error.Exception is ArgumentoNull)
        {
            return error
                .WithMessage(Constantes.ArgumentoNulo)
                .RemoveExtensions();
        }

        if (error.Exception is ValorNegativo)
        {
            return error
                .WithMessage(Constantes.ValorNegativo)
                .RemoveExtensions();
        }

        else if (error.Exception is ArgumentException)
        {
            return error
                .WithMessage(Constantes.ErroInterno)
                .RemoveExtensions();
        }
        return error;
    }
}