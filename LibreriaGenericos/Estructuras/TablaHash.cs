using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstructuraDato_Lab04.LibreriaGenericos.Estructuras;
using EstructuraDato_Lab04.Models;

namespace EstructuraDato_Lab04.LibreriaGenericos.Estructuras
{
    public class TablaHash<T>   
    {
        int TotalPocisiones = 75;
        T[] empleados = new T[75];
        public int ObtenerValorHash(string Nombre)
        {
            string DatoHash = Nombre.ToLower();
            int Valor = (DatoHash.Length)*19;
            char Letra = 'a';
            //Vocales
            if (DatoHash.Contains(Letra))
            {
                Valor *= 3;
                Valor *= (from letra in cadena where c == DatoHash select c).Count();

            }
            if (DatoHash.Contains('e'))
                Valor *= 5;
            if (DatoHash.Contains('i'))
                Valor *= 7;
            if (DatoHash.Contains('o'))
                Valor *= 11;
            if (DatoHash.Contains('u'))
                Valor *= 13;
            //Consonantes
            if (DatoHash.Contains('s'))
                Valor += 499;
            if (DatoHash.Contains('r'))
                Valor += 503;
            if (DatoHash.Contains('c'))
                Valor += 521;
            if (DatoHash.Contains('n'))
                Valor += 547;
            if (DatoHash.Contains('d'))
                Valor += 653;
            if (DatoHash.Contains('l'))
                Valor += 661;
                Valor %= (DatoHash.Length*11);
            return Valor;
        }
        public List<Tarea> TablaTareas = new List<Tarea>();
    }
}
