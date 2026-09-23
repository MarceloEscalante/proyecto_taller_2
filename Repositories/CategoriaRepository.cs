using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema_ModParts.Models;

namespace Sistema_ModParts.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        return await _context.Categorias.ToListAsync();
    }

    public async Task<Categoria?> GetByIdAsync(int id)
    {
        return await _context.Categorias.FindAsync(id);
    }

    public async Task AddAsync(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Categoria categoria)
    {
        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
    }
}
