using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SoftwareEngineeringQuizApp.Models;
using SoftwareEngineeringQuizApp.Services;
using System.Collections.ObjectModel;

namespace SoftwareEngineeringQuizApp.ViewModels;

public partial class PerfilViewModel : ObservableObject
{
    private readonly RepositorioBaseDatos _repositorio;

    [ObservableProperty]
    private string nombreUsuario;

    [ObservableProperty]
    private string fotoPerfil; // Ruta de la imagen

    [ObservableProperty]
    private ObservableCollection<HistorialQuiz> historial;

    public PerfilViewModel(RepositorioBaseDatos repositorio)
    {
        _repositorio = repositorio;
        Historial = new ObservableCollection<HistorialQuiz>();
    }

    [RelayCommand]
    public async Task InicializarDatosAsync()
    {
        // 1. Cargar Usuario
        var usuario = await _repositorio.ObtenerUsuarioAsync();
        NombreUsuario = usuario.Nombre;

        // Si no hay foto personalizada, usamos una por defecto o null (la UI manejará el placeholder)
        FotoPerfil = usuario.FotoPath;

        // 2. Cargar Historial
        var listaHistorial = await _repositorio.ObtenerHistorialAsync();
        Historial.Clear();
        foreach (var item in listaHistorial)
        {
            Historial.Add(item);
        }
    }

    [RelayCommand]
    public async Task GuardarNombreAsync()
    {
        var usuario = await _repositorio.ObtenerUsuarioAsync();
        usuario.Nombre = NombreUsuario;
        await _repositorio.GuardarUsuarioAsync(usuario);
        await Shell.Current.DisplayAlert("Éxito", "Nombre actualizado.", "OK");
    }

    [RelayCommand]
    public async Task CambiarFotoAsync()
    {
        string action = await Shell.Current.DisplayActionSheet("Cambiar Foto de Perfil", "Cancelar", null, "Tomar Foto", "Elegir de Galería");

        if (action == "Tomar Foto")
        {
            await TomarFotoCamara();
        }
        else if (action == "Elegir de Galería")
        {
            await ElegirFotoGaleria();
        }
    }

    private async Task TomarFotoCamara()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo != null)
            {
                await GuardarFotoLocalmente(photo);
            }
        }
    }

    private async Task ElegirFotoGaleria()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.PickPhotoAsync();
            if (photo != null)
            {
                await GuardarFotoLocalmente(photo);
            }
        }
    }

    // Método auxiliar para guardar la foto en la carpeta de la App y no perderla
    private async Task GuardarFotoLocalmente(FileResult photo)
    {
        // Ruta destino en el dispositivo
        var targetFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);

        using (var stream = await photo.OpenReadAsync())
        using (var newStream = File.OpenWrite(targetFile))
        {
            await stream.CopyToAsync(newStream);
        }

        // Actualizar en BD
        var usuario = await _repositorio.ObtenerUsuarioAsync();
        usuario.FotoPath = targetFile;
        await _repositorio.GuardarUsuarioAsync(usuario);

        // Actualizar UI
        FotoPerfil = targetFile;
    }
}