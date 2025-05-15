using System.Threading;
using System.Threading.Tasks;

namespace Cyclone.Persistence.Abstractions.Events;

/// <summary>
/// 定义事件订阅者的接口
/// </summary>
/// <typeparam name="TEvent">事件类型</typeparam>
public interface IEventSubscriber<in TEvent> where TEvent : IEvent
{
    /// <summary>
    /// 处理事件
    /// </summary>
    /// <param name="event">事件</param>
    void Handle(TEvent @event);

    /// <summary>
    /// 异步处理事件
    /// </summary>
    /// <param name="event">事件</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}