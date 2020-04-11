using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstructuraDato_Lab04.LibreriaGenericos.Estructuras;
using EstructuraDato_Lab04.Clases;

namespace EstructuraDato_Lab04.Models
{
    public class Desarrolladores
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public ColaPrioridad<TareaCola> TareasDV = new ColaPrioridad<TareaCola>(); 
    }
}
