using SoftwareEngineeringQuizApp.Services;

namespace SoftwareEngineeringQuizApp.Views;

public partial class SplashPage : ContentPage
{
    private readonly RepositorioBaseDatos _repositorio;

    public SplashPage(RepositorioBaseDatos repositorio)
    {
        InitializeComponent();
        _repositorio = repositorio;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // =================================================================
        // ANIMACIÓN DE ZOOM (CRÍTICO)
        // =================================================================
        // Antes: ScaleTo(1.5, ...) -> 150%
        // Ahora: ScaleTo(2.0, ...) -> 200% (Doble de tamaño)
        //
        // - 2.0: El tamaño final deseado (200%).
        // - 1500: La duración en milisegundos (1.5 segundos).
        // - Easing.CubicOut: Efecto de desaceleración suave al final.
        var animacionTask = LogoImage.ScaleTo(2.0, 1500, Easing.CubicOut);

        // 2. DATOS: Inicializar base de datos en segundo plano mientras anima
        var cargaDatosTask = _repositorio.ObtenerTemasDisponibles();

        // Esperamos a que AMBAS cosas terminen (la animación y la carga de datos)
        await Task.WhenAll(animacionTask, cargaDatosTask);

        // Opcional: Una pequeña pausa extra al final para apreciar el logo gigante
        // await Task.Delay(300); 

        // 3. NAVEGACIÓN: Cambiar al menú principal
        Application.Current.MainPage = new AppShell();
    }
}