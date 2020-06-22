using GraphQLCodeGen;
using HotChocolate.Types;

namespace webapiPgGql.models
{
	public class AccountType : ObjectType<Types.Account>
	{
		protected override void Configure(IObjectTypeDescriptor<Types.Account> descriptor)
		{
			base.Configure(descriptor);

			descriptor.Field(a => a.id).Type<NonNullType<IdType>>();
			descriptor.Field(a => a.name).Type<NonNullType<StringType>>();
			descriptor.Field(a => a.surname).Type<NonNullType<StringType>>();
			descriptor.Field(a => a.email).Type<NonNullType<StringType>>();
			descriptor.Field(a => a.reservations).ListType<ReservationType>();
			descriptor.Field(a => a.createdAt).Type<DateTimeType>();
			descriptor.Field(a => a.updatedAt).Type<DateTimeType>();
		}
	}
}