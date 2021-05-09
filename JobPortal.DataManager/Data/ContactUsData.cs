using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using JobPortal.DataManager.Internal.Data;
using JobPortal.DataManager.Models;

namespace JobPortal.DataManager.Data
{
	public class ContactUsData
	{
		public void SaveMessage(ContactUsModel model)
		{
			DataAccess access = new DataAccess();
			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@email", model.Email),
				new SqlParameter("@subject", model.Subject),
				new SqlParameter("@message", model.Message),
				new SqlParameter("@name", model.Name)
			};
			access.SaveData("dbo.AddMessageToContactUs", parameters.ToArray());
		}
	}
}
