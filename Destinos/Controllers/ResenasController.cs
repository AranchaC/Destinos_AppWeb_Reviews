using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Destinos.Models;
using System.IO;
using System.Diagnostics;
using System.Data.Entity.Validation;
using System.Text;
using System.Web.Helpers;

namespace Destinos.Controllers
{
    public class ResenasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Resenas
        public ActionResult Index()
        {
            //var resenas = db.Resenas.Include(r => r.destino);
            //return View(resenas.ToList());

            var resenasAgrupadas = db.Resenas
                .Include(r => r.destino) // coge el destino asociado a cada reseña.
                .OrderBy(r => r.destino.Nombre) // Agrupa las Resenas por destino.Nombre.
                .ToList() // Nos da una lista con las reseñas ordenadas por destino.nombre y destino.fecha.
                .GroupBy(r => r.destino.Nombre) // Agrupa por nombre de destino. Todas las reseñas con el mismo destino.Nombre estarán en el mismo grupo.
                .Select(group => group.OrderByDescending(r => r.FechaResena).ToList()) // Convierte cada grupo de reseñas en una lista.
                .ToList(); // Convierte el resultado final (una lista de listas) en una lista.

            // esta línea de código está obteniendo todas las Resenas de la base de datos,
            // ordenándolas por el nombre del destino, agrupándolas por ese nombre, y finalmente
            // generando una lista donde cada elemento es otra lista de Resenas asociadas al mismo destino.

            return View(resenasAgrupadas);
        }

        // GET: Resenas/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resena resena = db.Resenas.Find(id);
            //var resena = db.Resenas.Include(r => r.destino).Include(r => r.User).SingleOrDefault(r => r.Id == id);

            if (resena == null)
            {
                return HttpNotFound();
            }
            return View(resena);
        }

        // GET: Resenas/Create
        public ActionResult Create()
        {
            ViewBag.IdDestino = new SelectList(db.Destinos.ToList().OrderBy(d => d.Nombre), "Id", "Nombre");
            return View();
        }

        // POST: Resenas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Resena resena, HttpPostedFileBase[] FotosSubidas)
        {
            // USUARIOS
            var usuario = db.Users.SingleOrDefault(x => x.UserName == User.Identity.Name);
            if (usuario == null || string.IsNullOrEmpty(usuario.Id))
            {
                resena.IdUser = null;
            }
            else
            {
                resena.IdUser = usuario.Id;
            }


            // Punto de depuración para verificar el valor de IdDestino
            Debug.WriteLine($"IdDestino seleccionado: {resena.IdDestino}");

            if (ModelState.IsValid)
            {
                Debug.WriteLine($"Dentro del Model isValid");
                try
                {
                    //Primero creo reseña sin fotos:
                    db.Resenas.Add(resena);
                    db.SaveChanges();
                    Debug.WriteLine($"Reseña creada con ID: {resena.Id}");
                    //Ahora que ya tengo reseña con id, añado fotos si las hay:
                    var fotosList = new List<Foto>();
                    var resenaFolder = resena.Id.ToString(); // Nombre de la carpeta para la reseña

                    if (FotosSubidas != null && FotosSubidas.Any(f => f != null && f.ContentLength > 0))
                    {
                        var pathToSave = Server.MapPath(@"~/imgsResenas/" + resenaFolder);
                        Directory.CreateDirectory(pathToSave);
                        Debug.WriteLine($"Carpeta creada con nombre: {resenaFolder}");
                        //Directory.CreateDirectory(pathToSave);

                        foreach (var foto in FotosSubidas)
                        {
                            Debug.WriteLine($"Dentro del foreach");
                            if (foto != null && foto.ContentLength > 0)
                            {
                                Debug.WriteLine($"Dentro del if");

                                var newFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(foto.FileName);
                                var imagePath = Path.Combine("imgsResenas", resenaFolder, newFileName);

                                try
                                {
                                    string path = Server.MapPath(@"~/" + imagePath);
                                    foto.SaveAs(path);
                                    Debug.WriteLine($"Foto guardada en: {imagePath}");

                                    var nuevaFoto = new Foto
                                    {
                                        IdResena = resena.Id,
                                        UrlFoto = newFileName,
                                        FechaSubida = DateTime.Now
                                    };

                                    fotosList.Add(nuevaFoto);
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine($"Error al guardar la foto: {ex.Message}");
                                }
                            } //ifFoto
                        } //forEachFoto
                    }
                    else
                    {
                        Debug.WriteLine($"No hay fotos para guardar");
                    }

                    resena.Fotos = fotosList;
                    //  aplicar cambios
                    db.SaveChanges();

                    Debug.WriteLine($"Reseña añadida a la base de datos con ID: {resena.Id}");
                    Debug.WriteLine($"Cambios guardados en la base de datos.");

                    return RedirectToAction("Details", new { id = resena.Id });
                } // try
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                } //catch
            } //ifModelValid

            ViewBag.IdDestino = new SelectList(db.Destinos.ToList().OrderBy(d => d.Nombre), "Id", "Nombre", resena.IdDestino);
            return View(resena);
        } //create


        // GET: Resenas/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resena resena = db.Resenas.Find(id);
            if (resena == null)
            {
                return HttpNotFound();
            }
            ViewBag.Destino = new SelectList(db.Destinos, "Id", "Nombre", resena.IdDestino);
            return View(resena);
        }

        // POST: Resenas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Resena resena, HttpPostedFileBase[] FotosSubidas)
        {
            if (ModelState.IsValid)
            {
                // Cargar la reseña existente desde la base de datos
                var resenaExistente = db.Resenas.Find(resena.Id);

                if (resenaExistente != null)
                {
                    // Actualizar los campos de la reseña existente
                    resenaExistente.Titulo = resena.Titulo;
                    resenaExistente.Puntuacion = resena.Puntuacion;
                    resenaExistente.Comentario = resena.Comentario;
                    resenaExistente.FechaResena = DateTime.Now; // Actualizar la fecha de la reseña

                    // Guardar las fotos
                    if (FotosSubidas != null && FotosSubidas.Length > 0)
                    {
                        var resenaFolder = resena.Id.ToString(); // Nombre de la carpeta para la reseña
                        var pathToSave = Server.MapPath(@"~/imgsResenas/" + resenaFolder);

                        if (!Directory.Exists(pathToSave))
                        {
                            Directory.CreateDirectory(pathToSave);
                        }

                        foreach (var foto in FotosSubidas)
                        {
                            if (foto != null && foto.ContentLength > 0)
                            {
                                var newFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(foto.FileName);
                                var imagePath = @"imgsResenas\" + resena.Id + @"\" + newFileName;

                                try
                                {
                                    string path = Server.MapPath(@"~/" + imagePath);
                                    foto.SaveAs(path);

                                    var nuevaFoto = new Foto
                                    {
                                        IdResena = resena.Id,
                                        UrlFoto = newFileName,
                                        FechaSubida = DateTime.Now
                                    };

                                    resenaExistente.Fotos.Add(nuevaFoto);
                                }
                                catch (Exception)
                                {
                                    // Manejar el error
                                } 
                            } // ifFoto
                        } //foreach
                    } //IfFotosSubidas

                    // Guardar los cambios
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = resena.Id });
                }
            }
            // Si llegas aquí, hay un problema, así que regresa a la vista de edición
            return View(resena);
        } //Edit

        // Método para eliminar foto
        public ActionResult EliminarFoto(Guid idFoto)
        {
            var foto = db.Fotos.Find(idFoto);
            if (foto != null)
            {
                db.Fotos.Remove(foto);
                db.SaveChanges();
            }
            return RedirectToAction("Edit", new { id = foto.IdResena });
        }

        // GET: Resenas/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resena resena = db.Resenas.Find(id);
            if (resena == null)
            {
                return HttpNotFound();
            }
            return View(resena);
        }

        // POST: Resenas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid? id)
        {
                Resena resena = db.Resenas.Find(id);

                db.Resenas.Remove(resena);
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
    }
}
