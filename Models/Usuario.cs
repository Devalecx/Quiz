using SQLite;

namespace SoftwareEngineeringQuizApp.Models;

[Table("Usuario")]
public class Usuario
{
    [PrimaryKey]
    public int Id { get; set; } = 1; // Solo tendremos un usuario local por ahora

    public string Nombre { get; set; } = "ESTUDIANTE"; // Nombre por defecto
    public string FotoPath { get; set; } // Ruta del archivo de imagen en el dispositivo
}