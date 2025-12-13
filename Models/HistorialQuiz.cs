using SQLite;

namespace SoftwareEngineeringQuizApp.Models;

[Table("HistorialQuiz")]
public class HistorialQuiz
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Tema { get; set; }
    public int Aciertos { get; set; }
    public int TotalPreguntas { get; set; }
    public DateTime Fecha { get; set; }
    public double Porcentaje => (double)Aciertos / TotalPreguntas;
}