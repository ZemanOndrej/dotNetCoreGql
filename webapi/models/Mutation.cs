using System;
using System.Threading.Tasks;
using AutoMapper;
using db.Models;
using GraphQLCodeGen;
using HotChocolate;

namespace webapiPgGql.models
{
	public class Mutation
	{
		private readonly IMapper _mapper;

		public Mutation(IMapper mapper)
		{
			_mapper = mapper;
		}

		public async Task<Types.Account> AddAccount([Service] YogaDbContext dbContext, Types.AddAccountInput input)
		{
			var account = new Account
			{
				Email = input.email,
				Name = input.name,
				Surname = input.surname,
				IsAdmin = false,
				ReceiveNewsletters = false,
				CreatedAt = DateTime.Now
			};

			dbContext.Account.Add(account);

			await dbContext.SaveChangesAsync();
			return _mapper.Map<Types.Account>(account);
		}
	}
}