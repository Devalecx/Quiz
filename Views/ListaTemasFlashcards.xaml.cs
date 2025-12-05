using SoftwareEngineeringQuizApp.ViewModels;

namespace SoftwareEngineeringQuizApp.Views;

public partial class ListaTemasFlashcardsPage : ContentPage
{
    // 1. Guardamos la referencia al ViewModel
    private readonly ListaTemasFlashcardsViewModel _vm;

    public ListaTemasFlashcardsPage(ListaTemasFlashcardsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm; // Guardamos la referencia aquí
    }

    // 2. Este método es EL SECRETO. Se ejecuta cada vez que entras a la pantalla.
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // 3. Forzamos la ejecución del comando para cargar los temas e imágenes
        if (_vm.CargarTemasCommand.CanExecute(null))
        {
            await _vm.CargarTemasCommand.ExecuteAsync(null);
        }
    }
}