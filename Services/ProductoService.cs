using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sistema_ModParts.Models;
using Sistema_ModParts.Repositories;

namespace Sistema_ModParts.Services;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _productoRepository;

    public ProductoService(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
    {
        return await _productoRepository.GetAllAsync();
    }

    public async Task<(bool Exito, string Mensaje)> CrearProductoAsync(Producto producto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                return (false, "El nombre del producto es obligatorio.");
            
            if (producto.Precio < 0)
                return (false, "El precio no puede ser negativo.");

            if (producto.Stock < 0)
                return (false, "El stock no puede ser negativo.");

            await _productoRepository.AddAsync(producto);
            return (true, "Producto creado exitosamente.");
        }
        catch (Exception ex)
        {
            return (false, $"Error al crear el producto: {ex.Message}");
        }
    }

    public async Task<(bool Exito, string Mensaje)> ActualizarProductoAsync(Producto producto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                return (false, "El nombre del producto es obligatorio.");
            
            if (producto.Precio < 0)
                return (false, "El precio no puede ser negativo.");

            if (producto.Stock < 0)
                return (false, "El stock no puede ser negativo.");

            await _productoRepository.UpdateAsync(producto);
            return (true, "Producto actualizado exitosamente.");
        }
        catch (Exception ex)
        {
            return (false, $"Error al actualizar el producto: {ex.Message}");
        }
    }

    public async Task<(bool Exito, string Mensaje)> EliminarProductoAsync(int id)
    {
        try
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto == null)
                return (false, "El producto no existe.");

            // Si se requiere validación extra (ej. si el producto tiene ventas), se agregaría aquí.

            await _productoRepository.DeleteAsync(producto);
            return (true, "Producto eliminado exitosamente.");
        }
        catch (Exception ex)
        {
            return (false, $"Error al eliminar el producto: {ex.Message}");
        }
    }
}
