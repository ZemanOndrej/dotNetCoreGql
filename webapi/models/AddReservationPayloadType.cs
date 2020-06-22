using GraphQLCodeGen;
using HotChocolate.Types;

namespace webapiPgGql.models
{
	public class AddReservationPayloadType : ObjectType<Types.AddReservationPayload>
	{
		protected override void Configure(IObjectTypeDescriptor<Types.AddReservationPayload> descriptor)
		{
			base.Configure(descriptor);

			descriptor.Field(i => i.@event).Type<NonNullType<StringType>>();
			descriptor.Field(i => i.account).Type<AccountType>(nullable: false);
			descriptor.Field(i => i.spotCount).Type<NonNullType<IntType>>();
		}
	}
}