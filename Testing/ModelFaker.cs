using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using JobPortalBlazor.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    public class ModelFaker
    {
        private static int RANDOM_LISTS_COUNT = 5;
        private static List<string> clientIds = new();
        private static List<string> random_users = new();
        private static List<int> freelancerIds = new();
        private static List<int> gigIds = new();
        private static List<int> categoryIds = new();
        private static List<int> orderIds = new();
        private static HttpClient client = new HttpClient();
        public const string ENV_URL = "https://localhost:44324/api/";
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[Faker.RandomNumber.Next(s.Length)%s.Length]).ToArray());
        }
        private async Task<string> GetRandomClient()
        {
            if(clientIds.Count()==0)
            {
                for(int i = 0; i<RANDOM_LISTS_COUNT; i++)
                {
                    RegisterParameters registerParameters = await GetRegister();
                    var result = await client.PostAsJsonAsync(ENV_URL+"Authorize/Register", registerParameters);
                    Assert.AreNotEqual(result.StatusCode, System.Net.HttpStatusCode.BadRequest);
                    JobPortalBlazor.Client.Models.User user = await client.GetFromJsonAsync<JobPortalBlazor.Client.Models.User>(ENV_URL+"Authorize/CurrentUser");
                    clientIds.Add(user.UserID);
                }
            }
            return clientIds[Faker.RandomNumber.Next(clientIds.Count())%clientIds.Count()];
        }
        private async Task<int> GetRandomFreelancer()
        {
            if (freelancerIds.Count() == 0)
            {
                for (int i = 0; i < RANDOM_LISTS_COUNT; i++)
                {
                    RegisterParameters registerParameters = await GetRegister();
                    var result = await client.PostAsJsonAsync(ENV_URL+"Authorize/Register", registerParameters);
                    Assert.AreNotEqual(result.StatusCode, System.Net.HttpStatusCode.BadRequest);
                    JobPortalBlazor.Client.Models.User user = await client.GetFromJsonAsync<JobPortalBlazor.Client.Models.User>(ENV_URL+"Authorize/CurrentUser");
                    random_users.Add(user.UserID);
                    Freelancer freelancer = await GetFreelancer();
                    freelancer.Id = 0;
                    freelancer.UserId = user.UserID;
                    result = await client.PostAsJsonAsync(ENV_URL+"Freelancers", freelancer);
                    freelancer = await result.Content.ReadFromJsonAsync<Freelancer>();
                    Assert.AreNotEqual(freelancer, null);
                    Assert.AreNotEqual(freelancer.Id, 0);
                    freelancerIds.Add(freelancer.Id);
                }
            }
            return freelancerIds[Faker.RandomNumber.Next(freelancerIds.Count()) % RANDOM_LISTS_COUNT];
        }
        private async Task<int> GetRandomGig()
        {
            if (gigIds.Count() == 0)
            {
                for (int i = 0; i < RANDOM_LISTS_COUNT; i++)
                {
                    Gig gig = await GetGig();
                    gig.Id = 0;
                    var result = await client.PostAsJsonAsync(ENV_URL+"Gigs", gig);
                    gig = await result.Content.ReadFromJsonAsync<Gig>();
                    Assert.AreNotEqual(gig, null);
                    Assert.AreNotEqual(gig.Id, 0);
                    gigIds.Add(gig.Id);
                }
            }
            return gigIds[Faker.RandomNumber.Next(gigIds.Count()) % RANDOM_LISTS_COUNT];
        }
        private async Task<int> GetRandomCategory()
        {
            if (categoryIds.Count() == 0)
            {
                for (int i = 0; i < RANDOM_LISTS_COUNT; i++)
                {
                    Category category = await GetCategory();
                    category.Id = 0;
                    var result = await client.PostAsJsonAsync(ENV_URL+"Categories", category);
                    category = await result.Content.ReadFromJsonAsync<Category>();
                    Assert.AreNotEqual(category, null);
                    Assert.AreNotEqual(category.Id, 0);
                    categoryIds.Add(category.Id);
                }
            }
            return categoryIds[Faker.RandomNumber.Next(categoryIds.Count()) % RANDOM_LISTS_COUNT];
        }
        private async Task<int> GetRandomOrder()
        {
            if (orderIds.Count() == 0)
            {
                for (int i = 0; i < RANDOM_LISTS_COUNT; i++)
                {
                    Order order = await GetOrder();
                    order.Id = 0;
                    var result = await client.PostAsJsonAsync(ENV_URL+"Orders", order);
                    order = await result.Content.ReadFromJsonAsync<Order>();
                    Assert.AreNotEqual(order, null);
                    Assert.AreNotEqual(order.Id, 0);
                    orderIds.Add(order.Id);
                }
            }
            return orderIds[Faker.RandomNumber.Next(orderIds.Count()) % RANDOM_LISTS_COUNT];
        }

        public async Task CleanUp()
        {
            var result = await client.GetAsync(ENV_URL + "reset_all");
            Assert.AreEqual(result.IsSuccessStatusCode, true);
            foreach (string id in clientIds)
            {
                result = await client.GetAsync(ENV_URL + "Authorize/DeleteUser/" + id);
                Assert.AreEqual(result.IsSuccessStatusCode, true);
            }
            foreach (string id in random_users)
            {
                result = await client.GetAsync(ENV_URL + "Authorize/DeleteUser/" + id);
                Assert.AreEqual(result.IsSuccessStatusCode, true);
            }
            clientIds = new();
            random_users = new();
            freelancerIds = new();
            orderIds = new();
            gigIds = new();
            categoryIds = new();
        }
        public async Task<Category> GetCategory()
        {
            Category category = new Category();
            category.Id = Faker.RandomNumber.Next(1000);
            category.ImageLink = RandomString(50);
            category.Name = Faker.Company.Name();
            category.Slug = RandomString(20);
            return category;
        }
        public async Task<Freelancer> GetFreelancer()
        {
            Freelancer freelancer = new Freelancer();
            freelancer.Id = Faker.RandomNumber.Next(1000);
            freelancer.Detail = Faker.Lorem.Paragraph(10);
            freelancer.UserId = await GetRandomClient();
            return freelancer;
        }
        public async Task<Gig> GetGig()
        {
            Gig gig = new();
            gig.Id = Faker.RandomNumber.Next(1000);
            gig.CategoryId = await GetRandomCategory();
            gig.FreelancerId = await GetRandomFreelancer();
            gig.Title = Faker.Company.Name();
            gig.Description = Faker.Lorem.Paragraph(10);
            gig.ImageUrl = RandomString(50);
            gig.PriceUnit = Faker.Currency.Name();
            gig.Pricing = Faker.RandomNumber.Next(1000);
            return gig;
        }
        public async Task<CustomOrderRequest> GetRequest()
        {
            CustomOrderRequest request = new();
            request.Id = Faker.RandomNumber.Next(1000);
            request.Title = Faker.Company.Name();
            request.Description = Faker.Lorem.Paragraph(10);
            request.Budget = Faker.RandomNumber.Next(1000);
            request.CategoryId = await GetRandomCategory();
            request.ClientId = await GetRandomClient();
            request.Duration = 10;
            request.ImageUrl = RandomString(50);
            request.RequestDate = Faker.Identification.DateOfBirth();
            return request;
        }
        public async Task<Order> GetOrder()
        {
            Order order = new();
            order.Id = Faker.RandomNumber.Next(1000);
            order.ClientId = await GetRandomClient();
            order.GigId = await GetRandomGig();
            order.Details = Faker.Lorem.Paragraph(10);
            order.EndDate = Faker.Identification.DateOfBirth();
            order.StartDate = Faker.Identification.DateOfBirth();
            order.Status = "COMPLETE";
            return order;
        }
        public async Task<OrderDelivery> GetDelivery()
        {
            OrderDelivery delivery = new();
            delivery.Id = Faker.RandomNumber.Next(1000);
            delivery.DeliveryDate = Faker.Identification.DateOfBirth();
            delivery.OrderId = await GetRandomOrder();
            delivery.Details = Faker.Lorem.Paragraph(10);
            delivery.FileUrl = RandomString(50);
            return delivery;
        }
        public async Task<SupportMessage> GetSupport()
        {
            SupportMessage support = new();
            support.Id = Faker.RandomNumber.Next(1000);
            support.UserId = await GetRandomClient();
            support.Message = Faker.Lorem.Paragraph(10);
            support.MessageDate = Faker.Identification.DateOfBirth();
            support.Subject = Faker.Lorem.Sentence();
            return support;
        }
        public async Task<RegisterParameters> GetRegister()
        {
            RegisterParameters register = new();
            register.UserEmail = Faker.Internet.Email();
            register.UserName = Faker.Name.FullName();
            register.Password = "Password_123";
            register.ImageUrl = RandomString(50);
            return register;
        }
    }
}
