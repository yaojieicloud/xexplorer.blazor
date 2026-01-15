using System.ComponentModel;

namespace Xexplorer.Blazor.ViewModels;

public class ViewModelBase
{
    /// <summary>
    /// 当状态发生改变时触发的事件
    /// </summary>
    public event Action? OnChange;
    
    /// <summary>
    /// 通知状态已发生改变，触发OnChange事件
    /// </summary>
    protected void NotifyStateChanged() => OnChange?.Invoke();


}