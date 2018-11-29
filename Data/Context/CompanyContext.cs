using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Models;


namespace Data.Context
{
    public class CompanyContext
    {
        private string ConnectionString = "Server=tcp:joshhq.database.windows.net,1433;Initial Catalog=Rockstar;Persist Security Info=False;User ID=joshhq;Password=Admin1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public Company GetCompanyById(int id)
        {
            string query = "SELECT * FROM Company WHERE Id=@Id";
            var company = new Company();
            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            company = new Company()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Email = (string)reader["Email"],
                                Password = (string)reader["Password"],
                                Location = (string)reader["Location"],
                                Employee = (int)reader["Employees"],
                                Link = (string)reader["Link"],
                                Info = (string)reader["Info"]
                            };
                        }

                        return company;
                    }
                }
            }
        }

        public List<Company> GetAllCompanies()
        {
            string query = "SELECT * FROM Company";
            var companies = new List<Company>();

            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var company = new Company()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Email = (string)reader["Email"],
                                Password = (string)reader["Password"],
                                Location = (string)reader["Location"],
                                Employee = (int)reader["Employees"],
                                Link = (string)reader["Link"],
                                Info = (string)reader["Info"]
                            };
                            companies.Add(company);
                        }

                        return companies;
                    }
                }
            }
        }

        public List<Review> GetReviewsByCompany(int id)
        {
            throw new NotImplementedException();
        }
      
    }
}
