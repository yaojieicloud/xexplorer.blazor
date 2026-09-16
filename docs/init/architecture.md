# 架构说明

## 应用类型
Blazor Hybrid + .NET MAUI，跨平台桌面/移动端。

## 模块划分

- **UI 层 (Components)**
  - Layout：NavLayout / MainLayout，统一导航抽屉与页面容器
  - Pages：Home/Index（浏览器内嵌）、Images（图片/视频管理）
  - Interactive components：异步加载、权限控制、下拉刷新

- **服务层 (Services)**
  - ApiClient：对外 HTTP 调用统一封装
  - SettingsService：本地设置持久化
  - Config/Extensions：扩展方法与配置键值
  - HttpUtils / NetClientUtils：底层网络工具

- **模型层 (Models)**
  - HomeViewModels、ImagesViewModels：页面状态模型
  - DirModels、MediaPlayerModels：目录与媒体播放数据结构

- **资源层**
  - Resources/AppIcon、Splash、Images、Fonts、Raw：平台资源打包

## 数据流
外部资源 → NetClientUtils → ApiClient → Services → ViewModels → Components

## 平台适配
- Android / iOS / MacCatalyst / Windows 共用业务代码，平台特定入口通过 MAUI Shell
- `appsettings.json` 统一配置，`CopyToOutputDirectory=Always` 确保打包时随应用
