using System;
using Cyclone.Persistence.Abstractions.Repository;
using Cyclone.Persistence.Abstractions.UnitOfWork;

namespace Cyclone.Persistence.Abstractions.Provider;

/// <summary>
/// 定义 ORM 适配器的接口
/// </summary>
public interface IOrmAdapter
{
    /// <summary>
    /// 获取适配器名称
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 创建仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>实体仓储</returns>
    IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class;

    /// <summary>
    /// 创建工作单元
    /// </summary>
    /// <returns>工作单元</returns>
    IUnitOfWork CreateUnitOfWork();

    /// <summary>
    /// 创建工作单元并指定连接字符串
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <returns>工作单元</returns>
    IUnitOfWork CreateUnitOfWork(string connectionString);

    /// <summary>
    /// 获取或设置数据库提供程序
    /// </summary>
    IDbProvider DbProvider { get; set; }

    /// <summary>
    /// 配置 ORM
    /// </summary>
    /// <param name="configuration">配置 Action</param>
    void Configure(Action<object> configuration);

    /// <summary>
    /// 初始化 ORM
    /// </summary>
    void Initialize();
}