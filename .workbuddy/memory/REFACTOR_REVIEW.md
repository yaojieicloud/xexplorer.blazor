# Xexplorer.Blazor 代码重构与优化建议

> 基于 .NET MAUI + Blazor Hybrid 多媒体浏览器项目的源码审查

## 一、架构层面（高优先级）

### 1. 依赖注入 / HttpClient 管理
**问题**：`ViewModelBase` 在每个子类构造函数中手写 `new HttpClient()`，BaseAddress 硬编码到 `AppsettingsUtils.Default.Api.BaseUrl`，且 Timeout=5 小时。
- 无法通过 DI 替换 HttpClient（如注入 Polly 策略、MockHttp）
- 静态配置耦合，测试极其困难
- `AppsettingsUtils.Default` 是静态单例，导致 ViewModel 无法被正确 Unit Test

**建议**：注册 `IHttpClientFactory`，按服务注入。

### 2. 静态工具类耦合 MudBlazor 生命周期
**问题**：`DialogUtils` / `SnackbarUtils` 是 `static` 类，依赖 `IDialogService` / `ISnackbar` 的静态属性注入。
- 生命周期管理不可控
- 在 Scoped ViewModel 中调用 static 服务，容易在多平台 MAUI Shell 中拿到 null

**建议**：改为普通 service 或封装 `IDialogService` 为实例方法。

### 3. 主视图模型“委派爆炸”
**问题**：`MainViewModel` 通过 `Func<Task>` 委派子页操作（OnQuery, OnBathPlay 等），类似事件总线但不是强类型。
- 委托为 null 时静默丢弃（e.g. `?.Invoke()`），排障困难
- 子页面 VM 在构造函数中硬编码绑定到 `MainViewModel`

**建议**：使用 Mediator/EventAggregator 或 `IObservable` 替代。最少改为抽象接口 `IVideoQueryService`。

### 4. JSON 序列化混用
**问题**：
- 模型层同时贴了 `System.Text.Json` 和 `Newtonsoft.Json` 的 attribute（JsonProperty + JsonPropertyName）
- `InitDirsAsync` 用 `JsonSerializer.Deserialize<Result<List<DirEntry>>>`，`QueryAsync` 用 `Newtonsoft.Json.JsonConvert`
- 双序列化器共存导致版本不匹配风险

**建议**：统一使用一种序列化器。鉴于 `MudBlazor` / `Serilog` 生态，优先保留 Newtonsoft 或迁移到 System.Text.Json。

### 5. Model 与 UI 耦合
**问题**：`Video.CaptionColor` 类型是 `Microsoft.Maui.Graphics.Color`（UI 层类型），直接出现在 Core Modes 命名空间实体里。
- 导致 Core 层引用 MAUI 程序集，破坏分层

**建议**：把 `CaptionColor` 改成 `string`（hex）或 `int?`（group_no）在 Core 层，UI 层映射。

## 二、代码质量（中优先级）

### 6. 构造函数里的 Fire-and-Forget 异步
**问题**：`MainViewModel` 构造函数里直接调用 `this.InitDirsAsync();` 不 await，且无错误处理。
- 如果 API 异常，构造函数外无法感知
- Blazor MAUI 生命周期内，初始化时序不确定

**建议**：改为 `async Task InitializeAsync()` 并显式在 `OnInitializedAsync` 中 await。

### 7. 无 CancellationToken / 并发控制
**问题**：`FolderCleanAsync` 在循环中多次并发 POST；`BathPlayAsync` 无取消；`QueryAsync` 无超时。

**建议**：传入 `CancellationToken`，使用 `SemaphoreSlim` 限制并发，配置 Polly。

### 8. 日志反射开销
**问题**：`Log.Error(ex, $"{MethodBase.GetCurrentMethod().Name} Is Error");` 大量使用 `MethodBase.GetCurrentMethod()`，性能差。

**建议**：改为 `nameof(MethodName)`，或用 Source Generator 记录调用点。

### 9. 平台条件编译散落
**问题**：`#if WINDOWS` / `#elif OSX` 在多个 ViewModel 里出现，代码重复。

**建议**：封装到 `IPlayerLauncher` 接口，按平台 DI 注入。

### 10. 重复且无效代码
- `PlayPorts` 静态数组声明但未使用
- `picExts` / `videoExts` 列表声明但无引用
- `videoMiniSize` 字段声明但无引用
- `InitKeywords()` 包含不当关键字（"乱伦"）
- `TWO_RIGHT1_PORT = TWO_RIGHT2_PORT = 34412` 端口重复

## 三、健壮性与安全（中优先级）

### 11. 无重试 / 无降级
**问题**：所有 HTTP 调用单次失败即弹错。

**建议**：增加 Polly 重试策略；对非关键操作（如 PlayCount 上报）做 fire-and-forget 降级。

### 12. VLC HTTP 密码硬编码
**问题**：`"123456"` 硬编码在 `AddPlayListOnlyAsync` / `StartPlayAsync`。

**建议**：放入配置中心或 secrets 管理。

### 13. UI 路径拼接与异常处理
- `GetBreadcrumbs(string path)` 若 `path` 为 null 直接 `path.Split('/')` 抛异常
- `GetInfoString` 疑似错误：`{video.Minute} Minute {video.Minute} MB`（应该是 Length）

## 四、建议交付物

1. **统一 HttpClient 注入** + 封装 `IApiClient`（含 Get/Post 统一方法）
2. **MainViewModel 拆职责**：目录管理 / 播放控制 / 搜索 拆到独立 Service
3. **取消 static 工具类**，改为实例注入
4. **统一 JSON 序列化器**
5. **模型层去除 MAUI 依赖**
6. **Unit Test 框架搭建**
