using GraphQLCodeGen;
using HotChocolate.Types;

namespace webapiPgGql.models
{
	public class ReservationType : ObjectType<Types.Reservation>
	{
		protected override void Configure(IObjectTypeDescriptor<Types.Reservation> descriptor)
		{
			base.Configure(descriptor);

			descriptor.Field(a => a.@event).Type<StringType>(nullable: false);
			descriptor.Field(a => a.spotCount).Type<IntType>(nullable: false);
			descriptor.Field(a => a.account).Type<AccountType>(nullable: false);
		}
	}
}