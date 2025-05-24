using AutoFixture;

namespace BDFuncional.Tests;

public class GeradorDados
{
    private readonly Fixture _fixture;

    public GeradorDados()
    {
        _fixture = new Fixture();
    }

    public T Criar<T>() where T : class
    {
        return _fixture.Create<T>();
    }
}