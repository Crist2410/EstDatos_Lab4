using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstructuraDato_Lab04.Clases
{
    public class TareaCola
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Prioridad { get; set; }

        public Comparison<TareaCola> CompararPrioridad = delegate (TareaCola Far1, TareaCola Far2)
        {
            return Far1.Prioridad.CompareTo(Far2.Prioridad);
        };

        public Comparison<TareaCola> CompararTitulo = delegate (TareaCola Far1, TareaCola Far2)
        {
            return Far1.Titulo.CompareTo(Far2.Titulo);
        };

        public Comparison<TareaCola> CompararID = delegate (TareaCola Far1, TareaCola Far2)
        {
            return Far1.Titulo.CompareTo(Far2.Titulo);
        };
    }
}
