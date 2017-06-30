﻿﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FinalAssignment.Models;
using FinalAssignment.Data;
using AutoMapper;
using FinalAssignment.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FinalAssignment
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			using (var Database = new DatabaseContext())
			{
				Database.Database.EnsureCreatedAsync();
			}
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddMvc();
			services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>();
			services.AddAuthorization(options =>
			                          options.AddPolicy("Medic", policy => policy.RequireClaim("Role", "Medic"))                         
         	);
			services.AddAuthorization(options =>
									  options.AddPolicy("Patient", policy => policy.RequireClaim("Role", "Patient"))
			 );
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			Mapper.Initialize(Mapper =>
			{
				Mapper.CreateMap<CreateViewModel, Patients>();
				Mapper.CreateMap<CreateViewModel, Medics>();
				Mapper.CreateMap<LoginViewModel, Patients>();
				Mapper.CreateMap<LoginViewModel, Medics>();
			});

			app.UseStaticFiles();

			app.UseCookieAuthentication(new CookieAuthenticationOptions()
			{
				AuthenticationScheme = "CookieMiddleware",
				LoginPath = new PathString("/Account/Login"),
				AccessDeniedPath = new PathString("/Account/Forbidden/"),
				AutomaticAuthenticate = true,
				AutomaticChallenge = true
			});

			app.UseClaimsTransformation(context =>
			{
				if (context.Principal.Identity.IsAuthenticated)
				{
					context.Principal.Identities.First().AddClaim(new Claim("IsAuthenticated", DateTime.Now.ToString()));
				}
				return Task.FromResult(context.Principal);
			});

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

			DbInitializer.Initialize(new DatabaseContext());
		}
	}
}
