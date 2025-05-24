using BDFuncional.Domain.Interface;

namespace BancoDigitalFuncional.GraphQL;

public class Query
{
    public async Task<decimal> Saldo(string numeroConta, [Service] IContaService service)
    {
        return await service.ObterSaldo(numeroConta);
    }
}
