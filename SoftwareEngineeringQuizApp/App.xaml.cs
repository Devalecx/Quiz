using SoftwareEngineeringQuizApp.Services;

namespace SoftwareEngineeringQuizApp;

public partial class App : Application
{
    private readonly RepositorioBaseDatos _repositorio;

    // Inyectamos el repositorio directamente en el constructor de la App
    public App(RepositorioBaseDatos repositorio)
    {
        InitializeComponent();

        _repositorio = repositorio;

        // Establecemos el Shell como la página principal
        MainPage = new AppShell();
    }

    protected override async void OnStart()
    {
        base.OnStart();

        // Inicializamos la Base de Datos al arrancar la app.
        // Esto crea tablas y siembra los datos (Seed) si no existen.
        try
        {
            // NOTA: Asegúrate de que tengas un método público en tu Repositorio 
            // que llame internamente a Init(). 
            // Si tu método Init() es privado, crea un método público envoltorio.
            // Ejemplo: await _repositorio.ObtenerTemasDisponibles(); 
            // (Esto forzará la inicialización interna definida en Fase 1).

            await _repositorio.ObtenerTemasDisponibles();
            Console.WriteLine("Base de datos SQLite inicializada correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error crítico al iniciar DB: {ex.Message}");
        }
    }
}