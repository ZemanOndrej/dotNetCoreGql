using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using db.Models;
using GraphQLCodeGen;
using HotChocolate;
using Microsoft.EntityFrameworkCore;

namespace webapiPgGql.resolver
{
	public class QueryResolver
	{
		private readonly IMapper _mapper;

		public QueryResolver(IMapper mapper)
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

		public async Task<IReadOnlyList<CustomReservation>> GetAccountReservations([Service] YogaDbContext dbContext,
			string id)
		{
			var reservations = await dbContext.Reservation.Where(r => r.AccountId.ToString() == id).ToListAsync();
			return _mapper.Map<List<CustomReservation>>(reservations);
		}

		public async Task<Types.Account> GetNode([Service] YogaDbContext context, string id)
		{
			return _mapper.Map<Types.Account>(await context.Account.FirstOrDefaultAsync());
		}

		public async Task<List<Types.Event>> GetEvents([Service] YogaDbContext context)
		{
			var events = await context.Event.ToListAsync();
			return _mapper.Map<List<Types.Event>>(events);
		}
	}
}