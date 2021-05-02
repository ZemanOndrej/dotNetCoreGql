using System.Collections.Generic;
using System.Threading.Tasks;

namespace db.IRepo
{
	public interface IBaseRepository<T>
	{
		Task<List<T>> GetAll();
		Task<T> Add(T entity);
	}
}