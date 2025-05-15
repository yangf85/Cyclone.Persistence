using System;

namespace Cyclone.Persistence.Abstractions.Events;

/// <summary>
/// 定义事件的接口
/// </summary>
public interface IEvent
{
    /// <summary>
    /// 获取事件 ID
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// 获取事件发生的时间
    /// </summary>
    DateTime Timestamp { get; }
}