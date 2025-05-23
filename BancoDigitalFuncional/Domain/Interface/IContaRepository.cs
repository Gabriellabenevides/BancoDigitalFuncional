using Domain.Entities;

namespace BDFuncional.Domain.Interface;

public interface IContaRepository
{
    Task<Conta?> ObterPorNumero(string numeroConta);
    Task Atualizar(Conta conta);
}
