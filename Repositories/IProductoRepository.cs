using System.Collections.Generic;
using System.Threading.Tasks;
using Sistema_ModParts.Models;

namespace Sistema_ModParts.Repositories;

public interface IProductoRepository
{
    Task<IEnumerable<Producto>> GetAllAsync();
    Task<Producto?> GetByIdAsync(int id);
    Task AddAsync(Producto producto);
    Task UpdateAsync(Producto producto);
    Task DeleteAsync(Producto producto);
}
