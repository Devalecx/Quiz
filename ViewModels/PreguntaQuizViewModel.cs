using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SoftwareEngineeringQuizApp.Models;
using SoftwareEngineeringQuizApp.Services;
using System.Collections.ObjectModel;

namespace SoftwareEngineeringQuizApp.ViewModels;

public partial class PreguntaQuizViewModel : ObservableObject, IQueryAttributable
{
    private readonly RepositorioBaseDatos _repositorio;
    private readonly AudioService _audioService;

    private List<PreguntaQuiz> _preguntasDelQuiz = new();
    private int _indicePreguntaActual = 0;
    private int _aciertos = 0;

    [ObservableProperty] private string temaActual = string.Empty;
    [ObservableProperty] private PreguntaQuiz preguntaActual;
    [ObservableProperty] private string textoProgreso = string.Empty;
    [ObservableProperty] private bool estaCargando;

    // ✅ Estados de retroalimentación
    [ObservableProperty] private bool mostrarFeedback;
    [ObservableProperty] private bool esRespuestaCorrecta;
    [ObservableProperty] private string mensajeFeedback = string.Empty;
    [ObservableProperty] private string explicacionFeedback = string.Empty;
    [ObservableProperty] private string respuestaSeleccionada = string.Empty;
    [ObservableProperty] private bool botonesHabilitados = true;

    // Menú y Configuración
    [ObservableProperty] private bool menuVisible;
    [ObservableProperty] private bool isMusicEnabled = Preferences.Get("MusicEnabled", true);
    [ObservableProperty] private bool isSoundEffectsEnabled = Preferences.Get("SoundEnabled", true);

    public PreguntaQuizViewModel(RepositorioBaseDatos repositorio, AudioService audioService)
    {
        _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
        _audioService = audioService ?? throw new ArgumentNullException(nameof(audioService));

        System.Diagnostics.Debug.WriteLine("PreguntaQuizViewModel: Constructor ejecutado");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        try
        {
            System.Diagnostics.Debug.WriteLine("PreguntaQuizViewModel: ApplyQueryAttributes llamado");

            if (query == null)
            {
                System.Diagnostics.Debug.WriteLine("Query es null");
                return;
            }

            if (query.ContainsKey("tema"))
            {
                var tema = query["tema"]?.ToString();
                System.Diagnostics.Debug.WriteLine($"Tema recibido: {tema}");

                if (!string.IsNullOrEmpty(tema))
                {
                    TemaActual = tema;
                    CargarPreguntas();

                    // Iniciar música al entrar al Quiz
                    try
                    {
                        _audioService.IniciarMusicaFondoAsync("pista_3.ogg");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error al iniciar música: {ex.Message}");
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No se encontraron 'tema' en query");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en ApplyQueryAttributes: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
        }
    }

    private async void CargarPreguntas()
    {
        EstaCargando = true;
        System.Diagnostics.Debug.WriteLine($"Cargando preguntas para tema: {TemaActual}");

        try
        {
            var preguntasBD = await _repositorio.ObtenerPreguntasPorTema(TemaActual);

            if (preguntasBD == null || !preguntasBD.Any())
            {
                System.Diagnostics.Debug.WriteLine("No se encontraron preguntas");
                await Shell.Current.DisplayAlert("Error", "No hay preguntas disponibles para este tema.", "OK");
                await Shell.Current.GoToAsync("..");
                return;
            }

            // Mezclamos el orden de las PREGUNTAS
            _preguntasDelQuiz = preguntasBD.OrderBy(x => Guid.NewGuid()).ToList();
            _indicePreguntaActual = 0;
            _aciertos = 0;

            System.Diagnostics.Debug.WriteLine($"{_preguntasDelQuiz.Count} preguntas cargadas");
            CargarSiguientePregunta();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error cargando preguntas: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            await Shell.Current.DisplayAlert("Error", $"Error al cargar preguntas: {ex.Message}", "OK");
            await Shell.Current.GoToAsync("..");
        }
        finally
        {
            EstaCargando = false;
        }
    }

    private void CargarSiguientePregunta()
    {
        try
        {
            if (_indicePreguntaActual < _preguntasDelQuiz.Count)
            {
                // Obtenemos la pregunta actual
                var pregunta = _preguntasDelQuiz[_indicePreguntaActual];

                //  NUEVO: Aleatorizar Opciones para evitar memorización 
                if (pregunta.Opciones != null && pregunta.Opciones.Count > 1)
                {
                    pregunta.Opciones = pregunta.Opciones.OrderBy(x => Guid.NewGuid()).ToList();
                }

                PreguntaActual = pregunta;
                TextoProgreso = $"Pregunta {_indicePreguntaActual + 1} de {_preguntasDelQuiz.Count}";

                // Resetear estado de feedback
                MostrarFeedback = false;
                BotonesHabilitados = true;
                RespuestaSeleccionada = string.Empty;

                System.Diagnostics.Debug.WriteLine($"Pregunta cargada: {PreguntaActual?.Pregunta}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Quiz completado");
                try
                {
                    _audioService.DetenerMusicaFondo();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error deteniendo música: {ex.Message}");
                }
                FinalizarQuiz();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en CargarSiguientePregunta: {ex.Message}");
        }
    }

    [RelayCommand]
    public async Task ResponderAsync(string opcionSeleccionada)
    {
        if (EstaCargando || !BotonesHabilitados)
        {
            System.Diagnostics.Debug.WriteLine("Respuesta bloqueada (cargando o botones deshabilitados)");
            return;
        }

        if (string.IsNullOrEmpty(opcionSeleccionada))
        {
            System.Diagnostics.Debug.WriteLine("Opción seleccionada es null o vacía");
            return;
        }

        if (PreguntaActual == null)
        {
            System.Diagnostics.Debug.WriteLine("PreguntaActual es null");
            return;
        }

        try
        {
            // Deshabilitar botones inmediatamente
            BotonesHabilitados = false;
            RespuestaSeleccionada = opcionSeleccionada;

            bool esCorrecta = opcionSeleccionada == PreguntaActual.RespuestaCorrecta;
            EsRespuestaCorrecta = esCorrecta;

            System.Diagnostics.Debug.WriteLine($"Respuesta: {opcionSeleccionada} | Correcta: {esCorrecta}");

            if (esCorrecta)
            {
                _aciertos++;
                try
                {
                    await _audioService.ReproducirEfectoAsync("acierto.ogg");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error reproduciendo sonido acierto: {ex.Message}");
                }
                MensajeFeedback = "¡Correcto!";
            }
            else
            {
                try
                {
                    await _audioService.ReproducirEfectoAsync("error.ogg");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error reproduciendo sonido error: {ex.Message}");
                }
                MensajeFeedback = $"Incorrecto. La respuesta correcta es: {PreguntaActual.RespuestaCorrecta}";
            }

            
            ExplicacionFeedback = string.IsNullOrEmpty(PreguntaActual.Explicacion)
                ? "No hay explicación disponible para esta pregunta."
                : PreguntaActual.Explicacion;

           
            MostrarFeedback = true;

            System.Diagnostics.Debug.WriteLine("Feedback mostrado");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en ResponderAsync: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            await Shell.Current.DisplayAlert("Error", "Ocurrió un error al procesar tu respuesta.", "OK");
            BotonesHabilitados = true;
        }
    }

    [RelayCommand]
    public void SiguientePregunta()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine("Avanzando a siguiente pregunta");
            _indicePreguntaActual++;
            CargarSiguientePregunta();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en SiguientePregunta: {ex.Message}");
        }
    }

    private async void FinalizarQuiz()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"Finalizando quiz - Aciertos: {_aciertos}/{_preguntasDelQuiz.Count}");

            //Ahora pasamos también el tema para poder repetir el quiz
            await Shell.Current.GoToAsync($"ResultadoQuiz?aciertos={_aciertos}&total={_preguntasDelQuiz.Count}&tema={TemaActual}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en FinalizarQuiz: {ex.Message}");
        }
    }

    // --- COMANDOS DEL MENÚ ---
    [RelayCommand]
    public void AbrirMenu()
    {
        System.Diagnostics.Debug.WriteLine("Abriendo menú");
        MenuVisible = true;
    }

    [RelayCommand]
    public void CerrarMenu()
    {
        System.Diagnostics.Debug.WriteLine("Cerrando menú");
        MenuVisible = false;
    }

    [RelayCommand]
    public async Task SalirAlMenuAsync()
    {
        MenuVisible = false;
        bool confirmar = await Shell.Current.DisplayAlert(
            "Salir",
            "¿Estás seguro? Perderás el progreso actual.",
            "Sí, salir",
            "Cancelar");

        if (confirmar)
        {
            try
            {
                _audioService.DetenerMusicaFondo();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deteniendo música: {ex.Message}");
            }

            await Shell.Current.GoToAsync("//MenuPrincipal");
        }
    }

    partial void OnIsMusicEnabledChanged(bool value)
    {
        Preferences.Set("MusicEnabled", value);
        try
        {
            _audioService.ActualizarEstadoMusica(value);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error actualizando música: {ex.Message}");
        }
    }

    partial void OnIsSoundEffectsEnabledChanged(bool value)
    {
        Preferences.Set("SoundEnabled", value);
        if (value)
        {
            try
            {
                _audioService.ReproducirEfectoAsync("acierto.ogg");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reproduciendo efecto prueba: {ex.Message}");
            }
        }
    }

    public void DetenerMusicaAlSalir()
    {
        try
        {
            _audioService.DetenerMusicaFondo();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en DetenerMusicaAlSalir: {ex.Message}");
        }
    }
}