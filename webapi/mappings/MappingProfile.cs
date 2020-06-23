using AutoMapper;
using db.Models;
using GraphQLCodeGen;
using webapiPgGql.resolver;

namespace WebApiPgGql.mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Account, Types.Account>();
			CreateMap<Reservation, CustomReservation>();
			CreateMap<Types.Account, Account>();
			CreateMap<CustomReservation, Reservation>();
			CreateMap<Types.Event, Event>();
			CreateMap<Event, Types.Event>();
		}
	}
}