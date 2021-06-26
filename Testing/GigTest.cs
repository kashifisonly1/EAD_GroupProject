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
    public class GigTest
    {
        ModelFaker modelFaker = new();
        private TestContext testContextInstance;
        static HttpClient client = new HttpClient();
        private const string ENV_URL = "https://localhost:44324/api/";

        public GigTest() { }
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

        public async Task<Gig> CreateGig()
        {
            Gig gig = await modelFaker.GetGig();
            gig.Id = 0;
            var response = await client.PostAsJsonAsync(ENV_URL + "gigs", gig);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Gig new_gig = await response.Content.ReadFromJsonAsync<Gig>();
            Assert.AreNotEqual(new_gig, null);
            Assert.AreNotEqual(new_gig.Id, gig.Id);
            Assert.AreEqual(new_gig.Title, gig.Title);
            return new_gig;
        }
        public async Task<List<Gig>> CreateGigList(int count)
        {
            List<Gig> gigs = new();
            for (int i = 0; i < count; i++)
                gigs.Add(await CreateGig());
            Assert.AreEqual(gigs.Count, count);
            return gigs;
        }
        [TestMethod()]
        public async Task GetGigListTest()
        {
            // empty list by default
            var response = await client.GetAsync(ENV_URL + "Gigs");
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            var data = await response.Content.ReadFromJsonAsync<List<Gig>>();
            Assert.AreNotEqual(data, null);
            Assert.AreEqual(data.Count, 0);
            // after adding some items
            var gigs = await CreateGigList(10);
            Assert.AreEqual(gigs.Count, 10);
        }

        [TestMethod()]
        public async Task GetSingleGigSuccessTest()
        {
            var gig = await CreateGig();
            var response = await client.GetAsync(ENV_URL + "Gigs/" + gig.Id);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            var data = await response.Content.ReadFromJsonAsync<Gig>();
            Assert.AreNotEqual(data, null);
            Assert.AreEqual(data.Id, gig.Id);
            Assert.AreEqual(data.Title, gig.Title);
        }

        [TestMethod()]
        public async Task GetSingleGigFailureTest()
        {
            var response = await client.GetAsync(ENV_URL + "Gigs/" + 0);
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }
        [TestMethod()]
        public async Task UpdateGigSuccessTest()
        {
            Gig gig = await CreateGig();
            gig.Title = Faker.Company.Name();
            var response = await client.PutAsJsonAsync(ENV_URL + "Gigs/" + gig.Id, gig);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NoContent);
        }
        [TestMethod()]
        public async Task UpdatedGigBadDataTest()
        {
            Gig gig = await CreateGig();
            gig.Title = Faker.Company.Name();
            var response = await client.PutAsJsonAsync(ENV_URL + "Gigs/" + gig.Id, "");
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }
        [TestMethod()]
        public async Task UpdateGigNotFoundTest()
        {
            Gig gig = await modelFaker.GetGig();
            gig.Id = 0;
            var response = await client.PutAsJsonAsync(ENV_URL + "Gigs/" + 0, gig);
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod()]
        public async Task DeleteGigSuccessTest()
        {
            Gig gig = await CreateGig();
            var response = await client.DeleteAsync(ENV_URL + "Gigs/" + gig.Id);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NoContent);
        }
        [TestMethod()]
        public async Task DeleteGigNotFoundTest()
        {
            var response = await client.DeleteAsync(ENV_URL + "Gigs/" + 0);
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
