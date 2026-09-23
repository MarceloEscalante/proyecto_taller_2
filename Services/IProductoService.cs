using System.Collections.Generic;
using System.Threading.Tasks;
using Sistema_ModParts.Models;

namespace Sistema_ModParts.Services;

public interface IProductoService
{
    Task<IEnumerable<Producto>> ObtenerTodosAsync();
    Task<(bool Exito, string Mensaje)> CrearProductoAsync(Producto producto);
    Task<(bool Exito, string Mensaje)> ActualizarProductoAsync(Producto producto);
    Task<(bool Exito, string Mensaje)> EliminarProductoAsync(int id);
}
