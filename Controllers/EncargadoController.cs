using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EstructuraDato_Lab04.Models;
using EstructuraDato_Lab04.Clases;
using EstructuraDato_Lab04.LibreriaGenericos.Estructuras;
using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace EstructuraDato_Lab04.Controllers
{
    public class EncargadoController : Controller
    {
        public static Encargado LiderDV = new Encargado();
        
        private void CargarDatos()
        {
            using (TextFieldParser Archivo = new TextFieldParser(@"Documentos\\Encargado.csv"))
            {
                Archivo.TextFieldType = FieldType.Delimited;
                Archivo.SetDelimiters(",");
                string[] Texto = Archivo.ReadFields();
                LiderDV.Nombre = Texto[0];
                LiderDV.Apellido = Texto[1];
                LiderDV.Usiario = Texto[2];
                LiderDV.Contraseña = Texto[3];
            }
            using (TextFieldParser Archivo = new TextFieldParser(@"Documentos\\Desarrolladores.csv"))
            {
                Archivo.TextFieldType = FieldType.Delimited;
                Archivo.SetDelimiters(",");
                while (!Archivo.EndOfData)
                {
                    string[] Texto = Archivo.ReadFields();
                    Desarrolladores AuxDV = new Desarrolladores();
                    AuxDV.Id = Convert.ToInt32(Texto[0]);
                    AuxDV.Nombre = Texto[1];
                    AuxDV.Apellido = Texto[2];
                    LiderDV.EmpleadosDV.Add(AuxDV);
                    LiderDV.TotalDesarrolladores++;
                }
            }
            using (TextFieldParser Archivo = new TextFieldParser(@"Documentos\\Tareas.csv"))
            {
                Archivo.TextFieldType = FieldType.Delimited;
                Archivo.SetDelimiters(",");
                while (!Archivo.EndOfData)
                {
                    string[] Texto = Archivo.ReadFields();
                    Tarea TareaAux = new Tarea()
                    {
                        Titulo = Texto[1],
                        Descripccion = Texto[2],
                        Proyecto = Texto[3],
                        Fecha = DateTime.Parse(Texto[4]),
                        Id = Convert.ToInt32(Texto[0]),
                        IdDesarrollador = Convert.ToInt32(Texto[5]),
                        Prioridad = Convert.ToInt32(Texto[6])
                    };
                    TareaCola TareaColaAux = new TareaCola()
                    {
                        Id = TareaAux.Id,
                        Titulo = TareaAux.Titulo,
                        Prioridad = TareaAux.Prioridad
                    };
                    LiderDV.EmpleadosDV.Find(x => x.Id == TareaAux.IdDesarrollador).TareasDV.Add(TareaColaAux);
                    LiderDV.HashTable.TablaTareas.Add(TareaAux);
                }
            }
        }
        private void EscribirCSV(string Texto, string Ruta)
        {
            using (StreamWriter ArchivoEscritura = new StreamWriter(Ruta, true))
            {
                ArchivoEscritura.WriteLineAsync(Texto);
                ArchivoEscritura.Flush();
            }
        }
        
        // Carga los Datos de los CSV al iniciar el programa
        public ActionResult InicioApp()
        {
            CargarDatos();
            return View("Principal");
        }
        //Muestra el Menu Principal
        public ActionResult Principal()
        {
            return View();
        }
        //Inicio de Sesion Para el Admin 
        public ActionResult IniciarSesion()
        {
            return View();
        }

        // Iniciar Encargado 
        [HttpPost]
        public ActionResult ComprobarDatos(IFormCollection collection)
        {
            string UsuarioAux = collection["Usiario"];
            string Contraseña = collection["Contraseña"];
            if (UsuarioAux == LiderDV.Usiario && Contraseña == LiderDV.Contraseña)
                return View("IndexEncargado", LiderDV); 
            else
            return View("IniciarSesion");
        }
        public ActionResult IndexEncargado()
        {
            return View(LiderDV);
        }

        // Iniciar Encargado 
        public ActionResult IniciarDesarrollador()
        {
            ViewBag.Desarrolladores = LiderDV.EmpleadosDV;
            return View("EscogerDesarrollador");
        }


        //Crear Tarea
        public ActionResult AgregarTarea(int id)
        {
            Tarea AuxTarea = new Tarea
            {
                IdDesarrollador = id
            };
            return View(AuxTarea);
        }
        [HttpPost]
        public ActionResult GuardarTarea(IFormCollection collection, int idDesarrollador)
        {
            Tarea AuxTarea = new Tarea
            {
                Titulo = collection["Titulo"],
                Descripccion = collection["Descripccion"],
                Proyecto = collection["Proyecto"],
                Fecha = Convert.ToDateTime(collection["Fecha"]),
                Id = LiderDV.HashTable.ObtenerValorHash(collection["Titulo"]),
                IdDesarrollador = Convert.ToInt32(idDesarrollador),
                Prioridad = Convert.ToInt32(collection["Prioridad"])
            };
            TareaCola TareaColaAux = new TareaCola()
            {
                Id = AuxTarea.Id,
                Titulo = AuxTarea.Titulo,
                Prioridad = AuxTarea.Prioridad
            };
            LiderDV.EmpleadosDV.Find(x => x.Id == AuxTarea.IdDesarrollador).TareasDV.Add(TareaColaAux);
            LiderDV.HashTable.TablaTareas.Add(AuxTarea);
            string Texto = AuxTarea.Id+",\""+AuxTarea.Titulo+"\",\"" + AuxTarea.Descripccion + "\",\"" +
            AuxTarea.Proyecto + "\",\"" + AuxTarea.Fecha.ToString() + "\","+AuxTarea.IdDesarrollador+","+ AuxTarea.Prioridad;
            EscribirCSV(Texto, @"Documentos\\Tareas.csv");
            return View("IndexEncargado", LiderDV);
        }
        // Ver Desarrolladores
        public ActionResult VerDesarrolladores()
        {
            ViewBag.Desarrolladores = LiderDV.EmpleadosDV;
            return View();
        }
        //Ver Tareas
        public ActionResult VerTareas()
        {
            ViewBag.Tareas = LiderDV.HashTable.TablaTareas;
            return View();
        }
        //Agregar Desarrollador
        public ActionResult AgregarDesarrollador()
        {            
            return View();
        }
        [HttpPost]
        public ActionResult GuardarDesarrollador(IFormCollection collection)
        {
            LiderDV.TotalDesarrolladores++;
            Desarrolladores AuxDesarrollador = new Desarrolladores()
            {
                Apellido = collection["Apellido"],
                Nombre = collection["Nombre"],
                Id = LiderDV.TotalDesarrolladores,
            };
            LiderDV.EmpleadosDV.Add(AuxDesarrollador);
            string Texto = AuxDesarrollador.Id + "," + AuxDesarrollador.Nombre + "," + AuxDesarrollador.Apellido;
            EscribirCSV(Texto, @"Documentos\\Desarrolladores.csv");
            return View("IndexEncargado", LiderDV);
        }
        //Editar Datos Usuario
        public ActionResult EditarDatos()
        {
            return View(LiderDV);
        }
        [HttpPost]
        public ActionResult GuardarDatos(IFormCollection collection)
        {
            LiderDV.Nombre = collection["Nombre"];
            LiderDV.Apellido = collection["Apellido"];
            LiderDV.Usiario = collection["Usiario"];
            LiderDV.Contraseña = collection["Contraseña"];
            string Texto = LiderDV.Nombre + "," + LiderDV.Apellido + ",\"" + LiderDV.Usiario + "\",\"" + LiderDV.Contraseña + "\"" ;
            using (StreamWriter ArchivoEscritura = new StreamWriter(@"Documentos\\Encargado.csv"))
            {
                ArchivoEscritura.WriteLine(Texto);
                ArchivoEscritura.Flush();
            }
            return View("IndexEncargado", LiderDV);
        }
        // POST: Encargado/Create
        [HttpPost]
        public ActionResult Crear(IFormCollection collection)
        {
            string UsuarioAux = collection["Usiario"];
            string Contraseña = collection["Contraseña"];
            //Utilizando Listas de C# 
            
            return View("MostrarJugadores");
        }
        //Ver Tareas desde Desarrollador 
        public ActionResult VerTarea(int idDV)
        {
            try
            {
                Desarrolladores AuxDesarrollador = LiderDV.EmpleadosDV.Find(x => x.Id == idDV);
                ViewBag.Nombre = AuxDesarrollador.Nombre + AuxDesarrollador.Apellido;
                Tarea AuxTarea = LiderDV.HashTable.TablaTareas.Find(x => x.Id == AuxDesarrollador.TareasDV.Get().Id);
                return View("VerTareaDesarrollador", AuxTarea);
            }
            catch (Exception)
            {
                TempData["Mensaje"] = "El Desarrollador No Tiene Tareas Pendientes :)";
                ViewBag.Desarrolladores = LiderDV.EmpleadosDV;
                return View("EscogerDesarrollador");
            }
           
        }
        [HttpPost]
        public ActionResult FinalizarDesarrollador (IFormCollection collection)
        {
            int ID = Convert.ToInt32(collection["Id"]);
            int IDDV = Convert.ToInt32(collection["IdDesarrollador"]);
            List<EliminacionTareas> ListaEliminacion = new List<EliminacionTareas>();
            using (StreamReader ArchivoLectura = new StreamReader(@"Documentos\\Tareas.csv"))
            {
                string TextoAux;
                do
                {
                    TextoAux = ArchivoLectura.ReadLine();
                    try
                    {
                        EliminacionTareas AuxTarea = new EliminacionTareas()
                        {
                            Texto = TextoAux,
                            Id = Convert.ToInt32(TextoAux.Split(',')[0])
                        };
                        ListaEliminacion.Add(AuxTarea);
                    }
                    catch (Exception)
                    { }
                } while (TextoAux != null);
            }
            EliminacionTareas ElimiarAux = ListaEliminacion.Find(x => x.Id == ID);
            ListaEliminacion.Remove(ElimiarAux);
            using (StreamWriter ArchivoLimpiar = new StreamWriter(@"Documentos\\Tareas.csv"))
            {
                ArchivoLimpiar.WriteLine(ListaEliminacion[0].Texto);
                ListaEliminacion.RemoveAt(0);
                ArchivoLimpiar.Flush();
            }

            using (StreamWriter ArchivoEscritura = new StreamWriter(@"Documentos\\Tareas.csv", true))
            {
                foreach (EliminacionTareas item in ListaEliminacion)
                {
                    ArchivoEscritura.WriteLineAsync(item.Texto);
                    ArchivoEscritura.Flush();
                }
            }
            LiderDV.EmpleadosDV.Find(x => x.Id == IDDV).TareasDV.Delete();
            LiderDV.HashTable.TablaTareas.Clear();
            using (TextFieldParser Archivo = new TextFieldParser(@"Documentos\\Tareas.csv"))
            {
                Archivo.TextFieldType = FieldType.Delimited;
                Archivo.SetDelimiters(",");
                while (!Archivo.EndOfData)
                {
                    string[] Texto = Archivo.ReadFields();
                    Tarea TareaAux = new Tarea()
                    {
                        Titulo = Texto[1],
                        Descripccion = Texto[2],
                        Proyecto = Texto[3],
                        Fecha = DateTime.Parse(Texto[4]),
                        Id = Convert.ToInt32(Texto[0]),
                        IdDesarrollador = Convert.ToInt32(Texto[5]),
                        Prioridad = Convert.ToInt32(Texto[6])
                    };
                    LiderDV.HashTable.TablaTareas.Add(TareaAux);
                }
            }
            ViewBag.Desarrolladores = LiderDV.EmpleadosDV;
            return View("EscogerDesarrollador");
        }
        // Finalizar Tarea
        public ActionResult EliminarTarea(int idDV)
        {
            try
            {
                Desarrolladores AuxDesarrollador = LiderDV.EmpleadosDV.Find(x => x.Id == idDV);
                TareaCola AuxTareaCola = AuxDesarrollador.TareasDV.Get();
                ViewBag.Nombre = AuxDesarrollador.Nombre + AuxDesarrollador.Apellido;
                Tarea AuxTarea = LiderDV.HashTable.TablaTareas.Find(x => x.Id == AuxTareaCola.Id);
                return View(AuxTarea);
            }
            catch (Exception)
            {
                TempData["Mensaje"] = "El Desarrollador No Tiene Tareas Pendientes :)";
                ViewBag.Desarrolladores = LiderDV.EmpleadosDV;
                return View("VerDesarrolladores");
            }

        }
        [HttpPost]
        public ActionResult FinalizarTarea(IFormCollection collection)
        {
            int ID = Convert.ToInt32(collection["Id"]);
            int IDDV = Convert.ToInt32(collection["IdDesarrollador"]);
            List<EliminacionTareas> ListaEliminacion = new List<EliminacionTareas>();
            using (StreamReader ArchivoLectura = new StreamReader(@"Documentos\\Tareas.csv"))
            {
                string TextoAux;
                do
                {
                    TextoAux = ArchivoLectura.ReadLine();
                    try
                    {
                        EliminacionTareas AuxTarea = new EliminacionTareas()
                        {
                            Texto = TextoAux,
                            Id = Convert.ToInt32(TextoAux.Split(',')[0])
                        };
                        ListaEliminacion.Add(AuxTarea);
                    }
                    catch (Exception)
                    {}
                } while (TextoAux != null);
            }
            EliminacionTareas ElimiarAux = ListaEliminacion.Find(x => x.Id == ID);
            ListaEliminacion.Remove(ElimiarAux);
            using (StreamWriter ArchivoLimpiar = new StreamWriter(@"Documentos\\Tareas.csv"))
            {
                ArchivoLimpiar.WriteLine(ListaEliminacion[0].Texto);
                ListaEliminacion.RemoveAt(0);
                ArchivoLimpiar.Flush();
            }
               
            using (StreamWriter ArchivoEscritura = new StreamWriter(@"Documentos\\Tareas.csv", true))
            {
                foreach (EliminacionTareas item in ListaEliminacion)
                {
                    ArchivoEscritura.WriteLineAsync(item.Texto);
                    ArchivoEscritura.Flush();
                }
            }
            LiderDV.EmpleadosDV.Find(x => x.Id == IDDV).TareasDV.Delete();
            LiderDV.HashTable.TablaTareas.Clear(); 
            using (TextFieldParser Archivo = new TextFieldParser(@"Documentos\\Tareas.csv"))
            {
                Archivo.TextFieldType = FieldType.Delimited;
                Archivo.SetDelimiters(",");
                while (!Archivo.EndOfData)
                {
                    string[] Texto = Archivo.ReadFields();
                    Tarea TareaAux = new Tarea()
                    {
                        Titulo = Texto[1],
                        Descripccion = Texto[2],
                        Proyecto = Texto[3],
                        Fecha = DateTime.Parse(Texto[4]),
                        Id = Convert.ToInt32(Texto[0]),
                        IdDesarrollador = Convert.ToInt32(Texto[5]),
                        Prioridad = Convert.ToInt32(Texto[6])
                    };
                    LiderDV.HashTable.TablaTareas.Add(TareaAux);
                }
            }
            return View("IndexEncargado",LiderDV);
        }

        // GET: Encargado/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Encargado/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}