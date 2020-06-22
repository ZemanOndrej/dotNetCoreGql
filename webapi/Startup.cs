using System;
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
using webapiPgGql.models;


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
					.BindComplexType<Query>(b => b
						.To("Query")
						.Field(t => t.GetAccounts(default)).Name("accounts")
						.Field(t => t.GetNode(default, "")).Name("node")) // todo id 
					.BindComplexType<Types.Mutation>()
					.BindComplexType<Types.Account>()
					.BindComplexType<Types.AddReservationInput>()
					.BindComplexType<Types.AddAccountInput>()
					.BindComplexType<Types.AddAccountPayload>()
					.BindComplexType<Types.AddReservationPayload>()
					.BindComplexType<Types.Reservation>()
					.BindClrType<DateTime, TimeType>()
					.Create());
			services.AddEntityFrameworkNpgsql().AddDbContext<YogaDbContext>();
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