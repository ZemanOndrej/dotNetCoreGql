using HotChocolate.Types;

namespace webapiPgGql.models
{
	public class MutationType : ObjectType<Mutation>
	{
		protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
		{
			base.Configure(descriptor);

			descriptor.Field(m => m.AddAccount(default, default))
				.Type<AccountType>()
				.Argument("input", a => a.Type<AddAccountInputType>(nullable: false));
		}
	}
}