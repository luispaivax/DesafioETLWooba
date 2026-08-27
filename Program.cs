using DesafioETLWooba.Models;
using DesafioETLWooba.Services;
using DesafioETLWooba.Interfaces;

Console.WriteLine("+=+= ETL =+=+\n");

string caminhoCsv = "clientes_lote_a.csv";

IReader reader = new CsvReaderService();
ITransform transform = new CsvTransformService();

try
{
    // 1. Leitura
    Console.WriteLine("Lendo o CSV...");
    List<Cliente> registros = reader.Read(caminhoCsv);
    Console.WriteLine($"{registros.Count} linhas lidas.\n");

    // 2. Tratamento
    Console.WriteLine("Tratando dados do CSV...");
    List<Cliente> registrosTratados = transform.Transform(registros);
    Console.WriteLine($"\n{registrosTratados.Count} registros válidos após tratamento");

}
catch (Exception ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
}

Console.WriteLine("\nPressione qualquer tecla para sair");
Console.ReadKey();