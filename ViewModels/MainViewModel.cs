using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ViewModels/MainViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SoftwareEngineeringQuizApp.Views;

namespace SoftwareEngineeringQuizApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task GoToQuiz()
        {
            // Enviamos el modo "Quiz" como parámetro
            await Shell.Current.GoToAsync($"///TopicSelectionPage?Mode=Quiz");
        }

        [RelayCommand]
        private async Task GoToFlashcards()
        {
            // Enviamos el modo "Flashcards" como parámetro
            await Shell.Current.GoToAsync($"///TopicSelectionPage?Mode=Flashcards");
        }
    }
}
