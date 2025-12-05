using SQLite;

namespace SoftwareEngineeringQuizApp.Models;

[Table("Flashcards")]
public class TarjetaFlashcard
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Tema { get; set; } = string.Empty;
    public string Anverso { get; set; } = string.Empty; // Frente de la tarjeta
    public string Reverso { get; set; } = string.Empty; // Respuesta o reverso
}