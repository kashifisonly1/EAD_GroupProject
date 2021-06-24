using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Services
{
	public class FreelancerService
	{
		private readonly HttpClient httpClient;

		public FreelancerService(HttpClient httpClient) { this.httpClient = httpClient; }

		public async Task<Models.Freelancer> AddFreelancer(Models.Freelancer free)
		{
			var receivedCat = await this.httpClient.PostAsJsonAsync<JobPortalBlazor.Shared.Freelancer>("/api/Freelancers", free);
			JobPortalBlazor.Shared.Freelancer cat = await receivedCat.Content.ReadFromJsonAsync<JobPortalBlazor.Shared.Freelancer>();
			return new Models.Freelancer(cat);
		}
	}
}
