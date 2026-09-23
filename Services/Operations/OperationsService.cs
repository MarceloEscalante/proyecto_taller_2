using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema_ModParts.Models;

namespace Sistema_ModParts.Services.Operations;

public class OperationsService : IOperationsService
{
    private readonly AppDbContext _context;

    public OperationsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<Operacion>> ProcessSaleAsync(long? idEmpleado, List<DetalleOperacion> detalles, decimal total, string notas = "")
    {
        return await ProcessOperationAsync(TipoOperacion.Venta, TipoMovimiento.Salida, idEmpleado, detalles, total, notas);
    }

    public async Task<OperationResult<Operacion>> ProcessInboundAsync(long? idEmpleado, List<DetalleOperacion> detalles, decimal total, string notas = "")
    {
        return await ProcessOperationAsync(TipoOperacion.Ingreso, TipoMovimiento.Entrada, idEmpleado, detalles, total, notas);
    }

    private async Task<OperationResult<Operacion>> ProcessOperationAsync(
        TipoOperacion tipoOperacion, 
        TipoMovimiento tipoMovimiento, 
        long? idEmpleado, 
        List<DetalleOperacion> detalles, 
        decimal total, 
        string notas)
    {
        // Usamos una transacción para garantizar que si algo falla, no se descuadre el stock.
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var operacion = new Operacion
            {
                TipoOperacion = tipoOperacion,
                IdEmpleado = idEmpleado,
                Total = total,
                Notas = notas,
                FechaOperacion = DateTime.UtcNow,
                Estado = EstadoOperacion.Completada
            };

            _context.Operaciones.Add(operacion);
            await _context.SaveChangesAsync(); // Se guarda para obtener el IdOperacion autogenerado

            foreach (var det in detalles)
            {
                var producto = await _context.Productos.FindAsync(det.IdProducto);
                if (producto == null)
                    return OperationResult<Operacion>.Failure($"Producto con ID {det.IdProducto} no encontrado.");

                if (tipoMovimiento == TipoMovimiento.Salida && producto.Stock < det.Cantidad)
                    return OperationResult<Operacion>.Failure($"Stock insuficiente para el producto {producto.Nombre}. Stock actual: {producto.Stock}.");

                int stockAnterior = producto.Stock;
                
                if (tipoMovimiento == TipoMovimiento.Salida)
                    producto.Stock -= det.Cantidad;
                else
                    producto.Stock += det.Cantidad;

                _context.Productos.Update(producto);

                det.IdOperacion = operacion.IdOperacion;
                _context.DetallesOperacion.Add(det);

                var movimiento = new MovimientoInventario
                {
                    IdProducto = det.IdProducto,
                    IdOperacion = operacion.IdOperacion,
                    TipoMovimiento = tipoMovimiento,
                    Cantidad = det.Cantidad,
                    StockAnterior = stockAnterior,
                    StockNuevo = producto.Stock,
                    Notas = $"Operacion de {tipoOperacion}"
                };
                
                _context.MovimientosInventario.Add(movimiento);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return OperationResult<Operacion>.Success(operacion);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return OperationResult<Operacion>.Failure($"Error al procesar la operación: {ex.Message}");
        }
    }

    public async Task<OperationResult<MovimientoInventario>> AdjustStockAsync(int idProducto, int cantidadAjuste, TipoMovimiento tipoMovimiento, string motivo)
    {
        if (string.IsNullOrWhiteSpace(motivo))
            return OperationResult<MovimientoInventario>.Failure("Debe especificar un motivo para el ajuste.");

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var producto = await _context.Productos.FindAsync(idProducto);
            if (producto == null)
                return OperationResult<MovimientoInventario>.Failure("Producto no encontrado.");

            int stockAnterior = producto.Stock;

            if (tipoMovimiento == TipoMovimiento.Salida && producto.Stock < cantidadAjuste)
                return OperationResult<MovimientoInventario>.Failure("No se puede ajustar por debajo de 0 stock.");

            if (tipoMovimiento == TipoMovimiento.Salida)
                producto.Stock -= cantidadAjuste;
            else
                producto.Stock += cantidadAjuste;

            _context.Productos.Update(producto);

            var operacionAjuste = new Operacion
            {
                TipoOperacion = TipoOperacion.Ajuste,
                Total = 0,
                Notas = motivo
            };
            
            _context.Operaciones.Add(operacionAjuste);
            await _context.SaveChangesAsync();

            var movimiento = new MovimientoInventario
            {
                IdProducto = idProducto,
                IdOperacion = operacionAjuste.IdOperacion,
                TipoMovimiento = tipoMovimiento,
                Cantidad = cantidadAjuste,
                StockAnterior = stockAnterior,
                StockNuevo = producto.Stock,
                Notas = $"Ajuste Manual: {motivo}"
            };

            _context.MovimientosInventario.Add(movimiento);
            
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return OperationResult<MovimientoInventario>.Success(movimiento);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return OperationResult<MovimientoInventario>.Failure($"Error al ajustar stock: {ex.Message}");
        }
    }

    public async Task<List<MovimientoInventario>> GetMovementsHistoryAsync(DateTime? fromDate, DateTime? toDate, int? idProducto)
    {
        var query = _context.MovimientosInventario
            .Include(m => m.Producto)
            .Include(m => m.Operacion)
            .AsQueryable();

        if (fromDate.HasValue)
            query = query.Where(m => m.FechaMovimiento >= fromDate.Value);
            
        if (toDate.HasValue)
            query = query.Where(m => m.FechaMovimiento <= toDate.Value);

        if (idProducto.HasValue)
            query = query.Where(m => m.IdProducto == idProducto.Value);

        return await query.OrderByDescending(m => m.FechaMovimiento).ToListAsync();
    }
}
