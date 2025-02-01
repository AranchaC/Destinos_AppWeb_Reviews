using Destinos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;


namespace Destinos.Controllers
{
    public class UsuariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext(); // Contexto de base de datos

        // Acción para mostrar la vista de gestión de usuarios
        [Authorize(Roles = "Administrador")]
        public ActionResult GestionUsuarios(int? page, string sortOrder)
        {
            var usuarios = db.Users.ToList(); // Obtener todos los usuarios desde la base de datos

            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserNameSortParm = String.IsNullOrEmpty(sortOrder) ? "userName_desc" : "";
            ViewBag.NombreSortParm = sortOrder == "Nombre" ? "nombre_desc" : "Nombre";
            ViewBag.ApellidoSortParm = sortOrder == "Apellido" ? "apellido_desc" : "Apellido";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "email_desc" : "Email";
            ViewBag.FechaRegistroSortParm = sortOrder == "FechaRegistro" ? "fechaRegistro_desc" : "FechaRegistro";

            switch (sortOrder)
            {
                case "userName_desc":
                    usuarios = usuarios.OrderByDescending(u => u.UserName).ToList();
                    break;
                case "Nombre":
                    usuarios = usuarios.OrderBy(u => u.Nombre).ToList();
                    break;
                case "nombre_desc":
                    usuarios = usuarios.OrderByDescending(u => u.Nombre).ToList();
                    break;
                case "Apellido":
                    usuarios = usuarios.OrderBy(u => u.Apellido).ToList();
                    break;
                case "apellido_desc":
                    usuarios = usuarios.OrderByDescending(u => u.Apellido).ToList();
                    break;
                case "Email":
                    usuarios = usuarios.OrderBy(u => u.Email).ToList();
                    break;
                case "email_desc":
                    usuarios = usuarios.OrderByDescending(u => u.Email).ToList();
                    break;
                case "FechaRegistro":
                    usuarios = usuarios.OrderBy(u => u.FechaRegistro).ToList();
                    break;
                case "fechaRegistro_desc":
                    usuarios = usuarios.OrderByDescending(u => u.FechaRegistro).ToList();
                    break;
                default:
                    usuarios = usuarios.OrderBy(u => u.UserName).ToList();
                    break;
            }

            int pageSize = 6; // Número de usuarios por página
            int pageNumber = (page ?? 1); // Número de página actual

            return View(usuarios.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult VerReseñasUsuario(string id)
        {
            // Obtener el usuario por su ID
            var usuario = db.Users.Find(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            // Obtener las reseñas del usuario
            var reseñas = db.Resenas.Where(r => r.IdUser == id).ToList();

            // Agrupar las reseñas por el nombre del destino
            var reseñasAgrupadas = reseñas.GroupBy(r => r.destino.Nombre).ToList();

            // Puedes pasar las reseñas agrupadas a la vista
            ViewBag.Usuario = usuario;
            return View(reseñasAgrupadas);
        }




        // GET: Usuarios
        public ActionResult Index(int? page)
        {
            var usuarios = db.Users.ToList(); // Obtener todos los usuarios desde la base de datos
            int pageSize = 6; // Número de usuarios por página
            int pageNumber = (page ?? 1); // Número de página actual

            return View(usuarios.ToPagedList(pageNumber, pageSize));
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
