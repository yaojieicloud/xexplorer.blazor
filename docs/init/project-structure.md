# 项目结构

## 目录树

```
xexplorer.blazor/
├── Xexplorer.Blazor/                      # 主应用项目
│   ├── Components/                        # Blazor 组件
│   │   ├── Layout/                        # 布局组件 MainLayout, NavLayout
│   │   └── Pages/                         # 页面：Home, Images
│   ├── Services/                          # 服务层
│   │   ├── ApiClient.cs
│   │   ├── SettingsService.cs
│   │   ├── Config/
│   │   └── Extensions/
│   ├── Utilities/                         # 工具类
│   │   ├── HttpUtils.cs
│   │   └── NetClientUtils.cs
│   ├── Models/                            # 数据模型
│   │   ├── HomeViewModels.cs
│   │   ├── ImagesViewModels.cs
│   │   ├── DirModels.cs
│   │   └── MediaPlayerModels.cs
│   ├── appsettings.json
│   ├── Program.cs
│   └── Xexplorer.Blazor.csproj
├── Xexplorer.Blazor-Android/              # Android 平台项目
├── Xexplorer.Blazor-iOS/                  # iOS 平台项目
├── Xexplorer.Blazor-MacCatalyst/          # Mac Catalyst 平台项目
├── Xexplorer.Blazor-Windows/              # Windows 平台项目
├── docs/                                  # 项目文档
│   ├── overview.md
│   ├── init/
│   │   ├── project-structure.md
│   │   ├── dependencies.md
│   │   └── architecture.md
│   ├── requirements/
│   ├── design/
│   ├── tasks/
│   └── issues/
├── README.md
└── .gitignore
```

## 目录职责

- `Components/Layout`: 应用整体布局与导航抽屉实现
- `Components/Pages`: 各功能页面的 UI 与事件绑定
- `Services`: API 客户端、设置持久化、本地存储访问
- `Utilities`: 通用工具方法，如 HTTP 请求封装、网络客户端工厂
- `Models`: DTO 与 ViewModel，负责数据传递与状态管理
- `docs/`: it-workflow 要求的文档体系
