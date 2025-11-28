using System.Text.Json.Serialization;

public class Conta
{
    public int Numero { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    [JsonIgnore]
    public string Senha { get; set; } = string.Empty;
    public decimal Saldo { get; set; }
    public decimal Limite { get; set; }

    public decimal SaldoDisponível => Saldo + Limite;

    public Conta(int numero, string cliente, string cpf, string senha, decimal saldo = 0, decimal limite = 0)
    {
        Numero = numero;
        Cliente = cliente;
        Cpf = cpf;
        Senha = senha;
        Saldo = saldo;
        Limite = limite;
    }

    public bool Depositar(decimal valor, out string mensagem)
    {
        if (valor <= 0)
        {
            mensagem = "O valor para depósito deve ser maior que zero.";
            return false;
        }
        Saldo += valor;
        mensagem = $"Depósito realizado com sucesso! Novo saldo: {Saldo:C}";
        return true;
    }

    public bool Sacar(decimal valor, out string mensagem)
    {
        if (valor <= 0)
        {
            mensagem = "O valor do saque deve ser maior que zero.";
            return false;
        }
        if (valor > SaldoDisponível)
        {
            mensagem = $"Saldo insuficiente! Seu saldo disponível é de {SaldoDisponível:C}.";
            return false;
        }
        Saldo -= valor;
        mensagem = $"Saque realizado com sucesso! Saldo atual: {Saldo:C}";
        return true;
    }

    public bool AumentarLimite(decimal valor, out string mensagem)
    {
        if (valor <= 0)
        {
            mensagem = "O valor para aumentar o limite deve ser maior que zero.";
            return false;
        }
        Limite += valor;
        mensagem = $"Limite aumentado com sucesso! Novo limite: {Limite:C}";
        return true;
    }

    public bool DiminuirLimite(decimal valor, out string mensagem)
    {
        if (valor <= 0)
        {
            mensagem = "O valor para reduzir o limite deve ser maior que zero.";
            return false;
        }

        if (valor > Limite)
        {
            mensagem = $"Não é possível diminuir mais do que o limite atual ({Limite:C}).";
            return false;
        }

        Limite -= valor;

        mensagem = $"Limite reduzido com sucesso! Novo limite: {Limite:C}";
        return true;
    }

}
