using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using db.Models;
using GraphQLCodeGen;
using HotChocolate;
using Microsoft.EntityFrameworkCore;

namespace webapiPgGql.models
{
	public class Query
	{
		private readonly IMapper _mapper;

		public Query(IMapper mapper)
		{
			_mapper = mapper;
		}

		public async Task<IReadOnlyList<Types.Account>> GetAccounts([Service] YogaDbContext dbContext)
		{
			var accounts = await dbContext.Account.ToListAsync();
			return _mapper.Map<List<Types.Account>>(accounts);
		}

		public async Task<Types.Account> GetAccount([Service] YogaDbContext dbContext, string id)
		{
			var account = await dbContext.Account.FindAsync(id);
			return _mapper.Map<Types.Account>(account);
		}

		public async Task<IReadOnlyList<Types.Reservation>> GetAccountReservations([Service] YogaDbContext dbContext,
			string id)
		{
			var reservations = await dbContext.Reservation.Where(r => r.AccountId.ToString() == id).ToListAsync();
			return _mapper.Map<List<Types.Reservation>>(reservations);
		}

		public async Task<Types.Account> GetNode([Service] YogaDbContext context, string id)
		{
			return _mapper.Map<Types.Account>(await context.Account.FirstOrDefaultAsync());
		}
	}
}