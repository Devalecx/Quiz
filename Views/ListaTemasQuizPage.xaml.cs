using SoftwareEngineeringQuizApp.ViewModels;

namespace SoftwareEngineeringQuizApp.Views;

public partial class ListaTemasQuizPage : ContentPage
{
    // Guardamos una referencia al ViewModel para usarla luego
    private readonly ListaTemasQuizViewModel _vm;

    public ListaTemasQuizPage(ListaTemasQuizViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm; // <--- Importante: Guardar la referencia
    }

    // Sobrescribimos este método para detectar cuando la pantalla se muestra
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Si el comando de carga existe y se puede ejecutar, lo ejecutamos manualmente.
        // Esto garantiza que los datos se pidan a la base de datos.
        if (_vm.CargarTemasCommand.CanExecute(null))
        {
            await _vm.CargarTemasCommand.ExecuteAsync(null);
        }
    }
}