using System;
using Cyclone.Persistence.Abstractions.Connection;

namespace Cyclone.Persistence.Abstractions.Provider;

/// <summary>
/// �������ݿ��ṩ����Ľӿ�
/// </summary>
public interface IDbProvider
{
    /// <summary>
    /// ��ȡ�ṩ��������
    /// </summary>
    string Name { get; }

    /// <summary>
    /// ��ȡ�ṩ��������
    /// </summary>
    DbProviderType ProviderType { get; }

    /// <summary>
    /// �Ƿ�֧������
    /// </summary>
    bool SupportsTransaction { get; }

    /// <summary>
    /// �Ƿ�֧�ּܹ�����
    /// </summary>
    bool SupportsSchemaCreation { get; }

    /// <summary>
    /// �������ݿ�����
    /// </summary>
    /// <param name="connectionString">�����ַ���</param>
    /// <returns>���ݿ�����</returns>
    IDbConnection CreateConnection(string connectionString);

    /// <summary>
    /// ������ݿ��Ƿ����
    /// </summary>
    /// <param name="connectionString">�����ַ���</param>
    /// <returns>�Ƿ����</returns>
    bool DatabaseExists(string connectionString);

    /// <summary>
    /// �������ݿ�
    /// </summary>
    /// <param name="connectionString">�����ַ���</param>
    /// <returns>�Ƿ�ɹ�����</returns>
    bool CreateDatabase(string connectionString);

    /// <summary>
    /// ɾ�����ݿ�
    /// </summary>
    /// <param name="connectionString">�����ַ���</param>
    /// <returns>�Ƿ�ɹ�ɾ��</returns>
    bool DropDatabase(string connectionString);
}