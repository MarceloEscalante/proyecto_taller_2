using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sistema_ModParts.Models;
using Sistema_ModParts.Services;
using Sistema_ModParts.Repositories;

namespace Sistema_ModParts.ViewModels;

public partial class ProductosViewModel : ViewModelBase
{
    private readonly IProductoService _productoService;
    private readonly ICategoriaRepository _categoriaRepository;

    public ProductosViewModel(IProductoService productoService, ICategoriaRepository categoriaRepository)
    {
        _productoService = productoService;
        _categoriaRepository = categoriaRepository;

        Productos = new ObservableCollection<Producto>();
        Categorias = new ObservableCollection<Categoria>();

        _ = CargarDatosAsync();
    }

    public ObservableCollection<Producto> Productos { get; }
    public ObservableCollection<Categoria> Categorias { get; }

    [ObservableProperty]
    private Producto? _productoSeleccionado;

    [ObservableProperty]
    private string _nombre = string.Empty;

    [ObservableProperty]
    private decimal _precio;

    [ObservableProperty]
    private int _stock;

    [ObservableProperty]
    private Categoria? _categoriaSeleccionada;

    [ObservableProperty]
    private bool _estado = true;

    [ObservableProperty]
    private string _mensaje = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TituloFormulario))]
    private bool _esEdicion;

    public string TituloFormulario => EsEdicion ? "Editar Producto" : "Crear Producto";

    partial void OnProductoSeleccionadoChanged(Producto? value)
    {
        if (value != null)
        {
            Nombre = value.Nombre;
            Precio = value.Precio;
            Stock = value.Stock;
            Estado = value.Estado;
            
            // Buscar la categoría correspondiente
            foreach (var cat in Categorias)
            {
                if (cat.IdCategoria == value.IdCategoria)
                {
                    CategoriaSeleccionada = cat;
                    break;
                }
            }
            EsEdicion = true;
        }
        else
        {
            LimpiarFormulario();
        }
    }

    [RelayCommand]
    private async Task CargarDatosAsync()
    {
        Categorias.Clear();
        var categoriasDb = await _categoriaRepository.GetAllAsync();
        foreach (var cat in categoriasDb)
        {
            Categorias.Add(cat);
        }

        await RecargarProductosAsync();
    }

    private async Task RecargarProductosAsync()
    {
        Productos.Clear();
        var productosDb = await _productoService.ObtenerTodosAsync();
        foreach (var prod in productosDb)
        {
            Productos.Add(prod);
        }
    }

    [RelayCommand]
    private async Task GuardarAsync()
    {
        if (CategoriaSeleccionada == null)
        {
            Mensaje = "Debe seleccionar una categoría.";
            return;
        }

        if (EsEdicion && ProductoSeleccionado != null)
        {
            ProductoSeleccionado.Nombre = Nombre;
            ProductoSeleccionado.Precio = Precio;
            ProductoSeleccionado.Stock = Stock;
            ProductoSeleccionado.IdCategoria = CategoriaSeleccionada.IdCategoria;
            ProductoSeleccionado.Estado = Estado;

            var (exito, msg) = await _productoService.ActualizarProductoAsync(ProductoSeleccionado);
            Mensaje = msg;
            if (exito)
            {
                await RecargarProductosAsync();
                LimpiarFormulario();
            }
        }
        else
        {
            var nuevoProducto = new Producto
            {
                Nombre = Nombre,
                Precio = Precio,
                Stock = Stock,
                IdCategoria = CategoriaSeleccionada.IdCategoria,
                Estado = Estado
            };

            var (exito, msg) = await _productoService.CrearProductoAsync(nuevoProducto);
            Mensaje = msg;
            if (exito)
            {
                await RecargarProductosAsync();
                LimpiarFormulario();
            }
        }
    }

    [RelayCommand]
    private async Task EliminarAsync()
    {
        if (ProductoSeleccionado == null) return;

        var (exito, msg) = await _productoService.EliminarProductoAsync(ProductoSeleccionado.IdProducto);
        Mensaje = msg;
        if (exito)
        {
            await RecargarProductosAsync();
            LimpiarFormulario();
        }
    }

    [RelayCommand]
    private void LimpiarFormulario()
    {
        ProductoSeleccionado = null;
        Nombre = string.Empty;
        Precio = 0;
        Stock = 0;
        Estado = true;
        CategoriaSeleccionada = null;
        EsEdicion = false;
    }
}