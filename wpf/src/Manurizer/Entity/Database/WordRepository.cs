using Dapper;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Manurizer.Entity.Database
{
	public class WordRepository : GenericRepository<Word>, IReadRepository<Word>
	{
		public WordRepository(string connectionString)
			: base(connectionString, "Word")
		{
		}

		public void CreateItem(Word item)
		{
			long wordId;
			using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
			{
				connection.Open();
				SQLiteTransaction transaction = connection.BeginTransaction();
				connection.Execute("insert into Word (Id, Name, Transcription, Class, GuideWord) values (@Id, @Name, @Transcription, @Class, @GuideWord)", item);
				wordId = connection.LastInsertRowId;
				transaction.Commit();
				connection.Close();
			}
			using (MeaningRepository repository = new MeaningRepository(_connectionString))
			{
				foreach (var meaning in item.MeaningList)
				{
					meaning.WordId = wordId;
					repository.CreateItem(meaning);
				}
			}
		}

		public Word[] GetAllItemsEx()
		{
			Word[] words = new Word[0];
			using (IDbConnection connection = new SQLiteConnection(_connectionString))
			{
				words = connection.Query<Word>("select * from Word").ToArray();
				foreach (var word in words)
				{
					var parameterWord = new { WordId = word.Id };
					word.MeaningList = connection.Query<Meaning>("select * from Meaning where WordId = @WordId", parameterWord).ToArray();
				}
			}
			return words;
		}
	}
}
