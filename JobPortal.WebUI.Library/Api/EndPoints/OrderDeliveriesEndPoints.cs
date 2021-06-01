using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JobPortal.BusinessModels.Orders;

namespace JobPortal.WebUI.Library.Api.EndPoints
{
    class OrderDeliveriesEndPoints
    {
        ApiHelper ApiHelper;
        public OrderDeliveriesEndPoints()
        {
            ApiHelper = new ApiHelper();
        }
        public async Task<List<OrderDelivery>> GetAllOrderDeliveries()
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("/api/OrderDeliveries"))
            {
                var result = await response.Content.ReadAsAsync<List<OrderDelivery>>();
                return result;
            }
        }
        public async Task<OrderDelivery> GetOrderDelivery(int id)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"/api/OrderDeliveries/{id}"))
            {
                var result = await response.Content.ReadAsAsync<OrderDelivery>();
                return result;
            }
        }
        public async Task<HttpResponseMessage> AddOrderDelivery(OrderDelivery orderDelivery)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync("/api/OrderDeliveries", orderDelivery))
            {
                return response;
            }
        }
        public async Task<HttpResponseMessage> UpdateOrderDelivery(OrderDelivery orderDelivery)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync($"/api/OrderDeliveries/{orderDelivery.Id}", orderDelivery))
            {
                return response;
            }
        }
        public async Task<HttpResponseMessage> DeleteOrderDelivery(int id)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync($"/api/OrderDeliveries/{id}"))
            {
                return response;
            }
        }
    }
}