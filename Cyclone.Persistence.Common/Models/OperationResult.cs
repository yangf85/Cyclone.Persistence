using System;

namespace Cyclone.Persistence.Common.Models
{
    /// <summary>
    /// 表示操作结果
    /// </summary>
    /// <typeparam name="T">结果数据类型</typeparam>
    public class OperationResult<T>
    {
        /// <summary>
        /// 获取或设置操作是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 获取或设置结果数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 获取或设置消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 获取或设置错误
        /// </summary>
        public Exception Error { get; set; }

        /// <summary>
        /// 初始化操作结果
        /// </summary>
        public OperationResult()
        {
        }

        /// <summary>
        /// 初始化操作结果
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="message">消息</param>
        public OperationResult(bool success, string message = null)
        {
            Success = success;
            Message = message;
        }

        /// <summary>
        /// 初始化操作结果
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="data">结果数据</param>
        /// <param name="message">消息</param>
        public OperationResult(bool success, T data, string message = null)
        {
            Success = success;
            Data = data;
            Message = message;
        }

        /// <summary>
        /// 初始化操作结果
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="message">消息</param>
        /// <param name="error">错误</param>
        public OperationResult(bool success, string message, Exception error)
        {
            Success = success;
            Message = message;
            Error = error;
        }

        /// <summary>
        /// 创建成功结果
        /// </summary>
        /// <param name="data">结果数据</param>
        /// <param name="message">消息</param>
        /// <returns>成功的操作结果</returns>
        public static OperationResult<T> Ok(T data, string message = null)
        {
            return new OperationResult<T>(true, data, message);
        }

        /// <summary>
        /// 创建失败结果
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="error">错误</param>
        /// <returns>失败的操作结果</returns>
        public static OperationResult<T> Fail(string message, Exception error = null)
        {
            return new OperationResult<T>(false, message, error);
        }

        /// <summary>
        /// 转换为无类型的操作结果
        /// </summary>
        /// <returns>无类型的操作结果</returns>
        public OperationResult ToResult()
        {
            return new OperationResult(Success, Message, Error);
        }
    }

    /// <summary>
    /// 表示无类型的操作结果
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// 获取或设置操作是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 获取或设置消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 获取或设置错误
        /// </summary>
        public Exception Error { get; set; }

        /// <summary>
        /// 初始化操作结果
        /// </summary>
        public OperationResult()
        {
        }

        /// <summary>
        /// 初始化操作结果
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="message">消息</param>
        public OperationResult(bool success, string message = null)
        {
            Success = success;
            Message = message;
        }

        /// <summary>
        /// 初始化操作结果
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="message">消息</param>
        /// <param name="error">错误</param>
        public OperationResult(bool success, string message, Exception error)
        {
            Success = success;
            Message = message;
            Error = error;
        }

        /// <summary>
        /// 创建成功结果
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>成功的操作结果</returns>
        public static OperationResult Ok(string message = null)
        {
            return new OperationResult(true, message);
        }

        /// <summary>
        /// 创建失败结果
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="error">错误</param>
        /// <returns>失败的操作结果</returns>
        public static OperationResult Fail(string message, Exception error = null)
        {
            return new OperationResult(false, message, error);
        }

        /// <summary>
        /// 转换为带类型的操作结果
        /// </summary>
        /// <typeparam name="T">结果数据类型</typeparam>
        /// <param name="data">结果数据</param>
        /// <returns>带类型的操作结果</returns>
        public OperationResult<T> ToResult<T>(T data = default)
        {
            return new OperationResult<T>(Success, data, Message) { Error = Error };
        }
    }
}