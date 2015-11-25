using System;

namespace NZBDash.DataAccessLayer.Interfaces
{
	public interface ISqliteConfiguration
	{
		void CheckDb();
		string AssemblyDirectory();
		string DbFile();
		SqliteConnectionWrapper DbConnection ();
		void CreateDatabase();
	}
}

