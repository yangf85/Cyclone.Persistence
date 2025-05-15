# Cyclone.Persistence

![License](https://img.shields.io/github/license/yourusername/Cyclone.Persistence)
![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-blue)
![.NET Standard](https://img.shields.io/badge/.NET%20Standard-2.0-blue)

Cyclone.Persistence 是一个灵活的数据库持久层框架，旨在提供统一的数据访问接口，同时支持多种数据库和 ORM 框架。它使您能够以一致的方式操作不同的数据库，同时保留各种 ORM 框架的特性和优势。

## 主要特性

- **多数据库支持**：内置支持 SQLite, SQL Server, PostgreSQL, LiteDB 等数据库
- **ORM 框架集成**：无缝集成 Entity Framework Core 和 FreeSql
- **统一 API**：提供一致的 API 体验，无论使用何种底层数据库和 ORM
- **可扩展性**：易于添加新的数据库提供程序和 ORM 适配器
- **跨平台兼容**：同时支持 .NET Framework 4.8 和 .NET Standard 2.0
- **轻松切换**：运行时轻松切换数据库提供程序
- **高级特性**：内置缓存、事务和迁移支持

## 项目结构

Cyclone.Persistence 遵循模块化设计原则，分为以下几个主要组件：

### 核心组件

- **Cyclone.Persistence.Abstractions**：定义所有接口和抽象类
- **Cyclone.Persistence.Common**：提供通用工具和辅助类
- **Cyclone.Persistence.Core**：实现核心功能和基础设施

### 数据库提供程序

- **Cyclone.Persistence.LiteDB**：LiteDB 数据库实现
- **Cyclone.Persistence.SQLite**：SQLite 数据库实现
- **Cyclone.Persistence.SqlServer**：SQL Server 数据库实现
- **Cyclone.Persistence.PostgreSQL**：PostgreSQL 数据库实现

### ORM 集成

- **Cyclone.Persistence.FreeSql**：FreeSql ORM 集成
- **Cyclone.Persistence.EFCore**：Entity Framework Core 集成

### 扩展功能

- **Cyclone.Persistence.Caching**：缓存扩展
- **Cyclone.Persistence.Transactions**：事务管理扩展
- **Cyclone.Persistence.Migration**：数据迁移扩展

## 安装

### NuGet 包

```bash
# 安装核心包
dotnet add package Cyclone.Persistence.Core

# 安装数据库提供程序
dotnet add package Cyclone.Persistence.SQLite
dotnet add package Cyclone.Persistence.SqlServer
dotnet add package Cyclone.Persistence.PostgreSQL
dotnet add package Cyclone.Persistence.LiteDB

# 安装 ORM 集成
dotnet add package Cyclone.Persistence.EFCore
dotnet add package Cyclone.Persistence.FreeSql

# 安装扩展功能
dotnet add package Cyclone.Persistence.Caching
dotnet add package Cyclone.Persistence.Transactions
dotnet add package Cyclone.Persistence.Migration

# 快速开始
// 配置服务
services.AddCyclonePersistence(options => {
    options.UseProvider<SQLiteProvider>()
           .WithConnectionString("Data Source=app.db")
           .EnableQueryLogging();
});

// 注册仓储
services.AddScoped<IRepository<User>, Repository<User>>();

// 使用仓储
public class UserService
{
    private readonly IRepository<User> _userRepository;
    
    public UserService(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User> GetUserAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }
}

# 与 Entity Framework Core 集成

services.AddCyclonePersistence(options => {
    options.UseProvider<SqlServerProvider>()
           .WithConnectionString("Server=...;Database=...")
           .WithORM<EFCoreAdapter>(orm => {
                orm.UseModel<AppDbContext>();
           });
});

# 与 FreeSql 集成

services.AddCyclonePersistence(options => {
    options.UseProvider<PostgreSQLProvider>()
           .WithConnectionString("Host=localhost;Database=mydb;")
           .WithORM<FreeSqlAdapter>(orm => {
                orm.ConfigureFreeSql(fsql => {
                    fsql.CodeFirst.IsAutoSyncStructure(true);
                });
           });
});

# 高级特性
## 事务处理
using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
{
    try
    {
        var repository = unitOfWork.GetRepository<Product>();
        
        var product = await repository.GetByIdAsync(id);
        product.Price = 19.99m;
        await repository.UpdateAsync(product);
        
        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();
    }
    catch
    {
        await unitOfWork.RollbackTransactionAsync();
        throw;
    }
}
## 缓存支持
services.AddCyclonePersistence(options => {
    options.UseProvider<SQLiteProvider>()
           .WithConnectionString("Data Source=app.db")
           .UseCache(cache => {
                cache.UseMemoryCache()
                     .WithExpiration(TimeSpan.FromMinutes(10));
           });
});
