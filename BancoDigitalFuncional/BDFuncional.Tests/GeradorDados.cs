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

    public List<T> CriarLista<T>(int quantidade = 5) where T : class
    {
        return _fixture.CreateMany<T>(quantidade).ToList();
    }
}