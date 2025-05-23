using BDFuncional.Domain;
using BDFuncional.Domain.Interface;
using Domain.Entities;

namespace DBFuncional.Application.Service;

public class ContaService(IContaRepository repository)
{
    public async Task<Conta> Depositar(string numeroConta, decimal valor)
    {
        var conta = ObterConta(numeroConta).Result;
        conta.Saldo += valor;
        await repository.Atualizar(conta);
        return conta;
    }
    public async Task<Conta> Sacar(string numeroConta, decimal valor)
    {
        var conta = ObterConta(numeroConta).Result;
        if (conta.Saldo < valor)
            throw new Exception(MessageException.SaldoInsuficiente);
        conta.Saldo -= valor;
        await repository.Atualizar(conta);
        return conta;
    }
    public decimal ObterSaldo(string numeroConta)
    {
        var conta = ObterConta(numeroConta).Result;
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
