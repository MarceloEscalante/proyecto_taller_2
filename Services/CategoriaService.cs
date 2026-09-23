using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sistema_ModParts.Models;
using Sistema_ModParts.Repositories;

namespace Sistema_ModParts.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<IEnumerable<Categoria>> ObtenerCategoriasAsync()
    {
        return await _categoriaRepository.GetAllAsync();
    }

    public async Task<(bool Exito, string Mensaje)> CrearCategoriaAsync(Categoria categoria)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(categoria.NombreCategoria))
                return (false, "El nombre de la categoría es obligatorio.");
            
            await _categoriaRepository.AddAsync(categoria);
            return (true, "Categoría creada exitosamente.");
        }
        catch (Exception ex)
        {
            return (false, $"Error al crear la categoría: {ex.Message}");
        }
    }

    public async Task<(bool Exito, string Mensaje)> ActualizarCategoriaAsync(Categoria categoria)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(categoria.NombreCategoria))
                return (false, "El nombre de la categoría es obligatorio.");
            
            await _categoriaRepository.UpdateAsync(categoria);
            return (true, "Categoría actualizada exitosamente.");
        }
        catch (Exception ex)
        {
            return (false, $"Error al actualizar la categoría: {ex.Message}");
        }
    }

    public async Task<(bool Exito, string Mensaje)> EliminarCategoriaAsync(int idCategoria)
    {
        try
        {
            var categoria = await _categoriaRepository.GetByIdAsync(idCategoria);
            if (categoria == null)
                return (false, "La categoría no existe.");

            // Add validations if necessary (e.g. if category has products)
            await _categoriaRepository.DeleteAsync(categoria);
            return (true, "Categoría eliminada exitosamente.");
        }
        catch (Exception ex)
        {
            return (false, $"Error al eliminar la categoría: {ex.Message}");
        }
    }
}
