using Xexplorer.Blazor.Enums;

namespace XExplorer.Core.Modes;

/// <summary>
/// 表示屏幕模式的类
/// </summary>
public class ScreenMode
{
    /// <summary>
    /// 获取或设置屏幕模式的名称
    /// </summary>
    public string Name { get; set; }
   
    /// <summary>
    /// 获取或设置屏幕模式的枚举值
    /// </summary>
    public ScreenEnum Mode { get; set; }
}