using DesafioETLWooba.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioETLWooba.Interfaces
{
    public interface ILoad
    {
        void Inserir(Cliente cliente);
        void InserirVarios(IEnumerable<Cliente> clientes);
        List<Cliente> ConsultarTodos();
        Cliente? ConsultarPorId(int id);
        bool Atualizar(Cliente cliente);
        bool Excluir(int id);
    }
}
