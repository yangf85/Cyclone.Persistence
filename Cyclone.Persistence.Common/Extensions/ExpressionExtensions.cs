using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Cyclone.Persistence.Common.Extensions
{
    /// <summary>
    /// 提供表达式树的扩展方法
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// 创建参数表达式
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="parameterName">参数名称</param>
        /// <returns>参数表达式</returns>
        public static ParameterExpression CreateParameter<T>(string parameterName = "p")
        {
            return Expression.Parameter(typeof(T), parameterName);
        }

        /// <summary>
        /// 组合两个表达式，使用 AND 运算符
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="left">左表达式</param>
        /// <param name="right">右表达式</param>
        /// <returns>组合后的表达式</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            if (left == null)
                return right;

            if (right == null)
                return left;

            var parameter = left.Parameters[0];
            var visitor = new ParameterReplacer(parameter);
            var rightBody = visitor.Visit(right.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(left.Body, rightBody),
                parameter);
        }

        /// <summary>
        /// 组合两个表达式，使用 OR 运算符
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="left">左表达式</param>
        /// <param name="right">右表达式</param>
        /// <returns>组合后的表达式</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            if (left == null)
                return right;

            if (right == null)
                return left;

            var parameter = left.Parameters[0];
            var visitor = new ParameterReplacer(parameter);
            var rightBody = visitor.Visit(right.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(left.Body, rightBody),
                parameter);
        }

        /// <summary>
        /// 否定表达式
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>否定后的表达式</returns>
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            return Expression.Lambda<Func<T, bool>>(
                Expression.Not(expression.Body),
                expression.Parameters);
        }

        /// <summary>
        /// 获取属性表达式
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        /// <returns>属性信息</returns>
        public static PropertyInfo GetPropertyInfo<T, TProperty>(this Expression<Func<T, TProperty>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));

            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member as PropertyInfo;
            }

            throw new ArgumentException("表达式不是属性表达式", nameof(propertyExpression));
        }

        /// <summary>
        /// 获取属性名
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        /// <returns>属性名</returns>
        public static string GetPropertyName<T, TProperty>(this Expression<Func<T, TProperty>> propertyExpression)
        {
            return GetPropertyInfo(propertyExpression)?.Name;
        }

        /// <summary>
        /// 创建排序表达式
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="ascending">是否升序</param>
        /// <returns>排序表达式</returns>
        public static LambdaExpression CreateOrderByExpression<T>(string propertyName, bool ascending = true)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            var parameter = Expression.Parameter(typeof(T), "p");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            return lambda;
        }

        /// <summary>
        /// 参数替换访问器
        /// </summary>
        private class ParameterReplacer : ExpressionVisitor
        {
            private readonly ParameterExpression _parameter;

            public ParameterReplacer(ParameterExpression parameter)
            {
                _parameter = parameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return _parameter;
            }
        }
    }
}