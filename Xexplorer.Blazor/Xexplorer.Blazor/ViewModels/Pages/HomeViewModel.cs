using XExplorer.Core.Modes;

namespace Xexplorer.Blazor.ViewModels.Pages;

/// <summary>
/// 首页视图模型类，继承自ViewModelBase
/// 用于实现首页的数据绑定和业务逻辑
/// </summary>
public class HomeViewModel : ViewModelBase
{
    public List<Video> Videos { get; set; } = new();

    public HomeViewModel()
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
    }
}