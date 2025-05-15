using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cyclone.Persistence.Common.Utils
{
    /// <summary>
    /// 表示带有超时检查的任务执行结果
    /// </summary>
    /// <typeparam name="T">任务结果的类型</typeparam>
    public class TimeoutResult<T>
    {
        /// <summary>
        /// 获取一个值，指示任务是否在超时前成功完成
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// 获取任务的结果。如果任务超时，则为默认值
        /// </summary>
        public T Result { get; }

        /// <summary>
        /// 初始化 <see cref="TimeoutResult{T}"/> 类的新实例
        /// </summary>
        /// <param name="success">指示任务是否在超时前成功完成</param>
        /// <param name="result">任务的结果</param>
        public TimeoutResult(bool success, T result)
        {
            Success = success;
            Result = result;
        }
    }

    /// <summary>
    /// 提供任务运行的工具类
    /// </summary>
    public static class TaskRunner
    {
        /// <summary>
        /// 运行任务并忽略异常
        /// </summary>
        /// <param name="task">任务</param>
        /// <param name="onException">异常处理函数</param>
        public static void RunIgnoreError(Task task, Action<Exception> onException = null)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            task.ContinueWith(t =>
            {
                if (t.IsFaulted && t.Exception != null)
                {
                    onException?.Invoke(t.Exception.GetBaseException());
                }
            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
        }

        /// <summary>
        /// 运行任务并忽略异常
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="task">任务</param>
        /// <param name="onException">异常处理函数</param>
        /// <returns>结果</returns>
        public static T RunIgnoreError<T>(Task<T> task, Func<Exception, T> onException = null)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            try
            {
                return task.Result;
            }
            catch (AggregateException ex)
            {
                var innerException = ex.GetBaseException();
                return onException != null ? onException(innerException) : default;
            }
        }

        /// <summary>
        /// 异步运行任务并忽略异常
        /// </summary>
        /// <param name="task">任务</param>
        /// <param name="onException">异常处理函数</param>
        /// <returns>表示异步操作的任务</returns>
        public static async Task RunIgnoreErrorAsync(Task task, Action<Exception> onException = null)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                onException?.Invoke(ex);
            }
        }

        /// <summary>
        /// 异步运行任务并忽略异常
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="task">任务</param>
        /// <param name="onException">异常处理函数</param>
        /// <returns>结果</returns>
        public static async Task<T> RunIgnoreErrorAsync<T>(Task<T> task, Func<Exception, T> onException = null)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            try
            {
                return await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return onException != null ? onException(ex) : default;
            }
        }

        /// <summary>
        /// 运行任务并重试
        /// </summary>
        /// <param name="action">操作</param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="retryInterval">重试间隔</param>
        /// <param name="shouldRetry">是否应该重试的判断函数</param>
        /// <returns>表示异步操作的任务</returns>
        public static async Task RunWithRetryAsync(Func<Task> action, int retryCount = 3, TimeSpan? retryInterval = null, Func<Exception, bool> shouldRetry = null)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (retryCount < 0)
                throw new ArgumentOutOfRangeException(nameof(retryCount), "重试次数不能小于0");

            var interval = retryInterval ?? TimeSpan.FromSeconds(1);
            var attempt = 0;

            while (true)
            {
                try
                {
                    await action().ConfigureAwait(false);
                    break;
                }
                catch (Exception ex)
                {
                    if (++attempt > retryCount || (shouldRetry != null && !shouldRetry(ex)))
                        throw;

                    await Task.Delay(interval).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// 运行任务并重试
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="action">操作</param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="retryInterval">重试间隔</param>
        /// <param name="shouldRetry">是否应该重试的判断函数</param>
        /// <returns>结果</returns>
        public static async Task<T> RunWithRetryAsync<T>(Func<Task<T>> action, int retryCount = 3, TimeSpan? retryInterval = null, Func<Exception, bool> shouldRetry = null)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (retryCount < 0)
                throw new ArgumentOutOfRangeException(nameof(retryCount), "重试次数不能小于0");

            var interval = retryInterval ?? TimeSpan.FromSeconds(1);
            var attempt = 0;

            while (true)
            {
                try
                {
                    return await action().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    if (++attempt > retryCount || (shouldRetry != null && !shouldRetry(ex)))
                        throw;

                    await Task.Delay(interval).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// 运行任务，带超时
        /// </summary>
        /// <param name="task">任务</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>表示异步操作的任务</returns>
        /// <exception cref="TimeoutException">任务超时时抛出</exception>
        public static async Task RunWithTimeoutAsync(Task task, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            if (timeout <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(timeout), "超时时间必须大于0");

            using var timeoutCts = new CancellationTokenSource(timeout);
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(timeoutCts.Token, cancellationToken);

            var completedTask = await Task.WhenAny(task, Task.Delay(Timeout.Infinite, linkedCts.Token)).ConfigureAwait(false);

            if (completedTask == task)
            {
                // 任务完成，取消延迟任务
                linkedCts.Cancel();
                await task.ConfigureAwait(false);
            }
            else
            {
                // 超时或取消
                if (timeoutCts.IsCancellationRequested)
                    throw new TimeoutException($"任务在 {timeout.TotalSeconds} 秒内未完成");

                // 取消
                throw new OperationCanceledException(cancellationToken);
            }
        }

        /// <summary>
        /// 运行任务，带超时
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="task">任务</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>结果</returns>
        /// <exception cref="TimeoutException">任务超时时抛出</exception>
        public static async Task<T> RunWithTimeoutAsync<T>(Task<T> task, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            if (timeout <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(timeout), "超时时间必须大于0");

            using var timeoutCts = new CancellationTokenSource(timeout);
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(timeoutCts.Token, cancellationToken);

            var completedTask = await Task.WhenAny(task, Task.Delay(Timeout.Infinite, linkedCts.Token)).ConfigureAwait(false);

            if (completedTask == task)
            {
                // 任务完成，取消延迟任务
                linkedCts.Cancel();
                return await task.ConfigureAwait(false);
            }
            else
            {
                // 超时或取消
                if (timeoutCts.IsCancellationRequested)
                    throw new TimeoutException($"任务在 {timeout.TotalSeconds} 秒内未完成");

                // 取消
                throw new OperationCanceledException(cancellationToken);
            }
        }

        /// <summary>
        /// 运行任务，带超时，返回是否超时
        /// </summary>
        /// <param name="task">任务</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>是否超时</returns>
        public static async Task<bool> RunWithTimeoutFlagAsync(Task task, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            if (timeout <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(timeout), "超时时间必须大于0");

            using var timeoutCts = new CancellationTokenSource(timeout);
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(timeoutCts.Token, cancellationToken);

            var completedTask = await Task.WhenAny(task, Task.Delay(Timeout.Infinite, linkedCts.Token)).ConfigureAwait(false);

            if (completedTask == task)
            {
                // 任务完成，取消延迟任务
                linkedCts.Cancel();
                await task.ConfigureAwait(false);
                return false;
            }
            else
            {
                // 超时或取消
                if (timeoutCts.IsCancellationRequested)
                    return true;

                // 取消
                throw new OperationCanceledException(cancellationToken);
            }
        }
    }
}