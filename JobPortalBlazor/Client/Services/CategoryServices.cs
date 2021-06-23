using JobPortalBlazor.Client;
using JobPortalBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Forms;
using System.IO;

namespace JobPortalBlazor.Client.Services
{
    public class CategoryServices
    {
        Uploader uploader { get; set; }
        private readonly HttpClient httpClient;
        
        public CategoryServices(HttpClient httpClient) { this.httpClient = httpClient; uploader = new Uploader(httpClient); }
        public async Task< List<Models.Category> > getAllCategories()
        {
            JobPortalBlazor.Shared.Category[] catList = 
                await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.Category[]>("/api/Categories");
            List<Models.Category> cats = new List<Models.Category>();
            foreach(JobPortalBlazor.Shared.Category cat in catList)
                cats.Add(new Models.Category { ID = cat.Id, ImageUrl = cat.ImageLink, Name = cat.Name, Slug = cat.Slug });
            return cats;
        }

        public async Task<Models.Category> getCategoryBySlug(int id)
        {
            JobPortalBlazor.Shared.Category cat = await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.Category>("/api/Categories/" + id);
            return new Models.Category { ID = cat.Id, ImageUrl = cat.ImageLink, Name = cat.Name, Slug = cat.Slug };
        }

        public async Task<Models.Category> addCategory(Models.Category category)
        {
            category.ImageUrl = await uploader.UploadFile(category.Image);
            JobPortalBlazor.Shared.Category sendCat = new JobPortalBlazor.Shared.Category { Id = 0, ImageLink = category.ImageUrl, Name = category.Name, Slug = category.Slug };
            HttpResponseMessage receivedCat = await this.httpClient.PostAsJsonAsync<JobPortalBlazor.Shared.Category>("/api/Categories", sendCat);
            JobPortalBlazor.Shared.Category cat = await receivedCat.Content.ReadFromJsonAsync<JobPortalBlazor.Shared.Category>();
            return new Models.Category { ID = cat.Id, ImageUrl = cat.ImageLink, Name = cat.Name, Slug = cat.Slug };
        }

        public async Task<Models.Category> updateCategory(Models.Category category)
        {
            category.ImageUrl = await uploader.UploadFile(category.Image);
            JobPortalBlazor.Shared.Category sendCat = new JobPortalBlazor.Shared.Category { Id = category.ID, ImageLink = category.ImageUrl, Name = category.Name, Slug = category.Slug };
            HttpResponseMessage receivedCat = await this.httpClient.PutAsJsonAsync<JobPortalBlazor.Shared.Category>("/api/Categories/"+category.ID, sendCat);
            return category;
        }

        public async Task<int> deleteCategory(Models.Category category)
        {
            await this.httpClient.DeleteAsync("/api/Categories/" + category.ID);
            return category.ID;
        }
    }
}