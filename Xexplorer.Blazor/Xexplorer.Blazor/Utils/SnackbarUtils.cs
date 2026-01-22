using MudBlazor;
using Color = MudBlazor.Color;
using Size = MudBlazor.Size;

namespace Xexplorer.Blazor.Utils;

public class SnackbarUtils
{
    public static ISnackbar Snackbar { get; set; }

    public static void Error(string message)
    {
        Snackbar.Add(message, Severity.Error, config =>
        {
            config.Icon = Icons.Custom.Brands.GitHub;
            config.IconColor = Color.Warning;
            config.IconSize = Size.Large;
            config.ShowCloseIcon = true;
            config.BackgroundBlurred = true;
            config.VisibleStateDuration = 3000;
        });
    }

    public static void Success(string message)
    {
        Snackbar.Add(message, Severity.Success, config =>
        {
            config.Icon = Icons.Custom.Brands.GitHub;
            config.IconColor = Color.Warning;
            config.IconSize = Size.Large;
            config.ShowCloseIcon = true;
            config.VisibleStateDuration = 3000;
        });
    }
    
    public static void Warning(string message)
    {
        Snackbar.Add(message, Severity.Warning, config =>
        {
            config.Icon = Icons.Custom.Brands.GitHub;
            config.IconColor = Color.Warning;
            config.IconSize = Size.Large;
            config.ShowCloseIcon = true;
            config.VisibleStateDuration = 3000;
        });
    }
}