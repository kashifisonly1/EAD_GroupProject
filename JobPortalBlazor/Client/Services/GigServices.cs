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
        [inject]
        Uploader uploader { get; set; }
        [inject]
        CategoryServices catService;
        [inject]
        UserServices userServices;
        private readonly HttpClient httpClient;

        public GigServices(HttpClient httpClient) => this.httpClient = httpClient;

        public async Task<List<Models.Gig>> getMyGigs(String userID)
        {
            JobPortalBlazor.Shared.Gig[] gigList =
                await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.Gig[]>("/api/Gigs/MyGigs/"+userID);
            List<Models.Gig> gigs = new List<Models.Gig>();
            foreach (JobPortalBlazor.Shared.Gig gig in gigList)
                gigs.Add(new Models.Gig
                {
                    ID = gig.Id,
                    ImageUrl = gig.ImageUrl,
                    Title = gig.Title,
                    user = new Models.User { UserID = gig.Freelancer.User.Id, ImageUrl = gig.Freelancer.User.ProfileImage, RoleName = "Freelancer", UserName = gig.Freelancer.User.FullName },
                    UserID = gig.Freelancer.User.Id,
                    CategoryID = gig.Category.Id.ToString(),
                    Description = gig.Description,
                    Price = gig.Pricing,
                    PriceInterval = gig.PriceUnit
                });
            return gigs;
        }

        public async Task<List<Models.Gig>> getGigsByCategory(int catID)
        {
            JobPortalBlazor.Shared.Gig[] gigList =
                await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.Gig[]>("/api/Gigs/GigsByCategory/"+catID);
            List<Models.Gig> gigs = new List<Models.Gig>();
            foreach (JobPortalBlazor.Shared.Gig gig in gigList)
                gigs.Add(new Models.Gig
                {
                    ID = gig.Id,
                    ImageUrl = gig.ImageUrl,
                    Title = gig.Title,
                    user = new Models.User { UserID = gig.Freelancer.User.Id, ImageUrl = gig.Freelancer.User.ProfileImage, RoleName = "Freelancer", UserName = gig.Freelancer.User.FullName },
                    UserID = gig.Freelancer.User.Id,
                    CategoryID = gig.Category.Id.ToString(),
                    Description = gig.Description,
                    Price = gig.Pricing,
                    PriceInterval = gig.PriceUnit
                });
            return gigs;
        }

        public async Task<Models.Gig> getGigByID(int id)
        {
            JobPortalBlazor.Shared.Gig gig = await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.Gig>("/api/Gigs/" + id);
            return new Models.Gig
            {
                ID = gig.Id,
                ImageUrl = gig.ImageUrl,
                Title = gig.Title,
                user = new Models.User { UserID = gig.Freelancer.User.Id, ImageUrl = gig.Freelancer.User.ProfileImage, RoleName = "Freelancer", UserName = gig.Freelancer.User.FullName },
                UserID = gig.Freelancer.User.Id,
                CategoryID = gig.Category.Id.ToString(),
                Description = gig.Description,
                Price = gig.Pricing,
                PriceInterval = gig.PriceUnit
            };
        }

        public async Task<Models.Gig> addGig(Models.Gig gig)
        {
            Models.Category cat = await catService.getCategoryBySlug(int.Parse(gig.CategoryID));
            Models.User user = await userServices.GetUserByID(gig.UserID);
            JobPortalBlazor.Shared.Gig sendCat = new JobPortalBlazor.Shared.Gig { 
                Id = 0,
                Category = new JobPortalBlazor.Shared.Category { Id=cat.ID, ImageLink=cat.ImageUrl, Name=cat.Name, Slug=cat.Slug },
                Description = gig.Description,
                Freelancer = new JobPortalBlazor.Shared.Freelancer { Id=user.UserID, Detail= },
                ImageUrl = await uploader.UploadFile(gig.Image),
                PriceUnit = gig.PriceInterval,
                Pricing = gig.Price,
                Title = gig.Title
            };
            HttpResponseMessage receivedCat = await this.httpClient.PostAsJsonAsync<JobPortalBlazor.Shared.Category>("/api/Categories", sendCat);
            JobPortalBlazor.Shared.Category cat = await receivedCat.Content.ReadFromJsonAsync<JobPortalBlazor.Shared.Category>();
            return new Models.Category { ID = cat.Id, ImageUrl = cat.ImageLink, Name = cat.Name, Slug = cat.Slug };
        }

        public async Task<Models.Category> updateCategory(Models.Category category)
        {
            category.ImageUrl = await uploader.UploadFile(category.Image);
            JobPortalBlazor.Shared.Category sendCat = new JobPortalBlazor.Shared.Category { Id = category.ID, ImageLink = category.ImageUrl, Name = category.Name, Slug = category.Slug };
            HttpResponseMessage receivedCat = await this.httpClient.PutAsJsonAsync<JobPortalBlazor.Shared.Category>("/api/Categories/" + category.ID, sendCat);
            return category;
        }

        public async Task<int> deleteCategory(Models.Category category)
        {
            await this.httpClient.DeleteAsync("/api/Categories/" + category.ID);
            return category.ID;
        }

    }
}
