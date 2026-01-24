using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Reflection; 
using Microsoft.AspNetCore.WebUtilities; 
using Newtonsoft.Json;
using Serilog;
using Xexplorer.Blazor.Utils;
using Xexplorer.Blazor.ViewModels.Layout;
using XExplorer.Core.Modes;
using Color = Microsoft.Maui.Graphics.Color;

namespace Xexplorer.Blazor.ViewModels.Pages;

/// <summary>
/// 首页视图模型类，继承自ViewModelBase
/// 用于实现首页的数据绑定和业务逻辑
/// </summary>
public class HomeViewModel : ViewModelBase
{
    private List<Video> _videos = new();
 
    /// <summary>
    /// 私有字段，用于存储主视图模型(MainViewModel)的实例
    /// </summary>
    private MainViewModel _mainViewModel;

    /// <summary>
    /// 视频集合属性，用于存储视频列表
    /// </summary>
    /// <remarks>
    /// 这是一个自动实现的属性，初始化为一个空的List<Video>集合
    /// 使用了C# 9.0及更高版本支持的简化初始化语法
    /// </remarks>
    public List<Video> Videos { get; set; } = new();

    /// <summary>
    /// HomeViewModel类的构造函数
    /// </summary>
    /// <param name="mainViewModel">主视图模型(MainViewModel)的实例，用于依赖注入</param>
    public HomeViewModel(MainViewModel mainViewModel)
    {
        this._mainViewModel = mainViewModel;
        if (this._mainViewModel != null)
        {
            this._mainViewModel.OnQuery = QueryAsync;
            this._mainViewModel.OnBathPlay = BathPlayAsync;
            this._mainViewModel.OnStopPlay = StopPlayAsync;
            this._mainViewModel.OnQueryDuplicates = QueryDuplicatesAsync;
        }
    }

    /// <summary>
    /// 异步查询方法，用于在指定目录中搜索包含关键词的内容
    /// </summary>
    /// <param name="dir">要搜索的目录路径</param>
    /// <param name="keyword">搜索关键词</param>
    /// <param name="mode">可选的屏幕模式参数，默认为null</param>
    /// <returns>返回一个Task对象，表示异步操作</returns>
    public async Task QueryAsync(string dir, string keyword, ScreenMode? mode = null)
    {
        DialogUtils.Visible = true;
        try
        {
            this.Videos ??= new List<Video>();
            this.Videos?.Clear();
            await Task.Delay(400);

            // 构建完整的API请求URL，包含基础URL和获取目录信息的API端点
            var api = AppsettingsUtils.Default.Api.GetVideosApi;
            // 创建查询参数字典，包含根目录路径和子目录路径
            var query = new Dictionary<string, string?>
            {
                ["screen"] = mode == null ? string.Empty : $"{(int)mode.Mode}", // 设置根目录路径
                ["dir"] = dir,
                ["keyword"] = keyword
            };


            // 将查询参数添加到URL中
            string apiUrl = QueryHelpers.AddQueryString(api, query);

            // 发送HTTP GET请求并获取响应结果，将结果反序列化为DirEntry对象列表
            var json = await _http.GetStringAsync(apiUrl);
            var result = JsonConvert.DeserializeObject<Result<List<Video>>>(json);
            if (result?.Code == 200)
            {
                this.Videos = result.Data
                    ?.OrderByDescending(m => m.Evaluate)
                    .ThenByDescending(m => m.PlayCount)
                    .ThenByDescending(m => m.Id).ToList();
                this.SetImages(this.Videos);
            }

            NotifyStateChanged();
            SnackbarUtils.Success($"视频查询完成!");
        }
        catch (Exception e)
        {
            Log.Error(e, "查询视频失败");
            DialogUtils.Error(e, $"查询视频失败：{e.Message}");
        }
        finally
        {
            DialogUtils.Visible = false;
        }
    }

    public async Task QueryDuplicatesAsync()
    {
        DialogUtils.Visible = true;
        try
        {
            var api = AppsettingsUtils.Default.Api.GetDuplicatesApi;
            var json = await _http.GetStringAsync(api);
            var result = JsonConvert.DeserializeObject<Result<List<Video>>>(json);
            if (result?.Code == 200)
            {
                this.Videos = result.Data;
                this.SetImages(this.Videos);
                this.SetColors(this.Videos);
            }

            NotifyStateChanged();
            SnackbarUtils.Success($"视频查询完成!");
        }
        catch (Exception e)
        {
            Log.Error(e, "查询视频失败");
            DialogUtils.Error(e, $"查询视频失败：{e.Message}");
        }
        finally
        {
            DialogUtils.Visible = false;
        }
    }

    /// <summary>
    ///     播放指定路径的视频文件。
    /// </summary>
    /// <param name="mode">表示文件路径的对象。</param>
    /// <remarks>
    ///     此方法首先将传入的参数转换为字符串路径，然后检查路径是否为空。如果路径不为空，那么它会使用PotPlayer播放器打开并播放该路径的视频文件，然后增加该视频的播放次数。
    /// </remarks>
    public async Task PlayAsync(Video mode)
    {
        try
        {
            var palyer = AppsettingsUtils.Default.Player.PlayerPath;
            var port = GetPortCmd();
            var currPath = AdjustPath(mode.VideoPath);
#if WINDOWS
            Process.Start(palyer, $"\"{currPath}\" --loop --rate=2.0{port}");
#elif OSX
            Process.Start(palyer,$"--no-one-instance \"{currPath}\" --loop --rate=2.0{port}");
#endif
            mode.PlayCount++;
            await this.SetPlayCount(mode);
            NotifyStateChanged();
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"{MethodBase.GetCurrentMethod().Name} Is Error");
        }
    }

    /// <summary>
    /// 异步添加视频到播放列表的方法
    /// </summary>
    /// <param name="mode">要添加的视频对象</param>
    /// <returns>表示异步操作的任务</returns>
    public async Task AddPlayListAsync(Video mode)
    {
        try
        {
            var port = this._mainViewModel.SelectedPort?.Port ?? Random.Shared.Next(50000, 60000);
            await this.AddPlayListOnlyAsync(mode, port);
            await this.SetPlayCount(mode);
            mode.PlayCount++;
            SnackbarUtils.Success($"视频[{mode.Caption}]已添加到播放列表!");
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"{MethodBase.GetCurrentMethod().Name} Is Error");
        }
    }
    
    /// <summary>
    /// 异步执行洗澡操作的方法
    /// </summary>
    /// <returns>返回一个Task对象，表示异步操作的执行状态</returns>
    public async Task BathPlayAsync()
    {
        try
        {
            var palyer = AppsettingsUtils.Default.Player.PlayerPath;
            var port = this._mainViewModel.SelectedPort?.Port ?? Random.Shared.Next(50000, 60000);
            var portStr = GetPortCmd();
#if WINDOWS
            Process.Start(palyer, $"--no-one-instance --loop --rate=2.0{portStr}");
#else
            Process.Start(palyer, $"--loop --rate=2.0{portStr}");
#endif

            await Task.Delay(500);
            foreach (var videoMode in this.Videos)
                await this.AddPlayListOnlyAsync(videoMode, port);

            await Task.Delay(500);
            await this.StartPlayAsync(port);
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"{MethodBase.GetCurrentMethod().Name} Is Error");
        }
    }

    /// <summary>
    /// 异步停止播放的方法
    /// </summary>
    /// <returns>一个表示异步操作的任务</returns>
    public async Task StopPlayAsync()
    {
#if WINDOWS
        foreach (var process in Process.GetProcessesByName("vlc"))
        {
            try
            {
                process.Kill();
                Log.Information($"Killed process {process.Id} on Windows.");
            }
            catch (Exception ex)
            {
                Log.Error($"Error killing process {process.Id}: {ex}", ex.Message);
            }
        }
#else
        foreach (var process in Process.GetProcessesByName("vlc"))
        {
            try
            {
                process.Kill();
                Log.Information($"Killed process {process.Id} {process.ProcessName} on macOS.");
            }
            catch (Exception ex)
            {
                Log.Error($"Error killing process {process.Id}: {ex}", ex.Message);
            }
        }
#endif

        await Task.CompletedTask;
    }

    /// <summary>
    /// 异步设置视频的评分值
    /// </summary>
    /// <param name="video">要设置评分的视频对象</param>
    /// <param name="newValue">新的评分值</param>
    /// <returns>表示异步操作的任务</returns>
    public async Task SetEvaluateAsync(Video video, int newValue)
    {
        try
        {
            var api = AppsettingsUtils.Default.Api.SetEvaluateApi;
            var body = new { id = video.Id, evaluate = newValue };
            var response = await _http.PostAsJsonAsync(api, body);
            response.EnsureSuccessStatusCode();
            SnackbarUtils.Success($"视频[{video.Caption}]已评分!");
        }
        catch (Exception e)
        {
            await DialogUtils.Error(e);
        }
    }

    /// <summary>
    /// 根据传入的颜色参数获取对应的颜色名称或描述
    /// </summary>
    /// <param name="labelColor">需要获取名称或描述的颜色对象</param>
    /// <returns>返回颜色的名称或描述信息</returns>
    public string GetColor(Color labelColor) =>
        $"#{(int)(labelColor.Red * 255):X2}{(int)(labelColor.Green * 255):X2}{(int)(labelColor.Blue * 255):X2}";

    /// <summary>
    /// 异步删除视频的方法
    /// </summary>
    /// <param name="video">要删除的视频对象</param>
    /// <returns>返回一个Task对象，表示异步操作的执行状态</returns>
    public async Task DeleteVideoAsync(Video video)
    {
        try
        {
            var api = AppsettingsUtils.Default.Api.DeleteVideoApi;
            var body = new { id = video.Id };
            var response = await _http.PostAsJsonAsync(api, body);
            response.EnsureSuccessStatusCode();
            this.Videos.Remove(video);
            SnackbarUtils.Success($"视频[{video.Caption}]已删除!");
        }
        catch (Exception e)
        {
            await DialogUtils.Error(e);
        }
    }

    /// <summary>
    /// 重置视频的异步方法
    /// </summary>
    /// <param name="video">要重置的视频对象</param>
    /// <returns>一个表示异步操作的任务对象</returns>
    public async Task ResetVideoAsync(Video video)
    {
        try
        {
            var api = AppsettingsUtils.Default.Api.ResetVideoApi;
            var body = new { id = video.Id };
            var response = await _http.PostAsJsonAsync(api, body);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<Video>>(json);
            if (result?.Code == 200)
            {
                if (result.Data?.Snapshots?.Any() ?? false)
                    video.Snapshots = result.Data.Snapshots;
            }

            SnackbarUtils.Success($"视频[{video.Caption}]已重置!");
        }
        catch (Exception e)
        {
            await DialogUtils.Error(e);
        }
    }

    /// <summary>
    /// 异步展示轮播视频内容
    /// </summary>
    /// <param name="video">要展示的视频对象，包含视频相关信息</param>
    /// <returns>一个表示异步操作的任务</returns>
    public async Task ShowCarouselAsync(Video video)
    { 
        var snapshots = video.Snapshots.Select(m => m.Path).ToList();
        await DialogUtils.Carousel(snapshots);
    }

    public void OpenFolder(Video video)
    {
        var path = Path.Combine(AppsettingsUtils.Default.Dir.Nas, video.VideoPath);
        var folderPath = Path.GetDirectoryName(path);
        Process.Start(new ProcessStartInfo
        {
            FileName = folderPath,
            UseShellExecute = true
        });
    }
    
    #region private

    /// <summary>
    ///     调整指定路径以适应当前操作系统环境的方法。
    ///     此方法根据配置的系统平台，将路径在 Windows 格式和 Mac 格式之间进行转换，
    ///     确保路径在不同平台上能够正确解析。
    /// </summary>
    /// <param name="path">需要调整的原始路径字符串。</param>
    /// <returns>适配当前操作系统后的路径字符串。</returns>
    private string AdjustPath(string path)
    {
        path = Path.Combine(AppsettingsUtils.Default.Dir.Nas, path);
        return path;
    }

    /// <summary>
    /// 生成可用端口的命令字符串。
    /// 此方法从预定义的端口列表中查找第一个未被占用的端口，
    /// 并生成对应的 HTTP 服务命令字符串，供播放器功能模块使用。
    /// </summary>
    /// <returns>
    /// 包含可用端口的 HTTP 服务命令字符串；
    /// 如果没有找到可用端口，则返回空字符串。
    /// </returns>
    private string GetPortCmd()
    {
        var port = this._mainViewModel.SelectedPort?.Port ?? Random.Shared.Next(50000, 60000);
        var cmd = "--extraintf http --http-port=";
        if (!IsPortInUse(port))
            return $" --extraintf http --http-port={port}"; // 找到未占用的端口  

        return string.Empty; // 没有可用端口
    }

    /// <summary>
    /// 检查指定的端口是否正在使用中。
    /// 此方法通过获取系统当前活动的 TCP 端点列表，判断目标端口是否被占用，
    /// 可以用于网络服务配置或端口冲突的检测场景。
    /// </summary>
    /// <param name="port">要检查的目标端口号。</param>
    /// <returns>如果指定端口正在使用，返回 true；否则返回 false。</returns>
    private bool IsPortInUse(int port)
    {
        var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
        var tcpEndPoints = ipProperties.GetActiveTcpListeners();
        return tcpEndPoints.Any(endPoint => endPoint.Port == port);
    }

    /// <summary>
    /// 设置视频列表对应的图片资源
    /// </summary>
    /// <param name="videos">视频列表，包含需要设置图片的视频信息</param>
    private void SetImages(List<Video> videos)
    {
        var indexs = new int[] { 1, 3, 5, 7 };
        if (!(videos?.Any() ?? false))
            return;

        foreach (var video in videos)
        {
            for (int i = 0; i < video.Snapshots?.Count; i++)
            {
                if (indexs.Contains(i))
                    video.Images.Add(video.Snapshots[i]);
            }
        }
    }

    /// <summary>
    /// 设置视频列表的颜色属性
    /// </summary>
    /// <param name="videos">视频对象列表，用于设置颜色</param>
    private void SetColors(List<Video> videos)
    {
        if (!(videos?.Any() ?? false))
            return;

        foreach (var video in videos)
        {
            if (DicColors.Instance.TryGetValue(video?.GroupNo ?? 0, out var color))
                video.CaptionColor = color;
        }
    }

    /// <summary>
    /// 异步添加视频到播放列表（仅添加操作）
    /// </summary>
    /// <param name="mode">要添加的视频对象</param>
    /// <param name="port">端口号信息</param>
    /// <returns>返回一个异步任务</returns>
    private async Task AddPlayListOnlyAsync(Video mode, int port)
    {
        var palyer = AppsettingsUtils.Default.Player.PlayerPath;
        var currPath = AdjustPath(mode.VideoPath);

        var password = "123456"; // VLC HTTP 接口密码
        var vlcUrl = $"http://localhost:{port}/requests/status.xml";
        var uriPath = ConvertToFileUri(currPath);
        var requestUrl = $"{vlcUrl}?command=in_enqueue&input={Uri.EscapeDataString(uriPath)}";
        using (var handler = new HttpClientHandler { Credentials = new NetworkCredential("", password) })
        using (var client = new HttpClient(handler))
        {
            var response = await client.GetAsync(requestUrl);
            var mesg = response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            if (mesg.StatusCode == HttpStatusCode.OK)
                Log.Information($"视频 [{mode.Id} {mode.VideoPath}] 已加入播放列表。");
            else
                Log.Error($"视频 [{mode.Id} {mode.VideoPath}] 加入播放列表失败。");
        }
    }

    /// <summary>
    /// 将指定的文件系统路径转换为文件 URI 的方法。
    /// 此方法支持普通磁盘路径与 UNC 路径，确保返回符合 URI 格式的字符串。
    /// </summary>
    /// <param name="path">待转换的文件系统路径，可以是普通磁盘路径或 UNC 路径。</param>
    /// <returns>返回一个符合文件 URI 格式的字符串。</returns>
    private string ConvertToFileUri(string path)
    {
        // 无论是 UNC 路径还是本地路径，new Uri(path).AbsoluteUri 都会自动处理：
        // 1. 自动补全 file:/// 协议头
        // 2. 将反斜杠 \ 转换为正斜杠 /
        // 3. 对路径中的 # (变为 %23) 和空格 (变为 %20) 等字符进行标准 URI 编码

        if (string.IsNullOrEmpty(path))
            return string.Empty;

        // GetFullPath 确保路径是规范的绝对路径
        string fullPath = Path.GetFullPath(path);
        return new Uri(fullPath).AbsoluteUri;
    }

    /// <summary>
    /// 异步开始播放的方法
    /// </summary>
    /// <param name="port">指定播放所使用的端口号</param>
    /// <returns>返回一个Task对象，表示异步操作的执行状态</returns>
    private async Task StartPlayAsync(int port)
    {
        var password = "123456"; // VLC HTTP 接口密码
        var vlcUrl = $"http://localhost:{port}/requests/status.xml";
        var requestUrl = $"{vlcUrl}?command=pl_play";
        using (var handler = new HttpClientHandler { Credentials = new NetworkCredential("", password) })
        using (var client = new HttpClient(handler))
        {
            var response = await client.GetAsync(requestUrl);
            var mesg = response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
        }
    }

    /// <summary>
    /// 设置视频播放次数的方法
    /// </summary>
    /// <param name="video">需要设置播放次数的视频对象</param>
    /// <returns>
    /// 返回一个元组，包含两个元素：
    /// - State: 布尔值，表示操作是否成功
    /// - Msg: 字符串，包含操作结果的描述信息
    /// </returns>
    private async Task<(bool State, string Msg)> SetPlayCount(Video video)
    {
        try
        {
            var api = AppsettingsUtils.Default.Api.SetPlayCountApi;
            var body = new { id = video.Id };
            var response = await _http.PostAsJsonAsync(api, body);
            response.EnsureSuccessStatusCode();
            return (State: true, Msg: "播放次数已更新");
        }
        catch (Exception exx)
        {
            Log.Error($"{exx}");
            return (State: false, Msg: exx.ToString());
        }
    }

    #endregion
}