using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Cyclone.Persistence.Common.Helpers
{
    /// <summary>
    /// 表达式树处理的辅助类
    /// </summary>
    internal static class ExpressionHelper
    {
        /// <summary>
        /// 获取属性访问表达式的属性信息
        /// </summary>
        /// <param name="propertyExpression">属性表达式</param>
        /// <returns>属性信息</returns>
        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));

            if (!(propertyExpression.Body is MemberExpression memberExpr))
            {
                if (propertyExpression.Body is UnaryExpression unaryExpr && unaryExpr.Operand is MemberExpression operandExpr)
                {
                    memberExpr = operandExpr;
                }
                else
                {
                    throw new ArgumentException("表达式必须是成员访问表达式", nameof(propertyExpression));
                }
            }

            if (!(memberExpr.Member is PropertyInfo propertyInfo))
                throw new ArgumentException("表达式必须是属性访问表达式", nameof(propertyExpression));

            return propertyInfo;
        }

        /// <summary>
        /// 获取属性访问表达式的属性名
        /// </summary>
        /// <param name="propertyExpression">属性表达式</param>
        /// <returns>属性名</returns>
        public static string GetPropertyName<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyExpression)
        {
            return GetPropertyInfo(propertyExpression).Name;
        }

        /// <summary>
        /// 创建属性相等比较表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">比较值</param>
        /// <returns>相等比较表达式</returns>
        public static Expression<Func<T, bool>> CreateEqualExpression<T>(string propertyName, object value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var convertedValue = Expression.Constant(ConvertValue(value, property.Type));
            var equal = Expression.Equal(property, convertedValue);
            return Expression.Lambda<Func<T, bool>>(equal, parameter);
        }

        /// <summary>
        /// 合并两个表达式，使用 AND 逻辑
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="expr1">表达式1</param>
        /// <param name="expr2">表达式2</param>
        /// <returns>合并后的表达式</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null)
                return expr2;
            if (expr2 == null)
                return expr1;

            var parameter = Expression.Parameter(typeof(T), "x");
            var leftVisitor = new ReplaceParameterVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);
            var rightVisitor = new ReplaceParameterVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }

        /// <summary>
        /// 合并两个表达式，使用 OR 逻辑
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="expr1">表达式1</param>
        /// <param name="expr2">表达式2</param>
        /// <returns>合并后的表达式</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null)
                return expr2;
            if (expr2 == null)
                return expr1;

            var parameter = Expression.Parameter(typeof(T), "x");
            var leftVisitor = new ReplaceParameterVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);
            var rightVisitor = new ReplaceParameterVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), parameter);
        }

        /// <summary>
        /// 否定表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="expr">表达式</param>
        /// <returns>否定后的表达式</returns>
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expr)
        {
            if (expr == null)
                return null;

            var parameter = Expression.Parameter(typeof(T), "x");
            var visitor = new ReplaceParameterVisitor(expr.Parameters[0], parameter);
            var body = visitor.Visit(expr.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.Not(body), parameter);
        }

        /// <summary>
        /// 从表达式中提取成员信息
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>成员信息列表</returns>
        public static List<MemberInfo> ExtractMembers(Expression expression)
        {
            var members = new List<MemberInfo>();
            ExtractMembersInternal(expression, members);
            return members;
        }

        private static void ExtractMembersInternal(Expression expression, List<MemberInfo> members)
        {
            if (expression == null)
                return;

            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    var memberExpr = (MemberExpression)expression;
                    members.Add(memberExpr.Member);
                    ExtractMembersInternal(memberExpr.Expression, members);
                    break;

                case ExpressionType.Call:
                    var callExpr = (MethodCallExpression)expression;
                    ExtractMembersInternal(callExpr.Object, members);
                    foreach (var arg in callExpr.Arguments)
                    {
                        ExtractMembersInternal(arg, members);
                    }
                    break;

                case ExpressionType.New:
                    var newExpr = (NewExpression)expression;
                    foreach (var arg in newExpr.Arguments)
                    {
                        ExtractMembersInternal(arg, members);
                    }
                    break;

                case ExpressionType.NewArrayInit:
                    var newArrayExpr = (NewArrayExpression)expression;
                    foreach (var item in newArrayExpr.Expressions)
                    {
                        ExtractMembersInternal(item, members);
                    }
                    break;

                case ExpressionType.Lambda:
                    var lambdaExpr = (LambdaExpression)expression;
                    ExtractMembersInternal(lambdaExpr.Body, members);
                    break;

                // 处理二元表达式
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                    var binaryExpr = (BinaryExpression)expression;
                    ExtractMembersInternal(binaryExpr.Left, members);
                    ExtractMembersInternal(binaryExpr.Right, members);
                    break;

                // 处理一元表达式
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                    var unaryExpr = (UnaryExpression)expression;
                    ExtractMembersInternal(unaryExpr.Operand, members);
                    break;

                // 处理条件表达式
                case ExpressionType.Conditional:
                    var conditionalExpr = (ConditionalExpression)expression;
                    ExtractMembersInternal(conditionalExpr.Test, members);
                    ExtractMembersInternal(conditionalExpr.IfTrue, members);
                    ExtractMembersInternal(conditionalExpr.IfFalse, members);
                    break;

                // 处理初始化表达式
                case ExpressionType.MemberInit:
                    var memberInitExpr = (MemberInitExpression)expression;
                    ExtractMembersInternal(memberInitExpr.NewExpression, members);
                    foreach (var binding in memberInitExpr.Bindings)
                    {
                        if (binding is MemberAssignment assignment)
                        {
                            ExtractMembersInternal(assignment.Expression, members);
                        }
                    }
                    break;

                case ExpressionType.ListInit:
                    var listInitExpr = (ListInitExpression)expression;
                    ExtractMembersInternal(listInitExpr.NewExpression, members);
                    foreach (var initializer in listInitExpr.Initializers)
                    {
                        foreach (var arg in initializer.Arguments)
                        {
                            ExtractMembersInternal(arg, members);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 将值转换为指定类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="type">目标类型</param>
        /// <returns>转换后的值</returns>
        private static object ConvertValue(object value, Type type)
        {
            if (value == null)
                return null;

            // 处理可空类型
            var targetType = Nullable.GetUnderlyingType(type) ?? type;

            if (value.GetType() == targetType)
                return value;

            // 处理枚举类型
            if (targetType.IsEnum)
                return Enum.Parse(targetType, value.ToString());

            // 使用Convert类进行转换
            return Convert.ChangeType(value, targetType);
        }

        /// <summary>
        /// 参数替换访问器，用于合并表达式
        /// </summary>
        private class ReplaceParameterVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParameter;
            private readonly ParameterExpression _newParameter;

            public ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                _oldParameter = oldParameter;
                _newParameter = newParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _oldParameter ? _newParameter : base.VisitParameter(node);
            }
        }
    }
}