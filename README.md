# DesafioETLWooba

Ferramenta em .NET que lê um CSV de clientes, trata os dados e grava em um banco **SQLite em memória**, com um menu no console para **Inserir, Consultar, Atualizar e Excluir**

## Pré-requisitos

- Visual Studio 2022 (carga de trabalho ".NET desktop development")
- .NET SDK 8.0

## Instalando as dependências

O projeto usa um pacote NuGet: **Microsoft.Data.Sqlite**.

Abra a solução no Visual Studio e compile (**Ctrl+Shift+B**) — o pacote é restaurado automaticamente. Se preferir pelo terminal:

```bash
dotnet restore
```

## Rodando o projeto

### Pelo Visual Studio

1. Confirme que `clientes_lote_a.csv` está na raiz do projeto
2. Pressione **F5** (ou `dotnet run` pelo terminal)

### Pelo VSCode

1. Instale a extensão **C# Dev Kit** (ou a extensão **C#** da OmniSharp), disponível na aba de Extensões
2. Abra a pasta do projeto (a que contém o `.csproj`) via **File → Open Folder**
3. Abra um terminal integrado (**Ctrl+`**) e restaure as dependências:

```bash
dotnet restore
```

4. Rode o projeto:

```bash
dotnet run
```

Para rodar com debug (breakpoints, inspeção de variáveis), pressione **F5** — na primeira vez, o VSCode pode pedir para gerar os arquivos de configuração (`launch.json` e `tasks.json`); aceite a opção sugerida para projetos **.NET**.

O programa lê o CSV, trata os dados (mostrando no console o que foi descartado ou ajustado) e grava tudo no banco em memória. Em seguida, abre o menu:

```
=+=+= MENU ETL =+=+=
[1] Inserir cliente
[2] Consultar todos
[3] Consultar por Id
[4] Atualizar cliente
[5] Excluir cliente
[0] Sair
```

> Como o banco é em memória tudo é perdido ao fechar o programa.

## Testando as quatro operações

**Inserir** — opção `1`, preencha os campos pedidos. Confira com a opção `2` que o cliente novo aparece.

**Consultar todos** — opção `2`, lista todos os clientes cadastrados.

**Consultar por Id** — opção `3`, informe um Id existente para ver os dados completos.

**Atualizar** — opção `4`, informe o Id e digite novos valores (ou deixe em branco para manter o valor atual). Confira com a opção `3`.

**Excluir** — opção `5`, informe o Id. Confira com a opção `2` que ele sumiu da lista.


Para sair, digite `0`.
