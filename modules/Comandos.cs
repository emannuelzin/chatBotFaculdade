using System;
using System.Linq;
using System.Collections.Generic;

namespace Comandos
{
    public class IniciarAtendimento
    {
        public void Iniciar()
        {
            Console.WriteLine($"Seja bem-vindo!");
            Console.WriteLine("Eu sou uma invenção maluca do Matheus, tentando copiar a UNIPÊ.");
            Console.WriteLine("Informe sobre o que deseja tratar!");
            Console.WriteLine("Atualmente atendemos:");
            Console.WriteLine("TESOURARIA");
            Console.WriteLine("SECRETARIA");
            Console.WriteLine("TUTORIA");
            Console.WriteLine("Digite 'sair' para encerrar.");
        }
    }
}

namespace Alunos
{
    public class Aluno
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Matricula { get; set; }

        public Aluno(string nome, string cpf, string telefone, string email, string matricula)
        {
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            Email = email;
            Matricula = matricula;
        }

        public string GerarMatricula()
        {
            return Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }

        public void ComandoTutoria()
        {
            Console.WriteLine($"Chatbot: {Nome}, você pode ligar para a tutoria no 0800 010 9000 (opção 2 após o CPF).");
        }

        public void ComandoTesouraria()
        {
            Console.WriteLine($"Chatbot: Ok {Nome}, consultando suas pendências financeiras...\n");

            Random rnd = new Random();
            int quantidadeDividas = rnd.Next(0, 4);

            if (quantidadeDividas == 0)
            {
                Console.WriteLine($"Chatbot: Parabéns, {Nome}, você está sem pendências financeiras!");
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
            Console.WriteLine($"Chatbot: A parte da secretaria está em construção, {Nome}.");
        }
    }
}
