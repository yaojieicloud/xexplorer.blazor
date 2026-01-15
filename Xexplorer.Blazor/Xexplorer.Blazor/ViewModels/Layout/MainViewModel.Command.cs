using MudBlazor;
using XExplorer.Core.Modes;

namespace Xexplorer.Blazor.ViewModels.Layout;

/// <summary>
/// 主视图模型类，继承自ViewModelBase
/// 该类通常用于应用程序的主界面数据绑定和业务逻辑处理
/// </summary>
public partial class MainViewModel
{
    #region Events
 
    /// <summary>
    /// 定义一个查询事件委托，用于处理查询操作
    /// </summary>
    /// <remarks>
    /// 该委托接受三个参数：
    /// - 第一个参数为字符串类型，表示选定的目录名称
    /// - 第二个参数为字符串类型，表示选定的关键字
    /// - 第三个参数为ScreenMode?可空类型，表示选定的模式
    /// 返回类型为Task，表示异步操作
    /// </remarks>
    public Func<string, string, ScreenMode?, Task>? OnQuery { get; set; }

    /// <summary>
    /// 异步执行查询操作的方法
    /// </summary>
    /// <remarks>
    /// 该方法会检查OnQuery委托是否有值，如果有，则调用它并传入三个选定的参数：
    /// - 当前选定的目录名称
    /// - 当前选定的关键字
    /// - 当前选定的模式
    /// 使用null条件运算符确保在OnQuery为null时不会引发异常
    /// </remarks>
    public async Task QueryAsync() =>
        await this.OnQuery?.Invoke(this.SelectedDir?.Name, this.SelectedKeyword, this.SelectedMode);

    #endregion
}