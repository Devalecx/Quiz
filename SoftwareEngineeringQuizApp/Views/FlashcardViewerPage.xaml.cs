using SoftwareEngineeringQuizApp.ViewModels;

namespace SoftwareEngineeringQuizApp.Views;

public partial class FlashcardViewerPage : ContentPage
{
    public FlashcardViewerPage(FlashcardViewerViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    // Lógica de Animación (View-Specific Logic)
    private async void OnVoltearClicked(object sender, EventArgs e)
    {
        // 1. Primera mitad de la animación: Rotar a 90 grados
        await TarjetaFrame.RotateYTo(90, 200, Easing.CubicIn);

        // 2. Cambiar el dato en el ViewModel (Anverso <-> Reverso)
        if (BindingContext is FlashcardViewerViewModel vm)
        {
            vm.IntercambiarLado();
        }

        // 3. (Opcional) Resetear rotación para efecto "continuo" o simplemente volver
        TarjetaFrame.RotationY = -90;

        // 4. Segunda mitad: Rotar de -90 a 0 grados
        await TarjetaFrame.RotateYTo(0, 200, Easing.CubicOut);
    }
}