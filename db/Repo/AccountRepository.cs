using System.Collections.Generic;
using System.Threading.Tasks;
using db.IRepo;
using db.Models;

namespace db.Repo
{
	public class AccountRepository : IAccountRepository
	{

		public Task<List<Account>> GetAll()
		{
			throw new System.NotImplementedException();
		}

		public Task<Account> Add(Account entity)
		{
			throw new System.NotImplementedException();
		}

		public Task<Account> Get(string id)
		{
			throw new System.NotImplementedException();
		}
	}
}