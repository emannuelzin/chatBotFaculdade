﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Alunos;

namespace Validadores
{
    public class ValidadorNome
    {
        public string SolicitarNome()
        {
            string nome = "";
            while (string.IsNullOrWhiteSpace(nome))
            {
                Console.Write("Chatbot: Me informe seu nome: ");
                nome = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nome))
                {
                    Console.WriteLine("Chatbot: Nome inválido. Por favor, digite seu nome.");
                }
            }

            return nome;
        }
    }

    public class ValidadorTelefone
    {
        public string Validar()
        {
            string telefoneDigitado = "";
            Regex regex = new Regex(@"^(\+\d{2})?([1-9]{2})?(\d{4,5})(\d{4})$");
            while (true)
            {
                Console.Write("Chatbot: Me informe seu Telefone: ");
                telefoneDigitado = Console.ReadLine();
                if (regex.IsMatch(telefoneDigitado))
                {
                    Console.WriteLine("Chatbot: Telefone VALIDADO COM SUCESSO");
                    return telefoneDigitado;
                }
                else
                {
                    Console.WriteLine("Chatbot: Telefone INVÁLIDO. Tente novamente.");
                }
            }
        }
    }

    public class ValidadorEmail
    {
        public string Validar()
        {
            string emailUsuario = "";
            Regex regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            while (true)
            {
                Console.Write("Chatbot: Me informe seu e-mail: ");
                emailUsuario = Console.ReadLine();
                if (regex.IsMatch(emailUsuario))
                {
                    Console.WriteLine("Chatbot: E-mail VALIDADO COM SUCESSO");
                    return emailUsuario;
                }
                else
                {
                    Console.WriteLine("Chatbot: e-mail INVÁLIDO. Tente novamente.");
                }
            }
        }
    }

    public class ValidadorCPF
    {
        public string Validar()
        {
            while (true)
            {
                Console.Write("Chatbot: Me informe seu CPF: ");
                string cpfDigitado = Console.ReadLine();
                if (ValidarCPF(cpfDigitado))
                {
                    Console.WriteLine("Chatbot: CPF VALIDADO COM SUCESSO");
                    return cpfDigitado;
                }
                else
                {
                    Console.WriteLine("Chatbot: CPF INVÁLIDO. Tente novamente.");
                }
            }
        }

        public static bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;
            cpf = new string(cpf.Where(char.IsDigit).ToArray());
            if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
                return false;
            return cpf.EndsWith(CalcularDigitosVerificadores(cpf.Substring(0, 9)));
        }

        private static string CalcularDigitosVerificadores(string baseCpf)
        {
            int CalcularDigito(string cpfParcial, int[] pesos)
            {
                int soma = 0;
                for (int i = 0; i < cpfParcial.Length; i++)
                {
                    soma += (cpfParcial[i] - '0') * pesos[i];
                }

                int resto = soma % 11;
                return resto < 2 ? 0 : 11 - resto;
            }

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int digito1 = CalcularDigito(baseCpf, multiplicador1);
            int digito2 = CalcularDigito(baseCpf + digito1, multiplicador2);
            return $"{digito1}{digito2}";
        }
    }

    public class ValidadorConfirmacao
    {
        public static bool ConfirmarDados(string nome, string telefone, string email, string cpf)
        {
            string resposta = "";
            bool dadosConfirmados = false;
            while (!dadosConfirmados)
            {
                Console.WriteLine("\nChatbot: Por favor, confirme os seus dados.");
                Console.WriteLine($"Nome: {nome}");
                Console.WriteLine($"Telefone: {telefone}");
                Console.WriteLine($"E-mail: {email}");
                Console.WriteLine($"CPF: {cpf}");
                Console.Write("Chatbot: Os dados estão corretos? (sim/não): ");
                resposta = Console.ReadLine()?.ToLower();
                if (resposta == "sim")
                {
                    Console.WriteLine("Chatbot: Dados CONFIRMADOS COM SUCESSO!");
                    dadosConfirmados = true;
                }
                else
                {
                    Console.WriteLine("Chatbot: Por favor, digite os dados novamente.");
                    return false;
                }
            }

            return true;
        }
    }

    public class VerificarMatriculaExistente
    {
        private Dictionary<string, Aluno> alunos;

        public VerificarMatriculaExistente()
        {
            alunos = new Dictionary<string, Aluno>
            {
                {
                    "56402793892",
                    new Aluno("Maria", "56402793892", "11988888888", "maria@email.com", "G824130")
                }
            };
        }

        public Aluno ValCandidato()
        {
            var nomeVal = new Validadores.ValidadorNome();
            var emailVal = new Validadores.ValidadorEmail();
            var telefoneVal = new Validadores.ValidadorTelefone();
            var cpfVal = new Validadores.ValidadorCPF();
            string nome = nomeVal.SolicitarNome();
            string telefone = telefoneVal.Validar();
            string email = emailVal.Validar();
            string cpf = cpfVal.Validar();
            if (alunos.ContainsKey(cpf))
            {
                var alunoExistente = alunos[cpf];
                Console.WriteLine($"Chatbot: CPF já matriculado! Matrícula: {alunoExistente.Matricula}");
                return null;
            }
            else
            {
                Console.Write("Chatbot: Não matriculado. Deseja realizar a inscrição? (sim/não): ");
                string resposta = Console.ReadLine()?.ToLower();
                if (resposta == "sim")
                {
                    bool dadosConfirmados = ValidadorConfirmacao.ConfirmarDados(nome, telefone, email, cpf);
                    if (dadosConfirmados)
                    {
                        string matricula = GerarMatricula();
                        var novoAluno = new Alunos.Aluno(nome, cpf, telefone, email, matricula);
                        alunos.Add(cpf, novoAluno);
                        Console.WriteLine($"Chatbot: Matrícula efetivada com sucesso! Matrícula: {matricula}");
                        return novoAluno;
                    }
                    else
                    {
                        Console.Write("Dados incorretos, altere, por gentileza.");
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine("Chatbot: Inscrição não realizada.");
                    return null;
                }
            }
        }

        private string GerarMatricula()
        {
            return Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }
    }
}