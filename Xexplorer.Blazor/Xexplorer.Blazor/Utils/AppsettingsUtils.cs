using System.Text.Json;
using System.Text.Json.Serialization;
using XExplorer.Core.Modes.Settings;

namespace Xexplorer.Blazor.Utils;

public class AppsettingsUtils
{
    /// <summary>
    /// 获取应用程序的默认设置。
    /// </summary>
    public static Appsettings? Default { get; private set; }

    /// <summary>
    /// 从指定的 JSON 文件路径加载应用程序设置。
    /// </summary>
    /// <param name="jsonPath">包含应用程序设置的 JSON 文件的路径。</param>
    public static void LoadJson(string jsonPath = null)
    {
        if (string.IsNullOrWhiteSpace(jsonPath))
            jsonPath = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
        
        if (!File.Exists(jsonPath))
            throw new FileNotFoundException(jsonPath);
    
        var jsonTxt = File.ReadAllText(jsonPath);
        Default = JsonSerializer.Deserialize<Appsettings>(jsonTxt);
    }
}