using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using JobPortal.Api.Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic.CompilerServices;

namespace JobPortal.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));
			services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();
			services.AddControllersWithViews();
			services.AddRazorPages();
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = "JwtBearer";
				options.DefaultChallengeScheme = "JwtBearer";
			})
				.AddJwtBearer("JwtBearer", jwtBearerOptions =>
				{
					jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMySuperSecretSecurityKeyDoNotShare")),
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						ClockSkew = TimeSpan.FromMinutes(5)
					};
				});

			services.AddSwaggerGen(setup =>
			{
				setup.SwaggerDoc(
					"v1",
					new OpenApiInfo
					{
						Title = "Job Portal Api",
						Version = "v1"
					}
					);

				setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header,
						},
						new List<string>()
					}
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{


			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseSwagger(c =>
			{
				c.RouteTemplate = "documentation/{documentName}/swagger.json";
			});
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/documentation/v1/swagger.json", "Job Portal Api");
				c.RoutePrefix = "documentation";
				//c.SwaggerEndpoint("/swagger/v1/swagger.json", "Job Portal Api");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				//endpoints.MapControllerRoute(
				//	name: "default",
				//	pattern: "{controller=Home}/{action=Index}/{id?}");
				//endpoints.MapRazorPages();
			});
		}
	}
}
