using System;
using System.Collections.Generic;
using System.Xml.Schema;
using ChatbotSemIA;
using System.Threading;
using Validadores;
public class Program
{
    public static void Main()
    {
        Console.WriteLine("Chatbot: Bem-vindo ao nosso atendimento!");


        ValidadorNome validadorNome = new ValidadorNome();
        string nomeUsuario = validadorNome.SolicitarNome();
        Console.WriteLine($"Chatbot: Olá, {nomeUsuario}!");

        ValidadorTelefone validadorTelefone = new ValidadorTelefone();
        string telefoneUsuario = validadorTelefone.Validar();

        ValidadorEmail validadorEmail = new ValidadorEmail();
        string emailUsuario= validadorEmail.Validar();

        ValidadorCPF validadorCpf = new ValidadorCPF();
        string cpfUsuario = validadorCpf.Validar();



        Console.WriteLine("Chatbot: Seus dados foram confirmados com sucesso.");
        Console.WriteLine("=================================================\n");


        var chatbot = new Chatbot(nomeUsuario);
        chatbot.Iniciar();
    }
}




public class Chatbot
{
    private readonly string nomeUsuario;
    private readonly Dictionary<string, Action> _comandos;

    public Chatbot(string nome)
    {
        nomeUsuario = nome;
        _comandos = new Dictionary<string, Action>
        {
            { "tutoria", ComandoTutoria },
            { "tesouraria", ComandoTesouraria },
            { "secretaria", ComandoSecretaria }
        };
    }

    public void Iniciar()
    {
        Console.WriteLine($"Seja bem-vindo(a), {nomeUsuario}!");
        Console.WriteLine("Eu sou uma invenção maluca do Matheus, tentando copiar a UNIPÊ.");
        Console.WriteLine("Informe sobre o que deseja tratar!");
        Console.WriteLine("Atualmente atendemos:");
        Console.WriteLine("TESOURARIA");
        Console.WriteLine("SECRETARIA");
        Console.WriteLine("TUTORIA");
        Console.WriteLine("Digite 'sair' para encerrar.");

        while (true)
        {
            Console.Write("\nVocê: ");
            string input = Console.ReadLine().ToLower();

            if (input == "sair")
            {
                Console.WriteLine($"\nChatbot: Até mais, {nomeUsuario}!");
                break;
            }

            if (input.Contains("tutoria"))
            {
                Thread.Sleep(500);
                ComandoTutoria();
            }
            else if (input.Contains("tesouraria"))
            {
                ComandoTesouraria();
            }
            else if (input.Contains("secretaria"))
            {
                ComandoSecretaria();
            }
            else
            {
                Console.WriteLine($"Chatbot: Desculpe, {nomeUsuario}, não entendi o que você disse.");
            }
        }
    }

    public void ComandoTutoria()
    {
        Console.WriteLine($"Chatbot: {nomeUsuario}, você pode ligar para a tutoria no 0800 010 9000 (opção 2 após o CPF).");
    }

    public void ComandoTesouraria()
    {


        Console.WriteLine($"Chatbot: Ok {nomeUsuario}, consultando suas pendências financeiras...\n");

        Random rnd = new Random();
        int quantidadeDividas = rnd.Next(0, 4);

        if (quantidadeDividas == 0)
        {
            Console.WriteLine($"Chatbot: Parabéns, {nomeUsuario}, você está sem pendências financeiras!");
            return;
        }

        string[] descricoes = {
        "Mensalidade Março",
        "Mensalidade Abril",
        "Mensalidade Janeiro",
        "Material Fevereiro",
        "Mensalidade Maio",
        "Mensalidade Junho"
    };

        decimal totalDividas = 0;

        for (int i = 1; i <= quantidadeDividas; i++)
        {
            string descricao = descricoes[rnd.Next(descricoes.Length)];
            decimal valor = rnd.Next(50, 7000);
            totalDividas += valor;

            Console.WriteLine($"- {descricao}: R$ {valor:F2}");
        }

        Console.WriteLine($"\nTotal em aberto: R$ {totalDividas:F2}");
        Console.WriteLine("Chatbot: Para resolver suas pendências, entre em contato com a tesouraria ou acesse o portal.");
    }


    public void ComandoSecretaria()
    {
        Console.WriteLine($"Chatbot: A parte da secretaria está em construção, {nomeUsuario}.");
    }
}



