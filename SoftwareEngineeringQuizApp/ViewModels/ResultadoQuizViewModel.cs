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

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("aciertos") && query.ContainsKey("total"))
        {
            Aciertos = int.Parse(query["aciertos"].ToString());
            TotalPreguntas = int.Parse(query["total"].ToString());
            CalcularResultado();
        }
    }

    private void CalcularResultado()
    {
        double porcentaje = (double)Aciertos / TotalPreguntas;

        if (porcentaje >= 0.8)
        {
            MensajeResultado = "¡Excelente Trabajo! 🏆";
            ColorResultado = "#2E7D32"; // Verde
        }
        else if (porcentaje >= 0.5)
        {
            MensajeResultado = "¡Buen Intento! 👍";
            ColorResultado = "#F9A825"; // Amarillo/Naranja
        }
        else
        {
            MensajeResultado = "Sigue Estudiando 📚";
            ColorResultado = "#C62828"; // Rojo
        }
    }

    [RelayCommand]
    public async Task VolverAlMenuAsync()
    {
        // Navegar a la raíz absoluta (resetea la pila de navegación)
        await Shell.Current.GoToAsync("//MenuPrincipal");
    }
}