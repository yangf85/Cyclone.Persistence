using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Cyclone.Persistence.Abstractions.Connection;

/// <summary>
/// �������ݿ����ӵĽӿ�
/// </summary>
public interface IDbConnection : IDisposable
{
    /// <summary>
    /// ��ȡ�����ַ���
    /// </summary>
    string ConnectionString { get; }

    /// <summary>
    /// ��ȡ����״̬
    /// </summary>
    ConnectionState State { get; }

    /// <summary>
    /// ������
    /// </summary>
    void Open();

    /// <summary>
    /// �첽������
    /// </summary>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>��ʾ�첽����������</returns>
    Task OpenAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// �ر�����
    /// </summary>
    void Close();

    /// <summary>
    /// ��������
    /// </summary>
    /// <returns>���ݿ�����</returns>
    IDbCommand CreateCommand();

    /// <summary>
    /// ��ʼ����
    /// </summary>
    /// <param name="isolationLevel">������뼶��</param>
    /// <returns>���ݿ�����</returns>
    IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    /// <summary>
    /// �첽��ʼ����
    /// </summary>
    /// <param name="isolationLevel">������뼶��</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>���ݿ�����</returns>
    Task<IDbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);

    /// <summary>
    /// ��ȡ���������ʱ���룩
    /// </summary>
    int CommandTimeout { get; set; }
}