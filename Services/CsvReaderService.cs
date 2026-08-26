using DesafioETLWooba.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioETLWooba.Services
{
    public class CsvReaderService
    {
        public List<Cliente> Read(string caminhoCsv)
        {
            var registros = new List<Cliente>();

            if (!File.Exists(caminhoCsv))
                throw new FileNotFoundException("Arquivo CSV não encontrado.", caminhoCsv);

            var linhas = File.ReadAllLines(caminhoCsv);


            for (int i = 1; i < linhas.Length; i++)
            {
                var linha = linhas[i];

                if (string.IsNullOrWhiteSpace(linha))
                    continue;

                var colunas = linha.Split(',');

                if (colunas.Length < 6)
                    continue;

                registros.Add(new Cliente
                {
                    Nome = colunas[0].Trim(),
                    Email = colunas[1].Trim(),
                    DataNascimento = colunas[2].Trim(),
                    Telefone = colunas[3].Trim(),
                    Cidade = colunas[4].Trim(),
                    UF = colunas[5].Trim()
                });
            }
            return registros;
        }
    }
}
