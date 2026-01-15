using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Xexplorer.Blazor.ViewModels.Layout;
using XExplorer.Core.Modes;

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
            this._mainViewModel.OnQuery = QueryAsync;
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
        for (int i = 0; i < 50; i++)
        {
            var video = new Video
            {
                Id = i,
                Caption = $"视频{i}",
                Snapshots = new()
                {
                    new Snapshot { Id = i + 1, Path = $"00b3d36d9ee64d85a165b054c8b72eab" },
                    new Snapshot { Id = i + 2, Path = $"0a2d9fcbc8cc43b0b651dc1c7fc8a89c" },
                    new Snapshot { Id = i + 3, Path = $"0a724ec1c03447b6a52bce17e7c7a71a" },
                    new Snapshot { Id = i + 4, Path = $"0a6d26b3605d41ecb6fad3120cc1aba1" }
                }
            };

            Videos.Add(video);
        }

        NotifyStateChanged();
        await Task.CompletedTask;
    }
}