using System;

namespace Cyclone.Persistence.Common.Models
{
    /// <summary>
    /// 表示数据库连接指标
    /// </summary>
    public class ConnectionMetrics
    {
        /// <summary>
        /// 获取连接打开次数
        /// </summary>
        public int OpenCount { get; private set; }

        /// <summary>
        /// 获取连接关闭次数
        /// </summary>
        public int CloseCount { get; private set; }

        /// <summary>
        /// 获取连接错误次数
        /// </summary>
        public int ErrorCount { get; private set; }

        /// <summary>
        /// 获取连接超时次数
        /// </summary>
        public int TimeoutCount { get; private set; }

        /// <summary>
        /// 获取连接池大小
        /// </summary>
        public int PoolSize { get; private set; }

        /// <summary>
        /// 获取连接池中的活动连接数
        /// </summary>
        public int ActiveConnections { get; private set; }

        /// <summary>
        /// 获取连接池中的空闲连接数
        /// </summary>
        public int IdleConnections { get; private set; }

        /// <summary>
        /// 获取上次活动时间
        /// </summary>
        public DateTime LastActivity { get; private set; }

        /// <summary>
        /// 获取平均连接时间（毫秒）
        /// </summary>
        public double AverageConnectionTime { get; private set; }

        /// <summary>
        /// 增加连接打开次数
        /// </summary>
        public void IncrementOpenCount()
        {
            OpenCount++;
            LastActivity = DateTime.UtcNow;
        }

        /// <summary>
        /// 增加连接关闭次数
        /// </summary>
        public void IncrementCloseCount()
        {
            CloseCount++;
            LastActivity = DateTime.UtcNow;
        }

        /// <summary>
        /// 增加连接错误次数
        /// </summary>
        public void IncrementErrorCount()
        {
            ErrorCount++;
            LastActivity = DateTime.UtcNow;
        }

        /// <summary>
        /// 增加连接超时次数
        /// </summary>
        public void IncrementTimeoutCount()
        {
            TimeoutCount++;
            LastActivity = DateTime.UtcNow;
        }

        /// <summary>
        /// 更新连接池信息
        /// </summary>
        /// <param name="poolSize">连接池大小</param>
        /// <param name="activeConnections">活动连接数</param>
        /// <param name="idleConnections">空闲连接数</param>
        public void UpdatePoolInfo(int poolSize, int activeConnections, int idleConnections)
        {
            PoolSize = poolSize;
            ActiveConnections = activeConnections;
            IdleConnections = idleConnections;
            LastActivity = DateTime.UtcNow;
        }

        /// <summary>
        /// 更新平均连接时间
        /// </summary>
        /// <param name="connectionTimeMs">连接时间（毫秒）</param>
        public void UpdateAverageConnectionTime(double connectionTimeMs)
        {
            if (OpenCount == 0)
            {
                AverageConnectionTime = connectionTimeMs;
            }
            else
            {
                AverageConnectionTime = ((AverageConnectionTime * (OpenCount - 1)) + connectionTimeMs) / OpenCount;
            }
            LastActivity = DateTime.UtcNow;
        }

        /// <summary>
        /// 重置所有指标
        /// </summary>
        public void Reset()
        {
            OpenCount = 0;
            CloseCount = 0;
            ErrorCount = 0;
            TimeoutCount = 0;
            PoolSize = 0;
            ActiveConnections = 0;
            IdleConnections = 0;
            AverageConnectionTime = 0;
            LastActivity = DateTime.UtcNow;
        }
    }
}