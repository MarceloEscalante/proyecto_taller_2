using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sistema_ModParts.Models;
using Sistema_ModParts.Services;

namespace Sistema_ModParts.ViewModels;

public partial class CategoriasViewModel : ViewModelBase
{
    private readonly ICategoriaService _categoriaService;
    private ObservableCollection<Categoria> _todasLasCategorias = new();

    public CategoriasViewModel(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
        Categorias = new ObservableCollection<Categoria>();
        
        _ = CargarDatosAsync();
    }

    [ObservableProperty]
    private ObservableCollection<Categoria> _categorias;

    [ObservableProperty]
    private Categoria? _categoriaSeleccionada;

    [ObservableProperty]
    private string _nombreCategoria = string.Empty;

    [ObservableProperty]
    private string _mensaje = string.Empty;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TituloFormulario))]
    private bool _esEdicion;

    public string TituloFormulario => EsEdicion ? "Editar Categoría" : "Nueva Categoría";

    partial void OnSearchTextChanged(string value)
    {
        AplicarFiltro();
    }

    partial void OnCategoriaSeleccionadaChanged(Categoria? value)
    {
        if (value != null)
        {
            NombreCategoria = value.NombreCategoria;
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
        try
        {
            IsLoading = true;
            Mensaje = string.Empty;
            
            var categoriasDb = await _categoriaService.ObtenerCategoriasAsync();
            _todasLasCategorias.Clear();
            foreach (var cat in categoriasDb)
            {
                _todasLasCategorias.Add(cat);
            }
            
            AplicarFiltro();
        }
        catch (Exception ex)
        {
            Mensaje = $"Error al cargar: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void AplicarFiltro()
    {
        var filtrado = string.IsNullOrWhiteSpace(SearchText) 
            ? _todasLasCategorias 
            : _todasLasCategorias.Where(c => c.NombreCategoria.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            
        Categorias.Clear();
        foreach (var cat in filtrado)
        {
            Categorias.Add(cat);
        }
    }

    [RelayCommand]
    private async Task GuardarAsync()
    {
        if (EsEdicion && CategoriaSeleccionada != null)
        {
            CategoriaSeleccionada.NombreCategoria = NombreCategoria;

            var (exito, msg) = await _categoriaService.ActualizarCategoriaAsync(CategoriaSeleccionada);
            Mensaje = msg;
            if (exito)
            {
                await CargarDatosAsync();
                LimpiarFormulario();
            }
        }
        else
        {
            var nuevaCategoria = new Categoria
            {
                NombreCategoria = NombreCategoria
            };

            var (exito, msg) = await _categoriaService.CrearCategoriaAsync(nuevaCategoria);
            Mensaje = msg;
            if (exito)
            {
                await CargarDatosAsync();
                LimpiarFormulario();
            }
        }
    }

    [RelayCommand]
    private async Task EliminarAsync()
    {
        if (CategoriaSeleccionada == null) return;

        var (exito, msg) = await _categoriaService.EliminarCategoriaAsync(CategoriaSeleccionada.IdCategoria);
        Mensaje = msg;
        if (exito)
        {
            await CargarDatosAsync();
            LimpiarFormulario();
        }
    }

    [RelayCommand]
    private void LimpiarFormulario()
    {
        CategoriaSeleccionada = null;
        NombreCategoria = string.Empty;
        EsEdicion = false;
    }
}
