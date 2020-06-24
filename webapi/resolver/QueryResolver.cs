using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using db.Models;
using GraphQLCodeGen;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace webapiPgGql.resolver
{
	public class QueryResolver
	{
		private readonly IMapper _mapper;

		public const string
			ACCOUNT = "Account",
			EVENT = "Event";


		public QueryResolver(IMapper mapper)
		{
			_mapper = mapper;
		}

		public async Task<IReadOnlyList<Types.Account>> GetAccounts([Service] YogaDbContext dbContext)
		{
			var accounts = await dbContext.Account.ToListAsync();
			return _mapper.Map<List<Types.Account>>(accounts);
		}


		public async Task<IReadOnlyList<CustomReservation>> GetAccountReservations([Service] YogaDbContext dbContext,
			string id)
		{
			var reservations = await dbContext.Reservation.Where(r => r.AccountId.ToString() == id).ToListAsync();
			return _mapper.Map<List<CustomReservation>>(reservations);
		}


		public async Task<Types.Node> GetNode([Service] YogaDbContext context, string id)
		{
			var type = FromGlobalId(id);
			throw new NotImplementedException("this is not viable code");
			//this needs to be here because graphql-code-gen doesnt create interface types in c#

//			switch (type.Type)
//			{
//				case ACCOUNT:
//					return await GetAccount(context, type.Id);
//				case EVENT:
//					return await GetEvent(context, type.Id);
//				default:
//					return new Types.Node();
//			}
		}

		public async Task<Types.Account> GetAccount([Service] YogaDbContext dbContext, string id)
		{
			var account = await dbContext.Account.FirstOrDefaultAsync(a => a.Id.ToString() == id);
			return _mapper.Map<Types.Account>(account);
		}

		public async Task<Types.Event> GetEvent([Service] YogaDbContext dbContext, string id)
		{
			var events = await dbContext.Event.FirstOrDefaultAsync(e => e.Id.ToString() == id);
			return _mapper.Map<Types.Event>(events);
		}

		public async Task<List<Types.Event>> GetEvents([Service] YogaDbContext context)
		{
			var events = await context.Event.ToListAsync();
			return _mapper.Map<List<Types.Event>>(events);
		}

		public class Local
		{
			public string Type { get; set; }
			public string Id { get; set; }
		}

		public Local FromGlobalId(string id)
		{
			var base64EncodedBytes = System.Convert.FromBase64String(id);
			var text = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
			var tokens = text.Split(':');
			return tokens.Length != 2 ? null : new Local {Type = tokens[0], Id = tokens[1]};
		}


	}
}