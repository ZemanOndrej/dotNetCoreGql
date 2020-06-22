using GraphQLCodeGen;
using HotChocolate.Types;

namespace webapiPgGql.models
{
	public class AddAccountInputType : InputObjectType<Types.AddAccountInput>
	{
		protected override void Configure(IInputObjectTypeDescriptor<Types.AddAccountInput> descriptor)
		{
			base.Configure(descriptor);

			descriptor.Field(i => i.email).Type<NonNullType<IdType>>();
			descriptor.Field(i => i.name).Type<NonNullType<StringType>>();
			descriptor.Field(i => i.surname).Type<NonNullType<StringType>>();
			descriptor.Field(i => i.clientMutationId).Type<NonNullType<StringType>>();
		}
	}
}