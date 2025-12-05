using SoftwareEngineeringQuizApp.Views;

namespace SoftwareEngineeringQuizApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // ---------------------------------------------------------
        // REGISTRO DE RUTAS DE NAVEGACIÓN
        // ---------------------------------------------------------
        // Aquí conectamos el "Nombre de la ruta" (string) con la "Página real" (Type).

        // FASE 3: Rutas del Quiz
        // Cuando el ViewModel llame a "ListaTemasQuiz", abrirá ListaTemasQuizPage
        Routing.RegisterRoute("ListaTemasQuiz", typeof(ListaTemasQuizPage));
        Routing.RegisterRoute("PreguntaQuiz", typeof(PreguntaQuizPage));
        Routing.RegisterRoute("ResultadoQuiz", typeof(ResultadoQuizPage));

        // FASE 4: Rutas de Flashcards
        // Cuando el ViewModel llame a "ListaTemasFlashcards", abrirá ListaTemasFlashcardsPage
        Routing.RegisterRoute("ListaTemasFlashcards", typeof(ListaTemasFlashcardsPage));
        Routing.RegisterRoute("FlashcardViewer", typeof(FlashcardViewerPage));
    }
}