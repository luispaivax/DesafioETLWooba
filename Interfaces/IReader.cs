using DesafioETLWooba.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioETLWooba.Interfaces
{
    public interface IReader
    {
        List<Cliente> Read(string caminhoArquivo);
    }
}
