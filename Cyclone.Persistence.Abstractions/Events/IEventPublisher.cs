using System.Threading;
using System.Threading.Tasks;

namespace Cyclone.Persistence.Abstractions.Events;

/// <summary>
/// 定义事件发布者的接口
/// </summary>
public interface IEventPublisher
{
    /// <summary>
    /// 发布事件
    /// </summary>
    /// <typeparam name="TEvent">事件类型</typeparam>
    /// <param name="event">事件</param>
    void Publish<TEvent>(TEvent @event) where TEvent : IEvent;

    /// <summary>
    /// 异步发布事件
    /// </summary>
    /// <typeparam name="TEvent">事件类型</typeparam>
    /// <param name="event">事件</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent;
}