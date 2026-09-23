using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sistema_ModParts.Models;

namespace Sistema_ModParts.Services.Operations;

public class OperationResult<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }

    public static OperationResult<T> Success(T data) => new OperationResult<T> { IsSuccess = true, Data = data };
    public static OperationResult<T> Failure(string errorMessage) => new OperationResult<T> { IsSuccess = false, ErrorMessage = errorMessage };
}

public interface IOperationsService
{
    Task<OperationResult<Operacion>> ProcessSaleAsync(long? idEmpleado, List<DetalleOperacion> detalles, decimal total, string notas = "");
    Task<OperationResult<Operacion>> ProcessInboundAsync(long? idEmpleado, List<DetalleOperacion> detalles, decimal total, string notas = "");
    Task<OperationResult<MovimientoInventario>> AdjustStockAsync(int idProducto, int cantidadAjuste, TipoMovimiento tipoMovimiento, string motivo);
    Task<List<MovimientoInventario>> GetMovementsHistoryAsync(DateTime? fromDate, DateTime? toDate, int? idProducto);
}
