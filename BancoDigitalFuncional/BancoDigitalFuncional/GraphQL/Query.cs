using BDFuncional.Domain.Interface;

namespace BancoDigitalFuncional.GraphQL;

public class Query
{
    private readonly IContaService _service;

    public Query(IContaService service)
    {
        _service = service;
    }

    public async Task<decimal> Saldo(string numeroConta)
    {
        return await _service.ObterSaldo(numeroConta);
    }
}
