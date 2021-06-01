using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JobPortal.BusinessModels.Freelancers;

namespace JobPortal.WebUI.Library.Api.EndPoints
{
    class FreelancersEndPoints
    {
        ApiHelper ApiHelper;
        public FreelancersEndPoints()
        {
            ApiHelper = new ApiHelper();
        }
        public async Task<List<Freelancer>> GetAllFreelancers()
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("/api/Freelancers"))
            {
                var result = await response.Content.ReadAsAsync<List<Freelancer>>();
                return result;
            }
        }
        public async Task<Freelancer> GetFreelancer(int id)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"/api/Freelancers/{id}"))
            {
                var result = await response.Content.ReadAsAsync<Freelancer>();
                return result;
            }
        }
        public async Task<HttpResponseMessage> AddFreelancer(Freelancer freelancer)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync("/api/Freelancers", freelancer))
            {
                return response;
            }
        }
        public async Task<HttpResponseMessage> UpdateFreelancer(Freelancer freelancer)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync($"/api/Freelancers/{freelancer.Id}", freelancer))
            {
                return response;
            }
        }
        public async Task<HttpResponseMessage> DeleteFreelancer(int id)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync($"/api/Freelancers/{id}"))
            {
                return response;
            }
        }
    }
}
