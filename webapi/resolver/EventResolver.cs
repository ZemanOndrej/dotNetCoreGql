using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using db.Models;
using GraphQLCodeGen;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace webapiPgGql.resolver
{

	[ExtendObjectType(Name = "Event")]
	public class EventResolver
	{
		private readonly IMapper _mapper;

		public EventResolver(IMapper mapper)
		{
			_mapper = mapper;
		}

		public async Task<IReadOnlyList<CustomReservation>> GetReservations([Parent] Types.Event @event,
			[Service] YogaDbContext dbContext)
		{
			var reservations =
				await dbContext.Reservation.Where(r => r.EventId.ToString() == @event.id).ToListAsync();
			return _mapper.Map<List<CustomReservation>>(reservations);
		}
	}
}