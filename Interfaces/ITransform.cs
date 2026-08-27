using DesafioETLWooba.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioETLWooba.Interfaces
{
    public interface ITransform
    {
        List<Cliente> Transform(List<Cliente> registros);
    }
}
