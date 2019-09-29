using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Seeds;
using Books.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Books
{
	public class Startup
	{
		public static IConfiguration Configuration { get; set; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			// add Mvc
			services.AddMvc();

			// add DBContext to services
			services.AddDbContext<BookDbContext>(c => c.UseSqlServer(Configuration["connectionStrings:bookDbConnectionString"]));

			// add ICountryRepository to services
			services.AddScoped<ICountryRepository, CountryRepository>();

			// add ICategoryRepository to services
			services.AddScoped<ICategoryRepository, CategoryRepository>();

			// add IReviewerRepository to services
			services.AddScoped<IReviewerRepository, ReviewerRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, BookDbContext context)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}            

			// add Mvc to the pipeline
			app.UseMvc();

			// Seeding the DB
			context.SeedDataContext();
		}
	}
}
