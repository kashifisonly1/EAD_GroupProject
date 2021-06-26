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
    public class SupportMessageTest
    {
        ModelFaker modelFaker = new();
        private TestContext testContextInstance;
        static HttpClient client = new HttpClient();
        private const string ENV_URL = "https://localhost:44324/api/";

        public SupportMessageTest() { }
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

        public async Task<SupportMessage> CreateSupportMessage()
        {
            SupportMessage supportMessages = await modelFaker.GetSupport();
            supportMessages.Id = 0;
            var response = await client.PostAsJsonAsync(ENV_URL + "supportMessages", supportMessages);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            SupportMessage new_supportMessages = await response.Content.ReadFromJsonAsync<SupportMessage>();
            Assert.AreNotEqual(new_supportMessages, null);
            Assert.AreNotEqual(new_supportMessages.Id, supportMessages.Id);
            Assert.AreEqual(new_supportMessages.Subject, supportMessages.Subject);
            return new_supportMessages;
        }
        public async Task<List<SupportMessage>> CreateSupportMessageList(int count)
        {
            List<SupportMessage> supportMessages = new();
            for (int i = 0; i < count; i++)
                supportMessages.Add(await CreateSupportMessage());
            Assert.AreEqual(supportMessages.Count, count);
            return supportMessages;
        }
        [TestMethod()]
        public async Task GetSupportMessageListTest()
        {
            // empty list by default
            var response = await client.GetAsync(ENV_URL + "SupportMessages");
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            var data = await response.Content.ReadFromJsonAsync<List<SupportMessage>>();
            Assert.AreNotEqual(data, null);
            Assert.AreEqual(data.Count, 0);
            // after adding some items
            var supportMessages = await CreateSupportMessageList(10);
            Assert.AreEqual(supportMessages.Count, 10);
        }

        [TestMethod()]
        public async Task GetSingleSupportMessageSuccessTest()
        {
            var supportMessages = await CreateSupportMessage();
            var response = await client.GetAsync(ENV_URL + "SupportMessages/" + supportMessages.Id);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            var data = await response.Content.ReadFromJsonAsync<SupportMessage>();
            Assert.AreNotEqual(data, null);
            Assert.AreEqual(data.Id, supportMessages.Id);
            Assert.AreEqual(data.Subject, supportMessages.Subject);
        }

        [TestMethod()]
        public async Task GetSingleSupportMessageFailureTest()
        {
            var response = await client.GetAsync(ENV_URL + "SupportMessages/" + 0);
            Assert.AreEqual(response.IsSuccessStatusCode, false);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }
        [TestMethod()]
        public async Task DeleteSupportMessageSuccessTest()
        {
            SupportMessage supportMessages = await CreateSupportMessage();
            var response = await client.DeleteAsync(ENV_URL + "SupportMessages/" + supportMessages.Id);
            Assert.AreEqual(response.IsSuccessStatusCode, true);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NoContent);
        }
        [TestMethod()]
        public async Task DeleteSupportMessageNotFoundTest()
        {
            var response = await client.DeleteAsync(ENV_URL + "SupportMessages/" + 0);
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
