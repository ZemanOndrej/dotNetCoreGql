using System;
using System.Collections.Generic;
using AutoMapper;
using db.Models;
using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GraphiQl;
using GraphQLCodeGen;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WebApiPgGql.mappings;
using webapiPgGql.resolver;


namespace WebApiPgGql
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}


		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
			var mapper = mappingConfig.CreateMapper();
			services.AddSingleton(mapper);
			services.AddDataLoaderRegistry();
			services.AddGraphQL(sp =>
				SchemaBuilder.New().AddDocumentFromFile(Configuration["schemaLocation"])
					.BindComplexType<Types.Query>()
					.BindResolver<QueryResolver>(c => c.To<Types.Query>())
					.BindComplexType<Types.Account>()
					.BindResolver<AccountResolver>(c => c.To<Types.Account>())
					.BindComplexType<Types.Mutation>()
					.BindResolver<MutationResolver>(c => c.To<Types.Mutation>())
					.BindComplexType<Types.Event>()
					.BindResolver<EventResolver>(c => c.To<Types.Event>())
					.BindComplexType<CustomReservation>(c => c.To("Reservation"))
					.BindResolver<ReservationResolver>(c => c.To<CustomReservation>())
					.BindComplexType<Types.AddReservationInput>()
					.BindComplexType<Types.AddAccountInput>()
					.BindComplexType<Types.AddEventInput>()
					.BindComplexType<Types.AddEventPayload>()
					.BindComplexType<Types.AddAccountPayload>()
					.BindComplexType<Types.AddReservationPayload>()
					.BindClrType<DateTime, TimeType>()
					.Create());
			services.AddDbContext<YogaDbContext>(ServiceLifetime.Transient);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHttpsRedirection();
			}

			app.UseRouting();
			app.UseGraphiQl();
			app
				.UseGraphQLHttpPost(new HttpPostMiddlewareOptions {Path = "/graphql"})
				.UseGraphQLHttpGetSchema(new HttpGetSchemaMiddlewareOptions {Path = "/graphql/schema"});
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}