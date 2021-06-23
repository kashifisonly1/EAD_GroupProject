using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Services
{
    public class OrderDeliveryService
    {
        [inject]
        Uploader uploader { get; set; }
        [inject]
        CategoryServices catService;
        [inject]
        UserServices userServices;
        private readonly HttpClient httpClient;

        public OrderDeliveryService(HttpClient httpClient) => this.httpClient = httpClient;

        public async Task<List<Models.OrderDelivery>> getAllOrderDeliviries(int id)
        {
            List<Models.OrderDelivery> gigs = new List<Models.OrderDelivery>();
            JobPortalBlazor.Shared.Order cat = await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.Order>("/api/Orders/" + id);
            foreach (JobPortalBlazor.Shared.OrderDelivery o in cat.OrderDeliveries)
                gigs.Add(new Models.OrderDelivery(o));
            return gigs;
        }

        public async Task<Models.OrderDelivery> addOrderDelivery(Models.OrderDelivery gig)
        {
            gig.ID = 0;
            HttpResponseMessage receivedCat = await this.httpClient.PostAsJsonAsync<JobPortalBlazor.Shared.OrderDelivery>("/api/OrderDeliveries", gig);
            JobPortalBlazor.Shared.OrderDelivery cat = await receivedCat.Content.ReadFromJsonAsync<JobPortalBlazor.Shared.OrderDelivery>();
            return new Models.OrderDelivery(cat);
        }
    }
}
