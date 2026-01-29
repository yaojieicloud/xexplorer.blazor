using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Xexplorer.Blazor.Utils;
using XExplorer.Core.Modes;
using Microsoft.AspNetCore.WebUtilities;
using Xexplorer.Blazor.Components.Pages;


namespace Xexplorer.Blazor.ViewModels.Layout;

/// <summary>
/// 主视图模型类，继承自ViewModelBase
/// 该类通常用于应用程序的主界面数据绑定和业务逻辑处理
/// </summary>
public partial class MainViewModel : ViewModelBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="navManager">导航管理</param>
    public MainViewModel(NavigationManager navManager)
    {
        this._navManager = navManager;
        this.InitDirsAsync();
        this.InItPorts();
        this.InitScrenn();
        this.InitKeywords();
    }

    #region api

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
    /// 异步解析方法
    /// </summary>
    /// <returns>返回一个Task对象，表示异步操作的完成</returns>
    public async Task ParseAsync()
    {
        try
        {
            if (this.SelectedDir == null)
            {
                await DialogUtils.Warning("请选择一个目录");
                return;
            }

            var dir = this.SelectedDir?.Name;
            var api = AppsettingsUtils.Default.Api.ParseVideosApi;
            // 创建查询参数字典，包含根目录路径和子目录路径
            var body = new { dir = dir, root = "" };

            // 发送HTTP GET请求并获取响应结果，将结果反序列化为DirEntry对象列表
            var response = await _http.PostAsJsonAsync(api, body);
            response.EnsureSuccessStatusCode();
            await this.QueryAsync();
            SnackbarUtils.Success($"解析完成!");
        }
        catch (Exception e)
        {
            await DialogUtils.Error(e);
        }
    }

    public async Task UnzipAsync()
    {
        try
        {
            if (this.SelectedDir == null)
            {
                await DialogUtils.Warning("请选择一个目录");
                return;
            }

            var pwdApi = AppsettingsUtils.Default.Api.GetPasswordsApi;
            var unzipApi = AppsettingsUtils.Default.Api.UnZipApi;
            var pwds = await _http.GetFromJsonAsync<string[]>(pwdApi);
            var body = new
            {
                dir = this.SelectedDir.Name,
                root = "/videos",
                is_del_zip = true,
                passwords = pwds
            };

            var response = await _http.PostAsJsonAsync(unzipApi, body);
            response.EnsureSuccessStatusCode();
            await DialogUtils.Info("解压完成");
        }
        catch (Exception e)
        {
            await DialogUtils.Error(e);
        }
    }

    /// <summary>
    /// 异步计算MD5值的异步方法
    /// </summary>
    /// <returns>
    /// 返回一个Task对象，表示异步操作的执行状态
    /// 该Task在完成时将包含计算得到的MD5哈希值
    /// </returns>
    public async Task CalcMd5Async()
    {
        try
        {
            var caclMd5ApiApi = AppsettingsUtils.Default.Api.CaclMd5Api;
            var body = new { max_workers = 5 };
            await _http.PostAsJsonAsync(caclMd5ApiApi, body);
            await DialogUtils.Info("MD5 计算已完成.");
        }
        catch (Exception e)
        {
            await DialogUtils.Error(e);
        }
    }


    /// <summary>
    /// 异步清理文件夹的方法
    /// </summary>
    /// <returns>返回一个Task对象，代表异步操作</returns>
    public async Task FolderCleanAsync()
    {
        try
        {
            var api = AppsettingsUtils.Default.Api.FolderCleanAPI;
            if (this.SelectedDir == null || string.IsNullOrWhiteSpace(this.SelectedDir.Name))
            {
                SnackbarUtils.Warning("请先选择一个文件夹");
                return;
            }

            var body = new { dir = this.SelectedDir.Name, pic_size_limit = 3 };
            await _http.PostAsJsonAsync(api, body);
            await DialogUtils.Info($"文件夹 [{this.SelectedDir.Name}] 资源清理完成.");
        }
        catch (Exception e)
        {
            await DialogUtils.Error(e);
        }
    }

    /// <summary>
    /// 异步清理快照的方法
    /// </summary>
    /// <returns>返回一个Task对象，表示异步操作的完成</returns>
    public async Task SnapshotsCleanAsync()
    {
        try
        {
            var api = AppsettingsUtils.Default.Api.SnapshotsCleanApi;
            var body = new { dir = string.Empty };
            await _http.PostAsJsonAsync(api, body);
            await DialogUtils.Info($"视频快照资源清理完成.");
        }
        catch (Exception e)
        {
            await DialogUtils.Error(e);
        }
    }

    #endregion
}