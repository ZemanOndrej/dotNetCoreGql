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

	[ExtendObjectType(Name = "Account")]
	public class AccountResolver
	{
		private readonly IMapper _mapper;

		public AccountResolver(IMapper mapper)
		{
			_mapper = mapper;
		}

		public async Task<IReadOnlyList<CustomReservation>> GetReservations([Parent] Types.Account account,
			[Service] YogaDbContext dbContext)
		{
			var reservations =
				await dbContext.Reservation.Where(r => r.AccountId.ToString() == account.id).ToListAsync();
			return _mapper.Map<List<CustomReservation>>(reservations);
		}
	}
}