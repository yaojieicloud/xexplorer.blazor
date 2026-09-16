# xexplorer.blazor

.NET MAUI Blazor Hybrid 多媒体浏览器应用，支持 Windows / Android / iOS / MacCatalyst 多平台部署。

## 项目简介

本项目是一个多媒体资源浏览器，主要功能包括：
- 视频列表浏览与播放
- 图片画廊浏览
- 目录管理（获取/清空/重复检测）
- 与后端 API 交互（通过 http://127.0.0.1:35888）

**技术栈**
- .NET 10.0 (MAUI + Blazor Hybrid)
- MudBlazor 9.0.0-preview.1
- Serilog 日志
- Newtonsoft.Json

## 文档导航

| 文档 | 说明 |
|------|------|
| [需求总览](docs/overview.md) | 业务需求与功能点 |
| [项目结构](docs/init/project-structure.md) | 目录树与职责说明 |
| [依赖清单](docs/init/dependencies.md) | 外部 NuGet 包与版本 |
| [架构设计](docs/init/architecture.md) | 架构模式、模块、数据流 |
| [进度跟踪](docs/PROGRESS.md) | 任务执行状态 |

## 快速开始

### 环境要求
- .NET 10.0 SDK
- Visual Studio 2022 或 JetBrains Rider
- macOS 开发者证书（iOS/MacCatalyst 构建需要）

### 构建运行
```bash
cd Xexplorer.Blazor/Xexplorer.Blazor
dotnet build
dotnet run -f net10.0-windows10.0.19041.0
```

### 配置说明
修改 `appsettings.json` 中的 API 地址与路径配置：
- `Api.BaseUrl`: 后端服务地址
- `Dir.VideoDir`: 视频目录
- `Player.PlayerPath`: 媒体播放器路径

## 开发规范
遵循 it-workflow 项目管理流程：
- `/需求` → 录入需求
- `/设计 REQ-{N}` → 方案设计
- `/任务 REQ-{N}-{M}-{K}` → 执行任务
- `/BUG BUG-{N}` → 修复问题
