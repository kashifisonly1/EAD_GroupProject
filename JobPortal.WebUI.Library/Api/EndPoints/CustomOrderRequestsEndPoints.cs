using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JobPortal.BusinessModels.Client;

namespace JobPortal.WebUI.Library.Api.EndPoints
{
    public class CustomOrderRequestsEndPoints
    {
        ApiHelper ApiHelper;
        public CustomOrderRequestsEndPoints()
        {
            ApiHelper = new ApiHelper();
        }
        public async Task<List<CustomOrderRequest>> GetAllCustomOrderRequests()
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("/api/CustomOrderRequests"))
            {
                var result = await response.Content.ReadAsAsync<List<CustomOrderRequest>>();
                return result;
            }
        }
        public async Task<CustomOrderRequest> GetCustomOrderRequest(int id)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"/api/CustomOrderRequests/{id}"))
            {
                var result = await response.Content.ReadAsAsync<CustomOrderRequest>();
                return result;
            }
        }
        public async Task<HttpResponseMessage> AddCustomOrderRequest(CustomOrderRequest customOrderRequest)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync("/api/CustomOrderRequests", customOrderRequest))
            {
                return response;
            }
        }
        public async Task<HttpResponseMessage> UpdateCustomOrderRequest(CustomOrderRequest customOrderRequest)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync($"/api/CustomOrderRequests/{customOrderRequest.Id}", customOrderRequest))
            {
                return response;
            }
        }
        public async Task<HttpResponseMessage> DeleteCustomOrderRequest(int id)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync($"/api/CustomOrderRequests/{id}"))
            {
                return response;
            }
        }
    }
}
