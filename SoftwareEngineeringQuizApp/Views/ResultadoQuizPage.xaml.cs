using SoftwareEngineeringQuizApp.ViewModels;

namespace SoftwareEngineeringQuizApp.Views;

public partial class ResultadoQuizPage : ContentPage
{
    public ResultadoQuizPage(ResultadoQuizViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}