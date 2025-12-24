using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SoftwareEngineeringQuizApp.Models;
using SoftwareEngineeringQuizApp.Services;

namespace SoftwareEngineeringQuizApp.ViewModels;

public partial class FlashcardViewerViewModel : ObservableObject, IQueryAttributable
{
    private readonly RepositorioBaseDatos _repositorio;
    private List<TarjetaFlashcard> _flashcardsDelMazo = new();
    private int _indiceActual = 0;

    // Propiedades Observables
    [ObservableProperty]
    private string temaActual;

    [ObservableProperty]
    private string textoMostrar; // El texto que se ve actualmente (Anverso o Reverso)

    [ObservableProperty]
    private string textoIndicador; // "Anverso" o "Reverso" para guiar al usuario

    [ObservableProperty]
    private string textoProgreso; // "1 / 10"

    [ObservableProperty]
    private bool estaCargando;

    // Estado interno para saber qué lado estamos viendo
    private bool _esAnverso = true;

    // Propiedad para desactivar botones si no hay anterior/siguiente
    [ObservableProperty]
    private bool puedeAvanzar;

    [ObservableProperty]
    private bool puedeRetroceder;

    public FlashcardViewerViewModel(RepositorioBaseDatos repositorio)
    {
        _repositorio = repositorio;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("tema"))
        {
            TemaActual = query["tema"].ToString();
            CargarFlashcards();
        }
    }

    private async void CargarFlashcards()
    {
        EstaCargando = true;
        try
        {
            var datos = await _repositorio.ObtenerFlashcardsPorTema(TemaActual);
            // Mezclamos un poco para variar el estudio
            _flashcardsDelMazo = datos.OrderBy(x => Guid.NewGuid()).ToList();

            _indiceActual = 0;

            if (_flashcardsDelMazo.Any())
            {
                MostrarTarjetaActual(ladoAnverso: true);
            }
            else
            {
                TextoMostrar = "No hay tarjetas para este tema.";
                PuedeAvanzar = false;
                PuedeRetroceder = false;
            }
        }
        finally
        {
            EstaCargando = false;
        }
    }

    // Método auxiliar para actualizar la UI
    private void MostrarTarjetaActual(bool ladoAnverso)
    {
        if (!_flashcardsDelMazo.Any()) return;

        var carta = _flashcardsDelMazo[_indiceActual];
        _esAnverso = ladoAnverso;

        TextoMostrar = _esAnverso ? carta.Anverso : carta.Reverso;
        TextoIndicador = _esAnverso ? "ANVERSO (Pregunta)" : "REVERSO (Respuesta)";

        TextoProgreso = $"{_indiceActual + 1} / {_flashcardsDelMazo.Count}";

        // Actualizar estado de botones
        PuedeRetroceder = _indiceActual > 0;
        PuedeAvanzar = _indiceActual < _flashcardsDelMazo.Count - 1;
    }

    /// <summary>
    /// Cambia el texto entre anverso y reverso.
    /// Este método será llamado a la mitad de la animación desde la Vista.
    /// </summary>
    public void IntercambiarLado()
    {
        MostrarTarjetaActual(!_esAnverso);
    }

    [RelayCommand]
    public void Siguiente()
    {
        if (_indiceActual < _flashcardsDelMazo.Count - 1)
        {
            _indiceActual++;
            MostrarTarjetaActual(ladoAnverso: true); // Al cambiar, siempre mostrar anverso primero
        }
    }

    [RelayCommand]
    public void Anterior()
    {
        if (_indiceActual > 0)
        {
            _indiceActual--;
            MostrarTarjetaActual(ladoAnverso: true);
        }
    }
}