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


    /// <summary>
    /// 定义一个委托属性，用于洗澡播放的异步回调函数
    /// 该属性是一个返回Task的委托，可以异步执行洗澡播放相关的操作
    /// </summary>
    public Func<Task> OnBathPlay { get; set; }

    /// <summary>
    /// 异步执行洗澡播放的方法
    /// 如果OnBathPlay委托不为null，则调用该委托并等待其执行完成
    /// </summary>
    /// <returns>返回一个Task对象，代表异步操作的执行</returns>
    public async Task BathPlayAsync() => await this.OnBathPlay?.Invoke();

    /// <summary>
    /// 定义一个委托属性，用于在停止播放时执行的操作
    /// 该属性是一个返回Task的委托，可以异步执行停止播放的相关逻辑
    /// </summary>
    public Func<Task> OnStopPlay { get; set; }

    /// <summary>
    /// 异步停止播放的方法
    /// 如果OnStopPlay委托不为空，则调用该委托执行停止播放操作
    /// 使用null条件运算符确保在委托为null时不会引发异常
    /// </summary>
    /// <returns>返回一个Task对象，表示异步操作的执行</returns>
    public async Task StopPlayAsync() => await this.OnStopPlay?.Invoke();

    /// <summary>
    /// 定义一个委托属性，用于在查询重复文件时执行的操作
    /// </summary>
    public Func<Task> OnQueryDuplicates { get; set; }
    
    /// <summary>
    /// 异步查询重复文件的方法
    /// </summary>
    public async Task QueryDuplicatesAsync() => await this.OnQueryDuplicates?.Invoke();
    
    #endregion
}