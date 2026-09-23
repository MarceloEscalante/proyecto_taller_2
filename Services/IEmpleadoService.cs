using System.Collections.Generic;
using System.Threading.Tasks;
using Sistema_ModParts.Models;

namespace Sistema_ModParts.Services;

public interface IEmpleadoService
{
    Task<IEnumerable<Empleado>> ObtenerTodosAsync();
    Task<Empleado?> ObtenerPorIdAsync(long id);
    Task<(bool Exito, string Mensaje)> CrearEmpleadoAsync(Empleado empleado, string contraseñaPlana);
    Task<(bool Exito, string Mensaje)> ActualizarEmpleadoAsync(Empleado empleado, string? nuevaContraseñaPlana);
    Task<(bool Exito, string Mensaje)> EliminarEmpleadoAsync(long id);
}
