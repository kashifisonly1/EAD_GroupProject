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
    public class OrderTest
    {
        ModelFaker modelFaker = new();
        private TestContext testContextInstance;
        static HttpClient client = new HttpClient();
        private const string ENV_URL = "https://localhost:44324/api/";

        public OrderTest() { }
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

        public async Task<Order> CreateOrder()
        {
            Order orders = await modelFaker.GetOrder();
            orders.Id = 0;
            var response = await client.PostAsJsonAsync(ENV_URL + "orders", orders);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Order new_orders = await response.Content.ReadFromJsonAsync<Order>();
            Assert.AreNotEqual(new_orders, null);
            Assert.AreNotEqual(new_orders.Id, orders.Id);
            Assert.AreEqual(new_orders.Status, orders.Status);
            return new_orders;
        }
        public async Task<List<Order>> CreateOrderList(int count)
        {
            List<Order> orderss = new();
            for (int i = 0; i < count; i++)
                orderss.Add(await CreateOrder());
            Assert.AreEqual(orderss.Count, count);
            return orderss;
        }
        [TestMethod()]
        public async Task GetOrderListTest()
        {
            // empty list by default
            var response = await client.GetAsync(ENV_URL + "Orders");
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            var data = await response.Content.ReadFromJsonAsync<List<Order>>();
            Assert.AreNotEqual(data, null);
            Assert.AreEqual(data.Count, 0);
            // after adding some items
            var orderss = await CreateOrderList(10);
            Assert.AreEqual(orderss.Count, 10);
        }

        [TestMethod()]
        public async Task GetSingleOrderSuccessTest()
        {
            var orders = await CreateOrder();
            var response = await client.GetAsync(ENV_URL + "Orders/" + orders.Id);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            var data = await response.Content.ReadFromJsonAsync<Order>();
            Assert.AreNotEqual(data, null);
            Assert.AreEqual(data.Id, orders.Id);
            Assert.AreEqual(data.Status, orders.Status);
        }

        [TestMethod()]
        public async Task GetSingleOrderFailureTest()
        {
            var response = await client.GetAsync(ENV_URL + "Orders/" + 0);
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }
        [TestMethod()]
        public async Task UpdateOrderSuccessTest()
        {
            Order orders = await CreateOrder();
            orders.Status = "PENDING";
            var response = await client.PutAsJsonAsync(ENV_URL + "Orders/" + orders.Id, orders);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NoContent);
        }
        [TestMethod()]
        public async Task UpdatedOrderBadDataTest()
        {
            Order orders = await CreateOrder();
            orders.Status = Faker.Company.Name();
            var response = await client.PutAsJsonAsync(ENV_URL + "Orders/" + orders.Id, "");
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }
        [TestMethod()]
        public async Task UpdateOrderNotFoundTest()
        {
            Order orders = await modelFaker.GetOrder();
            orders.Id = 0;
            var response = await client.PutAsJsonAsync(ENV_URL + "Orders/" + 0, orders);
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod()]
        public async Task DeleteOrderSuccessTest()
        {
            Order orders = await CreateOrder();
            var response = await client.DeleteAsync(ENV_URL + "Orders/" + orders.Id);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NoContent);
        }
        [TestMethod()]
        public async Task DeleteOrderNotFoundTest()
        {
            var response = await client.DeleteAsync(ENV_URL + "Orders/" + 0);
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
