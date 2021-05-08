using System;
using System.Threading.Tasks;
using AutoMapper;
using db.Models;
using GraphQLCodeGen;
using HotChocolate;
using Microsoft.EntityFrameworkCore;

namespace webapiPgGql.resolver
{
	public class MutationResolver
	{
		private readonly IMapper _mapper;

		public MutationResolver(IMapper mapper)
		{
			_mapper = mapper;
		}

		public async Task<Types.AddAccountPayload> AddAccount([Service] YogaDbContext dbContext,
			Types.AddAccountInput input)
		{
			var account = new Account
			{
				Email = input.email,
				Name = input.name,
				Surname = input.surname,
				IsAdmin = false,
				ReceiveNewsletters = false,
				CreatedAt = DateTime.Now,
				AuthId = Guid.NewGuid().ToString()
			};

			dbContext.Account.Add(account);

			await dbContext.SaveChangesAsync();

			return new Types.AddAccountPayload
			{
				account = _mapper.Map<Types.Account>(account),
				clientMutationId = "test"
			};
		}

		public async Task<Types.AddReservationPayload> AddReservation([Service] YogaDbContext context,
			Types.AddReservationInput input)
		{
			var eId = int.Parse(input.eventId);
			var accId = Guid.Parse("d4c96458-b161-11ea-9c62-a759c513c356"); //this will be authenticated users ID
			var acc = await context.Account.FirstOrDefaultAsync(a => a.Id == accId);

			var @event = await context.Event.FirstOrDefaultAsync(e => e.Id == eId);
			var newReservation = new Reservation
			{
				Account = acc,
				SpotCount = input.spotCount,
				Event = @event,
			};

			context.Reservation.Add(newReservation);
			var payload = new Types.AddReservationPayload
			{
				account = _mapper.Map<Types.Account>(acc),
				@event = _mapper.Map<Types.Event>(@event),
				spotCount = input.spotCount
			};

			await context.SaveChangesAsync();
			return payload;
		}
	}
}