using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace JobPortal.DataManager.Internal.Data
{
	internal class DataAccess
	{
		public string ConnectionString
		{
			get
			{
				// UNDONE -- Update connection string
				//return ConfigurationManager.ConnectionStrings["JobPortal-DB"].ConnectionString;
				return "Server=.;Database=JobPortal-DB;Trusted_Connection=True";
			}
		}

		public List<T> LoadData<T, U>(string storedProcedure, U parameters)
		{
			using (IDbConnection cnn = new SqlConnection(ConnectionString))
			{
				List<T> rows = cnn.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();
				return rows;
			}
		}

		public void SaveData<T>(string storedProcedure, T parameters)
		{
			using (IDbConnection cnn = new SqlConnection(ConnectionString))
			{
				cnn.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		public void SaveData(string storedProcedure, SqlParameter[] parameters = null)
		{
			using (SqlConnection cnn = new SqlConnection(ConnectionString))
			{
				cnn.Open();
				SqlCommand cmd = new SqlCommand(storedProcedure, cnn)
				{
					CommandType = CommandType.StoredProcedure
				};
				if (parameters != null)
					cmd.Parameters.AddRange(parameters);
				cmd.ExecuteNonQuery();
			}
		}
	}
}
