using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Cyclone.Persistence.Abstractions.Repository;

/// <summary>
/// ����ͨ�òִ��Ľӿ�
/// </summary>
/// <typeparam name="TEntity">ʵ������</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// ����ID��ȡʵ��
    /// </summary>
    /// <param name="id">ʵ��ID</param>
    /// <returns>ʵ�����</returns>
    TEntity GetById(object id);

    /// <summary>
    /// �첽����ID��ȡʵ��
    /// </summary>
    /// <param name="id">ʵ��ID</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>ʵ�����</returns>
    Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default);

    /// <summary>
    /// ��ȡ����ʵ��
    /// </summary>
    /// <returns>ʵ�弯��</returns>
    IEnumerable<TEntity> GetAll();

    /// <summary>
    /// �첽��ȡ����ʵ��
    /// </summary>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>ʵ�弯��</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// ������������ʵ��
    /// </summary>
    /// <param name="predicate">��ѯ����</param>
    /// <returns>����������ʵ�弯��</returns>
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// �첽������������ʵ��
    /// </summary>
    /// <param name="predicate">��ѯ����</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>����������ʵ�弯��</returns>
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// ���ʵ��
    /// </summary>
    /// <param name="entity">Ҫ��ӵ�ʵ��</param>
    /// <returns>��Ӻ��ʵ��</returns>
    TEntity Add(TEntity entity);

    /// <summary>
    /// �첽���ʵ��
    /// </summary>
    /// <param name="entity">Ҫ��ӵ�ʵ��</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>��Ӻ��ʵ��</returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// �������ʵ��
    /// </summary>
    /// <param name="entities">Ҫ��ӵ�ʵ�弯��</param>
    /// <returns>��Ӻ��ʵ�弯��</returns>
    IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// �첽�������ʵ��
    /// </summary>
    /// <param name="entities">Ҫ��ӵ�ʵ�弯��</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>��Ӻ��ʵ�弯��</returns>
    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// ����ʵ��
    /// </summary>
    /// <param name="entity">Ҫ���µ�ʵ��</param>
    void Update(TEntity entity);

    /// <summary>
    /// �첽����ʵ��
    /// </summary>
    /// <param name="entity">Ҫ���µ�ʵ��</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>��ʾ�첽����������</returns>
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// ��������ʵ��
    /// </summary>
    /// <param name="entities">Ҫ���µ�ʵ�弯��</param>
    void UpdateRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// �첽��������ʵ��
    /// </summary>
    /// <param name="entities">Ҫ���µ�ʵ�弯��</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>��ʾ�첽����������</returns>
    Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// ɾ��ʵ��
    /// </summary>
    /// <param name="entity">Ҫɾ����ʵ��</param>
    void Delete(TEntity entity);

    /// <summary>
    /// �첽ɾ��ʵ��
    /// </summary>
    /// <param name="entity">Ҫɾ����ʵ��</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>��ʾ�첽����������</returns>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// ����IDɾ��ʵ��
    /// </summary>
    /// <param name="id">ʵ��ID</param>
    void DeleteById(object id);

    /// <summary>
    /// �첽����IDɾ��ʵ��
    /// </summary>
    /// <param name="id">ʵ��ID</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>��ʾ�첽����������</returns>
    Task DeleteByIdAsync(object id, CancellationToken cancellationToken = default);

    /// <summary>
    /// ����ɾ��ʵ��
    /// </summary>
    /// <param name="entities">Ҫɾ����ʵ�弯��</param>
    void DeleteRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// �첽����ɾ��ʵ��
    /// </summary>
    /// <param name="entities">Ҫɾ����ʵ�弯��</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>��ʾ�첽����������</returns>
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// ��������ɾ��ʵ��
    /// </summary>
    /// <param name="predicate">�������ʽ</param>
    /// <returns>ɾ���ļ�¼��</returns>
    int DeleteWhere(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// �첽��������ɾ��ʵ��
    /// </summary>
    /// <param name="predicate">�������ʽ</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>ɾ���ļ�¼��</returns>
    Task<int> DeleteWhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// ��ȡ��¼��
    /// </summary>
    /// <param name="predicate">��ѯ����</param>
    /// <returns>��¼��</returns>
    int Count(Expression<Func<TEntity, bool>> predicate = null);

    /// <summary>
    /// �첽��ȡ��¼��
    /// </summary>
    /// <param name="predicate">��ѯ����</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>��¼��</returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// ����Ƿ�������������ļ�¼
    /// </summary>
    /// <param name="predicate">��ѯ����</param>
    /// <returns>�Ƿ����</returns>
    bool Exists(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// �첽����Ƿ�������������ļ�¼
    /// </summary>
    /// <param name="predicate">��ѯ����</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>�Ƿ����</returns>
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}