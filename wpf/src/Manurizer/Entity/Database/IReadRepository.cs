using System;
using System.Collections.Generic;

namespace Manurizer.Entity.Database
{
	public interface IReadRepository<T> : IDisposable
		where T : class
	{
		IEnumerable<T> GetAllItems();
		T GetItem(long id);
	}
}
