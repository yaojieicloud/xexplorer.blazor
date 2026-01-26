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
/// Represents the view model used for managing and interacting with image-related data and operations
/// in the application.
/// </summary>
public class ImagesViewModel : ViewModelBase
{
    /// <summary>
    /// 私有字段，用于存储主视图模型(MainViewModel)的实例
    /// </summary>
    private MainViewModel _mainViewModel;

    /// <summary>
    /// 属性，用于存储图片的列表。
    /// </summary>
    public List<string> Images { get; set; } = new();

    /// <summary>
    /// HomeViewModel类的构造函数
    /// </summary>
    /// <param name="mainViewModel">主视图模型(MainViewModel)的实例，用于依赖注入</param>
    public ImagesViewModel(MainViewModel mainViewModel)
    {
        this._mainViewModel = mainViewModel;
        if (this._mainViewModel != null)
        {
            this._mainViewModel.OnQueryImages = QueryAsync;
            this.QueryAsync(this._mainViewModel.SelectedDir?.Name);
        }
    }

    /// <summary>
    /// 异步查询指定目录中的图像文件。
    /// </summary>
    /// <param name="dir">要查询的目标目录路径，如果为 null，则使用默认目录。</param>
    /// <returns>表示异步操作的任务。</returns>
    public async Task QueryAsync(string dir = null)
    {
        DialogUtils.Visible = true;
        try
        {
            var api = AppsettingsUtils.Default.Api.GetImagesApi;
            var query = new Dictionary<string, string?> { ["dir"] = dir };
            var apiUrl = QueryHelpers.AddQueryString(api, query);
            var response = await _http.GetStringAsync(apiUrl);
            var result = JsonConvert.DeserializeObject<Result<List<string>>>(response);
            if (result?.Code == 200)
            {
                this.Images = result?.Data;
                SnackbarUtils.Success($"图片查询完成!");
            }
            else
            {
                SnackbarUtils.Error($"图片查询失败! {result?.Msg}");
            }

            NotifyStateChanged();
        }
        catch (Exception e)
        {
            Log.Error(e, "图片查询失败");
            DialogUtils.Error(e, $"图片查询失败：{e.Message}");
        }
        finally
        {
            DialogUtils.Visible = false;
        }
    }

    /// <summary>
    /// 异步展示轮播视频内容
    /// </summary>
    /// <param name="path">要展示的图片</param>
    /// <returns>一个表示异步操作的任务</returns>
    public async Task ShowCarouselAsync(string path)
    {
        var snapshots = new List<string>() { path };
        await DialogUtils.Carousel(snapshots, AppsettingsUtils.Default.Api.GetPicApi);
    }
}