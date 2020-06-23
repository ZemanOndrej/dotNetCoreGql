using System.Threading.Tasks;
using AutoMapper;
using db.Models;
using GraphQLCodeGen;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace webapiPgGql.resolver
{
	public class CustomReservation : Types.Reservation
	{
		public string accountId { get; set; }
		public int eventId { get; set; }
	}

	[ExtendObjectType(Name = "Reservation")]
	public class ReservationResolver
	{
		private readonly IMapper _mapper;

		public ReservationResolver(IMapper mapper)
		{
			_mapper = mapper;
		}

		public async Task<Types.Account> GetAccount([Parent] CustomReservation reservation,
			[Service] YogaDbContext dbContext)
		{
			var account =
				await dbContext.Account.FirstOrDefaultAsync(r => r.Id.ToString() == reservation.accountId);
			return _mapper.Map<Types.Account>(account);
		}

		public async Task<Types.Event> GetEvent([Parent] CustomReservation reservation,
			[Service] YogaDbContext dbContext)
		{
			var @event =
				await dbContext.Event.FirstOrDefaultAsync(r => r.Id == reservation.eventId);
			return _mapper.Map<Types.Event>(@event);
		}
	}
}