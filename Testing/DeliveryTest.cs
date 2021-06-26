using Microsoft.AspNetCore.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Testing;
using JobPortalBlazor.Shared;
namespace APITest
{
    [TestClass]
    public class DeliveryTest
    {
        ModelFaker modelFaker = new();
        private TestContext testContextInstance;
        static HttpClient client = new HttpClient();
        private const string ENV_URL = "https://localhost:44324/api/";

        public DeliveryTest() { }
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
        [TestInitialize()]
        public async Task SetUpTest()
        {
            await modelFaker.CleanUp();
        }

        public async Task<OrderDelivery> CreateDelivery()
        {
            OrderDelivery deliveries = await modelFaker.GetDelivery();
            deliveries.Id = 0;
            var response = await client.PostAsJsonAsync(ENV_URL + "OrderDeliveries", deliveries);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            OrderDelivery new_deliveries = await response.Content.ReadFromJsonAsync<OrderDelivery>();
            Assert.AreNotEqual(new_deliveries, null);
            Assert.AreNotEqual(new_deliveries.Id, deliveries.Id);
            Assert.AreEqual(new_deliveries.Details, deliveries.Details);
            return new_deliveries;
        }
        public async Task<List<OrderDelivery>> CreateDeliveryList(int count)
        {
            List<OrderDelivery> deliveriess = new();
            for (int i = 0; i < count; i++)
                deliveriess.Add(await CreateDelivery());
            Assert.AreEqual(deliveriess.Count, count);
            return deliveriess;
        }
        [TestMethod()]
        public async Task GetDeliveryListTest()
        {
            // empty list by default
            var response = await client.GetAsync(ENV_URL + "OrderDeliveries");
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            var data = await response.Content.ReadFromJsonAsync<List<OrderDelivery>>();
            Assert.AreNotEqual(data, null);
            Assert.AreEqual(data.Count, 0);
            // after adding some items
            var deliveriess = await CreateDeliveryList(10);
            Assert.AreEqual(deliveriess.Count, 10);
        }

        [TestMethod()]
        public async Task GetSingleDeliverySuccessTest()
        {
            var deliveries = await CreateDelivery();
            var response = await client.GetAsync(ENV_URL + "OrderDeliveries/" + deliveries.Id);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            var data = await response.Content.ReadFromJsonAsync<OrderDelivery>();
            Assert.AreNotEqual(data, null);
            Assert.AreEqual(data.Id, deliveries.Id);
            Assert.AreEqual(data.Details, deliveries.Details);
        }

        [TestMethod()]
        public async Task GetSingleDeliveryFailureTest()
        {
            var response = await client.GetAsync(ENV_URL + "OrderDeliveries/" + 0);
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }
        [TestMethod()]
        public async Task UpdateDeliverySuccessTest()
        {
            OrderDelivery deliveries = await CreateDelivery();
            deliveries.Details = Faker.Company.Name();
            var response = await client.PutAsJsonAsync(ENV_URL + "OrderDeliveries/" + deliveries.Id, deliveries);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NoContent);
        }
        [TestMethod()]
        public async Task UpdatedDeliveryBadDataTest()
        {
            OrderDelivery deliveries = await CreateDelivery();
            deliveries.Details = Faker.Company.Name();
            var response = await client.PutAsJsonAsync(ENV_URL + "OrderDeliveries/" + deliveries.Id, "");
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }
        [TestMethod()]
        public async Task UpdateDeliveryNotFoundTest()
        {
            OrderDelivery deliveries = await modelFaker.GetDelivery();
            deliveries.Id = 0;
            var response = await client.PutAsJsonAsync(ENV_URL + "OrderDeliveries/" + 0, deliveries);
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod()]
        public async Task DeleteDeliverySuccessTest()
        {
            OrderDelivery deliveries = await CreateDelivery();
            var response = await client.DeleteAsync(ENV_URL + "OrderDeliveries/" + deliveries.Id);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NoContent);
        }
        [TestMethod()]
        public async Task DeleteDeliveryNotFoundTest()
        {
            var response = await client.DeleteAsync(ENV_URL + "OrderDeliveries/" + 0);
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }
        [TestCleanup()]
        public async Task CleanupTest()
        {
            await modelFaker.CleanUp();
        }
    }
}
