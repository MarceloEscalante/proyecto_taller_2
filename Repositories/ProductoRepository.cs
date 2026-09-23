using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema_ModParts.Models;

namespace Sistema_ModParts.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly AppDbContext _context;

    public ProductoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _context.Productos.Include(p => p.Categoria).ToListAsync();
    }

    public async Task<Producto?> GetByIdAsync(int id)
    {
        return await _context.Productos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.IdProducto == id);
    }

    public async Task AddAsync(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Producto producto)
    {
        _context.Productos.Update(producto);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Producto producto)
    {
        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
    }
}
