using DesafioETLWooba.Models;
using DesafioETLWooba.Interfaces;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioETLWooba.Data
{
    public class LoadSQL : ILoad, IDisposable
    {
        private readonly SqliteConnection _connection;

        public LoadSQL()
        {
            // Conexão em memória
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();

            CriarTabela();
        }

        private void CriarTabela()
        {
            var comando = _connection.CreateCommand();
            comando.CommandText = @"
                CREATE TABLE IF NOT EXISTS Clientes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nome TEXT NOT NULL,
                    Email TEXT NOT NULL,
                    DataNascimento TEXT NOT NULL,
                    Telefone TEXT NOT NULL,
                    Cidade TEXT NOT NULL,
                    UF TEXT NOT NULL
                );";
            comando.ExecuteNonQuery();
        }

        // 1. Inserir
        public void Inserir(Cliente cliente)
        {
            var comando = _connection.CreateCommand();
            comando.CommandText = @"
                INSERT INTO Clientes (Nome, Email, DataNascimento, Telefone, Cidade, UF)
                VALUES ($nome, $email, $datanasc, $telefone, $cidade, $uf);";

            comando.Parameters.AddWithValue("$nome", cliente.Nome);
            comando.Parameters.AddWithValue("$email", cliente.Email);
            comando.Parameters.AddWithValue("$datanasc", cliente.DataNascimento);
            comando.Parameters.AddWithValue("$telefone", cliente.Telefone);
            comando.Parameters.AddWithValue("$cidade", cliente.Cidade);
            comando.Parameters.AddWithValue("$uf", cliente.UF);

            comando.ExecuteNonQuery();
        }

        public void InserirVarios(IEnumerable<Cliente> clientes)
        {
            using var transaction = _connection.BeginTransaction();

            foreach (var cliente in clientes)
            {
                var comando = _connection.CreateCommand();
                comando.Transaction = (SqliteTransaction)transaction;
                comando.CommandText = @"
                    INSERT OR REPLACE INTO Clientes (Nome, Email, DataNascimento, Telefone, Cidade, UF)
                    VALUES ($nome, $email, $datanasc, $telefone, $cidade, $uf);";

                comando.Parameters.AddWithValue("$nome", cliente.Nome);
                comando.Parameters.AddWithValue("$email", cliente.Email);
                comando.Parameters.AddWithValue("$datanasc", cliente.DataNascimento);
                comando.Parameters.AddWithValue("$telefone", cliente.Telefone);
                comando.Parameters.AddWithValue("$cidade", cliente.Cidade);
                comando.Parameters.AddWithValue("$uf", cliente.UF);

                comando.ExecuteNonQuery();
            }

            transaction.Commit();
        }

        // 2. Consultar
        public List<Cliente> ConsultarTodos()
        {
            var lista = new List<Cliente>();

            var comando = _connection.CreateCommand();
            comando.CommandText = @"
                SELECT Id, Nome, Email, DataNascimento, Telefone, Cidade, UF 
                FROM Clientes;";

            using var reader = comando.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Cliente
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Email = reader.GetString(2),
                    DataNascimento = reader.GetString(3),
                    Telefone = reader.GetString(4),
                    Cidade = reader.GetString(5),
                    UF = reader.GetString(6)
                });
            }

            return lista;
        }

        public Cliente? ConsultarPorId(int id)
        {
            var comando = _connection.CreateCommand();
            comando.CommandText = @"
                SELECT Id, Nome, Email, DataNascimento, Telefone, Cidade, UF 
                FROM Clientes 
                WHERE Id = $id;";
            comando.Parameters.AddWithValue("$id", id);

            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new Cliente
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Email = reader.GetString(2),
                    DataNascimento = reader.GetString(3),
                    Telefone = reader.GetString(4),
                    Cidade = reader.GetString(5),
                    UF = reader.GetString(6)
                };
            }

            return null;
        }

        // 3. Atualizar
        public bool Atualizar(Cliente cliente)
        {
            var comando = _connection.CreateCommand();
            comando.CommandText = @"
                UPDATE Clientes
                SET Nome = $nome, 
                    Email = $email,
                    DataNascimento = $datanasc,
                    Telefone = $telefone,
                    Cidade = $cidade,
                    UF = $uf
                WHERE Id = $id;";

            comando.Parameters.AddWithValue("$id", cliente.Id);
            comando.Parameters.AddWithValue("$nome", cliente.Nome);
            comando.Parameters.AddWithValue("$email", cliente.Email);
            comando.Parameters.AddWithValue("$datanasc", cliente.DataNascimento);
            comando.Parameters.AddWithValue("$telefone", cliente.Telefone);
            comando.Parameters.AddWithValue("$cidade", cliente.Cidade);
            comando.Parameters.AddWithValue("$uf", cliente.UF);

            int linhasAfetadas = comando.ExecuteNonQuery();
            return linhasAfetadas > 0;
        }

        // 4. Excluir
        public bool Excluir(int id)
        {
            var comando = _connection.CreateCommand();
            comando.CommandText = "DELETE FROM Clientes WHERE Id = $id;";
            comando.Parameters.AddWithValue("$id", id);

            int linhasAfetadas = comando.ExecuteNonQuery();
            return linhasAfetadas > 0;
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();         
        }
    }
}
