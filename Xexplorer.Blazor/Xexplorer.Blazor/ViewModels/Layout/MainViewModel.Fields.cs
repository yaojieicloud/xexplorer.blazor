using Microsoft.AspNetCore.Components;
using MudBlazor;
using XExplorer.Core.Modes;

namespace Xexplorer.Blazor.ViewModels.Layout;

/// <summary>
/// 主视图模型类，继承自ViewModelBase
/// 该类通常用于应用程序的主界面数据绑定和业务逻辑处理
/// </summary>
public partial class MainViewModel
{
    #region Consts

    /// <summary>
    /// 表示应用程序中左侧通信端口的数值，用于特定功能模块的网络通信和数据传输。
    /// </summary>
    private const int ONE_LEFT_PORT = 34212;

    /// <summary>
    /// 表示应用程序中右侧通信端口的数值，用于特定功能模块的网络通信和数据传输。
    /// </summary>
    private const int ONE_RIGHT_PORT = 34213;

    /// <summary>
    /// 表示应用程序中第二个左侧通信端口的数值，用于特定功能模块的网络通信和数据传输。
    /// </summary>
    private const int TWO_LEFT1_PORT = 34312;

    /// <summary>
    /// 表示应用程序中第二个左侧通信端口的数值，用于特定功能模块的网络通信和数据传输。
    /// </summary>
    private const int TWO_LEFT2_PORT = 34313;

    /// <summary>
    /// 表示应用程序中右侧通信端口的数值，用于特定功能模块的网络通信和数据传输。
    /// </summary>
    private const int TWO_RIGHT1_PORT = 34412;

    /// <summary>
    /// 表示应用程序中右侧通信端口的数值，用于特定功能模块的网络通信和数据传输。
    /// </summary>
    private const int TWO_RIGHT2_PORT = 34412;

    #endregion

    #region Static

    /// <summary>
    /// 表示应用程序中玩家通信端口的数组，包含多个用于特定功能模块的网络通信和数据传输的端口号。
    /// </summary>
    private static int[] PlayPorts =
        { ONE_LEFT_PORT, ONE_RIGHT_PORT, TWO_LEFT1_PORT, TWO_LEFT2_PORT, TWO_RIGHT1_PORT, TWO_RIGHT2_PORT };

    /// <summary>
    /// 支持的图片扩展名列表
    /// </summary>
    private static readonly List<string> picExts = new() { ".jpg", ".png", ".gif", ".bmp" };

    /// <summary>
    /// 支持的视频扩展名列表
    /// </summary>
    private static readonly List<string> videoExts = new()
        { ".mp4", ".avi", ".mkv", ".rmvb", ".wmv", ".ts", ".m4v", ".mov", ".flv" };

    /// <summary>
    /// 视频最小大小（单位：MB）
    /// </summary>
    private readonly decimal videoMiniSize = 110 * 1024 * 1024;

    #endregion

    #region Fields

    /// <summary>
    /// 表示当前状态是否打开的布尔值
    /// </summary>
    private bool _open;

    /// <summary>
    /// 抽屉组件的变体样式，默认值为DrawerVariant.Mini
    /// </summary>
    private DrawerVariant _variant = DrawerVariant.Mini;

    #endregion

    #region Properties

    private readonly NavigationManager _navManager;
    
    /// <summary>
    /// 获取或设置打开状态
    /// 当值改变时会触发状态变更通知
    /// </summary>
    public bool Open
    {
        get => _open;
        set
        {
            if (_open != value)
            {
                _open = value;
                NotifyStateChanged(); // 当状态改变时通知观察者
            }
        }
    }

    /// <summary>
    /// 获取或设置抽屉组件的变体样式
    /// 当值改变时会触发状态变更通知
    /// </summary>
    public DrawerVariant Variant
    {
        get => this._variant;
        set
        {
            if (this._variant != value)
            {
                this._variant = value;
                NotifyStateChanged(); // 当样式改变时通知观察者
            }
        }
    }

    /// <summary>
    /// 目录条目列表
    /// </summary>
    public List<DirEntry> Dirs { get; set; } = new();

    /// <summary>
    /// 当前选中的目录条目
    /// </summary>
    public DirEntry? SelectedDir { get; set; }

    /// <summary>
    /// 端口列表属性，用于存储所有可用的播放端口
    /// </summary>
    public List<PlayPort> Ports { get; set; }

    /// <summary>
    /// 当前选中的端口属性，默认初始化为左侧端口
    /// </summary>
    /// <remarks>
    /// 默认值创建一个新的PlayPort实例，名称属性设置为"ONE_LEFT_PORT"的字符串表示，端口值使用ONE_LEFT_PORT常量
    /// </remarks>
    public PlayPort? SelectedPort { get; set; }

    /// <summary>
    /// 屏幕模式列表属性，用于存储所有可用的屏幕模式
    /// 使用自动属性初始化器创建一个新的空列表
    /// </summary>
    public List<ScreenMode> ScrennModes { get; set; } = new();

    /// <summary>
    /// 当前选中的屏幕模式属性
    /// 用于存储用户当前选择的屏幕模式
    /// </summary>
    public ScreenMode? SelectedMode { get; set; }

    /// <summary>
    /// 关键字列表属性
    /// 用于存储一组字符串类型的关键字
    /// </summary>
    public List<string> Keywords { get; set; }

    /// <summary>
    /// 当前选中的关键字属性
    /// 用于存储用户当前选中的单个关键字
    /// </summary>
    public string SelectedKeyword { get; set; }

    /// <summary>
    /// 存储已选择的关键字集合
    /// </summary>
    public IEnumerable<string> SelectedKeywords = new HashSet<string>();

    /// <summary>
    /// 当前路径属性
    /// </summary>
    public string CurrentPath => new Uri(_navManager.Uri).AbsolutePath; 
    
    /// <summary>
    /// 是否为图片页面
    /// </summary>
    public bool IsImagesPage => CurrentPath.Equals("/images", StringComparison.OrdinalIgnoreCase); 
    
    /// <summary>
    /// 是否为视频页面
    /// </summary>
    public bool IsRootPage => CurrentPath.Equals("/", StringComparison.OrdinalIgnoreCase);
    
    #endregion
}