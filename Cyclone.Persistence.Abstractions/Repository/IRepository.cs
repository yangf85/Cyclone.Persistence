using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Cyclone.Persistence.Abstractions.Repository;

/// <summary>
/// 定义通用仓储的接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// 根据ID获取实体
    /// </summary>
    /// <param name="id">实体ID</param>
    /// <returns>实体对象</returns>
    TEntity GetById(object id);

    /// <summary>
    /// 异步根据ID获取实体
    /// </summary>
    /// <param name="id">实体ID</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>实体对象</returns>
    Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取所有实体
    /// </summary>
    /// <returns>实体集合</returns>
    IEnumerable<TEntity> GetAll();

    /// <summary>
    /// 异步获取所有实体
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>实体集合</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据条件查找实体
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>满足条件的实体集合</returns>
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 异步根据条件查找实体
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>满足条件的实体集合</returns>
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// 添加实体
    /// </summary>
    /// <param name="entity">要添加的实体</param>
    /// <returns>添加后的实体</returns>
    TEntity Add(TEntity entity);

    /// <summary>
    /// 异步添加实体
    /// </summary>
    /// <param name="entity">要添加的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>添加后的实体</returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 批量添加实体
    /// </summary>
    /// <param name="entities">要添加的实体集合</param>
    /// <returns>添加后的实体集合</returns>
    IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// 异步批量添加实体
    /// </summary>
    /// <param name="entities">要添加的实体集合</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>添加后的实体集合</returns>
    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新实体
    /// </summary>
    /// <param name="entity">要更新的实体</param>
    void Update(TEntity entity);

    /// <summary>
    /// 异步更新实体
    /// </summary>
    /// <param name="entity">要更新的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 批量更新实体
    /// </summary>
    /// <param name="entities">要更新的实体集合</param>
    void UpdateRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// 异步批量更新实体
    /// </summary>
    /// <param name="entities">要更新的实体集合</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除实体
    /// </summary>
    /// <param name="entity">要删除的实体</param>
    void Delete(TEntity entity);

    /// <summary>
    /// 异步删除实体
    /// </summary>
    /// <param name="entity">要删除的实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据ID删除实体
    /// </summary>
    /// <param name="id">实体ID</param>
    void DeleteById(object id);

    /// <summary>
    /// 异步根据ID删除实体
    /// </summary>
    /// <param name="id">实体ID</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task DeleteByIdAsync(object id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 批量删除实体
    /// </summary>
    /// <param name="entities">要删除的实体集合</param>
    void DeleteRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// 异步批量删除实体
    /// </summary>
    /// <param name="entities">要删除的实体集合</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据条件删除实体
    /// </summary>
    /// <param name="predicate">条件表达式</param>
    /// <returns>删除的记录数</returns>
    int DeleteWhere(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 异步根据条件删除实体
    /// </summary>
    /// <param name="predicate">条件表达式</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>删除的记录数</returns>
    Task<int> DeleteWhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取记录数
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>记录数</returns>
    int Count(Expression<Func<TEntity, bool>> predicate = null);

    /// <summary>
    /// 异步获取记录数
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>记录数</returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 检查是否存在满足条件的记录
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>是否存在</returns>
    bool Exists(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 异步检查是否存在满足条件的记录
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>是否存在</returns>
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}