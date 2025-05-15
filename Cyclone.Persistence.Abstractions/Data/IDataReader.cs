using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cyclone.Persistence.Abstractions.Data;

/// <summary>
/// �������ݶ�ȡ���Ľӿ�
/// </summary>
public interface IDataReader : IDisposable
{
    /// <summary>
    /// ��ȡ�ֶ�����
    /// </summary>
    int FieldCount { get; }

    /// <summary>
    /// �رն�ȡ��
    /// </summary>
    void Close();

    /// <summary>
    /// ��ȡָ��������ֵ
    /// </summary>
    /// <param name="name">����</param>
    /// <returns>��ֵ</returns>
    object GetValue(string name);

    /// <summary>
    /// ��ȡָ����������ֵ
    /// </summary>
    /// <param name="ordinal">������</param>
    /// <returns>��ֵ</returns>
    object GetValue(int ordinal);

    /// <summary>
    /// ��ȡָ���������ַ���ֵ
    /// </summary>
    /// <param name="name">����</param>
    /// <returns>�ַ���ֵ</returns>
    string GetString(string name);

    /// <summary>
    /// ��ȡָ�����������ַ���ֵ
    /// </summary>
    /// <param name="ordinal">������</param>
    /// <returns>�ַ���ֵ</returns>
    string GetString(int ordinal);

    /// <summary>
    /// ��ȡָ������������ֵ
    /// </summary>
    /// <param name="name">����</param>
    /// <returns>����ֵ</returns>
    int GetInt32(string name);

    /// <summary>
    /// ��ȡָ��������������ֵ
    /// </summary>
    /// <param name="ordinal">������</param>
    /// <returns>����ֵ</returns>
    int GetInt32(int ordinal);

    /// <summary>
    /// ��ȡָ�������ĳ�����ֵ
    /// </summary>
    /// <param name="name">����</param>
    /// <returns>������ֵ</returns>
    long GetInt64(string name);

    /// <summary>
    /// ��ȡָ���������ĳ�����ֵ
    /// </summary>
    /// <param name="ordinal">������</param>
    /// <returns>������ֵ</returns>
    long GetInt64(int ordinal);

    /// <summary>
    /// ��ȡָ�������ĸ���ֵ
    /// </summary>
    /// <param name="name">����</param>
    /// <returns>����ֵ</returns>
    double GetDouble(string name);

    /// <summary>
    /// ��ȡָ���������ĸ���ֵ
    /// </summary>
    /// <param name="ordinal">������</param>
    /// <returns>����ֵ</returns>
    double GetDouble(int ordinal);

    /// <summary>
    /// ��ȡָ�������Ĳ���ֵ
    /// </summary>
    /// <param name="name">����</param>
    /// <returns>����ֵ</returns>
    bool GetBoolean(string name);

    /// <summary>
    /// ��ȡָ���������Ĳ���ֵ
    /// </summary>
    /// <param name="ordinal">������</param>
    /// <returns>����ֵ</returns>
    bool GetBoolean(int ordinal);

    /// <summary>
    /// ��ȡָ������������ʱ��ֵ
    /// </summary>
    /// <param name="name">����</param>
    /// <returns>����ʱ��ֵ</returns>
    DateTime GetDateTime(string name);

    /// <summary>
    /// ��ȡָ��������������ʱ��ֵ
    /// </summary>
    /// <param name="ordinal">������</param>
    /// <returns>����ʱ��ֵ</returns>
    DateTime GetDateTime(int ordinal);

    /// <summary>
    /// ��ȡָ�������� GUID ֵ
    /// </summary>
    /// <param name="name">����</param>
    /// <returns>GUID ֵ</returns>
    Guid GetGuid(string name);

    /// <summary>
    /// ��ȡָ���������� GUID ֵ
    /// </summary>
    /// <param name="ordinal">������</param>
    /// <returns>GUID ֵ</returns>
    Guid GetGuid(int ordinal);

    /// <summary>
    /// ��ȡָ��������ǿ����ֵ
    /// </summary>
    /// <typeparam name="T">ֵ����</typeparam>
    /// <param name="name">����</param>
    /// <returns>ǿ����ֵ</returns>
    T GetFieldValue<T>(string name);

    /// <summary>
    /// ��ȡָ����������ǿ����ֵ
    /// </summary>
    /// <typeparam name="T">ֵ����</typeparam>
    /// <param name="ordinal">������</param>
    /// <returns>ǿ����ֵ</returns>
    T GetFieldValue<T>(int ordinal);

    /// <summary>
    /// ���ָ�����Ƿ�Ϊ DBNull
    /// </summary>
    /// <param name="name">����</param>
    /// <returns>�Ƿ�Ϊ DBNull</returns>
    bool IsDBNull(string name);

    /// <summary>
    /// ���ָ�����Ƿ�Ϊ DBNull
    /// </summary>
    /// <param name="ordinal">������</param>
    /// <returns>�Ƿ�Ϊ DBNull</returns>
    bool IsDBNull(int ordinal);

    /// <summary>
    /// ��ȡ��һ����¼
    /// </summary>
    /// <returns>�Ƿ�ɹ���ȡ</returns>
    bool Read();

    /// <summary>
    /// �첽��ȡ��һ����¼
    /// </summary>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>�Ƿ�ɹ���ȡ</returns>
    Task<bool> ReadAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// ��ȡ��������
    /// </summary>
    /// <typeparam name="T">ʵ������</typeparam>
    /// <returns>ʵ�弯��</returns>
    IEnumerable<T> GetData<T>() where T : class, new();

    /// <summary>
    /// �첽��ȡ��������
    /// </summary>
    /// <typeparam name="T">ʵ������</typeparam>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>ʵ�弯��</returns>
    Task<IEnumerable<T>> GetDataAsync<T>(CancellationToken cancellationToken = default) where T : class, new();
}