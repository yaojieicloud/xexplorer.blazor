namespace Xexplorer.Blazor.ViewModels;

public class ViewModelBase
{
    /// <summary>
    /// The current state of the view model.
    /// </summary>
    public event Action? StateChanged;

    /// <summary>
    /// 通知状态已更改的方法
    /// 当状态发生改变时调用此方法，触发状态改变事件
    /// </summary>
    protected void NotifyStateChanged() => StateChanged?.Invoke();
}