using SoftwareEngineeringQuizApp.ViewModels;

namespace SoftwareEngineeringQuizApp.Views;

public partial class ResultadoQuizPage : ContentPage
{
    public ResultadoQuizPage(ResultadoQuizViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    // --- INTERCEPTAR EL BOTÓN ATRÁS (FÍSICO) ---
    protected override bool OnBackButtonPressed()
    {
        // Navegamos directamente al Menú Principal
        // Usamos "//" para reiniciar la pila de navegación (limpiar historial)
        Shell.Current.GoToAsync("//MenuPrincipal");

        // Retornamos TRUE para decirle al sistema:
        // "Yo ya manejé la navegación, no hagas la acción por defecto".
        return true;
    }
}