using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using JobPortal.WebUI.Library.Api.Models;

namespace JobPortal.WebUI.Library.Api
{
	public class ApiHelper
	{
		private HttpClient ApiClient { get; set; }

		public ApiHelper()
		{
			InitializeClient();
		}

		private void InitializeClient()
		{
			ApiClient = new HttpClient();
			ApiClient.BaseAddress = new Uri("https://localhost:44343/"); // TODO -- Update Api Base address during production
			ApiClient.DefaultRequestHeaders.Accept.Clear();
			ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<AuthenticatedUser> Authenticate(string username, string password)
		{
			var data = new FormUrlEncodedContent(new[]
			{
				new KeyValuePair<string, string>("grant_type", "password"),
				new KeyValuePair<string, string>("username", username),
				new KeyValuePair<string, string>("password", password)
			});

			using (HttpResponseMessage response = await ApiClient.PostAsync("/Token", data))
			{
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
					return result;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		private async Task CreateRoles()
		{

		}

	}
}
