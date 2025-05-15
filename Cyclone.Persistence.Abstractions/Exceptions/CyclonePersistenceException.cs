using System;
using System.Runtime.Serialization;

namespace Cyclone.Persistence.Abstractions.Exceptions;

/// <summary>
/// Cyclone.Persistence 框架异常基类
/// </summary>
[Serializable]
public class CyclonePersistenceException : Exception
{
    /// <summary>
    /// 初始化异常
    /// </summary>
    public CyclonePersistenceException() : base()
    {
    }

    /// <summary>
    /// 使用指定的错误消息初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    public CyclonePersistenceException(string message) : base(message)
    {
    }

    /// <summary>
    /// 使用指定的错误消息和内部异常初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="innerException">内部异常</param>
    public CyclonePersistenceException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// 使用序列化数据初始化异常
    /// </summary>
    /// <param name="info">序列化信息</param>
    /// <param name="context">流上下文</param>
    protected CyclonePersistenceException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}