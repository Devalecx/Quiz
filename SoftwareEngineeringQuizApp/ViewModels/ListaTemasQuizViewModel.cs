using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SoftwareEngineeringQuizApp.Services;

namespace SoftwareEngineeringQuizApp.ViewModels;

public partial class ListaTemasQuizViewModel : ObservableObject
{
    private readonly RepositorioBaseDatos _repositorio;

    [ObservableProperty]
    private ObservableCollection<string> temas;

    [ObservableProperty]
    private bool estaCargando;

    public ListaTemasQuizViewModel(RepositorioBaseDatos repositorio)
    {
        _repositorio = repositorio;
        Temas = new ObservableCollection<string>();
    }

    // Se llama automáticamente cuando entramos a la pantalla si usamos el evento "Appearing" en la vista
    [RelayCommand]
    public async Task CargarTemasAsync()
    {
        EstaCargando = true;
        try
        {
            var listaTemas = await _repositorio.ObtenerTemasDisponibles();
            Temas.Clear();
            foreach (var tema in listaTemas)
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

        // Navegamos a la pantalla de preguntas pasando el tema como parámetro
        await Shell.Current.GoToAsync($"PreguntaQuiz?tema={tema}");
    }
}