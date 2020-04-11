using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstructuraDato_Lab04.LibreriaGenericos.Interfaces
{
    public abstract class EstructuraBase<T>
    {
        protected abstract void Insertar(T value);
        protected abstract void Borrar();
        protected abstract T Obtener();
    }
}