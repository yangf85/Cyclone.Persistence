using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cyclone.Persistence.Common.Utils
{
    /// <summary>
    /// 提供异步锁定的实用工具
    /// </summary>
    public class AsyncLock
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly Task<Releaser> _releaser;

        /// <summary>
        /// 初始化异步锁
        /// </summary>
        public AsyncLock()
        {
            _releaser = Task.FromResult(new Releaser(this));
        }

        /// <summary>
        /// 锁定
        /// </summary>
        /// <returns>锁定对象</returns>
        public Task<Releaser> LockAsync()
        {
            var wait = _semaphore.WaitAsync();
            return wait.IsCompleted
                ? _releaser
                : wait.ContinueWith((_, state) => new Releaser((AsyncLock)state),
                    this, CancellationToken.None,
                    TaskContinuationOptions.ExecuteSynchronously,
                    TaskScheduler.Default);
        }

        /// <summary>
        /// 锁定
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>锁定对象</returns>
        public Task<Releaser> LockAsync(CancellationToken cancellationToken)
        {
            var wait = _semaphore.WaitAsync(cancellationToken);
            return wait.IsCompleted
                ? _releaser
                : wait.ContinueWith((_, state) => new Releaser((AsyncLock)state),
                    this, cancellationToken,
                    TaskContinuationOptions.ExecuteSynchronously,
                    TaskScheduler.Default);
        }

        /// <summary>
        /// 锁定
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>锁定对象</returns>
        public async Task<Releaser> LockAsync(TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            if (await _semaphore.WaitAsync(timeout, cancellationToken).ConfigureAwait(false))
                return new Releaser(this);

            throw new TimeoutException("获取锁超时");
        }

        /// <summary>
        /// 尝试锁定
        /// </summary>
        /// <param name="releaser">锁定对象</param>
        /// <returns>是否成功锁定</returns>
        public bool TryLock(out Releaser releaser)
        {
            if (_semaphore.Wait(0))
            {
                releaser = new Releaser(this);
                return true;
            }

            releaser = default;
            return false;
        }

        /// <summary>
        /// 释放器
        /// </summary>
        public readonly struct Releaser : IDisposable
        {
            private readonly AsyncLock _toRelease;

            internal Releaser(AsyncLock toRelease)
            {
                _toRelease = toRelease;
            }

            /// <summary>
            /// 释放锁
            /// </summary>
            public void Dispose()
            {
                _toRelease?._semaphore.Release();
            }
        }
    }
}