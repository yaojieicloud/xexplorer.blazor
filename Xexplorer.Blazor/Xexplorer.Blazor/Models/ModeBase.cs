using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace XExplorer.Core.Modes;

/// <summary>
/// 实体基类
/// </summary>
public abstract class ModeBase
{
    [Key] 
    [JsonPropertyName("id")]
    [JsonProperty("id")]
    public long Id { get; set; }
}