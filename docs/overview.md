# 需求总览

## 项目目标
开发一套基于 .NET MAUI + Blazor Hybrid 的多平台多媒体浏览器，主要面向本地视频资源浏览、图片查看、目录管理与媒体播放。

## 功能模块

| 模块 | 描述 | 关联文件 |
|------|------|----------|
| 视频浏览 | 按目录/关键字查询视频，支持评分、删除、重置、查看快照 | Home.razor / HomeViewModel.cs |
| 图片浏览 | 图片画廊展示 | Images.razor / ImagesViewModel.cs |
| 目录管理 | 获取目录列表、清空文件夹、解压缩、MD5计算 | MainViewModel.cs |
| 重复检测 | 查询重复视频文件 | MainViewModel.cs |
| 播放控制 | 批量播放、停止播放、单视频播放、播放列表 | HomeViewModel.cs |
| 配置管理 | 从 appsettings.json 读取 API 地址、目录路径、播放器路径 | Appsettings.cs / AppsettingsUtils.cs |
| 布局与导航 | 抽屉导航、顶部工具栏、滚动定位 | MainLayout.razor / NavLayout.razor |

## 非功能约束
- 多平台支持：Windows / Android / iOS / MacCatalyst
- 后端依赖：http://127.0.0.1:35888 提供的 REST API
- 媒体播放器：VLC / PotPlayer，通过本地 HTTP 接口控制播放列表
- 日志：Serilog 写入文件

## 当前状态
- 项目处于初始开发阶段
- 需求总数：1（多媒体浏览器）
- 已完成：0
- 进行中：1
