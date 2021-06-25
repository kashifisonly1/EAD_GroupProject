using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Services
{
    public class OrderServices
    {
        private readonly HttpClient httpClient;

        public OrderServices(HttpClient httpClient) => this.httpClient = httpClient;

        public async Task<List<Models.Order>> getMyOrders(String userID)
        {
            List<Models.Order> req = new List<Models.Order>();
            /**************************************************************************************/
            /*******************************APPLY ODATA HERE***************************************/
            /*
             * Load all orders that has freelancer Id is equal to userId or clientId is equal to userId
             * You need to know that, client is a user object so it directly has Id that need to matched
             * but in orders table, gigs are linked, and gig has freelancer and freelancer is a user
             * so, actually, you need to manage it
             */
            JobPortalBlazor.Shared.Order[] fList =
                await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.Order[]>("/api/Orders");
            foreach (JobPortalBlazor.Shared.Order f in fList)
                if (userID == f.Client.Id || userID == f.Gig.Freelancer.User.Id)
                    req.Add(new Models.Order(f));
            /**************************************************************************************/
            return req;
        }

        public async Task<Models.Order> getOrderByID(int id)
        {
            List<Models.Order> gigs = new List<Models.Order>();
            JobPortalBlazor.Shared.Order cat = await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.Order>("/api/Orders/" + id);
            return new Models.Order(cat);
        }

        public async Task<Models.Order> addOrder(Models.Order gig)
        {
            gig.ID = 0;
            HttpResponseMessage receivedCat = await this.httpClient.PostAsJsonAsync<JobPortalBlazor.Shared.Order>("/api/Orders", gig);
            JobPortalBlazor.Shared.Order cat = await receivedCat.Content.ReadFromJsonAsync<JobPortalBlazor.Shared.Order>();
            return cat.Id!=0?gig:null;
        }

        public async Task<Models.Order> updateOrder(Models.Order category)
        {
            category.Status = "COMPLETE";
            category.EndDate = DateTime.UtcNow;
            HttpResponseMessage receivedCat = await this.httpClient.PutAsJsonAsync<JobPortalBlazor.Shared.Order>("/api/Orders/" + category.ID, category);
            return category;
        }

        public async Task<int> deleteOrder(Models.Order category)
        {
            await this.httpClient.DeleteAsync("/api/Orders/" + category.ID);
            return category.ID;
        }

    }
}
