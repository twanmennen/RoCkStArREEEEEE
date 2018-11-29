using System.Data.Common;
using System.Data.SqlClient;
using Models;

namespace Data.Context
{
    public class LogInContext
    {
        private string ConnectionString { get; set; } = "Server=tcp:joshhq.database.windows.net,1433;Initial Catalog=Rockstar;Persist Security Info=False;User ID=joshhq;Password=Admin1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public bool[] LoginCheck(string eMail, string passWord)
        {
            string query = $"SELECT Email, Password, HasLoggedIn FROM [User] WHERE Email=@EMail AND Password=@PassWord";
            bool logInIsValid = false;
            bool firstTimeLoggedIn = false;
            bool[] returnArray;

            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    
                    cmd.Parameters.Add(new SqlParameter("@EMail", eMail));
                    cmd.Parameters.Add(new SqlParameter("@PassWord", passWord));
                    conn.Open();

                    foreach (DbDataRecord record in cmd.ExecuteReader())
                    {
                        logInIsValid = true;
                        if (record.GetBoolean(record.GetOrdinal("HasLoggedIn")) == false)
                        {
                            firstTimeLoggedIn = true;
                        }
                    }

                    returnArray = new bool[2] { logInIsValid, firstTimeLoggedIn };
                }
            }

            return returnArray;
        }

        public Account GetAccountByEMail(string eMail)
        {
            Account account = new Account();
            string query = $"SELECT Id, Name, Password, Email, Role_Id FROM [User] WHERE Email=@EMail";

            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@EMail", eMail));
                    conn.Open();
                    foreach (DbDataRecord record in cmd.ExecuteReader())
                    {
                        account = new Account
                        {
                            Id = record.GetInt32(record.GetOrdinal("Id")),
                            Name = record.GetString(record.GetOrdinal("Name")),
                            Password = record.GetString(record.GetOrdinal("Password")),
                            Email = record.GetString(record.GetOrdinal("Email")),
                            RoleId = record.GetInt32(record.GetOrdinal("Role_Id")),
                        };
                    }
                }
            }

            return account;
        }
    }
}
