using SoftwareEngineeringQuizApp.ViewModels;

namespace SoftwareEngineeringQuizApp.Views;

public partial class MenuPrincipalPage : ContentPage
{
    // Usamos inyección por constructor para obtener el ViewModel del contenedor
    public MenuPrincipalPage(MenuPrincipalViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}