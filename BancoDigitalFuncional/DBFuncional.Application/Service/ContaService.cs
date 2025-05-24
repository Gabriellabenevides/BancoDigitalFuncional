using BDFuncional.Domain;
using BDFuncional.Domain.Interface;
using Domain.Entities;

namespace DBFuncional.Application.Service;

public class ContaService(IContaRepository repository) : IContaService
{
    public async Task<Conta> Depositar(string numeroConta, decimal valor)
    {
        ValidaDados(numeroConta, valor);
        var conta = await ObterConta(numeroConta);

        conta.Saldo += valor;
        await repository.Atualizar(conta);
        return conta;
    }

    public async Task<Conta> Sacar(string numeroConta, decimal valor)
    {
        ValidaDados(numeroConta, valor);
        var conta = await ObterConta(numeroConta);

        if (conta.Saldo < valor)
            throw new SaldoInsuficienteException();
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
            throw new ContaNaoEncontradaException();
        return conta;
    }

    private bool ValidaDados(string numeroConta, decimal valor)
    {
        if (string.IsNullOrEmpty(numeroConta))
            throw new ArgumentoNull();

        if (valor <= 0)
            throw new ValorNegativo();

        return true;
    }
}