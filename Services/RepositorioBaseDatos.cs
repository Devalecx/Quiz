using SQLite;
using SoftwareEngineeringQuizApp.Models;
using SoftwareEngineeringQuizApp.Data;

namespace SoftwareEngineeringQuizApp.Services;

public class RepositorioBaseDatos
{
    private SQLiteAsyncConnection _database;

    public RepositorioBaseDatos()
    {
    }

    private async Task Init()
    {
        if (_database is not null)
            return;

        _database = new SQLiteAsyncConnection(ConstantesBaseDatos.RutaBaseDatos, ConstantesBaseDatos.Flags);

        await _database.CreateTableAsync<PreguntaQuiz>();
        await _database.CreateTableAsync<TarjetaFlashcard>();

        await SembrarDatosIniciales();
    }

    /// <summary>
    /// ✅ ACTUALIZADO: Preguntas con explicaciones educativas
    /// </summary>
    private async Task SembrarDatosIniciales()
    {
        var conteo = await _database.Table<PreguntaQuiz>().CountAsync();

        if (conteo == 0)
        {
            var preguntas = new List<PreguntaQuiz>
            {
                // Tema: Arquitectura
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué significa la 'S' en SOLID?",
                    Opciones = new List<string> { "Single Responsibility", "Simple Object", "Static Typing", "Safe Mode" },
                    RespuestaCorrecta = "Single Responsibility",
                    Explicacion = "El principio de Responsabilidad Única (Single Responsibility) establece que una clase debe tener una única razón para cambiar, es decir, debe tener una sola responsabilidad bien definida."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué patrón arquitectónico separa la UI de la lógica de negocio usando ViewModels?",
                    Opciones = new List<string> { "MVC", "MVVM", "MVP", "Singleton" },
                    RespuestaCorrecta = "MVVM",
                    Explicacion = "MVVM (Model-View-ViewModel) es un patrón que permite la separación clara entre la interfaz de usuario (View) y la lógica de negocio (Model) mediante un intermediario (ViewModel) que facilita el data binding."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "En Clean Architecture, ¿qué capa no debe tener dependencias externas?",
                    Opciones = new List<string> { "Infraestructura", "Presentación", "Dominio", "Persistencia" },
                    RespuestaCorrecta = "Dominio",
                    Explicacion = "La capa de Dominio es el núcleo de la aplicación y debe ser completamente independiente de frameworks, bases de datos o UI. Esto garantiza que la lógica de negocio sea portable y testeable."
                },

                // Tema: C# & .NET
                new PreguntaQuiz
                {
                    Tema = "C# .NET",
                    Pregunta = "¿Qué palabra clave hace que un método se ejecute en otro hilo?",
                    Opciones = new List<string> { "void", "async/await", "static", "public" },
                    RespuestaCorrecta = "async/await",
                    Explicacion = "async/await es el patrón moderno en C# para programación asíncrona. 'async' marca el método como asíncrono y 'await' pausa la ejecución sin bloquear el hilo, permitiendo que otras operaciones continúen."
                },
                new PreguntaQuiz
                {
                    Tema = "C# .NET",
                    Pregunta = "¿Cuál es la clase base de todos los tipos en .NET?",
                    Opciones = new List<string> { "System.Object", "System.Type", "System.Base", "System.Root" },
                    RespuestaCorrecta = "System.Object",
                    Explicacion = "System.Object es la clase raíz de la jerarquía de tipos en .NET. Todos los tipos (incluyendo tipos por valor, referencia e interfaces) heredan implícita o explícitamente de Object."
                },
                new PreguntaQuiz
                {
                    Tema = "C# .NET",
                    Pregunta = "¿Qué colección almacena pares clave-valor únicos?",
                    Opciones = new List<string> { "List", "Array", "Dictionary", "Queue" },
                    RespuestaCorrecta = "Dictionary",
                    Explicacion = "Dictionary<TKey, TValue> es una colección que almacena pares clave-valor donde cada clave debe ser única. Proporciona búsqueda rápida O(1) usando hash tables."
                },

                // Tema: Testing
                new PreguntaQuiz
                {
                    Tema = "Testing",
                    Pregunta = "¿Qué técnica de prueba evalúa unidades individuales de código?",
                    Opciones = new List<string> { "Pruebas de Integración", "Pruebas Unitarias", "Pruebas E2E", "Pruebas de Estrés" },
                    RespuestaCorrecta = "Pruebas Unitarias",
                    Explicacion = "Las Pruebas Unitarias (Unit Tests) verifican el comportamiento de componentes individuales (métodos, clases) de forma aislada, utilizando mocks o stubs para las dependencias."
                },
                new PreguntaQuiz
                {
                    Tema = "Testing",
                    Pregunta = "¿Qué patrón se usa comúnmente para estructurar tests?",
                    Opciones = new List<string> { "AAA (Arrange-Act-Assert)", "MVC", "SOLID", "KISS" },
                    RespuestaCorrecta = "AAA (Arrange-Act-Assert)",
                    Explicacion = "El patrón AAA organiza los tests en tres fases: Arrange (preparar datos), Act (ejecutar acción) y Assert (verificar resultado). Esto hace los tests más legibles y mantenibles."
                },

                // Tema: Patrones
                new PreguntaQuiz
                {
                    Tema = "Patrones",
                    Pregunta = "¿Qué patrón garantiza que una clase tenga una única instancia?",
                    Opciones = new List<string> { "Factory", "Singleton", "Observer", "Strategy" },
                    RespuestaCorrecta = "Singleton",
                    Explicacion = "El patrón Singleton asegura que una clase tenga solo una instancia y proporciona un punto de acceso global a ella. Útil para gestores de recursos compartidos."
                },
                new PreguntaQuiz
                {
                    Tema = "Patrones",
                    Pregunta = "¿Qué patrón permite cambiar el algoritmo que usa un objeto en tiempo de ejecución?",
                    Opciones = new List<string> { "Singleton", "Factory", "Strategy", "Adapter" },
                    RespuestaCorrecta = "Strategy",
                    Explicacion = "El patrón Strategy define una familia de algoritmos, los encapsula y los hace intercambiables. Permite que el algoritmo varíe independientemente de los clientes que lo utilizan."
                },

                // Tema: Web
                new PreguntaQuiz
                {
                    Tema = "Web",
                    Pregunta = "¿Qué significa REST en arquitectura web?",
                    Opciones = new List<string> { "Remote Execution Service Tool", "Representational State Transfer", "Resource Encryption Standard", "Reliable External Service" },
                    RespuestaCorrecta = "Representational State Transfer",
                    Explicacion = "REST es un estilo arquitectónico que utiliza HTTP y sus verbos (GET, POST, PUT, DELETE) para crear servicios web escalables, stateless y basados en recursos."
                },
                new PreguntaQuiz
                {
                    Tema = "Web",
                    Pregunta = "¿Qué es JWT?",
                    Opciones = new List<string> { "Java Web Toolkit", "JSON Web Token", "JavaScript Web Template", "Joint Web Transfer" },
                    RespuestaCorrecta = "JSON Web Token",
                    Explicacion = "JWT es un estándar abierto (RFC 7519) para crear tokens de acceso que permiten la transmisión segura de información entre partes como un objeto JSON."
                },

                // Tema: DevOps
                new PreguntaQuiz
                {
                    Tema = "DevOps",
                    Pregunta = "¿Qué es Docker?",
                    Opciones = new List<string> { "Un lenguaje de programación", "Una plataforma de contenedores", "Un framework web", "Una base de datos" },
                    RespuestaCorrecta = "Una plataforma de contenedores",
                    Explicacion = "Docker es una plataforma que permite empaquetar aplicaciones y sus dependencias en contenedores ligeros y portables, garantizando que funcionen de manera consistente en cualquier entorno."
                },
                new PreguntaQuiz
                {
                    Tema = "DevOps",
                    Pregunta = "¿Qué significa CI/CD?",
                    Opciones = new List<string> { "Code Integration/Code Deployment", "Continuous Integration/Continuous Delivery", "Central Infrastructure/Cloud Distribution", "Certified Implementation/Certified Development" },
                    RespuestaCorrecta = "Continuous Integration/Continuous Delivery",
                    Explicacion = "CI/CD son prácticas de DevOps donde CI automatiza la integración de código y CD automatiza el despliegue, permitiendo entregas rápidas y confiables."
                }
            };
            await _database.InsertAllAsync(preguntas);

            // --- FLASHCARDS ---
            var flashcards = new List<TarjetaFlashcard>
            {
                new TarjetaFlashcard { Tema = "Patrones", Anverso = "Singleton", Reverso = "Garantiza que una clase tenga una única instancia y proporciona un punto de acceso global a ella." },
                new TarjetaFlashcard { Tema = "Patrones", Anverso = "Factory Method", Reverso = "Define una interfaz para crear un objeto, pero deja que las subclases decidan qué clase instanciar." },
                new TarjetaFlashcard { Tema = "Patrones", Anverso = "Observer", Reverso = "Define una dependencia uno-a-muchos entre objetos, de modo que cuando uno cambia de estado, notifica a los demás." },
                new TarjetaFlashcard { Tema = "Web", Anverso = "REST", Reverso = "Representational State Transfer. Estilo arquitectónico para sistemas hipermedia distribuidos." },
                new TarjetaFlashcard { Tema = "Web", Anverso = "JWT", Reverso = "JSON Web Token. Estándar para crear tokens de acceso seguros y compactos." },
                new TarjetaFlashcard { Tema = "DevOps", Anverso = "Docker", Reverso = "Plataforma para desarrollar, enviar y ejecutar aplicaciones dentro de contenedores." },
                new TarjetaFlashcard { Tema = "DevOps", Anverso = "CI/CD", Reverso = "Integración Continua y Entrega Continua." }
            };
            await _database.InsertAllAsync(flashcards);
        }
    }

    // --- MÉTODOS PÚBLICOS ---

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

    public async Task<List<string>> ObtenerTemasDisponibles()
    {
        await Init();
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