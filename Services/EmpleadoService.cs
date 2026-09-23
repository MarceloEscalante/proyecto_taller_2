using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sistema_ModParts.Models;
using Sistema_ModParts.Repositories;
using BCrypt.Net;

namespace Sistema_ModParts.Services;

public class EmpleadoService : IEmpleadoService
{
    private readonly IEmpleadoRepository _empleadoRepository;

    public EmpleadoService(IEmpleadoRepository empleadoRepository)
    {
        _empleadoRepository = empleadoRepository;
    }

    public async Task<IEnumerable<Empleado>> ObtenerTodosAsync()
    {
        return await _empleadoRepository.GetAllAsync();
    }

    public async Task<Empleado?> ObtenerPorIdAsync(long id)
    {
        return await _empleadoRepository.GetByIdAsync(id);
    }

    public async Task<(bool Exito, string Mensaje)> CrearEmpleadoAsync(Empleado empleado, string contraseñaPlana)
    {
        try
        {
            // Validación de negocio: Verificar que el email no exista
            var existente = await _empleadoRepository.GetByEmailAsync(empleado.Email);
            if (existente != null)
                return (false, "Ya existe un usuario registrado con este correo.");

            // Validar que se haya enviado contraseña
            if (string.IsNullOrWhiteSpace(contraseñaPlana))
                return (false, "La contraseña es obligatoria para un nuevo usuario.");

            // Seguridad: Hashing de la contraseña
            empleado.Contrasena = BCrypt.Net.BCrypt.HashPassword(contraseñaPlana);

            // Asignar default de estado si es necesario
            empleado.Estado = true;

            await _empleadoRepository.AddAsync(empleado);
            return (true, "Usuario creado exitosamente.");
        }
        catch (Exception ex)
        {
            return (false, $"Error al crear el usuario: {ex.Message}");
        }
    }

    public async Task<(bool Exito, string Mensaje)> ActualizarEmpleadoAsync(Empleado empleado, string? nuevaContraseñaPlana)
    {
        try
        {
            var existenteDb = await _empleadoRepository.GetByIdAsync(empleado.IdEmpleado);
            if (existenteDb == null)
                return (false, "El usuario que intenta modificar no existe.");

            // Si se cambia el email, validar que no choque con otro
            if (existenteDb.Email != empleado.Email)
            {
                var emailExistente = await _empleadoRepository.GetByEmailAsync(empleado.Email);
                if (emailExistente != null)
                    return (false, "Ya existe otro usuario con este correo.");
                
                existenteDb.Email = empleado.Email;
            }

            // Actualizar campos permitidos
            existenteDb.IdRol = empleado.IdRol;
            existenteDb.Estado = empleado.Estado;

            // Si envió nueva contraseña, aplicamos hash y reemplazamos
            if (!string.IsNullOrWhiteSpace(nuevaContraseñaPlana))
            {
                existenteDb.Contrasena = BCrypt.Net.BCrypt.HashPassword(nuevaContraseñaPlana);
            }

            await _empleadoRepository.UpdateAsync(existenteDb);
            return (true, "Usuario actualizado exitosamente.");
        }
        catch (Exception ex)
        {
            return (false, $"Error al actualizar el usuario: {ex.Message}");
        }
    }

    public async Task<(bool Exito, string Mensaje)> EliminarEmpleadoAsync(long id)
    {
        try
        {
            await _empleadoRepository.DeleteAsync(id);
            return (true, "Usuario eliminado exitosamente.");
        }
        catch (Exception ex)
        {
            return (false, $"Error al eliminar el usuario: {ex.Message}");
        }
    }
}
