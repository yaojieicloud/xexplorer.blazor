using System.Text.Json.Serialization;

namespace XExplorer.Core.Modes;

/// <summary>
/// 目录条目类，用于表示目录中的文件或文件夹信息
/// </summary>
public class DirEntry
{
    /// <summary>
    /// 获取或设置名称属性
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    /// <summary>
    /// 获取或设置路径属性
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; set; }
}