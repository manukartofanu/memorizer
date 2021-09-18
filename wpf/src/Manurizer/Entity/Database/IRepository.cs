
namespace Manurizer.Entity.Database
{
	public interface IRepository<T> : IReadRepository<T>
		where T : class
	{
		void CreateItem(T item);
	}
}
