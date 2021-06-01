using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JobPortal.BusinessModels.Gigs;

namespace JobPortal.WebUI.Library.Api.EndPoints
{
    class GigsEndPoints
    {
        ApiHelper ApiHelper;
        public GigsEndPoints()
        {
            ApiHelper = new ApiHelper();
        }
        public async Task<List<Gig>> GetAllGigs()
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("/api/Gigs"))
            {
                var result = await response.Content.ReadAsAsync<List<Gig>>();
                return result;
            }
        }
        public async Task<Gig> GetGig(int id)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"/api/Gigs/{id}"))
            {
                var result = await response.Content.ReadAsAsync<Gig>();
                return result;
            }
        }
        public async Task<HttpResponseMessage> AddGig(Gig gig)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync("/api/Gigs", gig))
            {
                return response;
            }
        }
        public async Task<HttpResponseMessage> UpdateGig(Gig gig)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync($"/api/Gigs/{gig.Id}", gig))
            {
                return response;
            }
        }
        public async Task<HttpResponseMessage> DeleteGig(int id)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync($"/api/Gigs/{id}"))
            {
                return response;
            }
        }
    }
}