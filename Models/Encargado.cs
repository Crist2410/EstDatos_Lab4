using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstructuraDato_Lab04.Models
{
    public class Encargado
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Usiario { get; set; }
        public string Contraseña{ get; set; }

        public List<Desarrolladores> EmpleadosDV = new List<Desarrolladores>();
        public List<Tarea> Tareas = new List<Tarea>();

    }
}
