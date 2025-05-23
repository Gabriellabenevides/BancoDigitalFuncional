using DBFuncional.Application.Service;

namespace BancoDigitalFuncional.GraphQL;

public class Query
{
    private readonly ContaService _service;

    public Query(ContaService service)
    {
        _service = service;
    }

    public decimal Saldo(string numeroConta)
    {
        return _service.ObterSaldo(numeroConta);
    }
}
