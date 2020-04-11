using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstructuraDato_Lab04.LibreriaGenericos.Interfaces;

namespace EstructuraDato_Lab04.LibreriaGenericos.Estructuras
{
    //INICIO COLA
    public class ColaPrioridad<T> :EstructuraBase<T> , IEnumerable<T>
    {
        private Nodo<T> First { get; set; }
        public void Add(T value)
        {
            Insertar(value);
        }

        public T Delete()
        {
            var Valor = Obtener();
            Borrar();
            return Valor;
        }
        protected override void Insertar(T value)
        {
            if (First == null)
            {
                First = new Nodo<T>
                {
                    Valor = value,
                    Siguiente = null
                };
            }
            else
            {
                var current = First;
                while (current.Siguiente != null)
                {
                    current = current.Siguiente;
                }
                current.Siguiente = new Nodo<T>
                {
                    Valor = value,
                    Siguiente = null
                };
            }
        }


        protected override void Borrar()
        {
            if (First != null)
            {
                First = First.Siguiente;
            }

        }

        protected override T Obtener()
        {
            return First.Valor;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var queueCopy = this;
            while (queueCopy.First != null)
            {
                yield return queueCopy.Delete();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}