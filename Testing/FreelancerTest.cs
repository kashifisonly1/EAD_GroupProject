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
    public class FreelancerTest
    {
        ModelFaker modelFaker = new();
        private TestContext testContextInstance;
        static HttpClient client = new HttpClient();
        private const string ENV_URL = "https://localhost:44324/api/";

        public FreelancerTest() { }
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

        public async Task<Freelancer> CreateFreelancer()
        {
            Freelancer freelancer = await modelFaker.GetFreelancer();
            freelancer.Id = 0;
            var response = await client.PostAsJsonAsync(ENV_URL + "freelancers", freelancer);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Freelancer new_freelancer = await response.Content.ReadFromJsonAsync<Freelancer>();
            Assert.AreNotEqual(new_freelancer, null);
            Assert.AreNotEqual(new_freelancer.Id, freelancer.Id);
            Assert.AreEqual(new_freelancer.Detail, freelancer.Detail);
            return new_freelancer;
        }
        public async Task<List<Freelancer>> CreateFreelancerList(int count)
        {
            List<Freelancer> freelancers = new();
            for (int i = 0; i < count; i++)
                freelancers.Add(await CreateFreelancer());
            Assert.AreEqual(freelancers.Count, count);
            return freelancers;
        }
        [TestMethod()]
        public async Task GetFreelancerListTest()
        {
            // empty list by default
            var response = await client.GetAsync(ENV_URL + "Freelancers");
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            var data = await response.Content.ReadFromJsonAsync<List<Freelancer>>();
            Assert.AreNotEqual(data, null);
            Assert.AreEqual(data.Count, 0);
            // after adding some items
            var freelancers = await CreateFreelancerList(10);
            Assert.AreEqual(freelancers.Count, 10);
        }

        [TestMethod()]
        public async Task GetSingleFreelancerSuccessTest()
        {
            var freelancer = await CreateFreelancer();
            var response = await client.GetAsync(ENV_URL + "Freelancers/" + freelancer.Id);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            var data = await response.Content.ReadFromJsonAsync<Freelancer>();
            Assert.AreNotEqual(data, null);
            Assert.AreEqual(data.Id, freelancer.Id);
            Assert.AreEqual(data.Detail, freelancer.Detail);
        }

        [TestMethod()]
        public async Task GetSingleFreelancerFailureTest()
        {
            var response = await client.GetAsync(ENV_URL + "Freelancers/" + 0);
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }
        [TestMethod()]
        public async Task UpdateFreelancerSuccessTest()
        {
            Freelancer freelancer = await CreateFreelancer();
            freelancer.Detail = Faker.Company.Name();
            var response = await client.PutAsJsonAsync(ENV_URL + "Freelancers/" + freelancer.Id, freelancer);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NoContent);
        }
        [TestMethod()]
        public async Task UpdatedFreelancerBadDataTest()
        {
            Freelancer freelancer = await CreateFreelancer();
            freelancer.Detail = Faker.Company.Name();
            var response = await client.PutAsJsonAsync(ENV_URL + "Freelancers/" + freelancer.Id, "");
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }
        [TestMethod()]
        public async Task UpdateFreelancerNotFoundTest()
        {
            Freelancer freelancer = await modelFaker.GetFreelancer();
            freelancer.Id = 0;
            var response = await client.PutAsJsonAsync(ENV_URL + "Freelancers/" + 0, freelancer);
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod()]
        public async Task DeleteFreelancerSuccessTest()
        {
            Freelancer freelancer = await CreateFreelancer();
            var response = await client.DeleteAsync(ENV_URL + "Freelancers/" + freelancer.Id);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NoContent);
        }
        [TestMethod()]
        public async Task DeleteFreelancerNotFoundTest()
        {
            var response = await client.DeleteAsync(ENV_URL + "Freelancers/" + 0);
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
