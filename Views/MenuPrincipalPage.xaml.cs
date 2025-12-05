using SoftwareEngineeringQuizApp.ViewModels;

namespace SoftwareEngineeringQuizApp.Views;

public partial class MenuPrincipalPage : ContentPage
{
    public MenuPrincipalPage(MenuPrincipalViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    // --- INTERCEPTAR BOTÓN ATRÁS (Cierre de App) ---
    protected override bool OnBackButtonPressed()
    {
        // Ejecutamos la alerta en el hilo principal de la UI
        Dispatcher.Dispatch(async () =>
        {
            bool cerrar = await DisplayAlert(
                "Salir",
                "¿Deseas cerrar la aplicación?",
                "Sí, cerrar",
                "Cancelar");

            if (cerrar)
            {
                // Este método cierra la aplicación correctamente en Android/iOS
                Application.Current.Quit();
            }
        });

        // Retornamos TRUE para decirle al sistema:
        // "No minimices la app automáticamente, espera a que el usuario decida en la alerta".
        return true;
    }
}