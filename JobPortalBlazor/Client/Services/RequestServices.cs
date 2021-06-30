using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Services
{
    public class RequestServices
    {
        Uploader uploader { get; set; }
        private readonly HttpClient httpClient;

        public RequestServices(HttpClient httpClient) { this.httpClient = httpClient; uploader = new Uploader(httpClient); }

        public async Task<List<Models.PurchaseRequest>> getMyRequest(String userID)
        {
            List<Models.PurchaseRequest> req = new List<Models.PurchaseRequest>();
            /**************************************************************************************/
            /*******************************APPLY ODATA HERE***************************************/
            /*
             * You need to load all request of a certain user,
             * it is simple, customerorderrequests table has client and you just need to load all requests of a particular client
             */
            JobPortalBlazor.Shared.CustomOrderRequest[] fList =
                await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.CustomOrderRequest[]>("/api/CustomOrderRequests");
            foreach (JobPortalBlazor.Shared.CustomOrderRequest f in fList)
                if (userID == f.Client.Id)
                    req.Add(new Models.PurchaseRequest(f));
            /**************************************************************************************/
            return req;
        }

        public async Task<List<Models.PurchaseRequest>> searchRequests(string s)
        {
            List<Models.PurchaseRequest> req = new List<Models.PurchaseRequest>();
            JobPortalBlazor.Shared.CustomOrderRequest[] fList =
                await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.CustomOrderRequest[]>("/api/CustomOrderRequests");
            foreach (JobPortalBlazor.Shared.CustomOrderRequest f in fList)
                if (f.Title.IndexOf(s) < 0 || f.Description.IndexOf(s) < 0 || f.Category.Name.IndexOf(s) < 0)
                    req.Add(new Models.PurchaseRequest(f));
            return req;
        }
        public async Task<List<Models.PurchaseRequest>> getRequestsByCategory(int catID)
        {
            List<Models.PurchaseRequest> gigs = new List<Models.PurchaseRequest>();
            JobPortalBlazor.Shared.Category cat = await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.Category>("/api/Categories/" + catID);
            foreach (JobPortalBlazor.Shared.CustomOrderRequest g in cat.CustomOrderRequests)
            {
                g.Category = cat;
                gigs.Add(new Models.PurchaseRequest(g));
            }
            return gigs;
        }

        public async Task<Models.PurchaseRequest> getRequestByID(int id)
        {
            JobPortalBlazor.Shared.CustomOrderRequest cat = await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.CustomOrderRequest>("/api/CustomOrderRequests/" + id);
            Models.PurchaseRequest t = new Models.PurchaseRequest(cat);
            return t;
        }

        public async Task<Models.PurchaseRequest> addRequest(Models.PurchaseRequest gig)
        {
            gig.RequestID = 0;
            gig.ImageUrl = await uploader.UploadFile(gig.Image);
            HttpResponseMessage receivedCat = await this.httpClient.PostAsJsonAsync<JobPortalBlazor.Shared.CustomOrderRequest>("/api/CustomOrderRequests", gig);
            JobPortalBlazor.Shared.CustomOrderRequest cat = null;
            try
            {
                cat = await receivedCat.Content.ReadFromJsonAsync<JobPortalBlazor.Shared.CustomOrderRequest>();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return await getRequestByID(cat.Id);
        }

        public async Task<Models.PurchaseRequest> updateRequest(Models.PurchaseRequest category)
        {
            category.ImageUrl = await uploader.UploadFile(category.Image);
            HttpResponseMessage receivedCat = await this.httpClient.PutAsJsonAsync<JobPortalBlazor.Shared.CustomOrderRequest>("/api/CustomOrderRequests/" + category.RequestID, category);
            return category;
        }

        public async Task<int> deleteRequest(Models.PurchaseRequest category)
        {
            await this.httpClient.DeleteAsync("/api/CustomOrderRequests/" + category.RequestID);
            return category.RequestID;
        }
    }
}
