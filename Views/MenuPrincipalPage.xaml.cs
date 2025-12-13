using SoftwareEngineeringQuizApp.ViewModels;

namespace SoftwareEngineeringQuizApp.Views;

public partial class MenuPrincipalPage : ContentPage
{
    private readonly MenuPrincipalViewModel _vm;

    public MenuPrincipalPage(MenuPrincipalViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }

    // Se ejecuta al iniciar la app Y al regresar de otra pantalla
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Recargamos la foto y el nombre
        await _vm.CargarDatosUsuarioAsync();
    }

    // --- Tu código existente para interceptar botón atrás ---
    protected override bool OnBackButtonPressed()
    {
        Dispatcher.Dispatch(async () =>
        {
            bool cerrar = await DisplayAlert(
                "Salir",
                "¿Deseas cerrar la aplicación?",
                "Sí, cerrar",
                "Cancelar");

            if (cerrar)
            {
                Application.Current.Quit();
            }
        });
        return true;
    }
}