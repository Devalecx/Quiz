using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SoftwareEngineeringQuizApp.ViewModels;

public partial class MenuPrincipalViewModel : ObservableObject
{
    public MenuPrincipalViewModel()
    {
        // Constructor
    }

    [RelayCommand]
    private async Task IrAQuizAsync()
    {
        // Navegamos a la ruta registrada en AppShell.xaml.cs
        // "ListaTemasQuiz" es el nombre de ruta que definimos.
        await Shell.Current.GoToAsync("ListaTemasQuiz");
    }

    [RelayCommand]
    private async Task IrAFlashcardsAsync()
    {
        // Navegación a la sección de estudio
        await Shell.Current.GoToAsync("ListaTemasFlashcards");
    }
}