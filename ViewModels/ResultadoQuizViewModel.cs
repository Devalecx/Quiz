using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SoftwareEngineeringQuizApp.ViewModels;

public partial class ResultadoQuizViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private int aciertos;

    [ObservableProperty]
    private int totalPreguntas;

    [ObservableProperty]
    private string mensajeResultado;

    [ObservableProperty]
    private string colorResultado; // Hex color para UI

    // ✅ NUEVA: Propiedad para guardar el tema del quiz
    [ObservableProperty]
    private string temaActual = string.Empty;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("aciertos") && query.ContainsKey("total"))
        {
            Aciertos = int.Parse(query["aciertos"].ToString());
            TotalPreguntas = int.Parse(query["total"].ToString());
            CalcularResultado();
        }

        // ✅ NUEVO: Guardamos el tema para poder repetir el quiz
        if (query.ContainsKey("tema"))
        {
            TemaActual = query["tema"].ToString();
        }
    }

    private void CalcularResultado()
    {
        double porcentaje = (double)Aciertos / TotalPreguntas;

        if (porcentaje >= 0.8)
        {
            MensajeResultado = "¡Excelente Trabajo!";
            ColorResultado = "#FFD700"; // Oro
        }
        else if (porcentaje >= 0.5)
        {
            MensajeResultado = "¡Buen Intento!";
            ColorResultado = "#F9A825"; // Amarillo/Naranja
        }
        else
        {
            MensajeResultado = "Sigue Estudiando";
            ColorResultado = "#FFFF00"; // Amarillo
        }
    }

    [RelayCommand]
    public async Task VolverAlMenuAsync()
    {
        // Navegar a la raíz absoluta (resetea la pila de navegación)
        await Shell.Current.GoToAsync("//MenuPrincipal");
    }

    // ✅ NUEVO: Comando para repetir el quiz del mismo tema
    [RelayCommand]
    public async Task RepetirQuizAsync()
    {
        if (string.IsNullOrWhiteSpace(TemaActual))
        {
            // Si no hay tema guardado, volver al menú
            await Shell.Current.GoToAsync("//MenuPrincipal");
            return;
        }

        // Navegar de vuelta a PreguntaQuiz con el mismo tema
        // Usamos ".." dos veces para salir de ResultadoQuiz y PreguntaQuiz anterior
        await Shell.Current.GoToAsync($"../..");

        // Luego navegamos de nuevo a PreguntaQuiz con el mismo tema
        await Shell.Current.GoToAsync($"PreguntaQuiz?tema={TemaActual}");
    }
}






