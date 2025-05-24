using BDFuncional.Domain;
using BDFuncional.Domain.Interface;
using Domain.Entities;

namespace DBFuncional.Application.Service;

public class ContaService(IContaRepository repository) : IContaService
{
    public async Task<Conta> Depositar(string numeroConta, decimal valor)
    {
        var conta = await ObterConta(numeroConta); 
        conta.Saldo += valor;
        await repository.Atualizar(conta);
        return conta;
    }

    public async Task<Conta> Sacar(string numeroConta, decimal valor)
    {
        var conta = await ObterConta(numeroConta); 
        if (conta.Saldo < valor)
            throw new Exception(MessageException.SaldoInsuficiente);
        conta.Saldo -= valor;
        await repository.Atualizar(conta);
        return conta;
    }

    public async Task<decimal> ObterSaldo(string numeroConta)
    {
        var conta = await ObterConta(numeroConta);
        return conta.Saldo;
    }


    private async Task<Conta> ObterConta(string numeroConta)
    {
        var conta = await repository.ObterPorNumero(numeroConta);
        if (conta == null)
            throw new Exception("Conta não encontrada");
        return conta;
    }
}
