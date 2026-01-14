using System.Net.Http.Json;
using System.Text.Json;
using MudBlazor;
using Xexplorer.Blazor.Utils;
using XExplorer.Core.Modes;
using Microsoft.AspNetCore.WebUtilities;


namespace Xexplorer.Blazor.ViewModels.Layout;

/// <summary>
/// 主视图模型类，继承自ViewModelBase
/// 该类通常用于应用程序的主界面数据绑定和业务逻辑处理
/// </summary>
public class MainViewModel : ViewModelBase
{
    private readonly HttpClient _http;

    private bool _open;

    public bool Open
    {
        get => _open;
        set
        {
            if (_open != value)
            {
                _open = value;
                NotifyStateChanged();
            }
        }
    }

    private DrawerVariant _variant = DrawerVariant.Mini;

    public DrawerVariant Variant
    {
        get => this._variant;
        set
        {
            if (this._variant != value)
            {
                this._variant = value;
                NotifyStateChanged();
            }
        }
    }

    public List<DirEntry> Dirs { get; set; } = new();
    public DirEntry? SelectedDir { get; set; }

    /// <summary>
    /// 切换抽屉状态的私有方法
    /// </summary>
    public void ToggleDrawer()
    {
        // 通过取反操作来切换_open的状态值
        this.Open = !this.Open;
        this.Variant = this.Open ? DrawerVariant.Persistent : DrawerVariant.Mini;
    }

    /// <summary>
    /// 主视图模型的构造函数
    /// </summary>
    public MainViewModel()
    {
        _http = new HttpClient() { BaseAddress = new Uri(AppsettingsUtils.Default.Api.BaseUrl) };
        this.InitDirsAsync();
    }

    /// <summary>
    /// 异步初始化目录信息
    /// 该方法通过调用API获取指定目录下的子目录和文件信息
    /// </summary>
    /// <returns>Task 异步任务</returns>
    public async Task InitDirsAsync()
    {
        // 构建完整的API请求URL，包含基础URL和获取目录信息的API端点
        var api = AppsettingsUtils.Default.Api.BaseUrl + AppsettingsUtils.Default.Api.GetDirsApi;
        // 创建查询参数字典，包含根目录路径和子目录路径
        var query = new Dictionary<string, string?>
        {
            ["root"] = AppsettingsUtils.Default.Dir.VideoDir, // 设置根目录路径
            ["dir"] = AppsettingsUtils.Default.Dir.SubDir // 设置子目录路径
        };

        // 将查询参数添加到URL中
        string apiUrl = QueryHelpers.AddQueryString(api, query);

        // 发送HTTP GET请求并获取响应结果，将结果反序列化为DirEntry对象列表
        var json = await _http.GetStringAsync(apiUrl);
        var result = JsonSerializer.Deserialize<Result<List<DirEntry>>>(json);
        this.Dirs = result?.Data ?? new();
    }
}