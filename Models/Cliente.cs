using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioETLWooba.Models
{
    public class Cliente
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DataNascimento { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string UF { get; set; } = string.Empty;
    }
}
