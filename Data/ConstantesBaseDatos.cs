namespace SoftwareEngineeringQuizApp.Data;

public static class ConstantesBaseDatos
{
    public const string NombreArchivoBaseDatos = "QuizApp.db3";

    // Flags para abrir la conexión: Lectura/Escritura | Crear si no existe | Caché compartido
    public const SQLite.SQLiteOpenFlags Flags =
        SQLite.SQLiteOpenFlags.ReadWrite |
        SQLite.SQLiteOpenFlags.Create |
        SQLite.SQLiteOpenFlags.SharedCache;

    // Ruta física en el dispositivo móvil
    public static string RutaBaseDatos =>
        Path.Combine(FileSystem.AppDataDirectory, NombreArchivoBaseDatos);
}