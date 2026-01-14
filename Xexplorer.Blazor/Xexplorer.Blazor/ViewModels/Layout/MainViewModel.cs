using MudBlazor;

namespace Xexplorer.Blazor.ViewModels.Layout;

/// <summary>
/// 主视图模型类，继承自ViewModelBase
/// 该类通常用于应用程序的主界面数据绑定和业务逻辑处理
/// </summary>
public class MainViewModel : ViewModelBase
{
    private bool _open;

    public bool Open
    {
        get => _open;
        set
        {
            if (_open != value)
            {
                _open = value;
                NotifyStateChanged();
            }
        }
    }
    
    private DrawerVariant _variant = DrawerVariant.Mini;

    public DrawerVariant Variant
    {
        get => this._variant;
        set
        {
            if (this._variant != value)
            {
                this._variant = value;
                NotifyStateChanged();
            }
        }
    }
    
    /// <summary>
    /// 切换抽屉状态的私有方法
    /// </summary>
    public void ToggleDrawer()
    {
        // 通过取反操作来切换_open的状态值
        this.Open = !this.Open;
        this.Variant = this.Open ? DrawerVariant.Persistent : DrawerVariant.Mini;
    }
}