using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Cyclone.Persistence.Abstractions.Provider;
using Cyclone.Persistence.Common.Utils;

namespace Cyclone.Persistence.Common.Helpers
{
    /// <summary>
    /// 提供 SQL 操作的辅助类
    /// </summary>
    public static class SqlHelper
    {
        /// <summary>
        /// 判断是否是 SQL 关键字
        /// </summary>
        /// <param name="word">单词</param>
        /// <returns>是否是 SQL 关键字</returns>
        public static bool IsSqlKeyword(string word)
        {
            if (string.IsNullOrEmpty(word))
                return false;

            var keywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "ADD", "ALL", "ALTER", "AND", "ANY", "AS", "ASC", "BACKUP", "BEGIN", "BETWEEN",
                "BREAK", "BROWSE", "BULK", "BY", "CASCADE", "CASE", "CHECK", "CHECKPOINT", "CLOSE",
                "CLUSTERED", "COALESCE", "COLLATE", "COLUMN", "COMMIT", "COMPUTE", "CONSTRAINT",
                "CONTAINS", "CONTAINSTABLE", "CONTINUE", "CONVERT", "CREATE", "CROSS", "CURRENT",
                "CURRENT_DATE", "CURRENT_TIME", "CURRENT_TIMESTAMP", "CURRENT_USER", "CURSOR",
                "DATABASE", "DBCC", "DEALLOCATE", "DECLARE", "DEFAULT", "DELETE", "DENY", "DESC",
                "DISK", "DISTINCT", "DISTRIBUTED", "DOUBLE", "DROP", "DUMP", "ELSE", "END", "ERRLVL",
                "ESCAPE", "EXCEPT", "EXEC", "EXECUTE", "EXISTS", "EXIT", "EXTERNAL", "FETCH", "FILE",
                "FILLFACTOR", "FOR", "FOREIGN", "FREETEXT", "FREETEXTTABLE", "FROM", "FULL", "FUNCTION",
                "GOTO", "GRANT", "GROUP", "HAVING", "HOLDLOCK", "IDENTITY", "IDENTITY_INSERT", "IDENTITYCOL",
                "IF", "IN", "INDEX", "INNER", "INSERT", "INTERSECT", "INTO", "IS", "JOIN", "KEY", "KILL",
                "LEFT", "LIKE", "LINENO", "LOAD", "MERGE", "NATIONAL", "NOCHECK", "NONCLUSTERED", "NOT",
                "NULL", "NULLIF", "OF", "OFF", "OFFSETS", "ON", "OPEN", "OPENDATASOURCE", "OPENQUERY",
                "OPENROWSET", "OPENXML", "OPTION", "OR", "ORDER", "OUTER", "OVER", "PERCENT", "PIVOT",
                "PLAN", "PRECISION", "PRIMARY", "PRINT", "PROC", "PROCEDURE", "PUBLIC", "RAISERROR",
                "READ", "READTEXT", "RECONFIGURE", "REFERENCES", "REPLICATION", "RESTORE", "RESTRICT",
                "RETURN", "REVERT", "REVOKE", "RIGHT", "ROLLBACK", "ROWCOUNT", "ROWGUIDCOL", "RULE",
                "SAVE", "SCHEMA", "SECURITYAUDIT", "SELECT", "SEMANTICKEYPHRASETABLE", "SEMANTICSIMILARITYDETAILSTABLE",
                "SEMANTICSIMILARITYTABLE", "SESSION_USER", "SET", "SETUSER", "SHUTDOWN", "SOME", "STATISTICS",
                "SYSTEM_USER", "TABLE", "TABLESAMPLE", "TEXTSIZE", "THEN", "TO", "TOP", "TRAN", "TRANSACTION",
                "TRIGGER", "TRUNCATE", "TRY_CONVERT", "TSEQUAL", "UNION", "UNIQUE", "UNPIVOT", "UPDATE",
                "UPDATETEXT", "USE", "USER", "VALUES", "VARYING", "VIEW", "WAITFOR", "WHEN", "WHERE",
                "WHILE", "WITH", "WITHIN GROUP", "WRITETEXT"
            };

            return keywords.Contains(word);
        }

        /// <summary>
        /// 判断是否是有效的表名
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>是否是有效的表名</returns>
        public static bool IsValidTableName(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
                return false;

            // 表名必须以字母或下划线开头，后面可以是字母、数字或下划线
            return Regex.IsMatch(tableName, @"^[a-zA-Z_][a-zA-Z0-9_]*$");
        }

        /// <summary>
        /// 判断是否是有效的列名
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <returns>是否是有效的列名</returns>
        public static bool IsValidColumnName(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                return false;

            // 列名必须以字母或下划线开头，后面可以是字母、数字或下划线
            return Regex.IsMatch(columnName, @"^[a-zA-Z_][a-zA-Z0-9_]*$");
        }

        /// <summary>
        /// 获取转义后的表名
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="providerType">提供程序类型</param>
        /// <returns>转义后的表名</returns>
        public static string GetEscapedTableName(string tableName, DbProviderType providerType)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));

            if (!IsValidTableName(tableName))
                throw new ArgumentException("无效的表名", nameof(tableName));

            switch (providerType)
            {
                case DbProviderType.SQLite:
                    return $"[{tableName}]";

                case DbProviderType.SqlServer:
                    return $"[{tableName}]";

                case DbProviderType.PostgreSQL:
                    return $"\"{tableName}\"";

                case DbProviderType.LiteDB:
                    return tableName;

                default:
                    return $"`{tableName}`";
            }
        }

        /// <summary>
        /// 获取转义后的列名
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="providerType">提供程序类型</param>
        /// <returns>转义后的列名</returns>
        public static string GetEscapedColumnName(string columnName, DbProviderType providerType)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException(nameof(columnName));

            if (!IsValidColumnName(columnName))
                throw new ArgumentException("无效的列名", nameof(columnName));

            switch (providerType)
            {
                case DbProviderType.SQLite:
                    return $"[{columnName}]";

                case DbProviderType.SqlServer:
                    return $"[{columnName}]";

                case DbProviderType.PostgreSQL:
                    return $"\"{columnName}\"";

                case DbProviderType.LiteDB:
                    return columnName;

                default:
                    return $"`{columnName}`";
            }
        }

        /// <summary>
        /// 获取参数名称前缀
        /// </summary>
        /// <param name="providerType">提供程序类型</param>
        /// <returns>参数名称前缀</returns>
        public static string GetParameterPrefix(DbProviderType providerType)
        {
            switch (providerType)
            {
                case DbProviderType.SQLite:
                    return "@";

                case DbProviderType.SqlServer:
                    return "@";

                case DbProviderType.PostgreSQL:
                    return "@";

                default:
                    return "?";
            }
        }

        /// <summary>
        /// 获取参数名称
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="providerType">提供程序类型</param>
        /// <returns>格式化后的参数名称</returns>
        public static string GetParameterName(string name, DbProviderType providerType)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var prefix = GetParameterPrefix(providerType);

            // 如果参数名称已经包含前缀，则不添加
            if (name.StartsWith(prefix))
                return name;

            return $"{prefix}{name}";
        }

        /// <summary>
        /// 从连接字符串获取数据库名称
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="providerType">提供程序类型</param>
        /// <returns>数据库名称</returns>
        public static string GetDatabaseName(string connectionString, DbProviderType providerType)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            var builder = new ConnectionStringBuilder(providerType, connectionString);

            switch (providerType)
            {
                case DbProviderType.SQLite:
                    return builder.GetValue<string>("Data Source");

                case DbProviderType.SqlServer:
                    return builder.GetValue<string>("Initial Catalog");

                case DbProviderType.PostgreSQL:
                    return builder.GetValue<string>("Database");

                case DbProviderType.LiteDB:
                    return builder.GetValue<string>("Filename");

                default:
                    return builder.GetValue<string>("Database");
            }
        }

        /// <summary>
        /// 获取表存在的 SQL
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="providerType">提供程序类型</param>
        /// <returns>SQL 语句</returns>
        public static string GetTableExistsSql(string tableName, DbProviderType providerType)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));

            switch (providerType)
            {
                case DbProviderType.SQLite:
                    return $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}'";

                case DbProviderType.SqlServer:
                    return $"SELECT OBJECT_ID('{tableName}', 'U')";

                case DbProviderType.PostgreSQL:
                    return $"SELECT to_regclass('{tableName}')";

                case DbProviderType.LiteDB:
                    return string.Empty; // LiteDB 不支持 SQL 查询
                default:
                    return $"SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";
            }
        }

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="direction">参数方向</param>
        /// <returns>参数</returns>
        public static DbParameter CreateParameter(DbCommand command, string name, object value, DbType? dbType = null, ParameterDirection direction = ParameterDirection.Input)
        {
            Guard.NotNull(command, nameof(command));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value ?? DBNull.Value;
            parameter.Direction = direction;

            if (dbType.HasValue)
                parameter.DbType = dbType.Value;

            return parameter;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="direction">参数方向</param>
        /// <returns>参数</returns>
        public static DbParameter AddParameter(DbCommand command, string name, object value, DbType? dbType = null, ParameterDirection direction = ParameterDirection.Input)
        {
            var parameter = CreateParameter(command, name, value, dbType, direction);
            command.Parameters.Add(parameter);
            return parameter;
        }

        /// <summary>
        /// 添加输出参数
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="name">参数名称</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns>参数</returns>
        public static DbParameter AddOutputParameter(DbCommand command, string name, DbType dbType)
        {
            return AddParameter(command, name, null, dbType, ParameterDirection.Output);
        }

        /// <summary>
        /// 添加输入输出参数
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns>参数</returns>
        public static DbParameter AddInputOutputParameter(DbCommand command, string name, object value, DbType dbType)
        {
            return AddParameter(command, name, value, dbType, ParameterDirection.InputOutput);
        }

        /// <summary>
        /// 添加返回值参数
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="name">参数名称</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns>参数</returns>
        public static DbParameter AddReturnValueParameter(DbCommand command, string name, DbType dbType)
        {
            return AddParameter(command, name, null, dbType, ParameterDirection.ReturnValue);
        }

        /// <summary>
        /// 添加参数集合
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="parameters">参数集合</param>
        public static void AddParameters(DbCommand command, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            Guard.NotNull(command, nameof(command));
            Guard.NotNull(parameters, nameof(parameters));

            foreach (var parameter in parameters)
            {
                AddParameter(command, parameter.Key, parameter.Value);
            }
        }

        /// <summary>
        /// 指定 SQL 和参数执行命令
        /// </summary>
        /// <param name="connection">连接</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(IDbConnection connection, string commandText, object parameters = null)
        {
            Guard.NotNull(connection, nameof(connection));

            if (string.IsNullOrEmpty(commandText))
                throw new ArgumentNullException(nameof(commandText));

            using var command = connection.CreateCommand();
            command.CommandText = commandText;

            if (parameters != null)
            {
                AddParameters(command as DbCommand, GetParameters(parameters));
            }

            if (connection.State != ConnectionState.Open)
                connection.Open();

            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// 指定 SQL 和参数异步执行命令
        /// </summary>
        /// <param name="connection">连接</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="parameters">参数集合</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>受影响的行数</returns>
        public static async Task<int> ExecuteNonQueryAsync(DbConnection connection, string commandText, object parameters = null, CancellationToken cancellationToken = default)
        {
            Guard.NotNull(connection, nameof(connection));

            if (string.IsNullOrEmpty(commandText))
                throw new ArgumentNullException(nameof(commandText));

            using var command = connection.CreateCommand();
            command.CommandText = commandText;

            if (parameters != null)
            {
                AddParameters(command, GetParameters(parameters));
            }

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            return await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 指定 SQL 和参数执行查询，返回第一个结果
        /// </summary>
        /// <param name="connection">连接</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>第一个结果</returns>
        public static object ExecuteScalar(IDbConnection connection, string commandText, object parameters = null)
        {
            Guard.NotNull(connection, nameof(connection));

            if (string.IsNullOrEmpty(commandText))
                throw new ArgumentNullException(nameof(commandText));

            using var command = connection.CreateCommand();
            command.CommandText = commandText;

            if (parameters != null)
            {
                AddParameters(command as DbCommand, GetParameters(parameters));
            }

            if (connection.State != ConnectionState.Open)
                connection.Open();

            return command.ExecuteScalar();
        }

        /// <summary>
        /// 指定 SQL 和参数异步执行查询，返回第一个结果
        /// </summary>
        /// <param name="connection">连接</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="parameters">参数集合</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>第一个结果</returns>
        public static async Task<object> ExecuteScalarAsync(DbConnection connection, string commandText, object parameters = null, CancellationToken cancellationToken = default)
        {
            Guard.NotNull(connection, nameof(connection));

            if (string.IsNullOrEmpty(commandText))
                throw new ArgumentNullException(nameof(commandText));

            using var command = connection.CreateCommand();
            command.CommandText = commandText;

            if (parameters != null)
            {
                AddParameters(command, GetParameters(parameters));
            }

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            return await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 指定 SQL 和参数执行查询，返回数据读取器
        /// </summary>
        /// <param name="connection">连接</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>数据读取器</returns>
        public static IDataReader ExecuteReader(IDbConnection connection, string commandText, object parameters = null)
        {
            Guard.NotNull(connection, nameof(connection));

            if (string.IsNullOrEmpty(commandText))
                throw new ArgumentNullException(nameof(commandText));

            var command = connection.CreateCommand();
            command.CommandText = commandText;

            if (parameters != null)
            {
                AddParameters(command as DbCommand, GetParameters(parameters));
            }

            if (connection.State != ConnectionState.Open)
                connection.Open();

            return command.ExecuteReader();
        }

        /// <summary>
        /// 指定 SQL 和参数异步执行查询，返回数据读取器
        /// </summary>
        /// <param name="connection">连接</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="parameters">参数集合</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>数据读取器</returns>
        public static async Task<DbDataReader> ExecuteReaderAsync(DbConnection connection, string commandText, object parameters = null, CancellationToken cancellationToken = default)
        {
            Guard.NotNull(connection, nameof(connection));

            if (string.IsNullOrEmpty(commandText))
                throw new ArgumentNullException(nameof(commandText));

            var command = connection.CreateCommand();
            command.CommandText = commandText;

            if (parameters != null)
            {
                AddParameters(command, GetParameters(parameters));
            }

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            return await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取参数集合
        /// </summary>
        /// <param name="parameters">参数对象</param>
        /// <returns>参数集合</returns>
        private static IEnumerable<KeyValuePair<string, object>> GetParameters(object parameters)
        {
            if (parameters == null)
                return Enumerable.Empty<KeyValuePair<string, object>>();

            if (parameters is IDictionary<string, object> dictionary)
                return dictionary;

            return parameters.GetType().GetProperties()
                .Where(p => p.CanRead)
                .Select(p => new KeyValuePair<string, object>(p.Name, p.GetValue(parameters)));
        }
    }
}