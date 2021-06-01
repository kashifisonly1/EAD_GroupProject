using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JobPortal.BusinessModels;
using JobPortal.DataManager.Internal.Data;

namespace JobPortal.DataManager.Data
{
	public class ContactUsData
	{
		public async Task SaveMessageAsync(ContactUsModel model)
		{
			//DataAccess access = new DataAccess();
			//access.SaveData<dynamic>("dbo.AddMessageToContactUs", new { Email = model.Email, Subject = model.Subject, Message = model.Message, Name = model.Name });
			var db = new JobPortalContext();
			db.ContactMessages.Add(model);
			await db.SaveChangesAsync();
		}

		public void DeleteMessage(int id)
		{
			var db = new JobPortalContext();
			var message = db.ContactMessages.Single(match => match.Id == id);
			db.ContactMessages.Remove(message);
			db.SaveChanges();

			//DataAccess access = new DataAccess();
			//access.SaveData<dynamic>("dbo.DeleteContactUsMessageById", new { IdentifierCase = id });
		}

		public void MarkMessageAsResolved(int id)
		{
			var db = new JobPortalContext();
			var message = db.ContactMessages.Single(match => match.Id == id);
			message.IsResponded = true;
			db.SaveChanges();

			DataAccess access = new DataAccess();
			access.SaveData<dynamic>("dbo.ResolveContactUsMessageById", new { IdentifierCase = id });
		}

		public ContactUsModel GetContactUsByEmail(string email)
		{
			var db = new JobPortalContext();
			var result = db.ContactMessages.Single(match => match.Email == email);

			//DataAccess access = new DataAccess();
			//var result = access.LoadData<ContactUsModel, dynamic>("[dbo].[GetContactUsMessageByEmail]", new { Email = email }).FirstOrDefault();

			return result;
		}

		public List<ContactUsModel> GetAllUnResolvedMessages()
		{
			var db = new JobPortalContext();
			var result = db.ContactMessages.Where(match => match.IsResponded == false);

			//DataAccess access = new DataAccess();
			//var result = access.LoadData<ContactUsModel, dynamic>("[dbo].[GetAllUnResolvedMessages]", new { });
			return result.ToList();
		}

		public List<ContactUsModel> GetAllContactUsMessagesByEmail(string email)
		{
			DataAccess access = new DataAccess();
			var result = access.LoadData<ContactUsModel, dynamic>("[dbo].[GetContactUsMessageByEmail]", new { Email = email });

			return result;
		}

	}
}
