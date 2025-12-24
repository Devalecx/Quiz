using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SoftwareEngineeringQuizApp.Models;
using SoftwareEngineeringQuizApp.Services;

namespace SoftwareEngineeringQuizApp.ViewModels;

// IQueryAttributable permite recibir parámetros de navegación (el tema seleccionado)
public partial class PreguntaQuizViewModel : ObservableObject, IQueryAttributable
{
    private readonly RepositorioBaseDatos _repositorio;

    // Lista completa de preguntas para la sesión
    private List<PreguntaQuiz> _preguntasDelQuiz = new();
    private int _indicePreguntaActual = 0;
    private int _aciertos = 0;

    [ObservableProperty]
    private string temaActual;

    [ObservableProperty]
    private PreguntaQuiz preguntaActual;

    [ObservableProperty]
    private string textoProgreso; // Ej: "Pregunta 1 de 5"

    [ObservableProperty]
    private bool estaCargando;

    public PreguntaQuizViewModel(RepositorioBaseDatos repositorio)
    {
        _repositorio = repositorio;
    }

    // Método que recibe el parámetro "tema" desde la navegación
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("tema"))
        {
            TemaActual = query["tema"].ToString();
            CargarPreguntas();
        }
    }

    private async void CargarPreguntas()
    {
        EstaCargando = true;
        try
        {
            // 1. Obtener preguntas de la BD
            var preguntasBD = await _repositorio.ObtenerPreguntasPorTema(TemaActual);

            // 2. Mezclar aleatoriamente (Shuffle)
            _preguntasDelQuiz = preguntasBD.OrderBy(x => Guid.NewGuid()).ToList();

            // 3. Reiniciar contadores
            _indicePreguntaActual = 0;
            _aciertos = 0;

            if (_preguntasDelQuiz.Any())
            {
                CargarSiguientePregunta();
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No hay preguntas para este tema.", "Volver");
                await Shell.Current.GoToAsync("..");
            }
        }
        finally
        {
            EstaCargando = false;
        }
    }

    private void CargarSiguientePregunta()
    {
        if (_indicePreguntaActual < _preguntasDelQuiz.Count)
        {
            PreguntaActual = _preguntasDelQuiz[_indicePreguntaActual];
            TextoProgreso = $"Pregunta {_indicePreguntaActual + 1} de {_preguntasDelQuiz.Count}";
        }
        else
        {
            FinalizarQuiz();
        }
    }

    [RelayCommand]
    public async Task ResponderAsync(string opcionSeleccionada)
    {
        if (EstaCargando) return;

        bool esCorrecta = opcionSeleccionada == PreguntaActual.RespuestaCorrecta;

        if (esCorrecta)
        {
            _aciertos++;
            await Shell.Current.DisplayAlert("¡Correcto!", "Muy bien contestado.", "Siguiente");
        }
        else
        {
            await Shell.Current.DisplayAlert("Incorrecto", $"La respuesta era: {PreguntaActual.RespuestaCorrecta}", "Siguiente");
        }

        // Avanzar índice
        _indicePreguntaActual++;
        CargarSiguientePregunta();
    }

    private async void FinalizarQuiz()
    {
        // Navegar a resultados pasando aciertos y total
        await Shell.Current.GoToAsync($"ResultadoQuiz?aciertos={_aciertos}&total={_preguntasDelQuiz.Count}");
    }
}