using System.Text.Json.Serialization;

namespace XExplorer.Core.Modes;

/// <summary>
/// 表示一个通用的结果类，用于封装API响应信息
/// </summary>
public class Result
{
    /// <summary>
    /// 获取或设置响应代码，用于表示请求的状态或结果类型
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }

    /// <summary>
    /// 获取或设置响应消息，用于描述请求的状态或结果信息
    /// </summary>
    [JsonPropertyName("msg")]
    public string Msg { get; set; }
}

/// <summary>
/// 泛型结果类，继承自非泛型Result类
/// 用于包装包含特定类型数据的结果
/// </summary>
/// <typeparam name="T">数据类型参数</typeparam>
public class Result<T> : Result
{
    /// <summary>
    /// 获取或设置结果数据
    /// 使用JsonPropertyName特性指定JSON序列化时的属性名为"data"
    /// </summary>
    [JsonPropertyName("data")]
    public T Data { get; set; }
}