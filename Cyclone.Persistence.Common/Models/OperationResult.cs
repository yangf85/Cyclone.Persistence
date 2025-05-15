using System;

namespace Cyclone.Persistence.Common.Models
{
    /// <summary>
    /// ��ʾ�������
    /// </summary>
    /// <typeparam name="T">�����������</typeparam>
    public class OperationResult<T>
    {
        /// <summary>
        /// ��ȡ�����ò����Ƿ�ɹ�
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// ��ȡ�����ý������
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// ��ȡ��������Ϣ
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// ��ȡ�����ô���
        /// </summary>
        public Exception Error { get; set; }

        /// <summary>
        /// ��ʼ���������
        /// </summary>
        public OperationResult()
        {
        }

        /// <summary>
        /// ��ʼ���������
        /// </summary>
        /// <param name="success">�Ƿ�ɹ�</param>
        /// <param name="message">��Ϣ</param>
        public OperationResult(bool success, string message = null)
        {
            Success = success;
            Message = message;
        }

        /// <summary>
        /// ��ʼ���������
        /// </summary>
        /// <param name="success">�Ƿ�ɹ�</param>
        /// <param name="data">�������</param>
        /// <param name="message">��Ϣ</param>
        public OperationResult(bool success, T data, string message = null)
        {
            Success = success;
            Data = data;
            Message = message;
        }

        /// <summary>
        /// ��ʼ���������
        /// </summary>
        /// <param name="success">�Ƿ�ɹ�</param>
        /// <param name="message">��Ϣ</param>
        /// <param name="error">����</param>
        public OperationResult(bool success, string message, Exception error)
        {
            Success = success;
            Message = message;
            Error = error;
        }

        /// <summary>
        /// �����ɹ����
        /// </summary>
        /// <param name="data">�������</param>
        /// <param name="message">��Ϣ</param>
        /// <returns>�ɹ��Ĳ������</returns>
        public static OperationResult<T> Ok(T data, string message = null)
        {
            return new OperationResult<T>(true, data, message);
        }

        /// <summary>
        /// ����ʧ�ܽ��
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="error">����</param>
        /// <returns>ʧ�ܵĲ������</returns>
        public static OperationResult<T> Fail(string message, Exception error = null)
        {
            return new OperationResult<T>(false, message, error);
        }

        /// <summary>
        /// ת��Ϊ�����͵Ĳ������
        /// </summary>
        /// <returns>�����͵Ĳ������</returns>
        public OperationResult ToResult()
        {
            return new OperationResult(Success, Message, Error);
        }
    }

    /// <summary>
    /// ��ʾ�����͵Ĳ������
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// ��ȡ�����ò����Ƿ�ɹ�
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// ��ȡ��������Ϣ
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// ��ȡ�����ô���
        /// </summary>
        public Exception Error { get; set; }

        /// <summary>
        /// ��ʼ���������
        /// </summary>
        public OperationResult()
        {
        }

        /// <summary>
        /// ��ʼ���������
        /// </summary>
        /// <param name="success">�Ƿ�ɹ�</param>
        /// <param name="message">��Ϣ</param>
        public OperationResult(bool success, string message = null)
        {
            Success = success;
            Message = message;
        }

        /// <summary>
        /// ��ʼ���������
        /// </summary>
        /// <param name="success">�Ƿ�ɹ�</param>
        /// <param name="message">��Ϣ</param>
        /// <param name="error">����</param>
        public OperationResult(bool success, string message, Exception error)
        {
            Success = success;
            Message = message;
            Error = error;
        }

        /// <summary>
        /// �����ɹ����
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <returns>�ɹ��Ĳ������</returns>
        public static OperationResult Ok(string message = null)
        {
            return new OperationResult(true, message);
        }

        /// <summary>
        /// ����ʧ�ܽ��
        /// </summary>
        /// <param name="message">��Ϣ</param>
        /// <param name="error">����</param>
        /// <returns>ʧ�ܵĲ������</returns>
        public static OperationResult Fail(string message, Exception error = null)
        {
            return new OperationResult(false, message, error);
        }

        /// <summary>
        /// ת��Ϊ�����͵Ĳ������
        /// </summary>
        /// <typeparam name="T">�����������</typeparam>
        /// <param name="data">�������</param>
        /// <returns>�����͵Ĳ������</returns>
        public OperationResult<T> ToResult<T>(T data = default)
        {
            return new OperationResult<T>(Success, data, Message) { Error = Error };
        }
    }
}