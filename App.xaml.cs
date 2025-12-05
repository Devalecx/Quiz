using SoftwareEngineeringQuizApp.Views;

namespace SoftwareEngineeringQuizApp;

public partial class App : Application
{
    // Inyectamos IServiceProvider para poder resolver la SplashPage y sus dependencias (Repositorio)
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        // EN LUGAR DE CARGAR EL SHELL DIRECTAMENTE:
        // Arrancamos con la página de Splash Animado.
        // Usamos GetService para que MAUI inyecte automáticamente el Repositorio dentro de SplashPage.
        MainPage = serviceProvider.GetService<SplashPage>();
    }

    // Nota: Hemos eliminado el método OnStart() porque la lógica de inicialización 
    // de la base de datos se movió a 'SplashPage.xaml.cs' para que ocurra durante la animación.
}