using DesafioETLWooba.Models;
using DesafioETLWooba.Services;
using DesafioETLWooba.Interfaces;
using DesafioETLWooba.Data;

Console.WriteLine("+=+= ETL =+=+\n");

string caminhoCsv = "clientes_lote_a.csv";

IReader reader = new CsvReaderService();
ITransform transform = new CsvTransformService();
ILoad banco = new LoadSQL();

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

    // 3. Gravação
    Console.WriteLine("Gravando os dados no banco em memória...");
    banco.InserirVarios(registrosTratados);
    Console.WriteLine("Gravação concluída");

    // 4. Menu
    var menu = new MenuService(banco);
    menu.Executar();

}
catch (Exception ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
}
finally
{
    if (banco is IDisposable descartavel)
        descartavel.Dispose();
}

Console.WriteLine("\nEncerrando...");
