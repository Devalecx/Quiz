using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SoftwareEngineeringQuizApp.Services;

namespace SoftwareEngineeringQuizApp.ViewModels;

public partial class MenuPrincipalViewModel : ObservableObject
{
    private readonly RepositorioBaseDatos _repositorio;

    [ObservableProperty]
    private string fotoPerfil;

    [ObservableProperty]
    private string nombreUsuario;

    public MenuPrincipalViewModel(RepositorioBaseDatos repositorio)
    {
        _repositorio = repositorio;
    }

    // Método para cargar la foto cada vez que entramos al menú
    public async Task CargarDatosUsuarioAsync()
    {
        var usuario = await _repositorio.ObtenerUsuarioAsync();

        // Si tiene foto, la usamos. Si no, usa la imagen por defecto
        FotoPerfil = !string.IsNullOrEmpty(usuario.FotoPath)
                     ? usuario.FotoPath
                     : "jugador.png"; // Asegúrate de tener esta imagen en Resources/Images

        NombreUsuario = usuario.Nombre;
    }

    [RelayCommand]
    private async Task IrAPerfilAsync()
    {
        // Navegamos a la página de perfil
        await Shell.Current.GoToAsync("Perfil");
    }

    [RelayCommand]
    private async Task IrAQuizAsync()
    {
        await Shell.Current.GoToAsync("ListaTemasQuiz");
    }

    [RelayCommand]
    private async Task IrAFlashcardsAsync()
    {
        await Shell.Current.GoToAsync("ListaTemasFlashcards");
    }
}