using Domain.Entities;

namespace BDFuncional.Domain.Interface;

public interface IContaService
{
    public Task<Conta> Depositar(string numeroConta, decimal valor);
    public Task<Conta> Sacar(string numeroConta, decimal valor);
    public Task<decimal> ObterSaldo(string numeroConta);
}
