using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sistema_ModParts.Models;
using Sistema_ModParts.Services.Operations;

namespace Sistema_ModParts.ViewModels.Operations;

public partial class PosViewModel : ViewModelBase
{
    private readonly IOperationsService _operationsService;

    [ObservableProperty]
    private ObservableCollection<CartItemViewModel> _cart = new();

    public decimal Total => Cart.Sum(x => x.Subtotal);
    public int TotalArticulos => Cart.Sum(x => x.Cantidad);

    [ObservableProperty]
    private string _quickSearchText = string.Empty;

    // Se asigna desde la Vista (PosView.axaml.cs)
    public Func<string?, Task<Producto?>>? RequestSearchPartAsync { get; set; }
    public Action<string, string>? ShowMessage { get; set; } // Title, Message

    public PosViewModel(IOperationsService operationsService)
    {
        _operationsService = operationsService;
        Cart.CollectionChanged += (s, e) => 
        {
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(TotalArticulos));
        };
    }

    [RelayCommand]
    private async Task OpenSearchAsync()
    {
        if (RequestSearchPartAsync == null) return;

        var selectedPart = await RequestSearchPartAsync(null);
        if (selectedPart != null)
        {
            var existing = Cart.FirstOrDefault(x => x.IdProducto == selectedPart.IdProducto);
            if (existing != null)
            {
                existing.Cantidad++;
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(TotalArticulos));
            }
            else
            {
                var item = new CartItemViewModel(selectedPart, 1);
                item.PropertyChanged += (s, e) => 
                { 
                    if (e.PropertyName == nameof(CartItemViewModel.Subtotal) || e.PropertyName == nameof(CartItemViewModel.Cantidad)) 
                    {
                        OnPropertyChanged(nameof(Total)); 
                        OnPropertyChanged(nameof(TotalArticulos));
                    }
                };
                Cart.Add(item);
            }
        }
    }

    [RelayCommand]
    private async Task QuickSearchAsync()
    {
        if (string.IsNullOrWhiteSpace(QuickSearchText) || RequestSearchPartAsync == null) return;
        
        var selectedPart = await RequestSearchPartAsync(QuickSearchText);
        if (selectedPart != null)
        {
            var existing = Cart.FirstOrDefault(x => x.IdProducto == selectedPart.IdProducto);
            if (existing != null)
            {
                existing.Cantidad++;
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(TotalArticulos));
            }
            else
            {
                var item = new CartItemViewModel(selectedPart, 1);
                item.PropertyChanged += (s, e) => 
                { 
                    if (e.PropertyName == nameof(CartItemViewModel.Subtotal) || e.PropertyName == nameof(CartItemViewModel.Cantidad)) 
                    {
                        OnPropertyChanged(nameof(Total)); 
                        OnPropertyChanged(nameof(TotalArticulos));
                    }
                };
                Cart.Add(item);
            }
        }
        QuickSearchText = string.Empty; // Clear after search
    }

    [RelayCommand]
    private void RemoveItem(CartItemViewModel item)
    {
        if (item != null)
        {
            Cart.Remove(item);
        }
    }

    [RelayCommand]
    private async Task ProcessSaleAsync()
    {
        if (!Cart.Any())
        {
            ShowMessage?.Invoke("Advertencia", "El carrito está vacío.");
            return;
        }

        var detalles = Cart.Select(c => new DetalleOperacion
        {
            IdProducto = c.IdProducto,
            Cantidad = c.Cantidad,
            PrecioUnitario = c.PrecioUnitario
        }).ToList();

        // Si no hay empleado con ID 1 en la BD, tirará error FK. Usamos null por ahora.
        long? idEmpleadoLogueado = null;

        try 
        {
            var result = await _operationsService.ProcessSaleAsync(idEmpleadoLogueado, detalles, Total, "Venta Mostrador");

            if (result.IsSuccess)
            {
                Cart.Clear();
                ShowMessage?.Invoke("Éxito", "Venta registrada y guardada en BD correctamente.");
            }
            else
            {
                ShowMessage?.Invoke("Error", result.ErrorMessage ?? "Error desconocido al procesar la venta.");
            }
        }
        catch (Exception ex)
        {
            ShowMessage?.Invoke("Excepción Crítica", $"Error al guardar en BD: {ex.InnerException?.Message ?? ex.Message}");
        }
    }
}
