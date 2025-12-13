using SoftwareEngineeringQuizApp.Views;

namespace SoftwareEngineeringQuizApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Rutas del Quiz
        Routing.RegisterRoute("ListaTemasQuiz", typeof(ListaTemasQuizPage));
        Routing.RegisterRoute("PreguntaQuiz", typeof(PreguntaQuizPage));
        Routing.RegisterRoute("ResultadoQuiz", typeof(ResultadoQuizPage));

        // Rutas de Flashcards
        Routing.RegisterRoute("ListaTemasFlashcards", typeof(ListaTemasFlashcardsPage));
        Routing.RegisterRoute("FlashcardViewer", typeof(FlashcardViewerPage));

        // ✅ NUEVA RUTA: Perfil (ya no está en tabs, así que la registramos aquí)
        Routing.RegisterRoute("Perfil", typeof(PerfilPage));
    }
}