using SQLite;

namespace SoftwareEngineeringQuizApp.Models;

[Table("PreguntasQuiz")] // Nombre de la tabla en SQLite
public class PreguntaQuiz
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Tema { get; set; } = string.Empty;
    public string Pregunta { get; set; } = string.Empty;

    // SQLite no guarda listas complejas. Guardamos un string serializado.
    // Ejemplo: "Opción A|Opción B|Opción C"
    public string OpcionesSerializadas { get; set; } = string.Empty;

    public string RespuestaCorrecta { get; set; } = string.Empty;

    // Propiedad auxiliar para usar en la App (no se guarda en BD)
    [Ignore]
    public List<string> Opciones
    {
        get => string.IsNullOrEmpty(OpcionesSerializadas)
               ? new List<string>()
               : OpcionesSerializadas.Split('|').ToList();
        set => OpcionesSerializadas = string.Join("|", value);
    }
}