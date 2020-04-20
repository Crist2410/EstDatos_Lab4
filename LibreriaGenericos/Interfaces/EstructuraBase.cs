using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstructuraDato_Lab04.LibreriaGenericos.Estructuras;

namespace EstructuraDato_Lab04.LibreriaGenericos.Interfaces
{
    public abstract class EstructuraBase<T>
    {
        protected abstract void Insertar(T Valor, Nodo<T> NodoRaiz, Delegate Delegado);
        protected abstract void Borrar(T valor, Nodo<T> NodoRaiz, int Nivel);
        protected abstract T Obtener();
    }
}