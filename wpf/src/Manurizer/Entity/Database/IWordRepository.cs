
namespace Manurizer.Entity.Database
{
	public interface IWordRepository : IReadRepository<Word>
	{
		void CreateItem(Word item);
		Word[] GetAllItemsEx();
		void DeleteItem(long id);
	}
}
