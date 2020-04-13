using System;
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

    }
}
