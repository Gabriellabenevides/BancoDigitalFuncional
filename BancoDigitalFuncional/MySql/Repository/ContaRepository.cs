using BDFuncional.Domain.Interface;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MySql;

namespace BDFuncional.MySql.Repository;

public class ContaRepository : IContaRepository
{
    private readonly MySqlContext _mySqlContext;
    public ContaRepository(MySqlContext mySqlContext) 
    {  
        _mySqlContext = mySqlContext; 
    }
    public async Task Atualizar(Conta conta)
    {
        _mySqlContext.Contas.Update(conta);
        await _mySqlContext.SaveChangesAsync();
    }

    public async Task<Conta?> ObterPorNumero(string numeroConta)
    {
        return await _mySqlContext.Contas
            .FirstOrDefaultAsync(c => c.NumeroConta == numeroConta);   
    }
}
