using Dapper;
using System.Data;
using System.Data.SQLite;

namespace Manurizer.Entity.Database
{
	public class DatabaseSchemaUpdater
	{
		public static void Update()
		{
			long version = 0;
			using (IDbConnection connection = new SQLiteConnection(DatabaseSourceDefinitor.ConnectionString))
			{
				version = (long)connection.ExecuteScalar("select Version from DbInfo");
			}
			if (version < 2)
			{
				Update002();
			}
			if (version < 3)
			{
				Update003();
			}
		}

		private static void Update002()
		{
			using (IDbConnection connection = new SQLiteConnection(DatabaseSourceDefinitor.ConnectionString))
			{
				connection.Execute("alter table Meaning rename to Meaning_Temp");
				connection.Execute(
	@"CREATE TABLE Meaning (
		Id  INTEGER NOT NULL UNIQUE,
		WordId  INTEGER NOT NULL,
		Text  TEXT NOT NULL,
		Example TEXT,
		DateCreated TEXT,
		PRIMARY KEY(Id AUTOINCREMENT));");
				connection.Execute(
	@"INSERT INTO Meaning(Id, WordId, Text, Example, DateCreated)
		SELECT Id, WordId, Text, Example, DateCreated
		FROM Meaning_Temp;");
				connection.Execute("drop table Meaning_Temp");
				connection.Execute("update	DbInfo set Version = 2");
			}
		}

		private static void Update003()
		{
			using (IDbConnection connection = new SQLiteConnection(DatabaseSourceDefinitor.ConnectionString))
			{
				connection.Execute(
	@"CREATE TABLE Word_Answer (
	WordId  INTEGER NOT NULL,
	Time  REAL NOT NULL,
	IsCorrect INTEGER NOT NULL); ");
				connection.Execute("update DbInfo set Version = 3");
			}
		}
	}
}
