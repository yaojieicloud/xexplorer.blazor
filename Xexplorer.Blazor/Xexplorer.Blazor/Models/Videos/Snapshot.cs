using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace XExplorer.Core.Modes;

/// <summary>
/// 表示视频快照的实体类。
/// </summary>
[Table("Snapshots")]
public class Snapshot : ModeBase
{
    /// <summary>
    /// 获取或设置与此快照关联的视频的标识符。
    /// </summary>
    [ForeignKey("Video")]
    [JsonPropertyName("video_id")]
    [JsonProperty("video_id")]
    public long VideoId { get; set; }

    /// <summary>
    /// 获取或设置快照的文件路径。
    /// </summary>
    [JsonPropertyName("path")]
    [JsonProperty("path")]
    public string Path { get; set; }
}