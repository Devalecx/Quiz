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
    /// ✅ BASE DE DATOS COMPLETA: 20 preguntas + 20 flashcards por cada tema (6 temas)
    /// Total: 120 preguntas + 120 flashcards = 240 elementos
    /// </summary>
    private async Task SembrarDatosIniciales()
    {
        var conteo = await _database.Table<PreguntaQuiz>().CountAsync();

        if (conteo == 0)
        {
            var preguntas = new List<PreguntaQuiz>
            {
                #region SQL (20 preguntas)
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué comando SQL se usa para obtener datos de una tabla?",
                    Opciones = new List<string> { "GET", "SELECT", "FETCH", "RETRIEVE" },
                    RespuestaCorrecta = "SELECT",
                    Explicacion = "SELECT es el comando fundamental en SQL para consultar y recuperar datos de una o más tablas."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué tipo de JOIN devuelve todas las filas cuando hay coincidencia en alguna de las tablas?",
                    Opciones = new List<string> { "INNER JOIN", "LEFT JOIN", "RIGHT JOIN", "FULL OUTER JOIN" },
                    RespuestaCorrecta = "FULL OUTER JOIN",
                    Explicacion = "FULL OUTER JOIN combina LEFT y RIGHT JOIN, devolviendo todas las filas de ambas tablas."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué cláusula filtra resultados después de un GROUP BY?",
                    Opciones = new List<string> { "WHERE", "HAVING", "FILTER", "ORDER BY" },
                    RespuestaCorrecta = "HAVING",
                    Explicacion = "HAVING filtra grupos después de GROUP BY, mientras WHERE filtra filas antes de agrupar."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué restricción garantiza que todos los valores en una columna sean únicos?",
                    Opciones = new List<string> { "PRIMARY KEY", "UNIQUE", "NOT NULL", "CHECK" },
                    RespuestaCorrecta = "UNIQUE",
                    Explicacion = "UNIQUE asegura que todos los valores sean diferentes, permitiendo un solo NULL."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué comando elimina todos los datos de una tabla sin eliminar su estructura?",
                    Opciones = new List<string> { "DROP", "DELETE", "TRUNCATE", "REMOVE" },
                    RespuestaCorrecta = "TRUNCATE",
                    Explicacion = "TRUNCATE elimina todos los registros rápidamente sin registrar cada eliminación individual."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué función SQL cuenta el número de filas?",
                    Opciones = new List<string> { "SUM()", "COUNT()", "TOTAL()", "NUMBER()" },
                    RespuestaCorrecta = "COUNT()",
                    Explicacion = "COUNT() devuelve el número de filas que coinciden con un criterio especificado."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué comando SQL crea una nueva tabla?",
                    Opciones = new List<string> { "NEW TABLE", "CREATE TABLE", "MAKE TABLE", "ADD TABLE" },
                    RespuestaCorrecta = "CREATE TABLE",
                    Explicacion = "CREATE TABLE define una nueva tabla con sus columnas y tipos de datos."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué operador SQL se usa para buscar un patrón en una columna?",
                    Opciones = new List<string> { "SEARCH", "LIKE", "MATCH", "FIND" },
                    RespuestaCorrecta = "LIKE",
                    Explicacion = "LIKE permite buscar patrones usando wildcards como % y _."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué hace la cláusula DISTINCT?",
                    Opciones = new List<string> { "Ordena los resultados", "Elimina duplicados", "Agrupa datos", "Cuenta filas" },
                    RespuestaCorrecta = "Elimina duplicados",
                    Explicacion = "DISTINCT devuelve solo valores únicos, eliminando duplicados del resultado."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué tipo de dato SQL almacena fechas y horas?",
                    Opciones = new List<string> { "DATE", "DATETIME", "TIMESTAMP", "Todos los anteriores" },
                    RespuestaCorrecta = "Todos los anteriores",
                    Explicacion = "SQL tiene varios tipos para fechas: DATE, DATETIME, TIMESTAMP, cada uno con diferentes precisiones."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué comando SQL actualiza datos existentes?",
                    Opciones = new List<string> { "MODIFY", "UPDATE", "CHANGE", "ALTER" },
                    RespuestaCorrecta = "UPDATE",
                    Explicacion = "UPDATE modifica registros existentes según condiciones especificadas en WHERE."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué índice se crea automáticamente al definir una PRIMARY KEY?",
                    Opciones = new List<string> { "INDEX", "UNIQUE INDEX", "CLUSTERED INDEX", "Depende del DBMS" },
                    RespuestaCorrecta = "Depende del DBMS",
                    Explicacion = "Diferentes DBMS crean diferentes tipos de índices automáticamente para PRIMARY KEY."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué hace la función AVG()?",
                    Opciones = new List<string> { "Cuenta filas", "Calcula el promedio", "Suma valores", "Encuentra el máximo" },
                    RespuestaCorrecta = "Calcula el promedio",
                    Explicacion = "AVG() calcula el valor promedio de una columna numérica."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué restricción previene valores NULL en una columna?",
                    Opciones = new List<string> { "UNIQUE", "CHECK", "NOT NULL", "DEFAULT" },
                    RespuestaCorrecta = "NOT NULL",
                    Explicacion = "NOT NULL obliga a que la columna siempre tenga un valor válido."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué comando SQL elimina una tabla completa?",
                    Opciones = new List<string> { "DELETE TABLE", "REMOVE TABLE", "DROP TABLE", "CLEAR TABLE" },
                    RespuestaCorrecta = "DROP TABLE",
                    Explicacion = "DROP TABLE elimina permanentemente la tabla y todos sus datos."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué operador combina resultados de dos SELECT?",
                    Opciones = new List<string> { "MERGE", "UNION", "COMBINE", "JOIN" },
                    RespuestaCorrecta = "UNION",
                    Explicacion = "UNION combina resultados de múltiples SELECT eliminando duplicados."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué hace ORDER BY ASC?",
                    Opciones = new List<string> { "Ordena descendente", "Ordena ascendente", "Agrupa datos", "Filtra resultados" },
                    RespuestaCorrecta = "Ordena ascendente",
                    Explicacion = "ORDER BY ASC ordena los resultados de menor a mayor (A-Z, 0-9)."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué es una subconsulta?",
                    Opciones = new List<string> { "Un error SQL", "Una consulta dentro de otra", "Un tipo de JOIN", "Una función agregada" },
                    RespuestaCorrecta = "Una consulta dentro de otra",
                    Explicacion = "Una subconsulta es un SELECT anidado dentro de otra consulta SQL."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué comando SQL inserta nuevos datos?",
                    Opciones = new List<string> { "ADD", "INSERT", "CREATE", "PUT" },
                    RespuestaCorrecta = "INSERT",
                    Explicacion = "INSERT INTO agrega nuevas filas a una tabla."
                },
                new PreguntaQuiz
                {
                    Tema = "SQL",
                    Pregunta = "¿Qué hace la función MAX()?",
                    Opciones = new List<string> { "Cuenta registros", "Encuentra el valor máximo", "Suma valores", "Calcula promedio" },
                    RespuestaCorrecta = "Encuentra el valor máximo",
                    Explicacion = "MAX() devuelve el valor más alto de una columna."
                },
                #endregion

                #region Arquitectura (20 preguntas)
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Cuál es la duración típica de un Sprint en Scrum?",
                    Opciones = new List<string> { "1 semana", "2-4 semanas", "1 mes fijo", "6 semanas" },
                    RespuestaCorrecta = "2-4 semanas",
                    Explicacion = "Los Sprints duran entre 2 y 4 semanas, permitiendo feedback rápido."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué ceremonia de Scrum se realiza al inicio de cada día?",
                    Opciones = new List<string> { "Sprint Planning", "Daily Standup", "Sprint Review", "Retrospectiva" },
                    RespuestaCorrecta = "Daily Standup",
                    Explicacion = "El Daily Standup sincroniza al equipo diariamente en 15 minutos."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué valor del Manifiesto Ágil prioriza el software funcionando?",
                    Opciones = new List<string> { "Sobre procesos", "Sobre documentación exhaustiva", "Sobre negociación", "Sobre planes" },
                    RespuestaCorrecta = "Sobre documentación exhaustiva",
                    Explicacion = "Se prioriza entregar software que funcione sobre crear documentación extensa."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué rol protege al equipo de distracciones en Scrum?",
                    Opciones = new List<string> { "Product Owner", "Scrum Master", "Tech Lead", "Stakeholder" },
                    RespuestaCorrecta = "Scrum Master",
                    Explicacion = "El Scrum Master facilita y protege al equipo de impedimentos."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué patrón arquitectónico separa UI, lógica y datos?",
                    Opciones = new List<string> { "Singleton", "MVC", "Observer", "Factory" },
                    RespuestaCorrecta = "MVC",
                    Explicacion = "MVC (Model-View-Controller) separa la aplicación en tres componentes."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué significa SOLID en programación?",
                    Opciones = new List<string> { "Un lenguaje", "Cinco principios de diseño", "Un framework", "Un patrón" },
                    RespuestaCorrecta = "Cinco principios de diseño",
                    Explicacion = "SOLID son cinco principios para escribir código mantenible y escalable."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué es una User Story en Agile?",
                    Opciones = new List<string> { "Un bug report", "Una descripción de funcionalidad", "Un test case", "Un sprint" },
                    RespuestaCorrecta = "Una descripción de funcionalidad",
                    Explicacion = "Una User Story describe una funcionalidad desde la perspectiva del usuario."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué ceremonia revisa el incremento del Sprint?",
                    Opciones = new List<string> { "Planning", "Review", "Retrospective", "Daily" },
                    RespuestaCorrecta = "Review",
                    Explicacion = "En la Sprint Review se presenta el trabajo completado a stakeholders."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué es el Product Backlog?",
                    Opciones = new List<string> { "Bugs pendientes", "Lista priorizada de trabajo", "Código antiguo", "Documentación" },
                    RespuestaCorrecta = "Lista priorizada de trabajo",
                    Explicacion = "El Product Backlog es la lista ordenada de todo lo que se necesita en el producto."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué principio SOLID dice que una clase debe tener una sola razón para cambiar?",
                    Opciones = new List<string> { "Open/Closed", "Single Responsibility", "Liskov Substitution", "Dependency Inversion" },
                    RespuestaCorrecta = "Single Responsibility",
                    Explicacion = "Single Responsibility Principle: cada clase debe tener una única responsabilidad."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué es un Microservicio?",
                    Opciones = new List<string> { "Un servicio pequeño", "Arquitectura de servicios independientes", "Un API", "Un contenedor" },
                    RespuestaCorrecta = "Arquitectura de servicios independientes",
                    Explicacion = "Microservicios son aplicaciones como conjunto de servicios pequeños e independientes."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué es la Definition of Done?",
                    Opciones = new List<string> { "Fecha límite", "Criterios de completitud", "Lista de bugs", "Plan de testing" },
                    RespuestaCorrecta = "Criterios de completitud",
                    Explicacion = "DoD define cuándo una historia de usuario está realmente completa."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué patrón permite añadir funcionalidad sin modificar código existente?",
                    Opciones = new List<string> { "Singleton", "Decorator", "Factory", "Observer" },
                    RespuestaCorrecta = "Decorator",
                    Explicacion = "Decorator añade responsabilidades a objetos dinámicamente."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué es Kanban?",
                    Opciones = new List<string> { "Un lenguaje", "Método visual de gestión", "Un framework", "Una herramienta" },
                    RespuestaCorrecta = "Método visual de gestión",
                    Explicacion = "Kanban visualiza el flujo de trabajo y limita el trabajo en progreso."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué es CI/CD?",
                    Opciones = new List<string> { "Un lenguaje", "Integración y entrega continua", "Un servidor", "Un framework" },
                    RespuestaCorrecta = "Integración y entrega continua",
                    Explicacion = "CI/CD automatiza integración de código y despliegue continuo."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué es refactoring?",
                    Opciones = new List<string> { "Agregar features", "Mejorar código sin cambiar funcionalidad", "Corregir bugs", "Testing" },
                    RespuestaCorrecta = "Mejorar código sin cambiar funcionalidad",
                    Explicacion = "Refactoring mejora la estructura interna sin alterar el comportamiento externo."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué es una API REST?",
                    Opciones = new List<string> { "Base de datos", "Interfaz de programación web", "Lenguaje", "Framework" },
                    RespuestaCorrecta = "Interfaz de programación web",
                    Explicacion = "REST es un estilo arquitectónico para crear servicios web."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué rol prioriza el Product Backlog?",
                    Opciones = new List<string> { "Scrum Master", "Product Owner", "Developer", "Tester" },
                    RespuestaCorrecta = "Product Owner",
                    Explicacion = "El Product Owner es responsable de maximizar el valor del producto."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué es TDD?",
                    Opciones = new List<string> { "Un framework", "Desarrollo dirigido por tests", "Un lenguaje", "Una metodología ágil" },
                    RespuestaCorrecta = "Desarrollo dirigido por tests",
                    Explicacion = "TDD escribe tests antes del código de producción."
                },
                new PreguntaQuiz
                {
                    Tema = "Arquitectura",
                    Pregunta = "¿Qué es la Velocity en Scrum?",
                    Opciones = new List<string> { "Rapidez del equipo", "Story points completados por sprint", "Tiempo de desarrollo", "Bugs resueltos" },
                    RespuestaCorrecta = "Story points completados por sprint",
                    Explicacion = "Velocity mide cuánto trabajo completa el equipo por sprint."
                },
                #endregion

                #region POO (20 preguntas)
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué pilar de POO permite que una clase herede de otra?",
                    Opciones = new List<string> { "Encapsulamiento", "Herencia", "Polimorfismo", "Abstracción" },
                    RespuestaCorrecta = "Herencia",
                    Explicacion = "La Herencia permite reutilizar código de clases existentes."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué modificador hace que un miembro sea accesible solo en su clase?",
                    Opciones = new List<string> { "public", "private", "protected", "internal" },
                    RespuestaCorrecta = "private",
                    Explicacion = "private restringe el acceso únicamente a la clase que lo define."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué permite el polimorfismo?",
                    Opciones = new List<string> { "Ocultar datos", "Usar interfaz común para diferentes tipos", "Crear clases", "Heredar" },
                    RespuestaCorrecta = "Usar interfaz común para diferentes tipos",
                    Explicacion = "Polimorfismo permite tratar objetos diferentes de manera uniforme."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué es una clase abstracta?",
                    Opciones = new List<string> { "Sin métodos", "No puede instanciarse", "Sellada", "Estática" },
                    RespuestaCorrecta = "No puede instanciarse",
                    Explicacion = "Una clase abstracta sirve de plantilla y no se puede instanciar."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué es el encapsulamiento?",
                    Opciones = new List<string> { "Heredar código", "Ocultar detalles internos", "Crear objetos", "Eliminar código" },
                    RespuestaCorrecta = "Ocultar detalles internos",
                    Explicacion = "Encapsulamiento oculta la implementación interna y expone solo lo necesario."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué es una interfaz?",
                    Opciones = new List<string> { "Una clase", "Un contrato de métodos", "Un objeto", "Una variable" },
                    RespuestaCorrecta = "Un contrato de métodos",
                    Explicacion = "Una interfaz define métodos que las clases deben implementar."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué es un constructor?",
                    Opciones = new List<string> { "Un destructor", "Método que inicializa objetos", "Una variable", "Una interfaz" },
                    RespuestaCorrecta = "Método que inicializa objetos",
                    Explicacion = "Un constructor se ejecuta al crear una instancia de clase."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué es la sobrecarga de métodos?",
                    Opciones = new List<string> { "Métodos duplicados", "Mismo nombre, diferentes parámetros", "Error", "Herencia" },
                    RespuestaCorrecta = "Mismo nombre, diferentes parámetros",
                    Explicacion = "Sobrecarga permite múltiples métodos con el mismo nombre pero diferentes firmas."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué palabra clave indica herencia en C#?",
                    Opciones = new List<string> { "extends", ":", "inherits", "from" },
                    RespuestaCorrecta = ":",
                    Explicacion = "En C# se usa dos puntos (:) para indicar herencia."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué es un método estático?",
                    Opciones = new List<string> { "No cambia", "Pertenece a la clase, no a instancias", "Privado", "Abstracto" },
                    RespuestaCorrecta = "Pertenece a la clase, no a instancias",
                    Explicacion = "Un método estático se llama desde la clase, no desde objetos."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué es la composición?",
                    Opciones = new List<string> { "Heredar", "Contener objetos de otras clases", "Crear clases", "Eliminar objetos" },
                    RespuestaCorrecta = "Contener objetos de otras clases",
                    Explicacion = "Composición es construir clases complejas usando objetos de otras clases."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué es el polimorfismo en tiempo de ejecución?",
                    Opciones = new List<string> { "Sobrecarga", "Override de métodos", "Interfaces", "Herencia" },
                    RespuestaCorrecta = "Override de métodos",
                    Explicacion = "Permite redefinir métodos de la clase base en clases derivadas."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué hace el modificador 'sealed' en C#?",
                    Opciones = new List<string> { "Hace público", "Previene herencia", "Oculta", "Elimina" },
                    RespuestaCorrecta = "Previene herencia",
                    Explicacion = "sealed impide que otras clases hereden de esta clase."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué son las propiedades en C#?",
                    Opciones = new List<string> { "Variables", "Métodos de acceso encapsulados", "Clases", "Interfaces" },
                    RespuestaCorrecta = "Métodos de acceso encapsulados",
                    Explicacion = "Las propiedades encapsulan campos con get/set accesors."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué es la abstracción?",
                    Opciones = new List<string> { "Ocultar complejidad", "Crear objetos", "Heredar", "Eliminar código" },
                    RespuestaCorrecta = "Ocultar complejidad",
                    Explicacion = "Abstracción muestra solo detalles esenciales ocultando la complejidad."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué es un destructor?",
                    Opciones = new List<string> { "Constructor", "Libera recursos al destruir objeto", "Método normal", "Interfaz" },
                    RespuestaCorrecta = "Libera recursos al destruir objeto",
                    Explicacion = "Un destructor se llama automáticamente cuando el objeto se destruye."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué es herencia múltiple?",
                    Opciones = new List<string> { "Muchos objetos", "Heredar de múltiples clases", "Muchos métodos", "No existe" },
                    RespuestaCorrecta = "Heredar de múltiples clases",
                    Explicacion = "Herencia múltiple permite heredar de varias clases (C# no lo soporta directamente)."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué palabra usa C# para implementar interfaces?",
                    Opciones = new List<string> { "extends", "implements", ":", "interface" },
                    RespuestaCorrecta = ":",
                    Explicacion = "C# usa dos puntos (:) tanto para herencia como para interfaces."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué es un método virtual?",
                    Opciones = new List<string> { "No real", "Puede ser sobrescrito", "Abstracto", "Estático" },
                    RespuestaCorrecta = "Puede ser sobrescrito",
                    Explicacion = "Un método virtual puede ser redefinido en clases derivadas."
                },
                new PreguntaQuiz
                {
                    Tema = "POO",
                    Pregunta = "¿Qué es el operador 'new' en POO?",
                    Opciones = new List<string> { "Elimina objetos", "Crea instancias", "Copia objetos", "Oculta miembros" },
                    RespuestaCorrecta = "Crea instancias",
                    Explicacion = "El operador new crea nuevas instancias de una clase."
                },
                #endregion

                #region Git (20 preguntas)
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué comando crea un nuevo repositorio Git?",
                    Opciones = new List<string> { "git start", "git init", "git create", "git new" },
                    RespuestaCorrecta = "git init",
                    Explicacion = "git init inicializa un nuevo repositorio Git."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué hace 'git commit'?",
                    Opciones = new List<string> { "Sube cambios", "Guarda cambios localmente", "Descarga", "Crea rama" },
                    RespuestaCorrecta = "Guarda cambios localmente",
                    Explicacion = "git commit guarda los cambios en el repositorio local."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué comando fusiona una rama con la actual?",
                    Opciones = new List<string> { "git join", "git merge", "git combine", "git integrate" },
                    RespuestaCorrecta = "git merge",
                    Explicacion = "git merge integra cambios de una rama en otra."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué hace 'git pull'?",
                    Opciones = new List<string> { "Solo descarga", "Descarga y fusiona", "Sube", "Crea rama" },
                    RespuestaCorrecta = "Descarga y fusiona",
                    Explicacion = "git pull descarga cambios y los fusiona automáticamente."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué comando muestra el estado del repositorio?",
                    Opciones = new List<string> { "git state", "git status", "git info", "git show" },
                    RespuestaCorrecta = "git status",
                    Explicacion = "git status muestra archivos modificados, agregados y el estado actual."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué hace 'git add .'?",
                    Opciones = new List<string> { "Elimina archivos", "Agrega todos los archivos", "Crea commit", "Sube cambios" },
                    RespuestaCorrecta = "Agrega todos los archivos",
                    Explicacion = "git add . prepara todos los archivos modificados para commit."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué comando crea una nueva rama?",
                    Opciones = new List<string> { "git branch <nombre>", "git create", "git new-branch", "git make" },
                    RespuestaCorrecta = "git branch <nombre>",
                    Explicacion = "git branch crea una nueva rama sin cambiar a ella."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué hace 'git clone'?",
                    Opciones = new List<string> { "Copia archivo", "Copia repositorio completo", "Duplica rama", "Crea backup" },
                    RespuestaCorrecta = "Copia repositorio completo",
                    Explicacion = "git clone crea una copia local de un repositorio remoto."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué comando sube cambios al repositorio remoto?",
                    Opciones = new List<string> { "git upload", "git push", "git send", "git commit" },
                    RespuestaCorrecta = "git push",
                    Explicacion = "git push envía commits locales al repositorio remoto."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué hace 'git checkout'?",
                    Opciones = new List<string> { "Elimina rama", "Cambia de rama", "Crea commit", "Sube cambios" },
                    RespuestaCorrecta = "Cambia de rama",
                    Explicacion = "git checkout cambia entre ramas o restaura archivos."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué es un 'merge conflict'?",
                    Opciones = new List<string> { "Error de sintaxis", "Cambios contradictorios", "Rama eliminada", "Commit fallido" },
                    RespuestaCorrecta = "Cambios contradictorios",
                    Explicacion = "Ocurre cuando Git no puede fusionar automáticamente cambios."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué hace 'git log'?",
                    Opciones = new List<string> { "Borra historial", "Muestra historial de commits", "Crea archivo", "Sube cambios" },
                    RespuestaCorrecta = "Muestra historial de commits",
                    Explicacion = "git log muestra el historial de commits del repositorio."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué hace 'git stash'?",
                    Opciones = new List<string> { "Elimina cambios", "Guarda cambios temporalmente", "Crea rama", "Sube cambios" },
                    RespuestaCorrecta = "Guarda cambios temporalmente",
                    Explicacion = "git stash guarda cambios sin commit para limpiar el working directory."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué es un 'commit hash'?",
                    Opciones = new List<string> { "Contraseña", "Identificador único de commit", "Rama", "Archivo" },
                    RespuestaCorrecta = "Identificador único de commit",
                    Explicacion = "Es un código SHA-1 que identifica únicamente cada commit."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué hace 'git reset'?",
                    Opciones = new List<string> { "Crea repo", "Deshace commits", "Sube cambios", "Crea rama" },
                    RespuestaCorrecta = "Deshace commits",
                    Explicacion = "git reset mueve el HEAD a un commit anterior."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué es '.gitignore'?",
                    Opciones = new List<string> { "Comando", "Archivo que especifica qué ignorar", "Rama", "Configuración" },
                    RespuestaCorrecta = "Archivo que especifica qué ignorar",
                    Explicacion = ".gitignore lista archivos/carpetas que Git debe ignorar."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué hace 'git diff'?",
                    Opciones = new List<string> { "Elimina cambios", "Muestra diferencias", "Crea rama", "Sube archivos" },
                    RespuestaCorrecta = "Muestra diferencias",
                    Explicacion = "git diff muestra cambios entre commits, branches o archivos."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué es un 'fork'?",
                    Opciones = new List<string> { "Error", "Copia de repositorio", "Rama", "Commit" },
                    RespuestaCorrecta = "Copia de repositorio",
                    Explicacion = "Un fork es una copia personal de un repositorio de otro usuario."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué hace 'git rebase'?",
                    Opciones = new List<string> { "Elimina repo", "Reaplica commits sobre otra base", "Crea rama", "Sube cambios" },
                    RespuestaCorrecta = "Reaplica commits sobre otra base",
                    Explicacion = "git rebase mueve commits a una nueva base, creando historial lineal."
                },
                new PreguntaQuiz
                {
                    Tema = "Git",
                    Pregunta = "¿Qué es un 'pull request'?",
                    Opciones = new List<string> { "Comando Git", "Solicitud para fusionar cambios", "Error", "Rama" },
                    RespuestaCorrecta = "Solicitud para fusionar cambios",
                    Explicacion = "Pull request propone cambios para ser revisados y fusionados."
                },
                #endregion

                #region Ciberseguridad (20 preguntas)
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es un ataque de Phishing?",
                    Opciones = new List<string> { "Robo físico", "Engaño para obtener información", "Ataque DDoS", "Virus" },
                    RespuestaCorrecta = "Engaño para obtener información",
                    Explicacion = "Phishing engaña a víctimas haciéndose pasar por entidades confiables."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué significa 'Man in the Middle' (MitM)?",
                    Opciones = new List<string> { "Firewall", "Interceptar comunicaciones", "Encriptar datos", "2FA" },
                    RespuestaCorrecta = "Interceptar comunicaciones",
                    Explicacion = "MitM intercepta secretamente comunicación entre dos partes."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es SQL Injection?",
                    Opciones = new List<string> { "Base de datos", "Ataque que explota vulnerabilidades SQL", "Herramienta", "Protocolo" },
                    RespuestaCorrecta = "Ataque que explota vulnerabilidades SQL",
                    Explicacion = "SQL Injection inserta código malicioso en queries SQL."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué protocolo asegura las comunicaciones web?",
                    Opciones = new List<string> { "HTTP", "HTTPS", "FTP", "SMTP" },
                    RespuestaCorrecta = "HTTPS",
                    Explicacion = "HTTPS usa encriptación TLS/SSL para proteger comunicaciones."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es un Firewall?",
                    Opciones = new List<string> { "Antivirus", "Sistema que controla tráfico de red", "Encriptación", "VPN" },
                    RespuestaCorrecta = "Sistema que controla tráfico de red",
                    Explicacion = "Firewall filtra tráfico de red basado en reglas de seguridad."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es 2FA?",
                    Opciones = new List<string> { "Encriptación", "Autenticación de dos factores", "Firewall", "Antivirus" },
                    RespuestaCorrecta = "Autenticación de dos factores",
                    Explicacion = "2FA requiere dos formas de verificación de identidad."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es malware?",
                    Opciones = new List<string> { "Software legal", "Software malicioso", "Firewall", "Protocolo" },
                    RespuestaCorrecta = "Software malicioso",
                    Explicacion = "Malware es software diseñado para dañar o infiltrarse en sistemas."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es encriptación?",
                    Opciones = new List<string> { "Borrar datos", "Convertir datos en código", "Firewall", "Antivirus" },
                    RespuestaCorrecta = "Convertir datos en código",
                    Explicacion = "Encriptación protege información convirtiéndola en código ilegible."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es una VPN?",
                    Opciones = new List<string> { "Virus", "Red privada virtual", "Firewall", "Protocolo" },
                    RespuestaCorrecta = "Red privada virtual",
                    Explicacion = "VPN crea conexión segura y encriptada sobre internet."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es un ataque DDoS?",
                    Opciones = new List<string> { "Robo de datos", "Denegación de servicio distribuida", "Virus", "Phishing" },
                    RespuestaCorrecta = "Denegación de servicio distribuida",
                    Explicacion = "DDoS satura un servidor con tráfico para dejarlo inaccesible."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es ransomware?",
                    Opciones = new List<string> { "Antivirus", "Malware que secuestra datos", "Firewall", "Encriptación" },
                    RespuestaCorrecta = "Malware que secuestra datos",
                    Explicacion = "Ransomware encripta datos y exige rescate para liberarlos."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es XSS?",
                    Opciones = new List<string> { "Protocolo", "Cross-Site Scripting", "Firewall", "Encriptación" },
                    RespuestaCorrecta = "Cross-Site Scripting",
                    Explicacion = "XSS inyecta scripts maliciosos en páginas web vistas por otros."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es un certificado SSL?",
                    Opciones = new List<string> { "Virus", "Certificado de seguridad digital", "Firewall", "Antivirus" },
                    RespuestaCorrecta = "Certificado de seguridad digital",
                    Explicacion = "SSL certifica la identidad de un sitio web y encripta datos."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es ingeniería social?",
                    Opciones = new List<string> { "Programación", "Manipular personas para obtener información", "Firewall", "Encriptación" },
                    RespuestaCorrecta = "Manipular personas para obtener información",
                    Explicacion = "Ingeniería social manipula psicológicamente para revelar información."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es un virus informático?",
                    Opciones = new List<string> { "Software legal", "Programa que se replica y daña sistemas", "Firewall", "Protocolo" },
                    RespuestaCorrecta = "Programa que se replica y daña sistemas",
                    Explicacion = "Un virus se replica insertándose en otros programas."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es spoofing?",
                    Opciones = new List<string> { "Encriptación", "Falsificar identidad", "Firewall", "Antivirus" },
                    RespuestaCorrecta = "Falsificar identidad",
                    Explicacion = "Spoofing suplanta la identidad de una fuente confiable."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es un exploit?",
                    Opciones = new List<string> { "Firewall", "Código que aprovecha vulnerabilidades", "Antivirus", "Protocolo" },
                    RespuestaCorrecta = "Código que aprovecha vulnerabilidades",
                    Explicacion = "Un exploit usa vulnerabilidades para comprometer sistemas."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es botnet?",
                    Opciones = new List<string> { "Antivirus", "Red de dispositivos infectados", "Firewall", "Protocolo" },
                    RespuestaCorrecta = "Red de dispositivos infectados",
                    Explicacion = "Botnet es red de computadoras comprometidas controladas remotamente."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es keylogger?",
                    Opciones = new List<string> { "Antivirus", "Programa que registra teclas", "Firewall", "Encriptación" },
                    RespuestaCorrecta = "Programa que registra teclas",
                    Explicacion = "Keylogger captura las teclas presionadas para robar información."
                },
                new PreguntaQuiz
                {
                    Tema = "Ciberseguridad",
                    Pregunta = "¿Qué es Zero-Day?",
                    Opciones = new List<string> { "Antivirus", "Vulnerabilidad sin parche", "Firewall", "Protocolo" },
                    RespuestaCorrecta = "Vulnerabilidad sin parche",
                    Explicacion = "Zero-Day es una vulnerabilidad desconocida sin solución disponible."
                },
                #endregion

                #region HTML5 (20 preguntas)
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué etiqueta HTML5 define una sección independiente?",
                    Opciones = new List<string> { "<div>", "<section>", "<article>", "<aside>" },
                    RespuestaCorrecta = "<article>",
                    Explicacion = "<article> representa contenido independiente y reutilizable."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué atributo hace que un campo sea obligatorio?",
                    Opciones = new List<string> { "mandatory", "required", "validate", "important" },
                    RespuestaCorrecta = "required",
                    Explicacion = "El atributo required valida que el campo esté completado."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué elemento HTML5 reproduce video?",
                    Opciones = new List<string> { "<media>", "<video>", "<movie>", "<play>" },
                    RespuestaCorrecta = "<video>",
                    Explicacion = "<video> incrusta video sin necesidad de plugins."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué API permite almacenar datos en el navegador?",
                    Opciones = new List<string> { "Cookies", "LocalStorage", "SessionCache", "BrowserDB" },
                    RespuestaCorrecta = "LocalStorage",
                    Explicacion = "LocalStorage almacena datos persistentes en el navegador."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué etiqueta define el encabezado de un documento?",
                    Opciones = new List<string> { "<top>", "<header>", "<head>", "<title>" },
                    RespuestaCorrecta = "<header>",
                    Explicacion = "<header> define el encabezado de una sección o página."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué etiqueta reproduce audio?",
                    Opciones = new List<string> { "<sound>", "<audio>", "<music>", "<play>" },
                    RespuestaCorrecta = "<audio>",
                    Explicacion = "<audio> permite incrustar contenido de audio."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué elemento se usa para dibujar gráficos?",
                    Opciones = new List<string> { "<draw>", "<canvas>", "<graphics>", "<svg>" },
                    RespuestaCorrecta = "<canvas>",
                    Explicacion = "<canvas> permite dibujar gráficos mediante JavaScript."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué etiqueta define navegación?",
                    Opciones = new List<string> { "<menu>", "<nav>", "<navigation>", "<links>" },
                    RespuestaCorrecta = "<nav>",
                    Explicacion = "<nav> define una sección de enlaces de navegación."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué atributo especifica el tipo de input?",
                    Opciones = new List<string> { "kind", "type", "format", "class" },
                    RespuestaCorrecta = "type",
                    Explicacion = "El atributo type define el tipo de campo de entrada."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué elemento define el pie de página?",
                    Opciones = new List<string> { "<bottom>", "<footer>", "<end>", "<base>" },
                    RespuestaCorrecta = "<footer>",
                    Explicacion = "<footer> define el pie de página de un documento o sección."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué input type crea un selector de fecha?",
                    Opciones = new List<string> { "calendar", "date", "datetime", "picker" },
                    RespuestaCorrecta = "date",
                    Explicacion = "<input type='date'> crea un selector de fecha."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué etiqueta define contenido lateral?",
                    Opciones = new List<string> { "<sidebar>", "<aside>", "<side>", "<extra>" },
                    RespuestaCorrecta = "<aside>",
                    Explicacion = "<aside> define contenido relacionado indirectamente."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué hace el atributo 'placeholder'?",
                    Opciones = new List<string> { "Valida", "Muestra texto de ayuda", "Formatea", "Oculta" },
                    RespuestaCorrecta = "Muestra texto de ayuda",
                    Explicacion = "placeholder muestra un texto de ejemplo en campos de entrada."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué API obtiene la ubicación del usuario?",
                    Opciones = new List<string> { "GPS API", "Geolocation", "Location API", "Maps API" },
                    RespuestaCorrecta = "Geolocation",
                    Explicacion = "Geolocation API obtiene la ubicación geográfica del dispositivo."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué elemento crea una lista desordenada?",
                    Opciones = new List<string> { "<list>", "<ul>", "<ol>", "<items>" },
                    RespuestaCorrecta = "<ul>",
                    Explicacion = "<ul> (unordered list) crea listas con viñetas."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué input type valida emails?",
                    Opciones = new List<string> { "text", "email", "mail", "validation" },
                    RespuestaCorrecta = "email",
                    Explicacion = "<input type='email'> valida formato de correo electrónico."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué son los Web Workers?",
                    Opciones = new List<string> { "Framework", "JavaScript en segundo plano", "Librería", "Protocolo" },
                    RespuestaCorrecta = "JavaScript en segundo plano",
                    Explicacion = "Web Workers ejecutan scripts en segundo plano sin bloquear la UI."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué etiqueta define una sección temática?",
                    Opciones = new List<string> { "<div>", "<section>", "<theme>", "<block>" },
                    RespuestaCorrecta = "<section>",
                    Explicacion = "<section> agrupa contenido relacionado temáticamente."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué atributo hace que un input acepte múltiples valores?",
                    Opciones = new List<string> { "many", "multiple", "multi", "array" },
                    RespuestaCorrecta = "multiple",
                    Explicacion = "El atributo multiple permite seleccionar varios valores."
                },
                new PreguntaQuiz
                {
                    Tema = "HTML5",
                    Pregunta = "¿Qué elemento define metadatos del documento?",
                    Opciones = new List<string> { "<header>", "<meta>", "<info>", "<data>" },
                    RespuestaCorrecta = "<meta>",
                    Explicacion = "<meta> proporciona metadatos sobre el documento HTML."
                }
                #endregion
            };

            await _database.InsertAllAsync(preguntas);

            // ==================== FLASHCARDS (20 por tema) ====================
            var flashcards = new List<TarjetaFlashcard>
            {
                #region SQL Flashcards (20)
                new TarjetaFlashcard { Tema = "SQL", Anverso = "SELECT", Reverso = "Comando para consultar datos de una tabla." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "PRIMARY KEY", Reverso = "Identificador único de cada fila. No puede ser NULL." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "FOREIGN KEY", Reverso = "Referencia a PRIMARY KEY de otra tabla." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "INDEX", Reverso = "Mejora velocidad de búsqueda en tablas." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "INNER JOIN", Reverso = "Devuelve filas con coincidencias en ambas tablas." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "LEFT JOIN", Reverso = "Devuelve todas las filas de la tabla izquierda." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "GROUP BY", Reverso = "Agrupa filas con valores iguales." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "HAVING", Reverso = "Filtra grupos después de GROUP BY." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "WHERE", Reverso = "Filtra filas antes de agrupar." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "ORDER BY", Reverso = "Ordena resultados ascendente o descendente." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "DISTINCT", Reverso = "Elimina duplicados del resultado." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "COUNT()", Reverso = "Cuenta número de filas." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "SUM()", Reverso = "Suma valores de una columna." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "AVG()", Reverso = "Calcula el promedio de valores." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "MAX()", Reverso = "Encuentra el valor máximo." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "MIN()", Reverso = "Encuentra el valor mínimo." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "INSERT INTO", Reverso = "Agrega nuevas filas a tabla." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "UPDATE", Reverso = "Modifica datos existentes." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "DELETE", Reverso = "Elimina filas de una tabla." },
                new TarjetaFlashcard { Tema = "SQL", Anverso = "TRUNCATE", Reverso = "Elimina todos los datos de tabla rápidamente." },
                #endregion

                #region Arquitectura Flashcards (20)
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "Sprint", Reverso = "Iteración de 2-4 semanas en Scrum." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "Product Backlog", Reverso = "Lista priorizada de trabajo pendiente." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "Sprint Backlog", Reverso = "Trabajo seleccionado para el Sprint actual." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "Daily Standup", Reverso = "Reunión diaria de 15 minutos del equipo." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "Sprint Review", Reverso = "Demostración del trabajo completado." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "Retrospectiva", Reverso = "Reflexión del equipo para mejorar." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "Scrum Master", Reverso = "Facilita el proceso y remueve impedimentos." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "Product Owner", Reverso = "Maximiza el valor del producto." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "User Story", Reverso = "Descripción de funcionalidad desde usuario." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "MVC", Reverso = "Patrón Model-View-Controller." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "SOLID", Reverso = "Cinco principios de diseño orientado a objetos." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "Microservicios", Reverso = "Arquitectura de servicios independientes." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "API REST", Reverso = "Interfaz de programación usando HTTP." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "CI/CD", Reverso = "Integración y entrega continua." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "TDD", Reverso = "Test-Driven Development." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "Kanban", Reverso = "Método visual de gestión de trabajo." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "Velocity", Reverso = "Story points completados por sprint." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "Definition of Done", Reverso = "Criterios para considerar trabajo completo." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "Refactoring", Reverso = "Mejorar código sin cambiar funcionalidad." },
                new TarjetaFlashcard { Tema = "Arquitectura", Anverso = "Decorator", Reverso = "Añade funcionalidad a objetos dinámicamente." },
                #endregion

                #region POO Flashcards (20)
                new TarjetaFlashcard { Tema = "POO", Anverso = "Clase", Reverso = "Plantilla para crear objetos." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Objeto", Reverso = "Instancia de una clase." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Herencia", Reverso = "Clase que adquiere propiedades de otra." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Polimorfismo", Reverso = "Usar interfaz común para diferentes tipos." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Encapsulamiento", Reverso = "Ocultar detalles internos del objeto." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Abstracción", Reverso = "Mostrar solo detalles esenciales." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Interfaz", Reverso = "Contrato que define métodos a implementar." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Constructor", Reverso = "Método que inicializa objetos." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Destructor", Reverso = "Libera recursos al destruir objeto." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Método Virtual", Reverso = "Puede ser sobrescrito en clases derivadas." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Método Abstracto", Reverso = "Declarado sin implementación." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Sobrecarga", Reverso = "Mismo nombre, diferentes parámetros." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Sobreescritura", Reverso = "Redefinir método de clase base." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Composición", Reverso = "Objeto contiene otros objetos." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Propiedad", Reverso = "Encapsula campo con get/set." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Modificador private", Reverso = "Accesible solo en su clase." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Modificador public", Reverso = "Accesible desde cualquier lugar." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Modificador protected", Reverso = "Accesible en clase y derivadas." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Clase Sealed", Reverso = "No puede ser heredada." },
                new TarjetaFlashcard { Tema = "POO", Anverso = "Método Static", Reverso = "Pertenece a la clase, no a instancias." },
                #endregion

                #region Git Flashcards (20)
                new TarjetaFlashcard { Tema = "Git", Anverso = "git init", Reverso = "Inicializa nuevo repositorio Git." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "git clone", Reverso = "Copia repositorio remoto localmente." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "git add", Reverso = "Prepara archivos para commit." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "git commit", Reverso = "Guarda cambios en repositorio local." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "git push", Reverso = "Sube commits a repositorio remoto." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "git pull", Reverso = "Descarga y fusiona cambios remotos." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "git branch", Reverso = "Crea, lista o elimina ramas." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "git checkout", Reverso = "Cambia entre ramas o restaura archivos." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "git merge", Reverso = "Fusiona una rama con otra." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "git status", Reverso = "Muestra estado del repositorio." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "git log", Reverso = "Muestra historial de commits." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "git diff", Reverso = "Muestra diferencias entre commits." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "git stash", Reverso = "Guarda cambios temporalmente." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "git reset", Reverso = "Deshace commits localmente." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "git rebase", Reverso = "Reaplica commits sobre nueva base." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "git fetch", Reverso = "Descarga cambios sin fusionar." },
                new TarjetaFlashcard { Tema = "Git", Anverso = ".gitignore", Reverso = "Archivo con patrones a ignorar." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "Fork", Reverso = "Copia personal de repositorio ajeno." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "Pull Request", Reverso = "Solicitud para fusionar cambios." },
                new TarjetaFlashcard { Tema = "Git", Anverso = "Commit Hash", Reverso = "Identificador único SHA-1 de commit." },
                #endregion

                #region Ciberseguridad Flashcards (20)
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "Phishing", Reverso = "Engaño para robar información personal." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "Malware", Reverso = "Software malicioso que daña sistemas." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "Ransomware", Reverso = "Secuestra datos y exige rescate." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "Firewall", Reverso = "Filtra tráfico de red por seguridad." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "VPN", Reverso = "Red privada virtual encriptada." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "Encriptación", Reverso = "Convierte datos en código secreto." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "2FA", Reverso = "Autenticación de dos factores." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "SSL/TLS", Reverso = "Protocolo de encriptación web." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "DDoS", Reverso = "Ataque de denegación de servicio." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "SQL Injection", Reverso = "Inyecta código SQL malicioso." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "XSS", Reverso = "Cross-Site Scripting en web." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "Man-in-the-Middle", Reverso = "Intercepta comunicación entre dos partes." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "Virus", Reverso = "Programa que se replica y daña." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "Trojan", Reverso = "Malware disfrazado de software legítimo." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "Botnet", Reverso = "Red de dispositivos comprometidos." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "Keylogger", Reverso = "Registra teclas presionadas." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "Exploit", Reverso = "Aprovecha vulnerabilidades del sistema." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "Zero-Day", Reverso = "Vulnerabilidad sin parche conocido." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "Spoofing", Reverso = "Falsifica identidad de fuente." },
                new TarjetaFlashcard { Tema = "Ciberseguridad", Anverso = "Ingeniería Social", Reverso = "Manipula personas para obtener info." },
                #endregion

                #region HTML5 Flashcards (20)
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "<header>", Reverso = "Define encabezado de documento/sección." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "<nav>", Reverso = "Define enlaces de navegación." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "<article>", Reverso = "Contenido independiente y reutilizable." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "<section>", Reverso = "Agrupa contenido temático." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "<aside>", Reverso = "Contenido relacionado indirectamente." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "<footer>", Reverso = "Define pie de página." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "<video>", Reverso = "Incrusta video en página." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "<audio>", Reverso = "Incrusta audio en página." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "<canvas>", Reverso = "Dibuja gráficos con JavaScript." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "LocalStorage", Reverso = "Almacena datos persistentes en navegador." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "SessionStorage", Reverso = "Almacena datos por sesión." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "Geolocation API", Reverso = "Obtiene ubicación del usuario." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "Web Workers", Reverso = "JavaScript en segundo plano." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "required", Reverso = "Atributo que hace campo obligatorio." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "placeholder", Reverso = "Texto de ayuda en campos." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "type='email'", Reverso = "Valida formato de email." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "type='date'", Reverso = "Selector de fecha." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "<meta>", Reverso = "Define metadatos del documento." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "Semantic HTML", Reverso = "Etiquetas con significado (header, nav, etc)." },
                new TarjetaFlashcard { Tema = "HTML5", Anverso = "multiple", Reverso = "Permite selección múltiple en inputs." }
                #endregion
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