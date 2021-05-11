using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

using JobPortal.WebUI.Library.Api.Models;

using System.Threading.Tasks;
using JobPortal.BusinessModels;

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

		public async Task DoSomethingWithAPi(string token, string data)
		{
			ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			ApiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

			using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync("/api/admin/doSomething", data))
			{
				if (response.IsSuccessStatusCode)
				{
					// log
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task<string> GetSomethingFromAPi(string token)
		{
			ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			ApiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

			using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("/api/admin/getSomething"))
			{
				if (response.IsSuccessStatusCode)
				{
					string result = await response.Content.ReadAsAsync<string>();
					return result;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task<string> GetSomethingByIdFromAPi(string token, string id)
		{
			ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			ApiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

			using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync("/api/admin/getSomething", id))
			{
				if (response.IsSuccessStatusCode)
				{
					string result = await response.Content.ReadAsAsync<string>();
					return result;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		public async Task<ContactUsModel> GetSomeObjectFromAPi(string token)
		{
			ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			ApiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

			using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("/api/admin/getContactUsModel"))
			{
				if (response.IsSuccessStatusCode)
				{
					ContactUsModel result = await response.Content.ReadAsAsync<ContactUsModel>();
					return result;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}
	}
}
