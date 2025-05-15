using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Cyclone.Persistence.Abstractions.Repository;

namespace Cyclone.Persistence.Abstractions.UnitOfWork;

/// <summary>
/// ���幤����Ԫ�Ľӿ�
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// ��ȡ�ִ�
    /// </summary>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    /// <returns>ʵ��ִ�</returns>
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

    /// <summary>
    /// �������
    /// </summary>
    /// <returns>��Ӱ�������</returns>
    int SaveChanges();

    /// <summary>
    /// �첽�������
    /// </summary>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>��Ӱ�������</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// ��ʼ����
    /// </summary>
    /// <param name="isolationLevel">������뼶��</param>
    /// <returns>�������</returns>
    IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    /// <summary>
    /// �첽��ʼ����
    /// </summary>
    /// <param name="isolationLevel">������뼶��</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>�������</returns>
    Task<IDbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);

    /// <summary>
    /// �ύ����
    /// </summary>
    void CommitTransaction();

    /// <summary>
    /// �첽�ύ����
    /// </summary>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>��ʾ�첽����������</returns>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// �ع�����
    /// </summary>
    void RollbackTransaction();

    /// <summary>
    /// �첽�ع�����
    /// </summary>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>��ʾ�첽����������</returns>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// ��ȡ��ǰ����
    /// </summary>
    /// <returns>��ǰ����</returns>
    IDbTransaction GetCurrentTransaction();

    /// <summary>
    /// ִ��ԭʼSQL��ѯ
    /// </summary>
    /// <param name="sql">SQL��ѯ</param>
    /// <param name="parameters">����</param>
    /// <returns>��Ӱ�������</returns>
    int ExecuteSqlCommand(string sql, params object[] parameters);

    /// <summary>
    /// �첽ִ��ԭʼSQL��ѯ
    /// </summary>
    /// <param name="sql">SQL��ѯ</param>
    /// <param name="parameters">����</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>��Ӱ�������</returns>
    Task<int> ExecuteSqlCommandAsync(string sql, object[] parameters, CancellationToken cancellationToken = default);
}