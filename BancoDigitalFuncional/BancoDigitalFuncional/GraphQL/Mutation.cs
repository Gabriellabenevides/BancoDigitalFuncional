using BDFuncional.Domain.Interface;
using Domain.Entities;

namespace BancoDigitalFuncional.GraphQL;

public class Mutation
{
    private readonly IContaService _service;

    public Mutation(IContaService service)
    {
        _service = service;
    }

    public async Task<Conta> Sacar(string numeroConta, decimal valor)
    {
        return await _service.Sacar(numeroConta, valor);
    }

    public async Task<Conta> Depositar(string numeroConta, decimal valor)
    {
        return await _service.Depositar(numeroConta, valor);
    }
}
