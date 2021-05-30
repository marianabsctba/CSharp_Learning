/*
 Uso de dict conforme enunciado
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CadastroDoacoes
{
    class Program
    {
        private const string pressButtons = "Para exibir o menu principal aperte qualquer tecla ...";
        private static Dictionary<string, DateTime> donationList = new Dictionary<string, DateTime>();

        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

            string option;
            do
            {
                ShowMenu();

                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddDonation();
                        break;
                    case "2":
                        SearchDonation();
                        break;
                    case "3":
                        Console.Write("Você optou por sair do programa! Obrigada e volte sempre! ");
                        break;
                    default:
                        Console.Write("Opcao inválida! Por favor, escolha uma opção válida. ");
                        break;
                }

                Console.WriteLine(pressButtons);
                Console.ReadKey();
            }
            while (option != "3");
        }

        static void ShowMenu()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("==== Cadastro de doações Good Hands ====");
            Console.ResetColor();
            Console.WriteLine("1 - Cadastrar produtos para doação");
            Console.WriteLine("2 - Pesquisar produtos para doação");
            Console.WriteLine("3 - Sair");
            Console.WriteLine("\nDigite o número correspondente à opção desejada: ");
        }

        static void AddDonation()
        {
            Console.WriteLine("=====");
            Console.WriteLine("Qual é o produto que pretende cadastrar? ");
            var typeOfProduct = Console.ReadLine();

            Console.WriteLine("=====");
            Console.WriteLine("Qual a data de registro da doação (formato dd/MM/yyyy)? ");
            if (!DateTime.TryParse(Console.ReadLine(), out var CalcAvailabilityDay))
            {
                Console.WriteLine("=====");
                Console.Write($"Data inválida! Dados não salvos. Tente novamente. ");
                return;
            }

            Console.WriteLine("=====");
            Console.WriteLine("Os dados estão corretos?");
            Console.WriteLine($"Tipo de produto doado: {typeOfProduct}");
            Console.WriteLine($"Data de cadastro do produto doado: {CalcAvailabilityDay:dd/MM/yyyy}");

            Console.WriteLine("1 - Sim \n2 - Não");

            var optionToAdd = Console.ReadLine();

            if (optionToAdd == "1")
            {
                Console.WriteLine("=====");
                donationList.Add(typeOfProduct, CalcAvailabilityDay);
                Console.Write($"Informações adicionadas com sucesso! ");
            }
            else if (optionToAdd == "2")
            {
                Console.WriteLine("=====");
                Console.Write($"Informações não salvas! Refaça o cadastro. ");
            }
            else
            {
                Console.WriteLine("=====");
                Console.Write($"Opção inválida! Tente novamente.");
            }
        }

        static void SearchDonation()
        {
            Console.WriteLine("=====");
            Console.WriteLine("Informe o nome do produto que deseja pesquisar:");
            var searchDonation = Console.ReadLine();

            var foundDonation = donationList.Where(x => x.Key.ToLower().Contains(searchDonation.ToLower()))
                                               .ToList();

            if (foundDonation.Count > 0)
            {
                Console.WriteLine("=====");
                Console.WriteLine("Selecione uma das opções abaixo para visualizar os dados da doação:");
                for (var index = 0; index < foundDonation.Count; index++)
                    Console.WriteLine($"{index} - {foundDonation[index].Key}");

                if (!ushort.TryParse(Console.ReadLine(), out var showIndex) || showIndex >= foundDonation.Count)
                {
                    Console.WriteLine("=====");
                    Console.Write($"Opção inválida! Tente novamente. ");
                    return;
                }

                if (showIndex < foundDonation.Count)
                {
                    var donation = foundDonation[showIndex];
                    
                    var totalDaysInRegister = DateTime.Now - donation.Value;

                    Console.WriteLine("=====Dados da doação=====");
                    Console.WriteLine($"Tipo de produto: {donation.Key}");
                    Console.WriteLine($"Data de registro da doação: {donation.Value:dd/MM/yyyy}");
                    Console.WriteLine($"A doação foi cadastrada há {totalDaysInRegister.TotalDays:N0} dias. ");
                    Console.WriteLine("=====");
                }
            }
            else
            {
                Console.WriteLine("=====");
                Console.Write("Não foi encontrada nenhuma doação cadastrada!Tente novamente. ");
            }
        }
    }
}

