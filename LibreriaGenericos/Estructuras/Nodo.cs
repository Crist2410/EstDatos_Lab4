using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstructuraDato_Lab04.LibreriaGenericos.Estructuras
{
    public class Nodo<T>
    {
        public Nodo<T> Siguiente { get; set; }
        public T Valor { get; set; }
    }
}
