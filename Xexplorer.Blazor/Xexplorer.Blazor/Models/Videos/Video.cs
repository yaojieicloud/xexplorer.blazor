using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace XExplorer.Core.Modes;

/// <summary>
/// 表示一个视频实体，用于数据库操作。
/// </summary>
[Table("Videos")]
public class Video : ModeBase
{
    /// <summary>
    /// 获取或设置视频的标题。
    /// </summary>
    [JsonPropertyName("caption")]
    [JsonProperty("caption")]
    public string Caption { get; set; }

    /// <summary>
    /// 获取或设置视频文件的存储目录。
    /// </summary>
    [JsonPropertyName("root_dir")]
    [JsonProperty("root_dir")]
    public string? RootDir { get; set; }

    /// <summary>
    /// 获取或设置视频文件的存储目录。
    /// </summary>
    [JsonPropertyName("video_dir")]
    [JsonProperty("video_dir")]
    public string? VideoDir { get; set; }

    /// <summary>
    /// 获取或设置视频文件的快照存储目录。
    /// </summary>
    [JsonPropertyName("data_dir")]
    [JsonProperty("data_dir")]
    public string? DataDir { get; set; }

    /// <summary>
    /// 获取或设置视频文件的完整路径。
    /// </summary>
    [JsonPropertyName("video_path")]
    [JsonProperty("video_path")]
    public string VideoPath { get; set; }

    /// <summary>
    /// 获取或设置视频的长度（单位：秒）。
    /// </summary>
    [JsonPropertyName("length")]
    [JsonProperty("length")]
    public long Length { get; set; }

    /// <summary>
    /// 获取或设置视频的播放次数。
    /// </summary>
    [JsonPropertyName("play_count")]
    [JsonProperty("play_count")]
    public long PlayCount { get; set; } = 0;

    /// <summary>
    /// 获取或设置视频的最后修改时间。
    /// </summary>
    [JsonPropertyName("modify_time")]
    [JsonProperty("modify_time")]
    public DateTime? ModifyTime { get; set; }

    /// <summary>
    /// 获取或设置视频评价分数。
    /// </summary>
    [JsonPropertyName("evaluate")]
    [JsonProperty("evaluate")]
    public int Evaluate { get; set; } = 0;

    /// <summary>
    /// MD5
    /// </summary>
    [JsonPropertyName("md5")]
    [JsonProperty("md5")]
    public string? MD5 { get; set; }

    /// <summary>
    /// 获取或设置视频的时长（单位：秒）。
    /// </summary>
    [JsonPropertyName("times")]
    [JsonProperty("times")]
    public long? Times { get; set; }

    /// <summary>
    /// 获取或设置视频的宽度。
    /// </summary>
    [JsonPropertyName("width")]
    [JsonProperty("width")]
    public int Width { get; set; }

    /// <summary>
    /// 获取或设置视频的高度。
    /// </summary>
    [JsonPropertyName("height")]
    [JsonProperty("height")]
    public int Height { get; set; }

    /// <summary>
    /// 指示视频是否为宽屏格式。
    /// </summary>
    [JsonPropertyName("wide_screen")]
    [JsonProperty("wide_screen")]
    public bool WideScrenn { get; set; }

    /// <summary>
    /// 获取或设置最小值，该属性未映射到数据库。
    /// </summary>
    [NotMapped]
    public long? Minute => this.Times / 60;

    /// <summary>
    /// Status
    /// </summary>
    [JsonPropertyName("status")]
    [JsonProperty("status")]
    public decimal Status { get; set; } = 1;

    /// <summary>
    /// 快照列表.
    /// </summary>
    [JsonPropertyName("snapshots")]
    [JsonProperty("snapshots")]
    public List<Snapshot> Snapshots { get; set; } = [];

    [NotMapped]
    public List<Snapshot> Images { get; set; } = [];
    
    /// <summary>
    /// 获取或设置视频所属的分组编号。
    /// </summary>
    [NotMapped]
    public int GroupNo { get; set; }
}