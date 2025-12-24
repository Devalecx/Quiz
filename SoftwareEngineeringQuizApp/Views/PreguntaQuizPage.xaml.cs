using SoftwareEngineeringQuizApp.ViewModels;

namespace SoftwareEngineeringQuizApp.Views;

public partial class PreguntaQuizPage : ContentPage
{
    public PreguntaQuizPage(PreguntaQuizViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}