using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SoftwareEngineeringQuizApp.Models;
using SoftwareEngineeringQuizApp.Services;

namespace SoftwareEngineeringQuizApp.ViewModels;

public partial class ListaTemasQuizViewModel : ObservableObject
{
    private readonly RepositorioBaseDatos _repositorio;

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

    // ✅ CORREGIDO: Nombres SIN caracteres especiales (paréntesis ni tildes)
    private string ObtenerIconoParaTema(string nombreTema)
    {
        return nombreTema switch
        {
            "SQL" => "basededatos.png",
            "Arquitectura" => "agil.png",
            "POO" => "poo.png",
            "Git" => "git.png",
            "Ciberseguridad" => "ciberseguridad.png",
            "HTML5" => "html.png",
            _ => "icono_default.png"
        };
    }

    [RelayCommand]
    public async Task SeleccionarTemaAsync(string tema)
    {
        if (string.IsNullOrWhiteSpace(tema)) return;
        await Shell.Current.GoToAsync($"PreguntaQuiz?tema={tema}");
    }
}