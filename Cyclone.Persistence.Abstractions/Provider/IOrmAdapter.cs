using System;
using Cyclone.Persistence.Abstractions.Repository;
using Cyclone.Persistence.Abstractions.UnitOfWork;

namespace Cyclone.Persistence.Abstractions.Provider;

/// <summary>
/// ���� ORM �������Ľӿ�
/// </summary>
public interface IOrmAdapter
{
    /// <summary>
    /// ��ȡ����������
    /// </summary>
    string Name { get; }

    /// <summary>
    /// �����ִ�
    /// </summary>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    /// <returns>ʵ��ִ�</returns>
    IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class;

    /// <summary>
    /// ����������Ԫ
    /// </summary>
    /// <returns>������Ԫ</returns>
    IUnitOfWork CreateUnitOfWork();

    /// <summary>
    /// ����������Ԫ��ָ�������ַ���
    /// </summary>
    /// <param name="connectionString">�����ַ���</param>
    /// <returns>������Ԫ</returns>
    IUnitOfWork CreateUnitOfWork(string connectionString);

    /// <summary>
    /// ��ȡ���������ݿ��ṩ����
    /// </summary>
    IDbProvider DbProvider { get; set; }

    /// <summary>
    /// ���� ORM
    /// </summary>
    /// <param name="configuration">���� Action</param>
    void Configure(Action<object> configuration);

    /// <summary>
    /// ��ʼ�� ORM
    /// </summary>
    void Initialize();
}