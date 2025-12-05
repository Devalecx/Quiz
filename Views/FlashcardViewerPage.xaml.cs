using SoftwareEngineeringQuizApp.ViewModels;

namespace SoftwareEngineeringQuizApp.Views;

public partial class FlashcardViewerPage : ContentPage
{
    private readonly FlashcardViewerViewModel _vm;

    public FlashcardViewerPage(FlashcardViewerViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Lógica de carga adicional si fuera necesaria
    }

    // ESTE ES EL NUEVO MÉTODO IMPORTANTE
    // Se ejecuta automáticamente al salir de la pantalla (Botón Atrás, Gesto, Menú, etc.)
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Detenemos la música para que no siga sonando en el menú principal
        _vm.DetenerMusicaAlSalir();
    }

    // Lógica de Animación + Sonido
    private async void OnVoltearClicked(object sender, EventArgs e)
    {
        // 1. Iniciar rotación (mitad)
        await TarjetaFrame.RotateYTo(90, 200, Easing.CubicIn);

        // 2. Reproducir SONIDO y cambiar TEXTO
        _vm.ReproducirSonidoFlip();
        _vm.IntercambiarLado();

        // 3. Terminar rotación
        TarjetaFrame.RotationY = -90;
        await TarjetaFrame.RotateYTo(0, 200, Easing.CubicOut);
    }
}