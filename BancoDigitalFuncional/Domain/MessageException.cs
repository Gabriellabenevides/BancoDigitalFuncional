namespace BDFuncional.Domain;

public class SaldoInsuficienteException : Exception
{
    public SaldoInsuficienteException() : base() { }
}

public class ContaNaoEncontradaException : Exception
{
    public ContaNaoEncontradaException() : base() { }
}

public class ArgumentoNull : Exception
{
    public ArgumentoNull() : base() { }
}

public class ValorNegativo : Exception
{
    public ValorNegativo() : base() { }
}