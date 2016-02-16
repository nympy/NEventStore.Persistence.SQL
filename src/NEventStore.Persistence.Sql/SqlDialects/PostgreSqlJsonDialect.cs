namespace NEventStore.Persistence.Sql.SqlDialects
{
    using System;

    public class PostgreSqlJsonDialect : CommonSqlDialect
    {
        public override string InitializeStorage
        {
            get { return PostgreSqlJsonStatements.InitializeStorage; }
        }

        public override string MarkCommitAsDispatched
        {
            get { return base.MarkCommitAsDispatched.Replace("1", "true"); }
        }

        public override string PersistCommit
        {
            get { return PostgreSqlJsonStatements.PersistCommits; }
        }

        public override string GetUndispatchedCommits
        {
            get { return base.GetUndispatchedCommits.Replace("0", "false"); }
        }

        public override bool IsDuplicate(Exception exception)
        {
            string message = exception.Message.ToUpperInvariant();
            return message.Contains("23505") || message.Contains("IX_COMMITS_COMMITSEQUENCE");
        }
    }
}