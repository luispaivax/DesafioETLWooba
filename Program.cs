using DesafioETLWooba.Models;
using DesafioETLWooba.Services;
using DesafioETLWooba.Interfaces;

Console.WriteLine("+=+= ETL =+=+\n");

string caminhoCsv = "clientes_lote_a.csv";

IReader reader = new CsvReaderService();

try
{
    // 1. Leitura
    Console.WriteLine("Lendo o CSV...");
    List<Cliente> registros = reader.Read(caminhoCsv);
    Console.WriteLine($"{registros.Count} linhas lidas.\n");

    // Teste de leitura temporário
    foreach (var c in registros)
    {
        Console.WriteLine($"Nome={c.Nome} | Email={c.Email} | DataNasc={c.DataNascimento} | Telefone={c.Telefone} | Cidade={c.Cidade} | UF={c.UF}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
}

Console.WriteLine("\nPressione qualquer tecla para sair");
Console.ReadKey();