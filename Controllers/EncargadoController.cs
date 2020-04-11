using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EstructuraDato_Lab04.Models;

namespace EstructuraDato_Lab04.Controllers
{
    public class EncargadoController : Controller
    {

        // Carga los Datos de los CSV al iniciar el programa
        public ActionResult InicioApp()
        {
            //Cargar Documentos 
            return View("Principal");
        }
        //Muestra el Menu Principal
        public ActionResult Principal()
        {
            return View();
        }
        // GET: Encargado/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // Iniciar Encargado 
        public ActionResult IniciarSesion()
        {
            return View();
        }
        // Iniciar Encargado 
        public ActionResult IniciarDesarrollador()
        {
            List<Desarrolladores> Listas = new List<Desarrolladores>();
            ViewBag.Desarrolladores = Listas;
            return View("EscogerDesarrollador");
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

        // GET: Encargado/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Encargado/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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