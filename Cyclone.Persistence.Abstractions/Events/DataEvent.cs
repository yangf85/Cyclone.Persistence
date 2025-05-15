using System;

namespace Cyclone.Persistence.Abstractions.Events;

/// <summary>
/// 数据事件基类
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public abstract class DataEvent<TEntity> : IEvent where TEntity : class
{
    /// <summary>
    /// 获取事件 ID
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();

    /// <summary>
    /// 获取事件发生的时间
    /// </summary>
    public DateTime Timestamp { get; } = DateTime.UtcNow;

    /// <summary>
    /// 获取实体
    /// </summary>
    public TEntity Entity { get; private set; }

    /// <summary>
    /// 初始化数据事件
    /// </summary>
    /// <param name="entity">实体</param>
    protected DataEvent(TEntity entity)
    {
        Entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }
}

/// <summary>
/// 实体添加事件
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public class EntityAddedEvent<TEntity> : DataEvent<TEntity> where TEntity : class
{
    /// <summary>
    /// 初始化实体添加事件
    /// </summary>
    /// <param name="entity">实体</param>
    public EntityAddedEvent(TEntity entity) : base(entity)
    {
    }
}

/// <summary>
/// 实体更新事件
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public class EntityUpdatedEvent<TEntity> : DataEvent<TEntity> where TEntity : class
{
    /// <summary>
    /// 获取原始实体
    /// </summary>
    public TEntity OriginalEntity { get; private set; }

    /// <summary>
    /// 初始化实体更新事件
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="originalEntity">原始实体</param>
    public EntityUpdatedEvent(TEntity entity, TEntity originalEntity) : base(entity)
    {
        OriginalEntity = originalEntity;
    }
}

/// <summary>
/// 实体删除事件
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public class EntityDeletedEvent<TEntity> : DataEvent<TEntity> where TEntity : class
{
    /// <summary>
    /// 初始化实体删除事件
    /// </summary>
    /// <param name="entity">实体</param>
    public EntityDeletedEvent(TEntity entity) : base(entity)
    {
    }
}