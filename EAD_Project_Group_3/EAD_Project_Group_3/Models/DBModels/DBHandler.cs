using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAD_Project_Group_3.Models.DBModels
{
    public class DBHandler
    {
        private String connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Blog;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
        protected SqlDataReader fetch(SqlConnection conn, String query, params SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(query, conn);
            foreach (SqlParameter p in parameters)
                command.Parameters.Add(p);
            SqlDataReader output = command.ExecuteReader();
            return output;
        }
        protected int execute(SqlConnection conn, String query, params SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(query, conn);
            foreach (SqlParameter p in parameters)
                command.Parameters.Add(p);
            int output = command.ExecuteNonQuery();
            return output;
        }
        protected int insert(SqlConnection conn, String query, params SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(query, conn);
            foreach (SqlParameter p in parameters)
                command.Parameters.Add(p);
            int output = Convert.ToInt32(command.ExecuteScalar());
            return output;
        }
    }
}
