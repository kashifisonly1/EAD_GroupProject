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
    public class CustomOrderRequestTest
    {
        ModelFaker modelFaker = new();
        private TestContext testContextInstance;
        static HttpClient client = new HttpClient();
        private const string ENV_URL = "https://localhost:44324/api/";

        public CustomOrderRequestTest() { }
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

        public async Task<CustomOrderRequest> CreateCustomOrderRequest()
        {
            CustomOrderRequest customOrderRequests = await modelFaker.GetRequest();
            customOrderRequests.Id = 0;
            var response = await client.PostAsJsonAsync(ENV_URL + "customOrderRequests", customOrderRequests);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            CustomOrderRequest new_customOrderRequests = await response.Content.ReadFromJsonAsync<CustomOrderRequest>();
            Assert.AreNotEqual(new_customOrderRequests, null);
            Assert.AreNotEqual(new_customOrderRequests.Id, customOrderRequests.Id);
            Assert.AreEqual(new_customOrderRequests.Title, customOrderRequests.Title);
            return new_customOrderRequests;
        }
        public async Task<List<CustomOrderRequest>> CreateCustomOrderRequestList(int count)
        {
            List<CustomOrderRequest> customOrderRequests = new();
            for (int i = 0; i < count; i++)
                customOrderRequests.Add(await CreateCustomOrderRequest());
            Assert.AreEqual(customOrderRequests.Count, count);
            return customOrderRequests;
        }
        [TestMethod()]
        public async Task GetCustomOrderRequestListTest()
        {
            // empty list by default
            var response = await client.GetAsync(ENV_URL + "CustomOrderRequests");
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            var data = await response.Content.ReadFromJsonAsync<List<CustomOrderRequest>>();
            Assert.AreNotEqual(data, null);
            Assert.AreEqual(data.Count, 0);
            // after adding some items
            var customOrderRequests = await CreateCustomOrderRequestList(10);
            Assert.AreEqual(customOrderRequests.Count, 10);
        }

        [TestMethod()]
        public async Task GetSingleCustomOrderRequestSuccessTest()
        {
            var customOrderRequests = await CreateCustomOrderRequest();
            var response = await client.GetAsync(ENV_URL + "CustomOrderRequests/" + customOrderRequests.Id);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            var data = await response.Content.ReadFromJsonAsync<CustomOrderRequest>();
            Assert.AreNotEqual(data, null);
            Assert.AreEqual(data.Id, customOrderRequests.Id);
            Assert.AreEqual(data.Title, customOrderRequests.Title);
        }

        [TestMethod()]
        public async Task GetSingleCustomOrderRequestFailureTest()
        {
            var response = await client.GetAsync(ENV_URL + "CustomOrderRequests/" + 0);
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }
        [TestMethod()]
        public async Task UpdateCustomOrderRequestSuccessTest()
        {
            CustomOrderRequest customOrderRequests = await CreateCustomOrderRequest();
            customOrderRequests.Title = Faker.Company.Name();
            var response = await client.PutAsJsonAsync(ENV_URL + "CustomOrderRequests/" + customOrderRequests.Id, customOrderRequests);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NoContent);
        }
        [TestMethod()]
        public async Task UpdatedCustomOrderRequestBadDataTest()
        {
            CustomOrderRequest customOrderRequests = await CreateCustomOrderRequest();
            customOrderRequests.Title = Faker.Company.Name();
            var response = await client.PutAsJsonAsync(ENV_URL + "CustomOrderRequests/" + customOrderRequests.Id, "");
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }
        [TestMethod()]
        public async Task UpdateCustomOrderRequestNotFoundTest()
        {
            CustomOrderRequest customOrderRequests = await modelFaker.GetRequest();
            customOrderRequests.Id = 0;
            var response = await client.PutAsJsonAsync(ENV_URL + "CustomOrderRequests/" + 0, customOrderRequests);
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod()]
        public async Task DeleteCustomOrderRequestSuccessTest()
        {
            CustomOrderRequest customOrderRequests = await CreateCustomOrderRequest();
            var response = await client.DeleteAsync(ENV_URL + "CustomOrderRequests/" + customOrderRequests.Id);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NoContent);
        }
        [TestMethod()]
        public async Task DeleteCustomOrderRequestNotFoundTest()
        {
            var response = await client.DeleteAsync(ENV_URL + "CustomOrderRequests/" + 0);
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
