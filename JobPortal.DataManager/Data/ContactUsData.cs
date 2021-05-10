using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using JobPortal.BusinessModels;
using JobPortal.DataManager.Internal.Data;

namespace JobPortal.DataManager.Data
{
	public class ContactUsData
	{
		public void SaveMessage(ContactUsModel model)
		{
			DataAccess access = new DataAccess();
			access.SaveData<dynamic>("dbo.AddMessageToContactUs", new { Email = model.Email, Subject = model.Subject, Message = model.Message, Name = model.Name });
		}

		public ContactUsModel GetContactUsModel(string email)
		{
			DataAccess access = new DataAccess();
			var result = access.LoadData<ContactUsModel, dynamic>("[dbo].[GetContactUsMessageByEmail]", new { Email = email }).FirstOrDefault();

			return result;
		}

		public List<ContactUsModel> GetAllContactUsModel()
		{
			DataAccess access = new DataAccess();
			var result = access.LoadData<ContactUsModel, dynamic>("[dbo].[GetAllContactUsMessage]", new { });

			return result;
		}

		public List<ContactUsModel> GetAllContactUsModelByEmail(string email)
		{
			DataAccess access = new DataAccess();
			var result = access.LoadData<ContactUsModel, dynamic>("[dbo].[GetAllContactUsMessage]", new { Email = email });

			return result;
		}

	}
}
