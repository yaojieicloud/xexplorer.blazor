# 依赖清单

## 核心 SDK
- .NET 10.0 SDK (Multi-targeting: android; ios; maccatalyst; windows10.0.19041.0)
- Microsoft.NET.Sdk.Razor (Blazor Hybrid / MAUI)

## NuGet 包

| 包 | 版本 | 用途 |
|----|------|------|
| Microsoft.AspNetCore.WebUtilities | 10.0.2 | HTTP 工具 / URL 构造 |
| Microsoft.Maui.Controls | $(MauiVersion) | MAUI UI 控件 |
| Microsoft.AspNetCore.Components.WebView.Maui | $(MauiVersion) | MAUI 中的 Blazor WebView |
| Microsoft.Extensions.Logging.Debug | 10.0.0 | 调试日志 |
| MudBlazor | 9.0.0-preview.1 | Material Design UI 组件库 |
| Newtonsoft.Json | 13.0.5-beta1 | JSON 序列化 / 反序列化 |
| Serilog.Enrichers.Thread | 4.0.0 | 日志线程 ID 增强 |
| Serilog.Sinks.File | 8.0.0-dev-02318 | 文件日志输出 |

## 资源依赖
- 字体：AlimamaFangYuanTiVF-Thin.ttf、OpenSans-Regular.ttf
- 图标：appicon.svg, splash.svg, dotnet_bot.svg
- 平台资源：AndroidManifest.xml, Info.plist, App.xaml, Package.appxmanifest
