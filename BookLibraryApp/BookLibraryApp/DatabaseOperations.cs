using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryApp
{
    class DatabaseOperations
    {
        public bool LogIn(string username, string password)
        {
            var connectionString = "Data Source=LAPTOP-POKG440J;Initial Catalog=BookLibraryDB;Persist Security Info=True;User ID=LibraryAppName;Password=LibraryNamePassword";
            var connection = new SqlConnection(connectionString);

            var querySelect = "SELECT * FROM [dbo].Users";
            var command = new SqlCommand(querySelect,connection);

            

            var adapter = new SqlDataAdapter(querySelect, connection);

            var dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Username", typeof(string));
            dataTable.Columns.Add("Password", typeof(string));
            dataTable.Columns.Add("IsAdmin", typeof(bool));


            adapter.Fill(dataTable);



            return true;
        }
    }
}
