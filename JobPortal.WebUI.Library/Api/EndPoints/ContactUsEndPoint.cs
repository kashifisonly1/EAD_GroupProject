using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using JobPortal.BusinessModels;

namespace JobPortal.WebUI.Library.Api.EndPoints
{
	public class ContactUsEndPoint
	{
		ApiHelper ApiHelper;

		public ContactUsEndPoint()
		{
			ApiHelper = new ApiHelper();
		}

		public async Task SendContactUsMessage(string token, ContactUsModel model)
		{
			ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			ApiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

			using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync("/api/ContactUs/SendMessage", model))
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

		public async Task DeleteMessage(string token, int id)
		{
			ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			ApiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

			using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync("/api/ContactUs/Delete", id))
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

		public async Task ResolveMessage(string token, int id)
		{
			ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			ApiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

			using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync("/api/ContactUs/Resolve", id))
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

		public async Task<List<ContactUsModel>> GetUnResolvedMessages(string token)
		{
			ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
			ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			ApiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

			using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("/api/ContactUs/Unresolved"))
			{
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsAsync<List<ContactUsModel>>();
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
