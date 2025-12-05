// Views/FlashcardPage.xaml.cs
using SoftwareEngineeringQuizApp.ViewModels;

namespace SoftwareEngineeringQuizApp.Views;

public partial class FlashcardPage : ContentPage
{
    public FlashcardPage(FlashcardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}