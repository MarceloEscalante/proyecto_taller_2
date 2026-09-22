using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Sistema_ModParts.ViewModels;

public partial class ProductosViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<ProductoPrueba> listaProductos;

    public ProductosViewModel()
    {
        // Datos falsos para probar la tabla rápido sin conexión a DB
        ListaProductos = new ObservableCollection<ProductoPrueba>
        {
            new ProductoPrueba { Id = 1, Nombre = "Aceite Sintético 5W-40", Categoria = "Lubricantes", Precio = 8500.00m },
            new ProductoPrueba { Id = 2, Nombre = "Filtro de Aire Deportivo", Categoria = "Filtros", Precio = 3200.50m },
            new ProductoPrueba { Id = 3, Nombre = "Pastillas de Freno", Categoria = "Frenos", Precio = 12400.00m }
        };
    }
}

// Clase temporal solo para mostrar la tabla hoy
public class ProductoPrueba
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public decimal Precio { get; set; }
}