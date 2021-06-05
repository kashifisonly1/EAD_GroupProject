using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JobPortal.BusinessModels;
using JobPortal.BusinessModels.General;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;



namespace JobPortal.WebApi
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
			services.AddDbContext<JobPortalContext>();

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<JobPortalContext>();

			services.AddMvcCore().AddApiExplorer();
			services.AddHealthChecks();

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

				//services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
				//.AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
				services.AddControllers();
			});
		}
		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

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
			});
		}
	}
}


