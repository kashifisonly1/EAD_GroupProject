using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

using JobPortal.WebUI.Library.Api.Models;

using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace JobPortal.WebUI.Library.Api.EndPoints
{
	public class AdminEndPoints
	{
		ApiHelper ApiHelper;

		public AdminEndPoints()
		{
			ApiHelper = new ApiHelper();
		}

		public async Task PostContactUsMessage(string token, ContactUsModel form)
		{
			ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			ApiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

			using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync("/api/admin/postmessage", form))
			{
				if (response.IsSuccessStatusCode)
				{

				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}
	}
}
