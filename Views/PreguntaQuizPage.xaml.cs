using SoftwareEngineeringQuizApp.ViewModels;

namespace SoftwareEngineeringQuizApp.Views;

public partial class PreguntaQuizPage : ContentPage
{
    private readonly PreguntaQuizViewModel _viewModel;

    public PreguntaQuizPage(PreguntaQuizViewModel vm)
    {
        InitializeComponent();
        _viewModel = vm;
        BindingContext = vm;

        // Suscribirse a cambios del ViewModel para actualizar colores de la UI
        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    // --- LÓGICA DE COLORES Y FEEDBACK VISUAL ---

    private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        // Cuando se activa MostrarFeedback (el usuario respondió)
        if (e.PropertyName == nameof(_viewModel.MostrarFeedback) && _viewModel.MostrarFeedback)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ActualizarColoresYOcultarBotones();
            });
        }
        // Cuando cambia la pregunta (se resetea para la siguiente)
        else if (e.PropertyName == nameof(_viewModel.PreguntaActual))
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ResetearColoresBotones();
            });
        }
    }

    private void ActualizarColoresYOcultarBotones()
    {
        try
        {
            if (_viewModel.PreguntaActual == null) return;

            // 1. Buscamos el contenedor de botones en el árbol visual
            if (Content is not Grid mainGrid) return;

            ScrollView? scrollView = null;
            foreach (var child in mainGrid.Children)
            {
                if (child is ScrollView sv) { scrollView = sv; break; }
            }

            if (scrollView?.Content is not VerticalStackLayout mainStack) return;

            StackLayout? opcionesStack = null;
            foreach (var child in mainStack.Children)
            {
                if (child is StackLayout sl) { opcionesStack = sl; break; }
            }

            if (opcionesStack == null) return;

            // 2. Iteramos los botones para pintarlos
            foreach (var child in opcionesStack.Children)
            {
                if (child is Button button)
                {
                    var opcion = button.Text;

                    // A. Es la respuesta CORRECTA -> Verde
                    if (opcion == _viewModel.PreguntaActual.RespuestaCorrecta)
                    {
                        button.BackgroundColor = Color.FromArgb("#4CAF50"); // Verde Material
                        button.TextColor = Colors.White;
                        button.IsVisible = true; // Siempre mostrar la correcta
                    }
                    // B. Es la respuesta INCORRECTA que seleccionó el usuario -> Rojo
                    else if (opcion == _viewModel.RespuestaSeleccionada && !_viewModel.EsRespuestaCorrecta)
                    {
                        button.BackgroundColor = Color.FromArgb("#F44336"); // Rojo Material
                        button.TextColor = Colors.White;
                        button.IsVisible = true;
                    }
                    // C. Otras opciones incorrectas -> Ocultar para limpiar la vista
                    else
                    {
                        button.IsVisible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error pintando botones: {ex.Message}");
        }
    }

    private void ResetearColoresBotones()
    {
        try
        {
            // Búsqueda del contenedor (repetimos lógica para asegurar referencia fresca)
            if (Content is not Grid mainGrid) return;

            ScrollView? scrollView = null;
            foreach (var child in mainGrid.Children)
            {
                if (child is ScrollView sv) { scrollView = sv; break; }
            }

            if (scrollView?.Content is not VerticalStackLayout mainStack) return;

            StackLayout? opcionesStack = null;
            foreach (var child in mainStack.Children)
            {
                if (child is StackLayout sl) { opcionesStack = sl; break; }
            }

            if (opcionesStack == null) return;

            // Restaurar estado original (Blanco con texto Morado)
            foreach (var child in opcionesStack.Children)
            {
                if (child is Button button)
                {
                    button.BackgroundColor = Colors.White;
                    button.TextColor = Color.FromArgb("#520D60"); // Tu morado oscuro
                    button.IsVisible = true;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error reseteando botones: {ex.Message}");
        }
    }

    // --- INTERCEPCIÓN DE NAVEGACIÓN Y SALIDA ---

    protected override bool OnBackButtonPressed()
    {
        Dispatcher.Dispatch(async () =>
        {
            bool confirmar = await DisplayAlert(
                "Salir",
                "¿Estás seguro? Perderás el progreso actual.",
                "Sí, salir",
                "Cancelar");

            if (confirmar)
            {
                _viewModel.DetenerMusicaAlSalir();
                await Shell.Current.GoToAsync("..");
            }
        });

        return true; // Cancelar comportamiento por defecto
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // 1. Desuscribirse del evento para evitar fugas de memoria
        if (_viewModel != null)
        {
            _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }

        // 2. Asegurar que la música se detenga
        _viewModel.DetenerMusicaAlSalir();
    }
}