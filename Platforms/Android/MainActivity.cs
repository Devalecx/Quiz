using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Graphics.Drawables;

namespace SoftwareEngineeringQuizApp;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        // 1. Antes de que MAUI inicie, establecemos el tema base
        base.SetTheme(Resource.Style.Maui_MainTheme_NoActionBar);

        base.OnCreate(savedInstanceState);

        // 2. SOLUCIÓN AL FLASH BLANCO (Fuerza bruta)
        // Pintamos la ventana y las barras del sistema manualmente con tu color morado (#704193)
        // Esto cubre cualquier hueco blanco que quede mientras carga el XAML.

        // CORRECCIÓN: Usamos el nombre completo 'Android.Graphics.Color' para evitar conflictos con MAUI
        var colorMorado = Android.Graphics.Color.ParseColor("#704193");

        // Pintar fondo de ventana
        Window?.SetBackgroundDrawable(new ColorDrawable(colorMorado));

        // Pintar barra de estado (arriba) y navegación (abajo)
        Window?.SetStatusBarColor(colorMorado);
        Window?.SetNavigationBarColor(colorMorado);
    }
}