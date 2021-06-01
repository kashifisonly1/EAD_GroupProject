using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JobPortal.BusinessModels.Freelancers;

namespace JobPortal.WebUI.Library.Api.EndPoints
{
    class SkillsEndPoints
    {
        ApiHelper ApiHelper;
        public SkillsEndPoints()
        {
            ApiHelper = new ApiHelper();
        }
        public async Task<List<Skill>> GetAllSkills()
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("/api/Skills"))
            {
                var result = await response.Content.ReadAsAsync<List<Skill>>();
                return result;
            }
        }
        public async Task<Skill> GetSkill(int id)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"/api/Skills/{id}"))
            {
                var result = await response.Content.ReadAsAsync<Skill>();
                return result;
            }
        }
        public async Task<HttpResponseMessage> AddSkill(Skill skill)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync("/api/Skills", skill))
            {
                return response;
            }
        }
        public async Task<HttpResponseMessage> UpdateSkill(Skill skill)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync($"/api/Skills/{skill.Id}", skill))
            {
                return response;
            }
        }
        public async Task<HttpResponseMessage> DeleteSkill(int id)
        {
            ApiHelper.ApiClient.DefaultRequestHeaders.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync($"/api/Skills/{id}"))
            {
                return response;
            }
        }
    }
}