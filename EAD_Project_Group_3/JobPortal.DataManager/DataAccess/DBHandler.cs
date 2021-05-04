using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace JobPortal.DataManager.DataAccess
{
	public class DBHandler
	{
		private String connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Blog;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

		protected SqlConnection GetConnection()
		{
			return new SqlConnection(connectionString);
		}

		protected SqlDataReader Fetch(SqlConnection conn, String query, params SqlParameter[] parameters)
		{
			SqlCommand command = new SqlCommand(query, conn);
			foreach (SqlParameter p in parameters)
				command.Parameters.Add(p);
			SqlDataReader output = command.ExecuteReader();
			return output;
		}

		protected int Execute(SqlConnection conn, String query, params SqlParameter[] parameters)
		{
			SqlCommand command = new SqlCommand(query, conn);
			foreach (SqlParameter p in parameters)
				command.Parameters.Add(p);
			int output = command.ExecuteNonQuery();
			return output;
		}

		protected int Insert(SqlConnection conn, String query, params SqlParameter[] parameters)
		{
			SqlCommand command = new SqlCommand(query, conn);
			foreach (SqlParameter p in parameters)
				command.Parameters.Add(p);
			int output = Convert.ToInt32(command.ExecuteScalar());
			return output;
		}
	}
}
