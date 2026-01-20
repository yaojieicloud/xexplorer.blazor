using System.Net.Http.Json;
using System.Text.Json;
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
    /// 主视图模型的构造函数
    /// </summary>
    public MainViewModel()
    {
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
            var dir = this.SelectedDir?.Name;
            var api = AppsettingsUtils.Default.Api.ParseVideosApi;
            // 创建查询参数字典，包含根目录路径和子目录路径
            var body = new { dir = dir, root = "" };

            // 发送HTTP GET请求并获取响应结果，将结果反序列化为DirEntry对象列表
            var response = await _http.PostAsJsonAsync(api, body);
            response.EnsureSuccessStatusCode();
            await this.QueryAsync();
            DialogUtils.Info("解析完成");
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
    
    #endregion
}