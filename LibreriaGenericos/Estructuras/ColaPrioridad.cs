using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstructuraDato_Lab04.LibreriaGenericos.Interfaces;

namespace EstructuraDato_Lab04.LibreriaGenericos.Estructuras
{
    //INICIO COLA
    public class ColaPrioridad<T> : EstructuraBase<T>, IEnumerable<T>
    {
        int CantidadNodos;
        Nodo<T> First = new Nodo<T>();
        public void Add(T value, Delegate Delegado)
        {
            Insertar(value, First, Delegado);
        }

        public T Delete(Delegate Delegado)
        {
            var Valor = Obtener();
            int Nivel = CalcularNivel();
            Borrar(Valor, First, Nivel);
            T Aux = First.Valor;
            OrdenamientoBorrar(First, Aux, Delegado);
            CantidadNodos--;
            return Valor;
        }

        public void OrdenamientoInsertar(Nodo<T> NodoRaiz, T Aux, Delegate Delegado)
        {
            if (Chequeo(NodoRaiz))
            {
                if (Convert.ToInt32(Delegado.DynamicInvoke(NodoRaiz.Valor, NodoRaiz.Izquierda.Valor)) == 1)
                {
                    NodoRaiz.Valor = NodoRaiz.Izquierda.Valor;
                    NodoRaiz.Izquierda.Valor = Aux;
                }
                if (Convert.ToInt32(Delegado.DynamicInvoke(NodoRaiz.Valor, NodoRaiz.Derecha.Valor)) == 1)
                {
                    NodoRaiz.Valor = NodoRaiz.Derecha.Valor;
                    NodoRaiz.Derecha.Valor = Aux;
                }
            }
            else if (NodoRaiz.Derecha.Valor == null)
            {
                if (Convert.ToInt32(Delegado.DynamicInvoke(NodoRaiz.Valor, NodoRaiz.Izquierda.Valor)) == 1)
                {
                    NodoRaiz.Valor = NodoRaiz.Izquierda.Valor;
                    NodoRaiz.Izquierda.Valor = Aux;
                }
            }
        }

        void OrdenamientoBorrar(Nodo<T> NodoRaiz, T Aux, Delegate Delegado)
        {
            if (NodoRaiz.Derecha != null)
            {
                if (Convert.ToInt32(Delegado.DynamicInvoke(NodoRaiz.Derecha.Valor, NodoRaiz.Izquierda.Valor)) == 1)
                {
                    if (Convert.ToInt32(Delegado.DynamicInvoke(NodoRaiz.Derecha.Valor, NodoRaiz.Valor)) == 1)
                    {
                        NodoRaiz.Valor = NodoRaiz.Derecha.Valor;
                        NodoRaiz.Derecha.Valor = Aux;
                        Aux = NodoRaiz.Valor;
                        OrdenamientoBorrar(NodoRaiz.Derecha, Aux, Delegado);
                    }
                }
                else if (Convert.ToInt32(Delegado.DynamicInvoke(NodoRaiz.Izquierda.Valor, NodoRaiz.Derecha.Valor)) == 1)
                {
                    if (Convert.ToInt32(Delegado.DynamicInvoke(NodoRaiz.Izquierda.Valor, NodoRaiz.Valor)) == 1)
                    {
                        NodoRaiz.Valor = NodoRaiz.Izquierda.Valor;
                        NodoRaiz.Izquierda.Valor = Aux;
                        Aux = NodoRaiz.Valor;
                        OrdenamientoBorrar(NodoRaiz.Derecha, Aux, Delegado);
                    }
                }
            }
            else
            {
                if (Convert.ToInt32(Delegado.DynamicInvoke(NodoRaiz.Izquierda.Valor, NodoRaiz.Valor)) == 1)
                {
                    NodoRaiz.Valor = NodoRaiz.Izquierda.Valor;
                    NodoRaiz.Izquierda.Valor = Aux;
                    Aux = NodoRaiz.Valor;
                    OrdenamientoBorrar(NodoRaiz.Derecha, Aux, Delegado);
                }
            }
        }

        bool Chequeo(Nodo<T> NodoRaiz)
        {
            if (NodoRaiz.Izquierda.Valor != null && NodoRaiz.Derecha.Valor != null)
                return true;
            else
                return false;
        }

        protected override void Insertar(T Valor, Nodo<T> NodoRaiz, Delegate Delegado)
        {
            if (NodoRaiz.Valor == null)
            {
                NodoRaiz.Valor = Valor;
                NodoRaiz.Derecha = new Nodo<T>();
                NodoRaiz.Izquierda = new Nodo<T>();
                CantidadNodos++;
                NodoRaiz.Posicion = CantidadNodos;
            }
            else if (NodoRaiz.Izquierda.Valor == null && NodoRaiz.Derecha.Valor == null)
                Insertar(Valor, NodoRaiz.Izquierda, Delegado);
            else if (NodoRaiz.Izquierda.Valor != null && NodoRaiz.Derecha.Valor == null)
                Insertar(Valor, NodoRaiz.Derecha, Delegado);
            else
            {
                if (Chequeo(NodoRaiz.Izquierda))
                    Insertar(Valor, NodoRaiz.Derecha, Delegado);
                else
                    Insertar(Valor, NodoRaiz.Izquierda, Delegado);
            }

            if (NodoRaiz.Derecha.Valor != null || NodoRaiz.Izquierda.Valor != null)
            {
                T Aux = NodoRaiz.Valor;
                OrdenamientoInsertar(NodoRaiz, Aux, Delegado);
            }

        }

        int CalcularNivel()
        {
            return Convert.ToInt32((Math.Log(Math.E, Convert.ToDouble(CantidadNodos)) / (Math.Log(Math.E, Convert.ToDouble(2)))));
        }

        protected override void Borrar(T valor, Nodo<T> NodoRaiz, int Nivel)
        {
            var Aux = NodoRaiz.Valor;
            while (NodoRaiz.Posicion != CantidadNodos)
            {
                if ((CantidadNodos / (2 ^ (Nivel - 1))) % 2 == 0)
                    Borrar(valor, NodoRaiz.Izquierda, Nivel--);
                else if ((CantidadNodos / (2 ^ (Nivel - 1))) % 2 == 0)
                    Borrar(valor, NodoRaiz.Derecha, Nivel--);
            }
            NodoRaiz = null;
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
                //yield return queueCopy.Delete();
            }
            throw new NotImplementedException();

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}