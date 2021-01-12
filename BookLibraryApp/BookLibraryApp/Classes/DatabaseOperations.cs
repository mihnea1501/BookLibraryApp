using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookLibraryApp
{
    class DatabaseOperations
    {
        
        public  User GetUserFromDatabase (string username, string password)
        {
           
                var connectionString_GetUserFromDatabase = "Data Source=LAPTOP-POKG440J;Initial Catalog=BookLibraryDB;Persist Security Info=True;User ID=LibraryAppName;Password=LibraryNamePassword";
                var connection_GetUserFromDatabase = new SqlConnection(connectionString_GetUserFromDatabase);
                var querySelect_GetUserFromDatabase = $"EXEC GetUserFromDatabase @Username = '{username}', @password = '{password}'";
                var command_GetUserFromDatabase = new SqlCommand(querySelect_GetUserFromDatabase, connection_GetUserFromDatabase);
                var sqlDataAdapter_GetUserFromDatabase = new SqlDataAdapter(command_GetUserFromDatabase);
                var userDt = new DataTable();
                sqlDataAdapter_GetUserFromDatabase.Fill(userDt);

                if (userDt.Rows.Count == 1)
                {
                    var user = new User();
                    user.Id = userDt.Rows[0].Field<int>(0);
                    user.Username = userDt.Rows[0].Field<string>(1);
                    user.IsAdmin = userDt.Rows[0].Field<bool>(2);
                    return user;
                }
                else
                {
                    return null;
                }
            
        }

        public bool usernameAlreadyInDatabase(string username)
        {
            var connectionString_usernameAlreadyInDatabase = "Data Source=LAPTOP-POKG440J;Initial Catalog=BookLibraryDB;Persist Security Info=True;User ID=LibraryAppName;Password=LibraryNamePassword";
            var connection_usernameAlreadyInDatabase = new SqlConnection(connectionString_usernameAlreadyInDatabase);
            var querySelect_usernameAlreadyInDatabase = $"EXEC GetUserNameFromDatabase @Username = '{username}'";
            var command_usernameAlreadyInDatabase = new SqlCommand(querySelect_usernameAlreadyInDatabase, connection_usernameAlreadyInDatabase);
            var sqlDataAdapter_usernameAlreadyInDatabase = new SqlDataAdapter(command_usernameAlreadyInDatabase);
            var usernameDt = new DataTable();
            sqlDataAdapter_usernameAlreadyInDatabase.Fill(usernameDt);

            if (usernameDt.Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void createUser(string username, string password, bool isAdmin)
        {
            var connectionString_createUser = "Data Source=LAPTOP-POKG440J;Initial Catalog=BookLibraryDB;Persist Security Info=True;User ID=LibraryAppName;Password=LibraryNamePassword";
            var connection_createUser = new SqlConnection(connectionString_createUser);
            var querySelect_createUser = $"EXEC CreateUser @Username = '{username}', @Password = '{password}', @IsAdmin = '{isAdmin}'";
            var command_createUser = new SqlCommand(querySelect_createUser, connection_createUser);
            command_createUser.Connection.Open();
            command_createUser.ExecuteNonQuery();
            command_createUser.Connection.Close();
        }


    }
}
