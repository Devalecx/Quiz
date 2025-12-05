using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SoftwareEngineeringQuizApp.Models; // Importante para TemaUI
using SoftwareEngineeringQuizApp.Services;

namespace SoftwareEngineeringQuizApp.ViewModels;

public partial class ListaTemasQuizViewModel : ObservableObject
{
    private readonly RepositorioBaseDatos _repositorio;

    // CAMBIO: Usamos ObservableCollection<TemaUI> en vez de string
    [ObservableProperty]
    private ObservableCollection<TemaUI> temasUI;

    [ObservableProperty]
    private bool estaCargando;

    public ListaTemasQuizViewModel(RepositorioBaseDatos repositorio)
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
            var listaNombres = await _repositorio.ObtenerTemasDisponibles();

            TemasUI.Clear();
            foreach (var nombreTema in listaNombres)
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

    // Mapeo de imagen por tema
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
            _ => "icono_default.png"
        };
    }

    [RelayCommand]
    public async Task SeleccionarTemaAsync(string tema)
    {
        if (string.IsNullOrWhiteSpace(tema)) return;
        // Navegamos a la pantalla de Preguntas
        await Shell.Current.GoToAsync($"PreguntaQuiz?tema={tema}");
    }
}