using SoftwareEngineeringQuizApp.ViewModels;

namespace SoftwareEngineeringQuizApp.Views;

public partial class ListaTemasFlashcardsPage : ContentPage
{
    public ListaTemasFlashcardsPage(ListaTemasFlashcardsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}