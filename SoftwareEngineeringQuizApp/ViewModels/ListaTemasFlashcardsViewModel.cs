using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SoftwareEngineeringQuizApp.Services;

namespace SoftwareEngineeringQuizApp.ViewModels;

public partial class ListaTemasFlashcardsViewModel : ObservableObject
{
    private readonly RepositorioBaseDatos _repositorio;

    [ObservableProperty]
    private ObservableCollection<string> temas;

    [ObservableProperty]
    private bool estaCargando;

    // Inyección de Dependencia del Repositorio
    public ListaTemasFlashcardsViewModel(RepositorioBaseDatos repositorio)
    {
        _repositorio = repositorio;
        Temas = new ObservableCollection<string>();
    }

    [RelayCommand]
    public async Task CargarTemasAsync()
    {
        EstaCargando = true;
        try
        {
            // Reutilizamos el método del repositorio. 
            // En una app más compleja, podrías crear un método específico ObtenerTemasSoloFlashcards()
            var lista = await _repositorio.ObtenerTemasDisponibles();

            Temas.Clear();
            foreach (var tema in lista)
            {
                Temas.Add(tema);
            }
        }
        finally
        {
            EstaCargando = false;
        }
    }

    [RelayCommand]
    public async Task SeleccionarTemaAsync(string tema)
    {
        if (string.IsNullOrWhiteSpace(tema)) return;

        // Navegamos al visor enviando el tema seleccionado
        await Shell.Current.GoToAsync($"FlashcardViewer?tema={tema}");
    }
}