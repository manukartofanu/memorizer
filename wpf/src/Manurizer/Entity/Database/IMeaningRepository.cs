
namespace Manurizer.Entity.Database
{
	public interface IMeaningRepository : IReadRepository<Meaning>
	{
		void CreateItem(Meaning item);
		void DeleteItem(long wordId);
	}
}
