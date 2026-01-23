using System.Formats.Asn1;
using MudBlazor;
using Serilog;
using Xexplorer.Blazor.Components.Pages;
using Color = MudBlazor.Color;

namespace Xexplorer.Blazor.Utils;

public class DialogUtils
{
    public static IDialogService Service { get; set; }
    public static bool Visible { get; set; }

    public static async Task<DialogResult> Info(string msg,
        string title = "提示",
        Color color = Color.Info,
        string okBtnText = "OK",
        string cancelBtnText = "Cancel",
        bool isShowOkButton = true,
        bool isShowCancelButton = false,
        MaxWidth maxWidth = MaxWidth.Medium,
        bool fullWidth = true)
    {
        var parameters = new DialogParameters<Dialog>
        {
            { x => x.ContentText, msg },
            { x => x.OkBtnText, okBtnText },
            { x => x.CancelBtnText, cancelBtnText },
            { x => x.Color, color },
            { x => x.ShowOK, isShowOkButton },
            { x => x.ShowCancel, isShowCancelButton },
        };

        var options = new DialogOptions() { CloseButton = true, FullWidth = fullWidth, MaxWidth = maxWidth };

        var reference = await Service.ShowAsync<Dialog>(title, parameters, options);
        return await reference?.Result ?? DialogResult.Cancel();
    }

    public static async Task<DialogResult> Error(
        Exception ex = null,
        string msg = null,
        string title = "提示",
        Color color = Color.Error,
        string okBtnText = "OK",
        string cancelBtnText = "Cancel",
        bool isShowOkButton = true,
        bool isShowCancelButton = false,
        MaxWidth maxWidth = MaxWidth.Medium,
        bool fullWidth = true)
    {
        if (ex != null)
            msg = $"{msg}\n{ex}";

        Log.Error(msg);
        
        return await Info(msg, title, color, okBtnText, cancelBtnText, isShowOkButton, isShowCancelButton, maxWidth,
            fullWidth);
    }
    
    public static async Task<DialogResult> Warning(string msg,
        string title = "提示",
        Color color = Color.Warning,
        string okBtnText = "OK",
        string cancelBtnText = "Cancel",
        bool isShowOkButton = true,
        bool isShowCancelButton = false,
        MaxWidth maxWidth = MaxWidth.Medium,
        bool fullWidth = true)
    { 
        return await Info(msg, title, color, okBtnText, cancelBtnText, isShowOkButton, isShowCancelButton, maxWidth,
            fullWidth);
    }

    public static async Task<DialogResult> Carousel(List<string> imags)
    {
        var parameters = new DialogParameters<DialogCarousel>
        {
            { x => x.Snapshots , imags},
        };

        var options = new DialogOptions() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.Large };

        var reference = await Service.ShowAsync<DialogCarousel>(null, parameters, options);
        return await reference?.Result ?? DialogResult.Cancel();
    }
}