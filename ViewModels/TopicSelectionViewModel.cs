using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SoftwareEngineeringQuizApp.Models;
using SoftwareEngineeringQuizApp.Repositories;

namespace SoftwareEngineeringQuizApp.ViewModels
{
    [QueryProperty(nameof(Mode), "Mode")]
    public partial class TopicSelectionViewModel : ObservableObject
    {
        private readonly ITopicRepository _topicRepository;

        [ObservableProperty]
        private ObservableCollection<Topic> _topics;

        [ObservableProperty]
        private string _mode = string.Empty;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private bool _hasError;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        public TopicSelectionViewModel(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
            _topics = new ObservableCollection<Topic>();

            System.Diagnostics.Debug.WriteLine("TopicSelectionViewModel: Constructor llamado");

            // Cargar topics automáticamente al crear el ViewModel
            _ = LoadTopicsAsync();
        }

        public async Task LoadTopicsAsync()
        {
            System.Diagnostics.Debug.WriteLine("TopicSelectionViewModel: LoadTopicsAsync iniciado");

            // Evitar cargar múltiples veces
            if (Topics.Any())
            {
                System.Diagnostics.Debug.WriteLine($"TopicSelectionViewModel: Ya hay {Topics.Count} temas cargados");
                return;
            }

            IsLoading = true;
            HasError = false;
            ErrorMessage = string.Empty;

            try
            {
                System.Diagnostics.Debug.WriteLine("TopicSelectionViewModel: Obteniendo temas del repositorio...");
                var topicsFromDb = await _topicRepository.GetAllTopicsAsync();

                System.Diagnostics.Debug.WriteLine($"TopicSelectionViewModel: Se obtuvieron {topicsFromDb?.Count ?? 0} temas");

                if (topicsFromDb == null || !topicsFromDb.Any())
                {
                    HasError = true;
                    ErrorMessage = "No hay temas disponibles.";
                    System.Diagnostics.Debug.WriteLine("TopicSelectionViewModel: No se encontraron temas");
                    return;
                }

                foreach (var topic in topicsFromDb)
                {
                    System.Diagnostics.Debug.WriteLine($"TopicSelectionViewModel: Agregando tema: {topic.Name}");
                    Topics.Add(topic);
                }

                System.Diagnostics.Debug.WriteLine($"TopicSelectionViewModel: {Topics.Count} temas cargados exitosamente");
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = "Error al cargar los temas. Por favor, intenta nuevamente.";
                System.Diagnostics.Debug.WriteLine($"TopicSelectionViewModel ERROR: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");

                await Shell.Current.DisplayAlert(
                    "Error",
                    ErrorMessage + $"\n\nDetalle: {ex.Message}",
                    "OK");
            }
            finally
            {
                IsLoading = false;
                System.Diagnostics.Debug.WriteLine($"TopicSelectionViewModel: LoadTopicsAsync finalizado. IsLoading={IsLoading}");
            }
        }

        [RelayCommand]
        private async Task SelectTopic(Topic selectedTopic)
        {
            if (selectedTopic == null) return;

            try
            {
                System.Diagnostics.Debug.WriteLine($"Tema seleccionado: {selectedTopic.Name}, Mode: {Mode}");

                if (Mode == "Quiz")
                {
                    await Shell.Current.GoToAsync($"///QuizPage?TopicId={selectedTopic.Id}");
                }
                else if (Mode == "Flashcards")
                {
                    await Shell.Current.GoToAsync($"///FlashcardPage?TopicId={selectedTopic.Id}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en SelectTopic: {ex.Message}");
                await Shell.Current.DisplayAlert(
                    "Error",
                    "No se pudo navegar a la página solicitada.",
                    "OK");
            }
        }
    }
}