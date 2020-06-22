using HotChocolate.Types;

namespace webapiPgGql.models
{
	public class QueryType : ObjectType<Query>
	{
		protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
		{
			base.Configure(descriptor);

			descriptor.Field(q => q.GetAccounts(default))
				.ListType<AccountType>();

			descriptor.Field(q => q.GetAccount(default, default))
				.Argument("id", a => a.Type<IdType>(nullable: false));

			descriptor.Field(q => q.GetAccountReservations(default, default))
				.ListType<ReservationType>()
				.Argument("id", a => a.Type<IdType>(nullable: false));
		}
	}
}