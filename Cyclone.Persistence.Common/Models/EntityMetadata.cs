using System;
using System.Collections.Generic;

namespace Cyclone.Persistence.Common.Models
{
    /// <summary>
    /// 表示实体元数据
    /// </summary>
    public class EntityMetadata
    {
        /// <summary>
        /// 获取表名
        /// </summary>
        public string TableName { get; private set; }

        /// <summary>
        /// 获取架构名
        /// </summary>
        public string SchemaName { get; private set; }

        /// <summary>
        /// 获取实体类型
        /// </summary>
        public Type EntityType { get; private set; }

        /// <summary>
        /// 获取主键属性
        /// </summary>
        public IReadOnlyList<PropertyMetadata> KeyProperties { get; private set; }

        /// <summary>
        /// 获取所有属性
        /// </summary>
        public IReadOnlyList<PropertyMetadata> Properties { get; private set; }

        /// <summary>
        /// 获取导航属性
        /// </summary>
        public IReadOnlyList<NavigationMetadata> Navigations { get; private set; }

        /// <summary>
        /// 初始化实体元数据
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="tableName">表名</param>
        /// <param name="schemaName">架构名</param>
        /// <param name="keyProperties">主键属性</param>
        /// <param name="properties">所有属性</param>
        /// <param name="navigations">导航属性</param>
        public EntityMetadata(Type entityType, string tableName, string schemaName,
            IReadOnlyList<PropertyMetadata> keyProperties, IReadOnlyList<PropertyMetadata> properties,
            IReadOnlyList<NavigationMetadata> navigations)
        {
            EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
            TableName = tableName ?? entityType.Name;
            SchemaName = schemaName;
            KeyProperties = keyProperties ?? Array.Empty<PropertyMetadata>();
            Properties = properties ?? Array.Empty<PropertyMetadata>();
            Navigations = navigations ?? Array.Empty<NavigationMetadata>();
        }

        /// <summary>
        /// 获取完整表名（包含架构名）
        /// </summary>
        /// <returns>完整表名</returns>
        public string GetFullTableName()
        {
            return string.IsNullOrEmpty(SchemaName) ? TableName : $"{SchemaName}.{TableName}";
        }
    }

    /// <summary>
    /// 表示属性元数据
    /// </summary>
    public class PropertyMetadata
    {
        /// <summary>
        /// 获取属性名
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// 获取列名
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        /// 获取属性类型
        /// </summary>
        public Type PropertyType { get; private set; }

        /// <summary>
        /// 获取或设置是否为主键
        /// </summary>
        public bool IsKey { get; set; }

        /// <summary>
        /// 获取或设置是否必填
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 获取或设置是否自增
        /// </summary>
        public bool IsAutoIncrement { get; set; }

        /// <summary>
        /// 获取或设置最大长度
        /// </summary>
        public int? MaxLength { get; set; }

        /// <summary>
        /// 获取或设置是否忽略此属性
        /// </summary>
        public bool IsIgnored { get; set; }

        /// <summary>
        /// 获取或设置默认值
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// 初始化属性元数据
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="columnName">列名</param>
        /// <param name="propertyType">属性类型</param>
        public PropertyMetadata(string propertyName, string columnName, Type propertyType)
        {
            PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            ColumnName = columnName ?? propertyName;
            PropertyType = propertyType ?? throw new ArgumentNullException(nameof(propertyType));
        }
    }

    /// <summary>
    /// 表示导航属性元数据
    /// </summary>
    public class NavigationMetadata
    {
        /// <summary>
        /// 获取导航属性名
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// 获取外键属性名
        /// </summary>
        public string ForeignKeyName { get; private set; }

        /// <summary>
        /// 获取导航类型
        /// </summary>
        public NavigationType Type { get; private set; }

        /// <summary>
        /// 获取相关实体类型
        /// </summary>
        public Type RelatedEntityType { get; private set; }

        /// <summary>
        /// 初始化导航属性元数据
        /// </summary>
        /// <param name="propertyName">导航属性名</param>
        /// <param name="foreignKeyName">外键属性名</param>
        /// <param name="type">导航类型</param>
        /// <param name="relatedEntityType">相关实体类型</param>
        public NavigationMetadata(string propertyName, string foreignKeyName, NavigationType type, Type relatedEntityType)
        {
            PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            ForeignKeyName = foreignKeyName;
            Type = type;
            RelatedEntityType = relatedEntityType ?? throw new ArgumentNullException(nameof(relatedEntityType));
        }
    }

    /// <summary>
    /// 表示导航类型
    /// </summary>
    public enum NavigationType
    {
        /// <summary>
        /// 一对一
        /// </summary>
        OneToOne,

        /// <summary>
        /// 一对多
        /// </summary>
        OneToMany,

        /// <summary>
        /// 多对一
        /// </summary>
        ManyToOne,

        /// <summary>
        /// 多对多
        /// </summary>
        ManyToMany
    }
}