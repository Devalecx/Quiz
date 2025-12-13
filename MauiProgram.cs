using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using SoftwareEngineeringQuizApp.Services;
using SoftwareEngineeringQuizApp.ViewModels;
using SoftwareEngineeringQuizApp.Views;
using Plugin.Maui.Audio; // NUEVO

namespace SoftwareEngineeringQuizApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // --- SERVICIOS ---
        builder.Services.AddSingleton<RepositorioBaseDatos>();

        // NUEVO: Registro de Audio
        builder.Services.AddSingleton(AudioManager.Current);
        builder.Services.AddSingleton<AudioService>();

        // --- VIEWMODELS ---
        builder.Services.AddTransient<MenuPrincipalViewModel>();
        builder.Services.AddTransient<ListaTemasQuizViewModel>();
        builder.Services.AddTransient<PreguntaQuizViewModel>();
        builder.Services.AddTransient<ResultadoQuizViewModel>();
        builder.Services.AddTransient<ListaTemasFlashcardsViewModel>();
        builder.Services.AddTransient<FlashcardViewerViewModel>();

        // --- VISTAS ---
        builder.Services.AddTransient<MenuPrincipalPage>();
        builder.Services.AddTransient<ListaTemasQuizPage>();
        builder.Services.AddTransient<PreguntaQuizPage>();
        builder.Services.AddTransient<ResultadoQuizPage>();
        builder.Services.AddTransient<ListaTemasFlashcardsPage>();
        builder.Services.AddTransient<FlashcardViewerPage>();
        builder.Services.AddTransient<SplashPage>();

        // ... dentro de CreateMauiApp
        // ViewModels
        builder.Services.AddTransient<PerfilViewModel>();
        // Views
        builder.Services.AddTransient<PerfilPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}