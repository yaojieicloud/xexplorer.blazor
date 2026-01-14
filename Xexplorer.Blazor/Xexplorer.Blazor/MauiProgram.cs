using System.Reflection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Xexplorer.Blazor.ViewModels;

namespace Xexplorer.Blazor;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddMudServices();
        builder.ConfigureMauiHandlers(handlers => { });
        // 扫描当前程序集  
        builder.Services.AddAllViewModels(Assembly.GetExecutingAssembly());
        // 如果你有其他程序集（例如单独的 ViewModel 项目），可以传入对应的 Assembly 
        // builder.Services.AddAllViewModels(typeof(SomeViewModel).Assembly);

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();
        return app;
    }

    /// <summary>
    /// 扫描指定程序集，把所有继承自 ViewModelBase 的类注册为 Scoped
    /// </summary>
    public static void AddAllViewModels(this IServiceCollection services, Assembly assembly)
    {
        var viewModelTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(ViewModelBase)));
        foreach (var vmType in viewModelTypes)
        {
            services.AddScoped(vmType);
        }
    }
}