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
			DataAccess access = new DataAccess();
			access.SaveData<dynamic>("dbo.DeleteContactUsMessageById", new { IdentifierCase = id });
		}

		public void MarkMessageAsResolved(int id)
		{
			DataAccess access = new DataAccess();
			access.SaveData<dynamic>("dbo.ResolveContactUsMessageById", new { IdentifierCase = id });
		}

		public ContactUsModel GetContactUsByEmail(string email)
		{
			DataAccess access = new DataAccess();
			var result = access.LoadData<ContactUsModel, dynamic>("[dbo].[GetContactUsMessageByEmail]", new { Email = email }).FirstOrDefault();

			return result;
		}

		public List<ContactUsModel> GetAllUnResolvedMessages()
		{
			DataAccess access = new DataAccess();
			var result = access.LoadData<ContactUsModel, dynamic>("[dbo].[GetAllUnResolvedMessages]", new { });
			return result;
		}

		public List<ContactUsModel> GetAllContactUsMessagesByEmail(string email)
		{
			DataAccess access = new DataAccess();
			var result = access.LoadData<ContactUsModel, dynamic>("[dbo].[GetContactUsMessageByEmail]", new { Email = email });

			return result;
		}

	}
}
