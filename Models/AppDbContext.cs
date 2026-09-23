using Microsoft.EntityFrameworkCore;

namespace Sistema_ModParts.Models;

public class AppDbContext : DbContext
{
    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Operacion> Operaciones { get; set; }
    public DbSet<DetalleOperacion> DetallesOperacion { get; set; }
    public DbSet<MovimientoInventario> MovimientosInventario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // NOTA: Reemplaza [TU_CONTRASENA_AQUI] por la contraseña real que creaste para tu proyecto.
        optionsBuilder.UseNpgsql("Host=aws-0-sa-east-1.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.ltbqnltzwwyhxwplcvng;Password=4iw2ieAP9-r?fC@;SSL Mode=Require;Trust Server Certificate=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la relación Empleado -> Rol
        modelBuilder.Entity<Empleado>()
            .HasOne(e => e.Rol)
            .WithMany(r => r.Empleados)
            .HasForeignKey(e => e.IdRol);

        // Configuración de la relación Producto -> Categoria
        modelBuilder.Entity<Producto>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.IdCategoria);

        // Seed data (Datos iniciales) para Rol
        modelBuilder.Entity<Rol>().HasData(
            new Rol { IdRol = 1, NombreRol = "Administrador" },
            new Rol { IdRol = 2, NombreRol = "Vendedor" },
            new Rol { IdRol = 3, NombreRol = "Almacén" }
        );

        // Seed data para Categoria
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { IdCategoria = 1, NombreCategoria = "Lubricantes" },
            new Categoria { IdCategoria = 2, NombreCategoria = "Filtros" },
            new Categoria { IdCategoria = 3, NombreCategoria = "Frenos" },
            new Categoria { IdCategoria = 4, NombreCategoria = "Accesorios" }
        );

    }
}