namespace NEventStore.Persistence.Sql.SqlDialects
{
    using System;

    public class PostgreSqlJsonDialect : CommonSqlDialect
    {
        public override string InitializeStorage => PostgreSqlJsonStatements.InitializeStorage;

        public override string MarkCommitAsDispatched => base.MarkCommitAsDispatched.Replace("1", "true");

        public override string PersistCommit => PostgreSqlJsonStatements.PersistCommits;

        public override string GetUndispatchedCommits => base.GetUndispatchedCommits.Replace("0", "false");

        public override bool IsDuplicate(Exception exception)
        {
            var message = exception.Message.ToUpperInvariant();
            return message.Contains("23505") || message.Contains("IX_COMMITS_COMMITSEQUENCE");
        }
    }
}