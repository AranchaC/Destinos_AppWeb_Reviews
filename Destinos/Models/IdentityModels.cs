using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Destinos.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [StringLength(25)]
        public string Nombre { get; set; }

        [StringLength(25)]
        public string Apellido { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relación con las Resenas
        public virtual List<Resena> Resenas { get; set; }

        public virtual List<Foto> Fotos { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }


    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            //Contexto bbdd, aqui definimos las tablas que tenga nuestro modelo.
            return new ApplicationDbContext();

        }


        public DbSet<Destino> Destinos { get; set; }
        public DbSet<Resena> Resenas { get; set; }
        public DbSet<Foto> Fotos { get; set; }

    }
}