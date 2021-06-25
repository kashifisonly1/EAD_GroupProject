using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Services
{
	public class GigServices
	{
        Uploader uploader { get; set; }
		private readonly HttpClient httpClient;

        public GigServices(HttpClient httpClient) { this.httpClient = httpClient; uploader = new Uploader(httpClient); }

        public async Task<List<Models.Gig>> getMyGigs(String userID)
        {
            List<Models.Gig> gigs = new List<Models.Gig>();
            /**************************************************************************************/
            /*******************************APPLY ODATA HERE***************************************/
            /*
             * you need to load all gigs that contains freelancers having user of Id = userID(argument received)
             * you need to know that, gigs table has freelancers, and freelancer table has user
             * userId is matched with user table Id so keep this in mind
             */
            JobPortalBlazor.Shared.Gig[] fList =
                await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.Gig[]>("/api/Gigs");
            foreach(JobPortalBlazor.Shared.Gig f in fList)
            {
                if(userID == f.Freelancer.User.Id)
                {
                    gigs.Add(new Models.Gig(f));
                    break;
                }
            }
            /**************************************************************************************/
            return gigs;
        }

        public async Task<List<Models.Gig>> getGigsByCategory(int catID)
        {
            List<Models.Gig> gigs = new List<Models.Gig>();
            JobPortalBlazor.Shared.Category cat = await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.Category>("/api/Categories/" + catID);
            foreach (JobPortalBlazor.Shared.Gig g in cat.Gigs)
            {
                g.Category = cat;
                gigs.Add(new Models.Gig(g));
            }
            return gigs;
        }

        public async Task<Models.Gig> getGigByID(int id)
        {
            JobPortalBlazor.Shared.Gig cat = await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.Gig>("/api/Gigs/" + id);
            var f = await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.Freelancer>("api/Freelancers/" + cat.Freelancer.Id);
            cat.Freelancer = f;
            return new Models.Gig(cat);
        }

        public async Task<Models.Gig> addGig(Models.Gig gig)
        {
            gig.ID = 0;
            gig.ImageUrl = await uploader.UploadFile(gig.Image);
            JobPortalBlazor.Shared.Freelancer[] fList =
                await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.Freelancer[]>("/api/Freelancers");
            foreach (JobPortalBlazor.Shared.Freelancer f in fList)
            {
                if (gig.UserID == f.User.Id)
                {
                    gig.freelancerID = f.Id;
                    break;
                }
            }
            HttpResponseMessage receivedCat = await this.httpClient.PostAsJsonAsync<JobPortalBlazor.Shared.Gig>("/api/Gigs", gig);
            JobPortalBlazor.Shared.Gig cat = await receivedCat.Content.ReadFromJsonAsync<JobPortalBlazor.Shared.Gig>();
            return new Models.Gig(cat);
        }

        public async Task<Models.Gig> updateGig(Models.Gig category)
        {
            category.ImageUrl = await uploader.UploadFile(category.Image);
            HttpResponseMessage receivedCat = await this.httpClient.PutAsJsonAsync<JobPortalBlazor.Shared.Gig>("/api/Gigs/" + category.ID, category);
            return category;
        }

        public async Task<int> deleteGig(Models.Gig category)
        {
            await this.httpClient.DeleteAsync("/api/Gigs/" + category.ID);
            return category.ID;
        }
    }
}
