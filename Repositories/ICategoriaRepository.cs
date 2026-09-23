using System.Collections.Generic;
using System.Threading.Tasks;
using Sistema_ModParts.Models;

namespace Sistema_ModParts.Repositories;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> GetAllAsync();
    Task<Categoria?> GetByIdAsync(int id);
    Task AddAsync(Categoria categoria);
    Task UpdateAsync(Categoria categoria);
    Task DeleteAsync(Categoria categoria);
}
