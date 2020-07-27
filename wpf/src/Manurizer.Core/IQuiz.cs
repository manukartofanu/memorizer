using System.Collections.Generic;

namespace Manurizer.Core
{
	public interface IQuiz
	{
		void Initialize(IEnumerable<IQuestion> questions);
		IQuestion Next();
	}
}
