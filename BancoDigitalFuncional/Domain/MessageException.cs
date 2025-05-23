namespace BDFuncional.Domain;

public static class MessageException
{
    public const string SaldoInsuficiente = "Saldo insuficiente.";
    public const string ContaNaoEncontrada = "Conta não encontrada.";
    public const string ValorInvalido = "Valor informado é inválido.";
    public const string ErroInterno = "Ocorreu um erro interno no servidor.";
    public const string AcessoNaoAutorizado = "Acesso não autorizado.";
    public const string ArgumentoNulo = "Um argumento obrigatório não foi informado.";
    public const string ArgumentoInvalido = "Um argumento informado é inválido.";
    public const string ConflitoBancoDados = "Conflito ao atualizar o banco de dados.";
}
