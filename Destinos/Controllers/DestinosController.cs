using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Destinos.Models;

namespace Destinos.Controllers
{
    public class DestinosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Destinos
        public ActionResult Index()
        {
            return View(db.Destinos.OrderBy(n => n.Nombre).ToList());
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
