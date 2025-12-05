using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SoftwareEngineeringQuizApp.Models;
using SoftwareEngineeringQuizApp.Repositories; // ✅ NUEVO USING

namespace SoftwareEngineeringQuizApp.ViewModels
{
    [QueryProperty(nameof(TopicId), "TopicId")]
    public partial class FlashcardViewModel : ObservableObject
    {
        private readonly ICardRepository _cardRepository; // ✅ CAMBIADO
        private List<Card> _allCards = new();
        private int _currentIndex = 0;

        [ObservableProperty]
        private int _topicId;

        [ObservableProperty]
        private string _cardFrontText = string.Empty;

        [ObservableProperty]
        private string _cardBackText = string.Empty;

        [ObservableProperty]
        private bool _isFlipped;

        [ObservableProperty]
        private string _progressText = string.Empty;

        [ObservableProperty]
        private bool _isLoading;

        // ✅ INYECCIÓN DEL REPOSITORY
        public FlashcardViewModel(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        async partial void OnTopicIdChanged(int value)
        {
            await LoadCardsAsync();
        }

        private async Task LoadCardsAsync()
        {
            IsLoading = true;

            try
            {
                // ✅ USANDO EL REPOSITORY
                _allCards = await _cardRepository.GetCardsByTopicAsync(TopicId);

                if (_allCards == null || !_allCards.Any())
                {
                    await Shell.Current.DisplayAlert(
                        "Sin Flashcards",
                        "No hay flashcards disponibles para este tema.",
                        "OK");
                    await Shell.Current.GoToAsync("///MainPage");
                    return;
                }

                LoadCurrentCard();
                NextCardCommand.NotifyCanExecuteChanged();
                PreviousCardCommand.NotifyCanExecuteChanged();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en LoadCardsAsync: {ex.Message}");
                await Shell.Current.DisplayAlert(
                    "Error",
                    "No se pudieron cargar las flashcards. Por favor, intenta nuevamente.",
                    "OK");
                await Shell.Current.GoToAsync("///MainPage");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void LoadCurrentCard()
        {
            if (_allCards != null && _allCards.Any())
            {
                var card = _allCards[_currentIndex];
                CardFrontText = card.QuestionText;
                CardBackText = card.AnswerText;
                IsFlipped = false;
                ProgressText = $"Tarjeta {_currentIndex + 1} de {_allCards.Count}";
            }
        }

        [RelayCommand]
        private void FlipCard()
        {
            IsFlipped = !IsFlipped;
        }

        [RelayCommand(CanExecute = nameof(CanGoNext))]
        private void NextCard()
        {
            _currentIndex++;
            LoadCurrentCard();
            NextCardCommand.NotifyCanExecuteChanged();
            PreviousCardCommand.NotifyCanExecuteChanged();
        }

        private bool CanGoNext()
        {
            return _allCards != null && _currentIndex < _allCards.Count - 1;
        }

        [RelayCommand(CanExecute = nameof(CanGoPrevious))]
        private void PreviousCard()
        {
            _currentIndex--;
            LoadCurrentCard();
            NextCardCommand.NotifyCanExecuteChanged();
            PreviousCardCommand.NotifyCanExecuteChanged();
        }

        private bool CanGoPrevious()
        {
            return _currentIndex > 0;
        }
    }
}