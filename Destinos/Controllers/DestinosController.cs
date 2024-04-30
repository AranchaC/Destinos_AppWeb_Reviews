using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Destinos.Models;
using PagedList;

namespace Destinos.Controllers
{
    public class DestinosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Destinos
        public ActionResult Index( string sortOrder, string buscar1String, 
            string buscar2String, string filtroActual, int? page )
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NombreSortParm = String.IsNullOrEmpty(sortOrder) ? "Nombre_desc" : "";
            ViewBag.CiudadSortParm = sortOrder == "Ciudad" ? "Ciudad_desc" : "Ciudad";
            ViewBag.PaisSortParm = sortOrder == "Pais" ? "Pais_desc" : "Pais";
            ViewBag.IdiomaSortParm = sortOrder == "Idioma" ? "Idioma_desc" : "Idioma";
            ViewBag.MonedaSortParm = sortOrder == "Moneda" ? "Moneda_desc" : "Moneda";
            ViewBag.ValSortParm = sortOrder == "ValoracionMedia" ? "ValoracionMedia_desc" : "ValoracionMedia";
            var destinos = db.Destinos.ToList(); // Trae todos los destinos de la base de datos

            // paginación
            if (buscar1String != null)
            {
                page = 1;
            }
            else
            {
                buscar1String = filtroActual;
            }

            ViewBag.CurrentFilter = buscar1String;

            // cuadros de Búsqueda
            if (!String.IsNullOrEmpty(buscar1String))
            {
                destinos = destinos.Where(s =>
                    CultureInfo.CurrentCulture.CompareInfo.IndexOf(
                        s.Nombre, buscar1String, 
                        CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) >= 0 ||
                    CultureInfo.CurrentCulture.CompareInfo.IndexOf(
                        s.Ciudad, buscar1String, 
                        CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) >= 0
                ).ToList();
            }

            if (!String.IsNullOrEmpty(buscar2String))
            {
                destinos = destinos.Where(s =>
                   CultureInfo.CurrentCulture.CompareInfo.IndexOf(s.Pais, buscar2String, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) >= 0
                ).ToList();
            }

            // Ordenación
            switch (sortOrder)
            {
                case "Nombre_desc":
                    destinos = destinos.OrderByDescending(s => s.Nombre).ToList();
                    break;
                case "Ciudad":
                    destinos = destinos.OrderBy(s => s.Ciudad).ToList();
                    break;
                case "Ciudad_desc":
                    destinos = destinos.OrderByDescending(s => s.Ciudad).ToList();
                    break;
                case "Pais":
                    destinos = destinos.OrderBy(s => s.Pais).ToList();
                    break;
                case "Pais_desc":
                    destinos = destinos.OrderByDescending(s => s.Pais).ToList();
                    break;
                case "Idioma":
                    destinos = destinos.OrderBy(s => s.Idioma).ToList();
                    break;
                case "Idioma_desc":
                    destinos = destinos.OrderByDescending(s => s.Idioma).ToList();
                    break;
                case "Moneda":
                    destinos = destinos.OrderBy(s => s.Moneda).ToList();
                    break;
                case "Moneda_desc":
                    destinos = destinos.OrderByDescending(s => s.Moneda).ToList();
                    break;
                case "ValoracionMedia":
                    destinos = destinos.OrderBy(s => s.ValoracionMedia).ToList();
                    break;
                case "ValoracionMedia_desc":
                    destinos = destinos.OrderByDescending(s => s.ValoracionMedia).ToList();
                    break;
                default:
                    destinos = destinos.OrderBy(s => s.Nombre).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNum = (page ?? 1);
            return View(destinos.ToPagedList(pageNum, pageSize));
            // return View(destinos);
            // return View(db.Destinos.OrderBy(n => n.Nombre).ToList());
        }

        // GET: Destinos/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destino destino = db.Destinos.Find(id);
            if (destino == null)
            {
                return HttpNotFound();
            }
            return View(destino);
        }

        // GET: Destinos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Destinos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Ciudad,Pais,Idioma,Moneda")] Destino destino)
        {
            if (ModelState.IsValid)
            {
                destino.Id = Guid.NewGuid();
                db.Destinos.Add(destino);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(destino);
        }

        // GET: Destinos/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destino destino = db.Destinos.Find(id);
            if (destino == null)
            {
                return HttpNotFound();
            }
            return View(destino);
        }

        // POST: Destinos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Ciudad, Comunidad_Region,Pais,Idioma,Moneda")] Destino destino)
        {
            if (ModelState.IsValid)
            {
                db.Entry(destino).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(destino);
        }

        // GET: Destinos/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destino destino = db.Destinos.Find(id);
            if (destino == null)
            {
                return HttpNotFound();
            }
            return View(destino);
        }

        // POST: Destinos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Destino destino = db.Destinos.Find(id);
            db.Destinos.Remove(destino);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult VerResenas(Guid id)
        {
            //Include: carga las reseñas asociadas a cada destino.
            //FirstOrDefault: carga el primer o único destino asociado al id.
            var destino = db.Destinos
                .Include(d => d.Resenas)
                .FirstOrDefault(d => d.Id == id);
            if (destino == null)
            {
                return HttpNotFound();
            }

            return View(destino);
        }
    }
}
