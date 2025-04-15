using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Validadores
{

    public class ValidadorEmail
    {

        public string Validar()
        {
            string emailUsuario = "";
            Regex regex = new Regex(@"^[^@\s]+@[^@\s]+.[^@\s]+$");

            {
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
    }
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


    }

        
    