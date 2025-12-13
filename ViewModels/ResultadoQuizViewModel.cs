using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SoftwareEngineeringQuizApp.Models;
using SoftwareEngineeringQuizApp.Services;

namespace SoftwareEngineeringQuizApp.ViewModels;

public partial class ResultadoQuizViewModel : ObservableObject, IQueryAttributable
{
    private readonly RepositorioBaseDatos _repositorio;

    [ObservableProperty]
    private int aciertos;

    [ObservableProperty]
    private int totalPreguntas;

    [ObservableProperty]
    private string mensajeResultado;

    [ObservableProperty]
    private string colorResultado;

    [ObservableProperty]
    private string temaActual = string.Empty;

    // ✅ NUEVO: Propiedad para el nombre
    [ObservableProperty]
    private string nombreUsuario = string.Empty;

    public ResultadoQuizViewModel(RepositorioBaseDatos repositorio)
    {
        _repositorio = repositorio;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        // 1. CARGAR NOMBRE DEL USUARIO
        // Lo hacemos al inicio para que aparezca rápido
        var usuario = await _repositorio.ObtenerUsuarioAsync();
        NombreUsuario = usuario.Nombre;

        // 2. PROCESAR RESULTADOS
        if (query.ContainsKey("aciertos") && query.ContainsKey("total"))
        {
            Aciertos = int.Parse(query["aciertos"].ToString());
            TotalPreguntas = int.Parse(query["total"].ToString());
            CalcularResultado();
        }

        // 3. GUARDAR HISTORIAL
        if (query.ContainsKey("tema"))
        {
            TemaActual = query["tema"].ToString();

            var nuevoIntento = new HistorialQuiz
            {
                Tema = TemaActual,
                Aciertos = Aciertos,
                TotalPreguntas = TotalPreguntas,
                Fecha = DateTime.Now
            };

            await _repositorio.GuardarHistorialAsync(nuevoIntento);
        }
    }

    private void CalcularResultado()
    {
        double porcentaje = 0;
        if (TotalPreguntas > 0)
        {
            porcentaje = (double)Aciertos / TotalPreguntas;
        }

        if (porcentaje >= 0.8)
        {
            MensajeResultado = "¡EXCELENTE TRABAJO!";
            ColorResultado = "#FFD700"; // Oro
        }
        else if (porcentaje >= 0.5)
        {
            MensajeResultado = "¡BUEN INTENTO!";
            ColorResultado = "#F9A825"; // Naranja
        }
        else
        {
            MensajeResultado = "SIGUE ESTUDIANDO";
            ColorResultado = "#FFFF00"; // Amarillo
        }
    }

    [RelayCommand]
    public async Task VolverAlMenuAsync()
    {
        await Shell.Current.GoToAsync("//MenuPrincipal");
    }

    [RelayCommand]
    public async Task RepetirQuizAsync()
    {
        if (string.IsNullOrWhiteSpace(TemaActual))
        {
            await Shell.Current.GoToAsync("//MenuPrincipal");
            return;
        }
        await Shell.Current.GoToAsync($"../..");
        await Shell.Current.GoToAsync($"PreguntaQuiz?tema={TemaActual}");
    }
}