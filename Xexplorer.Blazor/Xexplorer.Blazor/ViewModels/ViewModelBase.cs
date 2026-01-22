using System.ComponentModel;
using Xexplorer.Blazor.Utils;

namespace Xexplorer.Blazor.ViewModels;

public class ViewModelBase
{
    protected HttpClient _http;

    /// <summary>
    /// 当状态发生改变时触发的事件
    /// </summary>
    public event Action? OnChange;

    /// <summary>
    /// 通知状态已发生改变，触发OnChange事件
    /// </summary>
    protected void NotifyStateChanged() => OnChange?.Invoke();

    /// <summary>
    /// ViewModelBase 类的默认构造函数
    /// </summary>
    public ViewModelBase()
    {
        this._http = new HttpClient() { BaseAddress = new Uri(AppsettingsUtils.Default.Api.BaseUrl) ,Timeout = TimeSpan.FromHours(5) };
    }
}