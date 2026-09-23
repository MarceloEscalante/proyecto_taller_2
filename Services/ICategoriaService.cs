using System.Collections.Generic;
using System.Threading.Tasks;
using Sistema_ModParts.Models;

namespace Sistema_ModParts.Services;

public interface ICategoriaService
{
    Task<IEnumerable<Categoria>> ObtenerCategoriasAsync();
    Task<(bool Exito, string Mensaje)> CrearCategoriaAsync(Categoria categoria);
    Task<(bool Exito, string Mensaje)> ActualizarCategoriaAsync(Categoria categoria);
    Task<(bool Exito, string Mensaje)> EliminarCategoriaAsync(int idCategoria);
}
