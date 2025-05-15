using System;
using System.Linq.Expressions;

namespace Cyclone.Persistence.Abstractions.Query;

/// <summary>
/// �����ѯ�������Ľӿ�
/// </summary>
/// <typeparam name="TEntity">ʵ������</typeparam>
public interface IQueryBuilder<TEntity> where TEntity : class
{
    /// <summary>
    /// ��ӹ�������
    /// </summary>
    /// <param name="predicate">���˱��ʽ</param>
    /// <returns>��ѯ������</returns>
    IQueryBuilder<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// �����������������
    /// </summary>
    /// <typeparam name="TKey">���������</typeparam>
    /// <param name="keySelector">�����ѡ����</param>
    /// <returns>��ѯ������</returns>
    IQueryBuilder<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);

    /// <summary>
    /// �����������������
    /// </summary>
    /// <typeparam name="TKey">���������</typeparam>
    /// <param name="keySelector">�����ѡ����</param>
    /// <returns>��ѯ������</returns>
    IQueryBuilder<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);

    /// <summary>
    /// ��Ӵμ���������������
    /// </summary>
    /// <typeparam name="TKey">���������</typeparam>
    /// <param name="keySelector">�����ѡ����</param>
    /// <returns>��ѯ������</returns>
    IQueryBuilder<TEntity> ThenBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);

    /// <summary>
    /// ��Ӵμ���������������
    /// </summary>
    /// <typeparam name="TKey">���������</typeparam>
    /// <param name="keySelector">�����ѡ����</param>
    /// <returns>��ѯ������</returns>
    IQueryBuilder<TEntity> ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);

    /// <summary>
    /// ���������ļ�¼��
    /// </summary>
    /// <param name="count">�����ļ�¼��</param>
    /// <returns>��ѯ������</returns>
    IQueryBuilder<TEntity> Skip(int count);

    /// <summary>
    /// ���û�ȡ�ļ�¼��
    /// </summary>
    /// <param name="count">��ȡ�ļ�¼��</param>
    /// <returns>��ѯ������</returns>
    IQueryBuilder<TEntity> Take(int count);

    /// <summary>
    /// ���÷�ҳ
    /// </summary>
    /// <param name="pageNumber">ҳ�루��1��ʼ��</param>
    /// <param name="pageSize">ҳ���С</param>
    /// <returns>��ѯ������</returns>
    IQueryBuilder<TEntity> Page(int pageNumber, int pageSize);

    /// <summary>
    /// ������������
    /// </summary>
    /// <typeparam name="TProperty">������������</typeparam>
    /// <param name="navigationPropertyPath">��������·��</param>
    /// <returns>��ѯ������</returns>
    IQueryBuilder<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> navigationPropertyPath);

    /// <summary>
    /// ͶӰ��ѯ
    /// </summary>
    /// <typeparam name="TResult">�������</typeparam>
    /// <param name="selector">ѡ����</param>
    /// <returns>��ѯ������</returns>
    IQueryBuilder<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector) where TResult : class;

    /// <summary>
    /// �����Ƿ����ʵ��
    /// </summary>
    /// <param name="track">�Ƿ����</param>
    /// <returns>��ѯ������</returns>
    IQueryBuilder<TEntity> AsTracking(bool track = true);

    /// <summary>
    /// �����Ƿ񲻸���ʵ��
    /// </summary>
    /// <returns>��ѯ������</returns>
    IQueryBuilder<TEntity> AsNoTracking();
}