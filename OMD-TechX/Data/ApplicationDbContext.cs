using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OMD_TechX.Modelos;
using System.Reflection.Metadata;

namespace OMD_TechX.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Publicacion>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Adopcion>("Adopcion")
                .HasValue<Perdida>("Perdida");
            
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            //si quiero que todos los string en mi BD puedan
            //tener max 200 caracteres

            //configurationBuilder.Properties<string>().HaveMaxLength(200);
        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Perro> Perros => Set<Perro>();
        public DbSet<Atencion> Atenciones => Set<Atencion>();
        public DbSet<Turno> Turnos => Set<Turno>();
        public DbSet<PerroAtencion> PerroAtenciones => Set<PerroAtencion>();
        public DbSet<Disponibilidad> Disponibilidades => Set<Disponibilidad>();
        public DbSet<Publicacion> Publicaciones => Set<Publicacion>();

        public DbSet<Servicio> Servicios => Set<Servicio>();

    }
}