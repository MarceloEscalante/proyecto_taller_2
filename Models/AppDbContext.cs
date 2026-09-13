using Microsoft.EntityFrameworkCore;

namespace Sistema_ModParts.Models;

public class AppDbContext : DbContext
{
    public DbSet<Empleado> Empleados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=aws-0-sa-east-1.pooler.supabase.com;Port=6543;Database=postgres;Username=postgres.bzinmvwqqbekohvaovzq;Password=senhaforte2026;SSL Mode=Require;Trust Server Certificate=true;");
        }
}