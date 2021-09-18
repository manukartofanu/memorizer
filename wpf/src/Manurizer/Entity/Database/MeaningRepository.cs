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
				connection.Execute("insert into Meaning (Id, WordId, Text, Example, DateCreated) values (@Id, @WordId, @Text, @Example, @DateCreated)", item);
			}
		}
	}
}
