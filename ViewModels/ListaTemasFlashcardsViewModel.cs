using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SoftwareEngineeringQuizApp.Models; // Necesario para usar TemaUI
using SoftwareEngineeringQuizApp.Services;

namespace SoftwareEngineeringQuizApp.ViewModels;

public partial class ListaTemasFlashcardsViewModel : ObservableObject
{
    private readonly RepositorioBaseDatos _repositorio;

    // Usamos una colección de TemaUI en lugar de strings simples
    [ObservableProperty]
    private ObservableCollection<TemaUI> temasUI;

    [ObservableProperty]
    private bool estaCargando;

    public ListaTemasFlashcardsViewModel(RepositorioBaseDatos repositorio)
    {
        _repositorio = repositorio;
        TemasUI = new ObservableCollection<TemaUI>();
    }

    [RelayCommand]
    public async Task CargarTemasAsync()
    {
        EstaCargando = true;
        try
        {
            var listaNombresDb = await _repositorio.ObtenerTemasDisponibles();

            TemasUI.Clear();
            foreach (var nombreTema in listaNombresDb)
            {
                TemasUI.Add(new TemaUI
                {
                    Nombre = nombreTema,
                    IconoSource = ObtenerIconoParaTema(nombreTema)
                });
            }
        }
        finally
        {
            EstaCargando = false;
        }
    }

    // Método para mapear el nombre del tema al nombre del archivo de imagen
    // Asegúrate de que estos nombres coincidan con tus archivos en Resources/Images
    private string ObtenerIconoParaTema(string nombreTema)
    {
        return nombreTema switch
        {
            "C# .NET" => "basededatos.png",
            "Arquitectura" => "agil.png",
            "Patrones" => "poo.png",
            "Testing" => "git.png",
            "Web" => "ciberseguridad.png",
            "DevOps" => "html.png",
            _ => "icono_default.png" // Imagen por defecto si no encuentra coincidencia
        };
    }

    [RelayCommand]
    public async Task SeleccionarTemaAsync(string tema)
    {
        if (string.IsNullOrWhiteSpace(tema)) return;

        // Navegamos pasando el nombre del tema
        await Shell.Current.GoToAsync($"FlashcardViewer?tema={tema}");
    }
}