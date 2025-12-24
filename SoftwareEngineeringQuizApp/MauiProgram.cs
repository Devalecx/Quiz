using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using SoftwareEngineeringQuizApp.Services;
using SoftwareEngineeringQuizApp.ViewModels;
using SoftwareEngineeringQuizApp.Views;
using UraniumUI; // <--- NUEVO

namespace SoftwareEngineeringQuizApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseUraniumUI()             // <--- NUEVO: Inicializa Uranium
            .UseUraniumUIMaterial()     // <--- NUEVO: Inicializa Material Design
            .UseMauiCommunityToolkit() // Habilita el CommunityToolkit
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddMaterialIconFonts(); // <--- NUEVO: Carga los íconos
            });

        // ---------------------------------------------------------
        // 1. REGISTRO DE SERVICIOS (SINGLETON)
        // ---------------------------------------------------------
        // Creamos una única instancia de la base de datos para toda la app.
        builder.Services.AddSingleton<RepositorioBaseDatos>();

        // ---------------------------------------------------------
        // 2. REGISTRO DE VIEWMODELS (TRANSIENT)
        // ---------------------------------------------------------
        // Se crea una instancia nueva cada vez que navegamos a la página.
        // Esto es crucial para resetear puntuaciones y estados.

        // Fase 2: Menú Principal
        builder.Services.AddTransient<MenuPrincipalViewModel>();

        // Fase 3: Módulo de Quiz
        builder.Services.AddTransient<ListaTemasQuizViewModel>();
        builder.Services.AddTransient<PreguntaQuizViewModel>();
        builder.Services.AddTransient<ResultadoQuizViewModel>();

        // Fase 4: Módulo de Flashcards
        builder.Services.AddTransient<ListaTemasFlashcardsViewModel>();
        builder.Services.AddTransient<FlashcardViewerViewModel>();

        // ---------------------------------------------------------
        // 3. REGISTRO DE VISTAS / PÁGINAS (TRANSIENT)
        // ---------------------------------------------------------
        // Registramos las páginas para que MAUI pueda inyectarles el ViewModel automáticamente.

        // Fase 2: Menú Principal
        builder.Services.AddTransient<MenuPrincipalPage>();

        // Fase 3: Módulo de Quiz
        builder.Services.AddTransient<ListaTemasQuizPage>();
        builder.Services.AddTransient<PreguntaQuizPage>();
        builder.Services.AddTransient<ResultadoQuizPage>();

        // Fase 4: Módulo de Flashcards
        builder.Services.AddTransient<ListaTemasFlashcardsPage>();
        builder.Services.AddTransient<FlashcardViewerPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}