using JobPortalBlazor.Client.Services;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddOptions();
			builder.Services.AddAuthorizationCore();
			builder.Services.AddScoped<IdentityAuthenticationStateProvider>();
			builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<IdentityAuthenticationStateProvider>());
			builder.Services.AddScoped<IAuthorizeApi, AuthorizeApi>();

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			builder.Services.AddTransient<CategoryServices>();
			builder.Services.AddTransient<UserServices>();
			builder.Services.AddTransient<Uploader>();
			builder.Services.AddTransient<GigServices>();
			builder.Services.AddTransient<RequestServices>();
			builder.Services.AddTransient<OrderServices>();
			builder.Services.AddTransient<OrderDeliveryService>();
			builder.Services.AddTransient<ContactService>();
			builder.Services.AddTransient<SkillService>();
			await builder.Build().RunAsync();
		}
	}
}
