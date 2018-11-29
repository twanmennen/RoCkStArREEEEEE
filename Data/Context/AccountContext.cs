using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using Models;

namespace Data.Context
{
    public class AccountContext
    {
        private string ConnectionString = "Server=tcp:joshhq.database.windows.net,1433;Initial Catalog=Rockstar;Persist Security Info=False;User ID=joshhq;Password=Admin1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public List<Account> GetAllAccounts()
        {
            string query = "SELECT * FROM [User]";
            var accounts = new List<Account>();

            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var account = new Account()
                            {
                                Id = (int) reader["Id"],
                                RoleId = (int) reader["Role_Id"],
                                Name = (string) reader["Name"],
                                Email = (string) reader["Email"],
                                Password = (string) reader["Password"],
                                Phone = (string) reader["Phone"],
                                Location = (string) reader["Location"],
                                Gender = (string) reader["Gender"]
                            };
                            accounts.Add(account);
                        }

                        return accounts;
                    }
                }
            }
        }

        public List<Account> GetAllEmailsFromUsers()
        {
            List<Account> users = new List<Account>();

            string query = "SELECT Email, Name FROM User";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var user = new Account()
                                {
                                    Email = (string)reader["Email"]
                                };

                                users.Add(user);
                            }
                        }
                    }
                }
            }

            return users;
        }

        public Account GetCompanyByID(int Id, int RoleId)
        {
            Account company = new Account();
            string query = $"SELECT * FROM [User] WHERE Id=@Id AND Role_Id=@Role_Id";

            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    cmd.Parameters.Add(new SqlParameter("@Role_Id", RoleId));
                    conn.Open();
                    foreach (DbDataRecord record in cmd.ExecuteReader())
                    {
                        company = new Account
                        {
                            Id = record.GetInt32(record.GetOrdinal("Id")),
                            Name = record.GetString(record.GetOrdinal("Name")),
                            Email = record.GetString(record.GetOrdinal("Email")),
                            Phone = record.GetString(record.GetOrdinal("Phone")),
                            Location = record.GetString(record.GetOrdinal("Location")),
                        };
                    }
                }
            }

            return company;
        }

		public Account GetAccountById(int id)
		{
			Account account = new Account();
			string query = $"SELECT Id, Name, Password, Email, Phone, Role_Id FROM [User] WHERE Id = @Id";

			using (SqlConnection conn = new SqlConnection(this.ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand(query, conn))
				{
					cmd.Parameters.Add(new SqlParameter("@Id", id));
					conn.Open();
					foreach (DbDataRecord record in cmd.ExecuteReader())
					{
						account = new Account
						{
							Id = record.GetInt32(record.GetOrdinal("Id")),
							Name = record.GetString(record.GetOrdinal("Name")),
							Password = record.GetString(record.GetOrdinal("Password")),
                            Phone = record.GetString(record.GetOrdinal("Phone")),
                            Email = record.GetString(record.GetOrdinal("Email")),
							RoleId = record.GetInt32(record.GetOrdinal("Role_Id")),
						};
					}
				}
			}

			return account;
		}

        public List<Review> GetReviewsByUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}
