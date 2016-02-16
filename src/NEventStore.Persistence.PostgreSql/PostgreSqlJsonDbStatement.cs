using NEventStore.Persistence.Sql;
using NEventStore.Persistence.Sql.SqlDialects;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Transactions;

namespace NEventStore.Persistence.PostgreSql
{
    public class PostgreSqlJsonDbStatement : CommonDbStatement
    {
        private readonly ISqlDialect _dialect;

        public PostgreSqlJsonDbStatement(ISqlDialect dialect, TransactionScope scope, IDbConnection connection,
            IDbTransaction transaction) : base(dialect, scope, connection, transaction)
        {
            _dialect = dialect;
        }

        protected override void SetParameterValue(IDataParameter param, object value, DbType? type)
        {
            base.SetParameterValue(param, value, type);
            if (param.ParameterName == _dialect.Payload || param.ParameterName == _dialect.Headers)
                ((NpgsqlParameter)param).NpgsqlDbType = NpgsqlDbType.Jsonb;
        }
    }
}