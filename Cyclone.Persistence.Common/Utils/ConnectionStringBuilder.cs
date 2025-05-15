using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Cyclone.Persistence.Abstractions.Provider;

namespace Cyclone.Persistence.Common.Helpers
{
    /// <summary>
    /// 提供连接字符串构建的辅助类
    /// </summary>
    public class ConnectionStringBuilder
    {
        private readonly DbConnectionStringBuilder _builder;
        private readonly DbProviderType _providerType;

        /// <summary>
        /// 初始化连接字符串构建器
        /// </summary>
        /// <param name="providerType">提供程序类型</param>
        public ConnectionStringBuilder(DbProviderType providerType)
        {
            _providerType = providerType;
            _builder = CreateConnectionStringBuilder(providerType);
        }

        /// <summary>
        /// 初始化连接字符串构建器
        /// </summary>
        /// <param name="providerType">提供程序类型</param>
        /// <param name="connectionString">连接字符串</param>
        public ConnectionStringBuilder(DbProviderType providerType, string connectionString)
        {
            _providerType = providerType;
            _builder = CreateConnectionStringBuilder(providerType);

            if (!string.IsNullOrEmpty(connectionString))
            {
                _builder.ConnectionString = connectionString;
            }
        }

        /// <summary>
        /// 设置键值对
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>连接字符串构建器</returns>
        public ConnectionStringBuilder WithKeyValue(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            _builder[key] = value;
            return this;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public object GetValue(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _builder.TryGetValue(key, out var value) ? value : null;
        }

        /// <summary>
        /// 获取强类型值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public T GetValue<T>(string key)
        {
            var value = GetValue(key);

            if (value == null)
                return default;

            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// 设置服务器
        /// </summary>
        /// <param name="server">服务器</param>
        /// <returns>连接字符串构建器</returns>
        public ConnectionStringBuilder WithServer(string server)
        {
            switch (_providerType)
            {
                case DbProviderType.SQLite:
                    return this;

                case DbProviderType.SqlServer:
                    return WithKeyValue("Data Source", server);

                case DbProviderType.PostgreSQL:
                    return WithKeyValue("Host", server);

                case DbProviderType.LiteDB:
                    return this;

                default:
                    return WithKeyValue("Server", server);
            }
        }

        /// <summary>
        /// 设置数据库
        /// </summary>
        /// <param name="database">数据库</param>
        /// <returns>连接字符串构建器</returns>
        public ConnectionStringBuilder WithDatabase(string database)
        {
            switch (_providerType)
            {
                case DbProviderType.SQLite:
                    return WithKeyValue("Data Source", database);

                case DbProviderType.SqlServer:
                    return WithKeyValue("Initial Catalog", database);

                case DbProviderType.PostgreSQL:
                    return WithKeyValue("Database", database);

                case DbProviderType.LiteDB:
                    return WithKeyValue("Filename", database);

                default:
                    return WithKeyValue("Database", database);
            }
        }

        /// <summary>
        /// 设置用户名
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>连接字符串构建器</returns>
        public ConnectionStringBuilder WithUsername(string username)
        {
            switch (_providerType)
            {
                case DbProviderType.SQLite:
                    return this;

                case DbProviderType.SqlServer:
                    return WithKeyValue("User ID", username);

                case DbProviderType.PostgreSQL:
                    return WithKeyValue("Username", username);

                case DbProviderType.LiteDB:
                    return this;

                default:
                    return WithKeyValue("User ID", username);
            }
        }

        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>连接字符串构建器</returns>
        public ConnectionStringBuilder WithPassword(string password)
        {
            switch (_providerType)
            {
                case DbProviderType.SQLite:
                    return this;

                case DbProviderType.SqlServer:
                case DbProviderType.PostgreSQL:
                    return WithKeyValue("Password", password);

                case DbProviderType.LiteDB:
                    return this;

                default:
                    return WithKeyValue("Password", password);
            }
        }

        /// <summary>
        /// 设置集成安全
        /// </summary>
        /// <param name="integratedSecurity">是否集成安全</param>
        /// <returns>连接字符串构建器</returns>
        public ConnectionStringBuilder WithIntegratedSecurity(bool integratedSecurity)
        {
            switch (_providerType)
            {
                case DbProviderType.SQLite:
                case DbProviderType.PostgreSQL:
                case DbProviderType.LiteDB:
                    return this;

                case DbProviderType.SqlServer:
                    return WithKeyValue("Integrated Security", integratedSecurity);

                default:
                    return WithKeyValue("Integrated Security", integratedSecurity);
            }
        }

        /// <summary>
        /// 设置连接超时
        /// </summary>
        /// <param name="timeout">超时（秒）</param>
        /// <returns>连接字符串构建器</returns>
        public ConnectionStringBuilder WithTimeout(int timeout)
        {
            switch (_providerType)
            {
                case DbProviderType.SQLite:
                    return WithKeyValue("Default Timeout", timeout);

                case DbProviderType.SqlServer:
                    return WithKeyValue("Connect Timeout", timeout);

                case DbProviderType.PostgreSQL:
                    return WithKeyValue("Timeout", timeout);

                case DbProviderType.LiteDB:
                    return WithKeyValue("Timeout", timeout);

                default:
                    return WithKeyValue("Connection Timeout", timeout);
            }
        }

        /// <summary>
        /// 设置端口
        /// </summary>
        /// <param name="port">端口</param>
        /// <returns>连接字符串构建器</returns>
        public ConnectionStringBuilder WithPort(int port)
        {
            switch (_providerType)
            {
                case DbProviderType.SQLite:
                case DbProviderType.SqlServer:
                case DbProviderType.LiteDB:
                    return this;

                case DbProviderType.PostgreSQL:
                    return WithKeyValue("Port", port);

                default:
                    return WithKeyValue("Port", port);
            }
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns>连接字符串</returns>
        public override string ToString()
        {
            return _builder.ToString();
        }

        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns>键值对集合</returns>
        public IDictionary<string, string> ToDictionary()
        {
            return _builder.Keys.Cast<string>().ToDictionary(k => k, k => _builder[k]?.ToString());
        }

        private static DbConnectionStringBuilder CreateConnectionStringBuilder(DbProviderType providerType)
        {
            return new DbConnectionStringBuilder();
        }
    }
}