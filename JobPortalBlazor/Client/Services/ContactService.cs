using JobPortalBlazor.Client.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JobPortalBlazor.Client.Services
{
	public class ContactService
	{
		private readonly HttpClient httpClient;

		public ContactService(HttpClient httpClient) => this.httpClient = httpClient;
		public async Task<List<Models.Contact>> getAllContacts()
		{
			JobPortalBlazor.Shared.SupportMessage[] contacts =
				await this.httpClient.GetFromJsonAsync<JobPortalBlazor.Shared.SupportMessage[]>("/api/SupportMessages");
			List<Models.Contact> cats = new List<Models.Contact>();
			foreach (JobPortalBlazor.Shared.SupportMessage cat in contacts)
				cats.Add(new Models.Contact { ID = cat.Id, Name = cat.User.FullName, Subject = cat.Subject, Email = cat.User.Email, Message = cat.Message });
			return cats;
		}

        public async Task<Models.Contact> addCategory(Models.Contact contact)
        {
            JobPortalBlazor.Shared.SupportMessage sendCat = contact;
			sendCat.UserId = MainLayout.current_user.UserID;
            HttpResponseMessage receivedCat = await this.httpClient.PostAsJsonAsync<JobPortalBlazor.Shared.SupportMessage>("/api/SupportMessages", sendCat);
            JobPortalBlazor.Shared.SupportMessage cat = await receivedCat.Content.ReadFromJsonAsync<JobPortalBlazor.Shared.SupportMessage>();
            return cat.Id==0 ? null:contact;
        }
		public async Task<int> deleteContact(Models.Contact contact)
		{
			await this.httpClient.DeleteAsync("/api/SupportMessages/" + contact.ID);
			return contact.ID;
		}

	}
}
