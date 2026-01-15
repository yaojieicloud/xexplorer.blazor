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
    #endregion
}