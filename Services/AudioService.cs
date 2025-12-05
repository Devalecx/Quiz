using Plugin.Maui.Audio;

namespace SoftwareEngineeringQuizApp.Services;

public class AudioService
{
    private readonly IAudioManager _audioManager;
    private IAudioPlayer? _reproductorMusica;

    public AudioService(IAudioManager audioManager)
    {
        _audioManager = audioManager;
    }

    // Reproduce efectos cortos (Acierto, Error, Flip)
    public async Task ReproducirEfectoAsync(string nombreArchivo)
    {
        // Verificar si los efectos están activados en la configuración
        bool efectosActivados = Preferences.Get("SoundEnabled", true);
        if (!efectosActivados) return;

        try
        {
            // CORRECCIÓN: Separamos la apertura del archivo de la creación del player
            var audioStream = await FileSystem.OpenAppPackageFileAsync(nombreArchivo);

            // El método se llama 'CreatePlayer' (sin Async y sin await)
            var player = _audioManager.CreatePlayer(audioStream);

            player.Play();

            // Limpieza de memoria al terminar
            player.PlaybackEnded += (s, e) => { player.Dispose(); };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al reproducir efecto: {ex.Message}");
        }
    }

    // Inicia la música de fondo en bucle
    public async Task IniciarMusicaFondoAsync(string nombreArchivo)
    {
        bool musicaActivada = Preferences.Get("MusicEnabled", true);
        if (!musicaActivada) return;

        // Si ya está sonando, no hacemos nada para evitar doble música
        if (_reproductorMusica != null && _reproductorMusica.IsPlaying) return;

        try
        {
            // CORRECCIÓN: Separamos la apertura del archivo
            var audioStream = await FileSystem.OpenAppPackageFileAsync(nombreArchivo);

            // El método se llama 'CreatePlayer' (sin Async y sin await)
            _reproductorMusica = _audioManager.CreatePlayer(audioStream);

            _reproductorMusica.Loop = true; // Repetir infinitamente
            _reproductorMusica.Volume = 0.5; // Volumen medio
            _reproductorMusica.Play();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error música fondo: {ex.Message}");
        }
    }

    // Detiene la música (al salir del quiz)
    public void DetenerMusicaFondo()
    {
        _reproductorMusica?.Stop();
        _reproductorMusica?.Dispose();
        _reproductorMusica = null;
    }

    // Controla el interruptor de música en tiempo real
    public async Task ActualizarEstadoMusica(bool encender)
    {
        if (encender)
        {
            await IniciarMusicaFondoAsync("pista_3.ogg");
        }
        else
        {
            DetenerMusicaFondo();
        }
    }
}