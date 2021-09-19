using Dapper;
using System.Data;
using System.Data.SQLite;

namespace Manurizer.Entity.Database
{
	public class MeaningRepository : GenericRepository<Meaning>, IReadRepository<Meaning>
	{
		public MeaningRepository(string connectionString)
			: base(connectionString, "Meaning")
		{
		}

		public void CreateItem(Meaning item)
		{
			using (IDbConnection connection = new SQLiteConnection(_connectionString))
			{
				connection.Execute("insert into Meaning (WordId, Text, Example, DateCreated) values (@WordId, @Text, @Example, @DateCreated)", item);
			}
		}

		public void DeleteItem(long wordId)
		{
			using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
			{
				connection.Open();
				SQLiteTransaction transaction = connection.BeginTransaction();
				var parameter = new { WordId = wordId };
				connection.Execute($"delete from {_tableName} where WordId = @WordId", parameter);
				transaction.Commit();
				connection.Close();
			}
		}
	}
}
