
#if WINDOWS|| DEBUG
using System.Data.SQLite;
#endif
#if LINUX 
using Mono.Data.Sqlite;
#endif

namespace NZBDash.DataAccessLayer.Interfaces
{
	#if LINUX
	public class SqliteConnectionWrapper
	{
		private string ConnectionString { get; set; }
		public SqliteConnectionWrapper (string connectionString)
		{
			ConnectionString = connectionString;
		}

		public SqliteConnection GetConnection()
		{
			return new SqliteConnection(ConnectionString);
		}
	}
	#endif

#if WINDOWS|| DEBUG
    public class SqliteConnectionWrapper
	{
		private string ConnectionString { get; set; }

		public SqliteConnectionWrapper (string connectionString)
		{
			ConnectionString = connectionString;
		}

		public SQLiteConnection GetConnection()
		{
			return new SQLiteConnection(ConnectionString);
		}
	}
	#endif
}
