using System;
using System.Collections.Generic;
using System.Text;
using DesafioETLWooba.Interfaces;
using DesafioETLWooba.Models;

namespace DesafioETLWooba.Services
{
    public class MenuService
    {
        private readonly ILoad _banco;

        public MenuService(ILoad banco)
        {
            _banco = banco;
        }

        public void Executar()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("\n=+=+= MENU ETL =+=+=");
                Console.WriteLine("[1] Inserir cliente");
                Console.WriteLine("[2] Consultar todos");
                Console.WriteLine("[3] Consultar por Id");
                Console.WriteLine("[4] Atualizar cliente");
                Console.WriteLine("[5] Excluir cliente");
                Console.WriteLine("[0] Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine() ?? string.Empty;

                switch (opcao)
                {
                    case "1": InserirCliente(); break;
                    case "2": ConsultarTodos(); break;
                    case "3": ConsultarPorId(); break;
                    case "4": AtualizarCliente(); break;
                    case "5": ExcluirCliente(); break;
                    case "0": continuar = false; break;
                    default: Console.WriteLine("Opção inválida."); break;
                }

            }
        }

        private void InserirCliente()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? string.Empty;

            Console.Write("Data de nascimento (dd/MM/yyyy): ");
            string dataNascimento = Console.ReadLine() ?? string.Empty;

            Console.Write("Telefone: ");
            string telefone = Console.ReadLine() ?? string.Empty;

            Console.Write("Cidade: ");
            string cidade = Console.ReadLine() ?? string.Empty;

            Console.Write("UF: ");
            string uf = Console.ReadLine() ?? string.Empty;

            var cliente = new Cliente
            {
                Nome = nome,
                Email = email,
                DataNascimento = dataNascimento,
                Telefone = telefone,
                Cidade = cidade,
                UF = uf
            };

            _banco.Inserir(cliente);
            Console.WriteLine("Cliente inserido com sucesso.");
        }

        private void ConsultarTodos()
        {
            var clientes = _banco.ConsultarTodos();

            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
                return;
            }

            foreach (var c in clientes)
                Console.WriteLine($"[{c.Id}] {c.Nome} - {c.Email} - {c.Cidade}/{c.UF}");
        }

        private void ConsultarPorId()
        {
            Console.Write("Digite o Id do cliente: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            var cliente = _banco.ConsultarPorId(id);

            if (cliente is null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return;
            }

            Console.WriteLine($"[{cliente.Id}] {cliente.Nome} - {cliente.Email}");
            Console.WriteLine($"  Nascimento: {cliente.DataNascimento}");
            Console.WriteLine($"  Telefone: {cliente.Telefone}");
            Console.WriteLine($"  Cidade/UF: {cliente.Cidade}/{cliente.UF}");
        }

        private void AtualizarCliente()
        {
            Console.Write("Digite o Id do cliente que deseja atualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido!");
                return;
            }

            var clienteAtual = _banco.ConsultarPorId(id);

            if (clienteAtual is null)
            {
                Console.WriteLine("Cliente não encontrado!");
                return;
            }

            Console.WriteLine("Deixe em branco para manter o valor atual.\n");

            Console.Write($"Nome ({clienteAtual.Nome}): ");
            string nome = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(nome)) clienteAtual.Nome = nome;

            Console.Write($"Email ({clienteAtual.Email}): ");
            string email = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(email)) clienteAtual.Email = email;

            Console.Write($"Data de nascimento ({clienteAtual.DataNascimento}): ");
            string dataNascimento = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(dataNascimento)) clienteAtual.DataNascimento = dataNascimento;

            Console.Write($"Telefone ({clienteAtual.Telefone}): ");
            string telefone = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(telefone)) clienteAtual.Telefone = telefone;

            Console.Write($"Cidade ({clienteAtual.Cidade}): ");
            string cidade = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(cidade)) clienteAtual.Cidade = cidade;

            Console.Write($"UF ({clienteAtual.UF}): ");
            string uf = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(uf)) clienteAtual.UF = uf;

            bool atualizou = _banco.Atualizar(clienteAtual);
            Console.WriteLine(atualizou ? "Cliente atualizado com sucesso." : "Falha ao atualizar!");
        }

        private void ExcluirCliente()
        {
            Console.Write("Digite o Id do cliente que deseja excluir: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            bool excluiu = _banco.Excluir(id);
            Console.WriteLine(excluiu ? "Cliente excluído com sucesso." : "Nenhum cliente encontrado com esse Id.");
        }

    }
}
