using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Services
{
    public class ContactService
    {
        [inject]
        Uploader uploader { get; set; }
        private readonly HttpClient httpClient;

        public ContactService(HttpClient httpClient) => this.httpClient = httpClient;
        public async Task<List<Models.Contact>> getAllContacts()
        {
            JobPortalBlazor.Shared.SupportMessage[] contacts =
                await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.SupportMessage[]>("/api/SupportMessage");
            List<Models.Contact> cats = new List<Models.Contact>();
            foreach (JobPortalBlazor.Shared.SupportMessage cat in contacts)
                cats.Add(new Models.Contact { ID = cat.Id, Name = cat.User.FullName, Subject=cat.Subject, Email=cat.User.Email, Message=cat.Message });
            return cats;
        }

        public async Task<Models.Category> addCategory(Models.Category category)
        {
            category.ImageUrl = await uploader.UploadFile(category.Image);
            JobPortalBlazor.Shared.Category sendCat = new JobPortalBlazor.Shared.Category { Id = 0, ImageLink = category.ImageUrl, Name = category.Name, Slug = category.Slug };
            HttpResponseMessage receivedCat = await this.httpClient.PostAsJsonAsync<JobPortalBlazor.Shared.Category>("/api/Categories", sendCat);
            JobPortalBlazor.Shared.Category cat = await receivedCat.Content.ReadFromJsonAsync<JobPortalBlazor.Shared.Category>();
            return new Models.Category { ID = cat.Id, ImageUrl = cat.ImageLink, Name = cat.Name, Slug = cat.Slug };
        }

        public async Task<int> deleteCategory(Models.Category category)
        {
            await this.httpClient.DeleteAsync("/api/Categories/" + category.ID);
            return category.ID;
        }

    }
}
