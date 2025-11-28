using static System.Console;

public class UX
{
    private readonly Banco _banco;
    private readonly string _titulo;

    public UX(string titulo, Banco banco)
    {
        _titulo = titulo;
        _banco = banco;
    }

    public void Executar()
    {
        string opcao = "";

        while (opcao != "9")
        {
            CriarTitulo(_titulo);
            WriteLine(" [1] Criar Conta");
            WriteLine(" [2] Listar Contas");
            WriteLine(" [3] Efetuar Saque");
            WriteLine(" [4] Efetuar Depósito");
            WriteLine(" [5] Aumentar Limite");
            WriteLine(" [6] Diminuir Limite");

            ForegroundColor = ConsoleColor.Red;
            WriteLine(" [9] Sair");
            ForegroundColor = ConsoleColor.White;

            CriarLinha();
            ForegroundColor = ConsoleColor.Yellow;
            Write(" Digite a opção desejada: ");
            opcao = ReadLine() ?? "";
            ForegroundColor = ConsoleColor.White;

            switch (opcao)
            {
                case "1": CriarConta(); break;
                case "2": MenuListarContas(); break;
                case "3": MenuSaque(); break;
                case "4": MenuDeposito(); break;
                case "5": MenuAumentarLimite(); break;
                case "6": MenuDiminuirLimite(); break;
            }

            WriteLine("\nPressione ENTER para continuar...");
            ReadLine();
        }

        _banco.SaveContas();
    }

    private void CriarConta()
    {
        Clear();
        CriarTitulo("Criar Conta");

        Write("Nome do cliente: ");
        var nome = ReadLine() ?? "";

        Write("CPF: ");
        var cpf = ReadLine() ?? "";

        Write("Senha: ");
        var senha = ReadLine() ?? "";

        int numeroConta = _banco.Contas.Count > 0 ?
            _banco.Contas.Max(c => c.Numero) + 1 : 1;

        var conta = new Conta(numeroConta, nome, cpf, senha);

        _banco.Contas.Add(conta);

        CriarRodape($"Conta criada com sucesso! Número da conta: {numeroConta}");
    }

    private void MenuListarContas()
    {
        Clear();
        CriarTitulo("Lista de Contas");

        foreach (var c in _banco.Contas)
        {
            WriteLine($"Conta: {c.Numero} | Cliente: {c.Cliente} | Saldo: {c.Saldo:C}");
        }

        if (_banco.Contas.Count == 0)
            WriteLine("Nenhuma conta cadastrada.");
    }

    private void MenuSaque()
    {
        Clear();
        CriarTitulo("Efetuar Saque");

        Write("Número da conta: ");
        int numero = int.Parse(ReadLine() ?? "0");

        var conta = _banco.BuscarConta(numero);

        if (conta == null)
        {
            CriarRodape("Conta não encontrada!");
            return;
        }

        Write("Valor do saque: ");
        decimal valor = decimal.Parse(ReadLine() ?? "0");

        conta.Sacar(valor, out string msg);
        CriarRodape(msg);
    }

    private void MenuDeposito()
    {
        Clear();
        CriarTitulo("Efetuar Depósito");

        Write("Número da conta: ");
        int numero = int.Parse(ReadLine() ?? "0");

        var conta = _banco.BuscarConta(numero);

        if (conta == null)
        {
            CriarRodape("Conta não encontrada!");
            return;
        }

        Write("Valor do depósito: ");
        decimal valor = decimal.Parse(ReadLine() ?? "0");

        conta.Depositar(valor, out string msg);
        CriarRodape(msg);
    }

    private void MenuAumentarLimite()
    {
        Clear();
        CriarTitulo("Aumentar Limite");

        Write("Número da conta: ");
        int numero = int.Parse(ReadLine() ?? "0");

        var conta = _banco.BuscarConta(numero);

        if (conta == null)
        {
            CriarRodape("Conta não encontrada!");
            return;
        }

        Write("Valor: ");
        decimal valor = decimal.Parse(ReadLine() ?? "0");

        conta.AumentarLimite(valor, out string msg);
        CriarRodape(msg);
    }

    private void MenuDiminuirLimite()
    {
        Clear();
        CriarTitulo("Diminuir Limite");

        Write("Número da conta: ");
        int numero = int.Parse(ReadLine() ?? "0");

        var conta = _banco.BuscarConta(numero);

        if (conta == null)
        {
            CriarRodape("Conta não encontrada!");
            return;
        }

        Write("Valor: ");
        decimal valor = decimal.Parse(ReadLine() ?? "0");

        conta.DiminuirLimite(valor, out string msg);
        CriarRodape(msg);
    }

    private void CriarLinha()
    {
        WriteLine("-----------------------------------------");
    }

    private void CriarTitulo(string titulo)
    {
        Clear();
        WriteLine("=========================================");
        WriteLine($"      {titulo}");
        WriteLine("=========================================");
    }

    private void CriarRodape(string? mensagem = null)
    {
        WriteLine("-----------------------------------------");
        if (mensagem != null)
            WriteLine(mensagem);
        WriteLine("-----------------------------------------");
    }
}
