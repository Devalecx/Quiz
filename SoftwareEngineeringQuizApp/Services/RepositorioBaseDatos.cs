using SQLite;
using SoftwareEngineeringQuizApp.Models;
using SoftwareEngineeringQuizApp.Data;

namespace SoftwareEngineeringQuizApp.Services;

public class RepositorioBaseDatos
{
    private SQLiteAsyncConnection _database;

    // Constructor vacío
    public RepositorioBaseDatos()
    {
    }

    // Inicialización "Lazy": Se llama antes de cualquier operación
    private async Task Init()
    {
        if (_database is not null)
            return;

        // Conectamos a la ruta definida en constantes
        _database = new SQLiteAsyncConnection(ConstantesBaseDatos.RutaBaseDatos, ConstantesBaseDatos.Flags);

        // Crea las tablas si no existen
        await _database.CreateTableAsync<PreguntaQuiz>();
        await _database.CreateTableAsync<TarjetaFlashcard>();

        // Verificamos si necesitamos datos iniciales
        await SembrarDatosIniciales();
    }

    /// <summary>
    /// Inserta un conjunto robusto de datos para probar la app.
    /// </summary>
    private async Task SembrarDatosIniciales()
    {
        var conteo = await _database.Table<PreguntaQuiz>().CountAsync();

        if (conteo == 0)
        {
            // --- PREGUNTAS DEL QUIZ ---
            var preguntas = new List<PreguntaQuiz>
            {
                // Tema: Arquitectura
                new PreguntaQuiz { Tema = "Arquitectura", Pregunta = "¿Qué significa la 'S' en SOLID?", Opciones = new List<string> { "Single Responsibility", "Simple Object", "Static Typing", "Safe Mode" }, RespuestaCorrecta = "Single Responsibility" },
                new PreguntaQuiz { Tema = "Arquitectura", Pregunta = "¿Qué patrón arquitectónico separa la UI de la lógica de negocio usando ViewModels?", Opciones = new List<string> { "MVC", "MVVM", "MVP", "Singleton" }, RespuestaCorrecta = "MVVM" },
                new PreguntaQuiz { Tema = "Arquitectura", Pregunta = "En Clean Architecture, ¿qué capa no debe tener dependencias externas?", Opciones = new List<string> { "Infraestructura", "Presentación", "Dominio", "Persistencia" }, RespuestaCorrecta = "Dominio" },

                // Tema: C# & .NET
                new PreguntaQuiz { Tema = "C# .NET", Pregunta = "¿Qué palabra clave hace que un método se ejecute en otro hilo?", Opciones = new List<string> { "void", "async/await", "static", "public" }, RespuestaCorrecta = "async/await" },
                new PreguntaQuiz { Tema = "C# .NET", Pregunta = "¿Cuál es la clase base de todos los tipos en .NET?", Opciones = new List<string> { "System.Object", "System.Type", "System.Base", "System.Root" }, RespuestaCorrecta = "System.Object" },
                new PreguntaQuiz { Tema = "C# .NET", Pregunta = "¿Qué colección almacena pares clave-valor únicos?", Opciones = new List<string> { "List", "Array", "Dictionary", "Queue" }, RespuestaCorrecta = "Dictionary" },

                // Tema: Testing
                new PreguntaQuiz { Tema = "Testing", Pregunta = "¿Qué técnica de prueba evalúa unidades individuales de código?", Opciones = new List<string> { "Pruebas de Integración", "Pruebas Unitarias", "Pruebas E2E", "Pruebas de Estrés" }, RespuestaCorrecta = "Pruebas Unitarias" },
                new PreguntaQuiz { Tema = "Testing", Pregunta = "¿Qué patrón se usa comúnmente para estructurar tests?", Opciones = new List<string> { "AAA (Arrange-Act-Assert)", "MVC", "SOLID", "KISS" }, RespuestaCorrecta = "AAA (Arrange-Act-Assert)" }
            };
            await _database.InsertAllAsync(preguntas);

            // --- FLASHCARDS ---
            var flashcards = new List<TarjetaFlashcard>
            {
                // Tema: Patrones
                new TarjetaFlashcard { Tema = "Patrones", Anverso = "Singleton", Reverso = "Garantiza que una clase tenga una única instancia y proporciona un punto de acceso global a ella." },
                new TarjetaFlashcard { Tema = "Patrones", Anverso = "Factory Method", Reverso = "Define una interfaz para crear un objeto, pero deja que las subclases decidan qué clase instanciar." },
                new TarjetaFlashcard { Tema = "Patrones", Anverso = "Observer", Reverso = "Define una dependencia uno-a-muchos entre objetos, de modo que cuando uno cambia de estado, notifica a los demás." },

                // Tema: Conceptos Web
                new TarjetaFlashcard { Tema = "Web", Anverso = "REST", Reverso = "Representational State Transfer. Estilo arquitectónico para sistemas hipermedia distribuidos." },
                new TarjetaFlashcard { Tema = "Web", Anverso = "JWT", Reverso = "JSON Web Token. Estándar para crear tokens de acceso seguros y compactos." },
                
                // Tema: DevOps
                new TarjetaFlashcard { Tema = "DevOps", Anverso = "Docker", Reverso = "Plataforma para desarrollar, enviar y ejecutar aplicaciones dentro de contenedores." },
                new TarjetaFlashcard { Tema = "DevOps", Anverso = "CI/CD", Reverso = "Integración Continua y Entrega Continua." }
            };
            await _database.InsertAllAsync(flashcards);
        }
    }

    // --- MÉTODOS PÚBLICOS (API del Servicio) ---

    public async Task<List<PreguntaQuiz>> ObtenerPreguntasPorTema(string tema)
    {
        await Init();
        if (string.IsNullOrWhiteSpace(tema) || tema == "Todos")
            return await _database.Table<PreguntaQuiz>().ToListAsync();

        return await _database.Table<PreguntaQuiz>().Where(p => p.Tema == tema).ToListAsync();
    }

    public async Task<List<TarjetaFlashcard>> ObtenerFlashcardsPorTema(string tema)
    {
        await Init();
        if (string.IsNullOrWhiteSpace(tema) || tema == "Todos")
            return await _database.Table<TarjetaFlashcard>().ToListAsync();

        return await _database.Table<TarjetaFlashcard>().Where(f => f.Tema == tema).ToListAsync();
    }

    public async Task InsertarPregunta(PreguntaQuiz pregunta)
    {
        await Init();
        await _database.InsertAsync(pregunta);
    }

    public async Task InsertarFlashcard(TarjetaFlashcard flashcard)
    {
        await Init();
        await _database.InsertAsync(flashcard);
    }

    /// <summary>
    /// Devuelve una lista de temas únicos combinando Preguntas y Flashcards.
    /// Útil para inicializar la base de datos desde App.xaml.cs.
    /// </summary>
    public async Task<List<string>> ObtenerTemasDisponibles()
    {
        await Init();
        // Nota: SQLite no soporta "Select Distinct" directamente en LINQ simple en algunas versiones,
        // así que traemos los datos y filtramos en memoria (seguro para datasets pequeños/medianos).
        var preguntas = await _database.Table<PreguntaQuiz>().ToListAsync();
        var flashcards = await _database.Table<TarjetaFlashcard>().ToListAsync();

        var temas = preguntas.Select(p => p.Tema)
                    .Concat(flashcards.Select(f => f.Tema))
                    .Distinct()
                    .OrderBy(t => t)
                    .ToList();
        return temas;
    }
}