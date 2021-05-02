using System.Collections.Generic;
using System.Threading.Tasks;
using db.Models;

namespace db.IRepo
{
	public interface IAccountRepository : IBaseRepository<Account>
	{
		Task<Account> Get(string id);
	}
}