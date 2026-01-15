using System.Net.Http.Json;
using System.Text.Json;
using MudBlazor;
using Xexplorer.Blazor.Utils;
using XExplorer.Core.Modes;
using Microsoft.AspNetCore.WebUtilities;
using Xexplorer.Blazor.Enums;


namespace Xexplorer.Blazor.ViewModels.Layout;

/// <summary>
/// 主视图模型类，继承自ViewModelBase
/// 该类通常用于应用程序的主界面数据绑定和业务逻辑处理
/// </summary>
public partial class MainViewModel
{
    /// <summary>
    /// 初始化端口的方法
    /// </summary>
    private void InItPorts()
    {
        this.Ports = new();
        this.Ports.Add(new PlayPort { Name = nameof(ONE_LEFT_PORT), Port = ONE_LEFT_PORT });
        this.Ports.Add(new PlayPort { Name = nameof(ONE_RIGHT_PORT), Port = ONE_RIGHT_PORT });
        this.Ports.Add(new PlayPort { Name = nameof(TWO_LEFT1_PORT), Port = TWO_LEFT1_PORT });
        this.Ports.Add(new PlayPort { Name = nameof(TWO_LEFT2_PORT), Port = TWO_LEFT2_PORT });
        this.Ports.Add(new PlayPort { Name = nameof(TWO_RIGHT1_PORT), Port = TWO_RIGHT1_PORT });
        this.Ports.Add(new PlayPort { Name = nameof(TWO_RIGHT2_PORT), Port = TWO_RIGHT2_PORT });
        this.SelectedPort = this.Ports[0];
    }

    /// <summary>
    /// 初始化屏幕显示的私有方法
    /// </summary>
    private void InitScrenn()
    {
        this.ScrennModes = new();
        this.ScrennModes.Add(new ScreenMode { Name = Enum.GetName(ScreenEnum.Wide), Mode = ScreenEnum.Wide });
        this.ScrennModes.Add(new ScreenMode { Name = Enum.GetName(ScreenEnum.Narrow), Mode = ScreenEnum.Narrow });
    }

    /// <summary>
    /// 初始化关键字的方法
    /// 用于初始化系统所需的关键字数据
    /// </summary>
    private void InitKeywords()
    {
        this.Keywords = new();
        this.Keywords.Add("妈");
        this.Keywords.Add("母");
        this.Keywords.Add("姐");
        this.Keywords.Add("爸");
        this.Keywords.Add("父");
        this.Keywords.Add("哥");
        this.Keywords.Add("嫂");
        this.Keywords.Add("妹");
        this.Keywords.Add("乱伦");
        this.SelectedKeyword = string.Empty;
    }

    /// <summary>
    /// 异步初始化目录信息
    /// 该方法通过调用API获取指定目录下的子目录和文件信息
    /// </summary>
    /// <returns>Task 异步任务</returns>
    private async Task InitDirsAsync()
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