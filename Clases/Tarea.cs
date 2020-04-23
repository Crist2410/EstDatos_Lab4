﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstructuraDato_Lab04.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripccion { get; set; }
        public string Proyecto { get; set; }
        public DateTime Fecha { get; set; }
        public int IdDesarrollador { get; set; }
        public int Prioridad { get; set; }

        public Comparison<Tarea> CompararTitulo = delegate (Tarea Far1, Tarea Far2)
        {
            return Far1.Titulo.CompareTo(Far2.Titulo);
        };

        public Comparison<Tarea> CompararID = delegate (Tarea Far1, Tarea Far2)
        {
            return Far1.Id.CompareTo(Far2.Id);
        };
        public Comparison<Tarea> CompararPrioridad = delegate (Tarea Far1, Tarea Far2)
        {
            return Far1.Prioridad.CompareTo(Far2.Prioridad);
        };

    }
}
