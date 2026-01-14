namespace XExplorer.Core.Modes.Settings;

/// <summary>
/// 应用程序配置类
/// 用于存储和管理应用程序的全局设置和配置信息
/// </summary>
public class Appsettings
{
    /// <summary>
    /// API配置属性，包含API相关的设置信息
    /// </summary>
    public ApiConf Api { get; set; }

    /// <summary>
    /// 获取或设置目录配置对象
    /// </summary>
    /// <value>DirConf类型的目录配置实例</value>
    public DirConf Dir { get; set; } = new();
}

/// <summary>
/// API配置类，用于存储API相关配置信息
/// </summary>
public class ApiConf
{
    /// <summary>
    /// API基础URL属性，用于获取或设置API的基础地址
    /// </summary>
    public string BaseUrl { get; set; }

    /// <summary>
    /// 获取或设置图像API的地址
    /// </summary>
    public string GetImageApi { get; set; }
    
    public string GetDirsApi { get; set; }
}

/// <summary>
/// 目录配置类，用于存储和管理不同类型资源的目录路径
/// </summary>
public class DirConf
{
    /// <summary>
    /// 数据目录路径，默认值为"/data"
    /// </summary>
    public string DataDir { get; set; } = "/data";

    /// <summary>
    /// 视频目录路径，默认值为"/video"
    /// </summary>
    public string VideoDir { get; set; } = "/video";

    /// <summary>
    /// 子目录名称，默认值为"01_成人资源"
    /// </summary>
    public string SubDir { get; set; } = "01_成人资源";
}