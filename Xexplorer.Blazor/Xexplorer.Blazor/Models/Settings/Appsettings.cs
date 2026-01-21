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

    /// <summary>
    /// 玩家配置属性
    /// </summary>
    /// <remarks>
    /// 用于获取或设置玩家相关的配置信息
    /// </remarks>
    public PlayerConf Player { get; set; }
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

    /// <summary>
    /// 获取目录的API地址属性
    /// </summary>
    public string GetDirsApi { get; set; }

    /// <summary>
    /// 解压缩文件的API地址属性
    /// </summary>
    public string UnZipApi { get; set; }

    /// <summary>
    /// 获取视频API的属性
    /// </summary>
    public string GetVideosApi { get; set; }
    
    /// <summary>
    /// 获取视频信息的API地址属性
    /// </summary>
    public string ParseVideosApi { get; set; }
    
    /// <summary>
    /// 获取密码的API地址属性
    /// </summary>
    public string GetPasswordsApi { get; set; }
    
    /// <summary>
    /// 计算MD5 API
    /// </summary>
    public string CaclMd5Api { get; set; }
    
    /// <summary>
    /// 获取视频信息的API地址属性
    /// </summary>
    public string SetEvaluateApi{ get; set; }
    
    /// <summary>
    /// 获取视频信息的API地址属性
    /// </summary>
    public string SetPlayCountApi{ get; set; }
}

/// <summary>
/// 目录配置类，用于存储和管理不同类型资源的目录路径
/// </summary>
public class DirConf
{
    /// <summary>
    /// 获取或设置NAS的路径
    /// </summary>
    /// <value>NAS路径的字符串</value>
    public string Nas { get; set; }

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

/// <summary>
/// 玩家配置类，用于存储玩家相关的配置信息
/// </summary>
public class PlayerConf
{
    /// <summary>
    /// 玩家路径属性，用于获取或设置玩家相关的路径信息
    /// </summary>
    public string PlayerPath { get; set; }
}