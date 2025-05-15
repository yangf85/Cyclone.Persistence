using System;
using System.Data;

namespace Cyclone.Persistence.Common.Models
{
    /// <summary>
    /// 表示数据库参数
    /// </summary>
    public class DbParameter
    {
        /// <summary>
        /// 获取或设置参数名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置参数值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 获取或设置数据库类型
        /// </summary>
        public DbType? DbType { get; set; }

        /// <summary>
        /// 获取或设置参数方向
        /// </summary>
        public ParameterDirection Direction { get; set; } = ParameterDirection.Input;

        /// <summary>
        /// 获取或设置数据库参数类型
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 获取或设置参数大小
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// 获取或设置精度
        /// </summary>
        public byte? Precision { get; set; }

        /// <summary>
        /// 获取或设置小数位数
        /// </summary>
        public byte? Scale { get; set; }

        /// <summary>
        /// 获取或设置源列
        /// </summary>
        public string SourceColumn { get; set; }

        /// <summary>
        /// 获取或设置源列是否可为空
        /// </summary>
        public bool SourceColumnNullMapping { get; set; }

        /// <summary>
        /// 获取或设置值是否来自源列的当前值
        /// </summary>
        public DataRowVersion SourceVersion { get; set; } = DataRowVersion.Current;

        /// <summary>
        /// 初始化数据库参数
        /// </summary>
        public DbParameter()
        {
        }

        /// <summary>
        /// 初始化数据库参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        public DbParameter(string name, object value)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value;
        }

        /// <summary>
        /// 初始化数据库参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">数据库类型</param>
        public DbParameter(string name, object value, DbType dbType)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value;
            DbType = dbType;
        }

        /// <summary>
        /// 初始化数据库参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="direction">参数方向</param>
        public DbParameter(string name, object value, DbType dbType, ParameterDirection direction)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value;
            DbType = dbType;
            Direction = direction;
        }
    }
}