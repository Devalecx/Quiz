using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SoftwareEngineeringQuizApp.Models;
using SoftwareEngineeringQuizApp.Services;

namespace SoftwareEngineeringQuizApp.ViewModels;

public partial class FlashcardViewerViewModel : ObservableObject, IQueryAttributable
{
    private readonly RepositorioBaseDatos _repositorio;
    private readonly AudioService _audioService; // Inyección

    private List<TarjetaFlashcard> _flashcardsDelMazo = new();
    private int _indiceActual = 0;

    [ObservableProperty] private string temaActual;
    [ObservableProperty] private string textoMostrar;
    [ObservableProperty] private string textoIndicador;
    [ObservableProperty] private string textoProgreso;
    [ObservableProperty] private bool estaCargando;
    [ObservableProperty] private bool puedeAvanzar;
    [ObservableProperty] private bool puedeRetroceder;
    private bool _esAnverso = true;

    // Menú
    [ObservableProperty] private bool menuVisible;
    [ObservableProperty] private bool isMusicEnabled = Preferences.Get("MusicEnabled", true);
    [ObservableProperty] private bool isSoundEffectsEnabled = Preferences.Get("SoundEnabled", true);

    public FlashcardViewerViewModel(RepositorioBaseDatos repositorio, AudioService audioService)
    {
        _repositorio = repositorio;
        _audioService = audioService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("tema"))
        {
            TemaActual = query["tema"].ToString();
            CargarFlashcards();

            // Iniciar música relajante
            _audioService.IniciarMusicaFondoAsync("pista_3.ogg");
        }
    }

    private async void CargarFlashcards()
    {
        EstaCargando = true;
        try
        {
            var datos = await _repositorio.ObtenerFlashcardsPorTema(TemaActual);
            _flashcardsDelMazo = datos.OrderBy(x => Guid.NewGuid()).ToList();
            _indiceActual = 0;

            if (_flashcardsDelMazo.Any()) MostrarTarjetaActual(ladoAnverso: true);
            else TextoMostrar = "No hay tarjetas.";
        }
        finally { EstaCargando = false; }
    }

    private void MostrarTarjetaActual(bool ladoAnverso)
    {
        if (!_flashcardsDelMazo.Any()) return;
        var carta = _flashcardsDelMazo[_indiceActual];
        _esAnverso = ladoAnverso;
        TextoMostrar = _esAnverso ? carta.Anverso : carta.Reverso;
        TextoIndicador = _esAnverso ? "ANVERSO (Pregunta)" : "REVERSO (Respuesta)";

        // CAMBIO AQUÍ: Formato de texto más amigable "Tarjeta 1 de 5"
        TextoProgreso = $"Tarjeta {_indiceActual + 1} de {_flashcardsDelMazo.Count}";

        PuedeRetroceder = _indiceActual > 0;
        PuedeAvanzar = _indiceActual < _flashcardsDelMazo.Count - 1;
    }

    // Método público para llamar desde la Vista cuando gira la tarjeta
    public void ReproducirSonidoFlip()
    {
        _audioService.ReproducirEfectoAsync("flip.mp3");
    }

    public void IntercambiarLado() => MostrarTarjetaActual(!_esAnverso);

    [RelayCommand]
    public void Siguiente()
    {
        if (_indiceActual < _flashcardsDelMazo.Count - 1)
        {
            _indiceActual++;
            MostrarTarjetaActual(ladoAnverso: true);
            _audioService.ReproducirEfectoAsync("flip.mp3");
        }
    }

    [RelayCommand]
    public void Anterior()
    {
        if (_indiceActual > 0)
        {
            _indiceActual--;
            MostrarTarjetaActual(ladoAnverso: true);
            _audioService.ReproducirEfectoAsync("flip.mp3");
        }
    }

    // --- COMANDOS MENÚ ---
    [RelayCommand] public void AbrirMenu() => MenuVisible = true;
    [RelayCommand] public void CerrarMenu() => MenuVisible = false;

    [RelayCommand]
    public async Task SalirAlMenuAsync()
    {
        MenuVisible = false;
        _audioService.DetenerMusicaFondo();
        await Shell.Current.GoToAsync("//MenuPrincipal");
    }

    partial void OnIsMusicEnabledChanged(bool value)
    {
        Preferences.Set("MusicEnabled", value);
        _audioService.ActualizarEstadoMusica(value);
    }

    partial void OnIsSoundEffectsEnabledChanged(bool value)
    {
        Preferences.Set("SoundEnabled", value);
        if (value) _audioService.ReproducirEfectoAsync("flip.mp3");
    }

    // Método público para limpiar recursos al salir
    public void DetenerMusicaAlSalir()
    {
        _audioService.DetenerMusicaFondo();
    }
}