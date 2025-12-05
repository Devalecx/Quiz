// Views/TopicSelectionPage.xaml.cs
using SoftwareEngineeringQuizApp.ViewModels;

namespace SoftwareEngineeringQuizApp.Views;

public partial class TopicSelectionPage : ContentPage
{
    // Este es un campo PRIVADO para guardar una referencia al ViewModel.
    // Su nombre (_viewModel) es DIFERENTE al de la clase (TopicSelectionPage).
    private readonly TopicSelectionViewModel _viewModel;

    // Este es el CONSTRUCTOR. Su nombre DEBE coincidir con el de la clase.
    // Aquí es donde probablemente estaba el error.
    public TopicSelectionPage(TopicSelectionViewModel viewModel)
    {
        // Esta llamada es crucial y debe estar aquí dentro del constructor.
        // Soluciona el error CS0120.
        InitializeComponent();

        // Asignamos el ViewModel al BindingContext para que el XAML funcione.
        BindingContext = viewModel;
        _viewModel = viewModel; // Guardamos la referencia para usarla en OnAppearing.
    }

    // Se ejecuta cada vez que la página aparece en pantalla.
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadTopicsAsync();
    }
}
