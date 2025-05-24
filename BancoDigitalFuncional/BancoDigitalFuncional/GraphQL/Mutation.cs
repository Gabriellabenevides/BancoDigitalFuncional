using BDFuncional.Domain.Interface;
using Domain.Entities;

namespace BancoDigitalFuncional.GraphQL;

public class Mutation
{
    public async Task<Conta> Sacar(string numeroConta, decimal valor, [Service] IContaService service)
    {
        return await service.Sacar(numeroConta, valor);
    }

    public async Task<Conta> Depositar(string numeroConta, decimal valor, [Service] IContaService service)
    {
        return await service.Depositar(numeroConta, valor);
    }
}
