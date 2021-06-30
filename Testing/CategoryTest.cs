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
    public class CategoryTest
    {
        ModelFaker modelFaker = new();
        private TestContext testContextInstance;
        static HttpClient client = new HttpClient();
        private const string ENV_URL = "https://localhost:44324/api/";

        public CategoryTest() { }
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
        [TestInitialize()]
        public async Task SetUpTest() {
            await modelFaker.CleanUp();
        }

        public async Task<Category> CreateCategory()
        {
            Category category = await modelFaker.GetCategory();
            category.Id = 0;
            var response = await client.PostAsJsonAsync(ENV_URL + "categories", category);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Category new_category = await response.Content.ReadFromJsonAsync<Category>();
            Assert.AreNotEqual(new_category, null);
            Assert.AreNotEqual(new_category.Id, 0);
            Assert.AreEqual(new_category.Name, category.Name);
            return new_category;
        }
        public async Task<List<Category>> CreateCategoryList(int count)
        {
            List<Category> categories = new();
            for (int i = 0; i < count; i++)
                categories.Add(await CreateCategory());
            Assert.AreEqual(categories.Count, count);
            return categories;
        }
        [TestMethod()]
        public async Task GetCategoryListTest()
        {
            // empty list by default
            var response = await client.GetAsync(ENV_URL+"Categories");
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            var data = await response.Content.ReadFromJsonAsync<List<Category>>();
            Assert.AreNotEqual(data, null);
            Assert.AreEqual(data.Count, 0);
            // after adding some items
            var categories = await CreateCategoryList(10);
            Assert.AreEqual(categories.Count, 10);
        }

        [TestMethod()]
        public async Task GetSingleCategorySuccessTest()
        {
            var category = await CreateCategory();
            var response = await client.GetAsync(ENV_URL + "Categories/"+category.Id);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            var data = await response.Content.ReadFromJsonAsync<Category>();
            Assert.AreNotEqual(data, null);
            Assert.AreEqual(data.Id, category.Id);
            Assert.AreEqual(data.Name, category.Name);
        }

        [TestMethod()]
        public async Task GetSingleCategoryFailureTest()
        {
            var response = await client.GetAsync(ENV_URL + "Categories/" + 0);
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }
        [TestMethod()]
        public async Task UpdateCategorySuccessTest()
        {
            Category category = await CreateCategory();
            category.Name = Faker.Company.Name();
            var response = await client.PutAsJsonAsync(ENV_URL + "Categories/" + category.Id, category);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NoContent);
        }
        [TestMethod()]
        public async Task UpdatedCategoryBadDataTest()
        {
            Category category = await CreateCategory();
            category.Name = Faker.Company.Name();
            var response = await client.PutAsJsonAsync(ENV_URL + "Categories/" + category.Id, "");
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }
        [TestMethod()]
        public async Task UpdateCategoryNotFoundTest()
        {
            Category category = await modelFaker.GetCategory();
            category.Id = 0;
            var response = await client.PutAsJsonAsync(ENV_URL + "Categories/" + 0, category);
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }

        [TestMethod()]
        public async Task DeleteCategorySuccessTest()
        {
            Category category = await CreateCategory();
            var response = await client.DeleteAsync(ENV_URL + "Categories/" + category.Id);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NoContent);
        }
        [TestMethod()]
        public async Task DeleteCategoryNotFoundTest()
        {
            var response = await client.DeleteAsync(ENV_URL + "Categories/" + 0);
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
