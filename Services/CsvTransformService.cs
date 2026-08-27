using DesafioETLWooba.Interfaces;
using DesafioETLWooba.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DesafioETLWooba.Services
{
    public class CsvTransformService : ITransform
    {
        private static readonly string[] FormatoData =
        {
            "dd/MM/yyyy",
            "yyyy-MM-dd",
            "dd-MM-yyyy"
        };

        private static readonly DateTime DataPadrao = new DateTime(1900, 1, 1);

        private static readonly Regex EmailRegex =
            new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public List<Cliente> Transform(List<Cliente> registros)
        {
            var tratados = new List<Cliente>();
            var emailsVisualizados = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var dado in registros)
            {
                string nome = dado.Nome?.Trim() ?? string.Empty;
                string email = dado.Email?.Trim() ?? string.Empty;

                if (string.IsNullOrEmpty(nome))
                {
                    Console.WriteLine($"[DESCARTADO] Nome vazio -> Email={email}");
                    continue;
                }

                if (!EmailValido(email))
                {
                    Console.WriteLine($"[DESCARTADO] Email inválido: '{email}' (Nome={nome})");
                    continue;
                }

                if (emailsVisualizados.Contains(email))
                {
                    Console.WriteLine($"[DESCARTADO] Duplicado (email já existe): '{email}' (Nome={nome})");
                    continue;
                }

                emailsVisualizados.Add(email);

                tratados.Add(new Cliente
                {
                    Nome = nome,
                    Email = email,
                    DataNascimento = NormalizarData(dado.DataNascimento),
                    Telefone = dado.Telefone?.Trim() ?? string.Empty,
                    Cidade = dado.Cidade?.Trim() ?? string.Empty,
                    UF = dado.UF?.Trim().ToUpperInvariant() ?? string.Empty
                });
            }

            return tratados;
        }

        private bool EmailValido(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && EmailRegex.IsMatch(email);
        }

        private string NormalizarData(string dataTexto)
        {
            if (DateTime.TryParseExact(
                    dataTexto?.Trim(),
                    FormatoData,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime data))
            {
                return data.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            Console.WriteLine($"[AVISO] Data inválida '{dataTexto}', usando padrão {DataPadrao:dd/MM/yyyy}");
            return DataPadrao.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
    }
}
