using EstructuraDato_Lab04.LibreriaGenericos.Estructuras;
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
        public int TotalDesarrolladores { get; set; }

        public List<Desarrolladores> EmpleadosDV = new List<Desarrolladores>();
        // La lista de tareas debe ser una tabla Hash
        public TablaHash HashTable = new TablaHash();

    }
}
