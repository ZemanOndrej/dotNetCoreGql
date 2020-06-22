using AutoMapper;
using db.Models;
using GraphQLCodeGen;

namespace WebApiPgGql.mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Account, Types.Account>();
			CreateMap<Reservation, Types.Reservation>();
			CreateMap<Types.Account, Account>();
			CreateMap<Types.Reservation, Reservation>();
		}
	}
}