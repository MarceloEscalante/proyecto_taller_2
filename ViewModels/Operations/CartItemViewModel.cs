using CommunityToolkit.Mvvm.ComponentModel;
using Sistema_ModParts.Models;

namespace Sistema_ModParts.ViewModels.Operations;

public partial class CartItemViewModel : ObservableObject
{
    public Producto Producto { get; }

    public int IdProducto => Producto.IdProducto;
    public string Nombre => Producto.Nombre;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Subtotal))]
    private int _cantidad;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Subtotal))]
    private decimal _precioUnitario;

    public decimal Subtotal => Cantidad * PrecioUnitario;

    public CartItemViewModel(Producto producto, int cantidad = 1, decimal? precioUnitarioOverride = null)
    {
        Producto = producto;
        Cantidad = cantidad;
        PrecioUnitario = precioUnitarioOverride ?? producto.Precio;
    }
}
