using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cyclone.Persistence.Common.Utils
{
    /// <summary>
    /// 提供可释放对象的基类
    /// </summary>
    public abstract class Disposable : IDisposable, IAsyncDisposable
    {
        private bool _disposed;
        private int _disposeFlag;

        /// <summary>
        /// 获取是否已释放
        /// </summary>
        protected bool IsDisposed => _disposed;

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (Interlocked.Exchange(ref _disposeFlag, 1) == 0)
            {
                _disposed = true;
                GC.SuppressFinalize(this);
                Dispose(true);
            }
        }

        /// <summary>
        /// 异步释放资源
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        public async ValueTask DisposeAsync()
        {
            if (Interlocked.Exchange(ref _disposeFlag, 1) == 0)
            {
                _disposed = true;
                GC.SuppressFinalize(this);
                await DisposeAsyncCore().ConfigureAwait(false);
                Dispose(false);
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing">是否正在释放托管资源</param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// 异步释放资源
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        protected virtual ValueTask DisposeAsyncCore()
        {
            return new ValueTask();
        }

        /// <summary>
        /// 检查是否已释放
        /// </summary>
        /// <exception cref="ObjectDisposedException">对象已释放时抛出</exception>
        protected void CheckDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~Disposable()
        {
            Dispose(false);
        }
    }
}