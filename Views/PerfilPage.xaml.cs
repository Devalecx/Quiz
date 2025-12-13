using SoftwareEngineeringQuizApp.ViewModels;

namespace SoftwareEngineeringQuizApp.Views;

public partial class PerfilPage : ContentPage
{
    private readonly PerfilViewModel _vm;

    public PerfilPage(PerfilViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.InicializarDatosCommand.ExecuteAsync(null);
    }
}