using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace Data.Context
{
    public class ShowReviewContext
    {
        private string ConnectionString { get; set; } =
            "Server=tcp:joshhq.database.windows.net,1433;Initial Catalog=Rockstar;Persist Security Info=False;User ID=joshhq;Password=Admin1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public List<Review> SearchReviews(string search)
        {
            List<Review> reviews = new List<Review>();
            string query = "SELECT * From REVIEW where Company_Id = (Select Id from [Company] where Name LIKE '%" + search + "%')";
            using (var conn = new SqlConnection(ConnectionString))
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
                                int id = (int) reader["Company_Id"];
                                Review review = new Review()
                                {
                                    Function = (string) reader["Function"],
                                    StartDate = (DateTime) reader["StartDate"],
                                    EndDate = (DateTime) reader["EndDate"],
                                    Overall = (int) reader["Overall"],
                                    Explanation = (string) reader["Explanation"],
                                    Categories = GetCategories((int)reader["Id"])
                                };

                                CompanyContext _companyContext = new CompanyContext();
                                Company company = _companyContext.GetCompanyById(id);

                                review.Company = company;

                                reviews.Add(review);
                            }
                        }
                    }
                }

                conn.Close();




                return reviews;
            }
        }

        public List<Category> GetCategories(int id)
        {
            List<Category> categories = new List<Category>();
            string query = "SELECT * FROM Review_Category where Review_Id = " + id + "";
            using (var conn = new SqlConnection(ConnectionString))
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
                                int reviewid = (int)reader["Review_Id"];
                                Category category = new Category()
                                {
                                    Name = GetCategoryName((int)reader["Category_Id"]),
                                    Rating = (int)reader["Rating"],
                                    Explanation = (string)reader["Explanation"]
                                    //cc.GetCompanyById((int)reader["Company_Id"]),
                                };


                                categories.Add(category);
                            }
                        }
                    }
                }
            }
            return categories;
        }

        public string GetCategoryName(int id)
        {

            string name = "";
            string query = "SELECT * FROM Category where Id = " + id + "";
            using (var conn = new SqlConnection(ConnectionString))
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
                                name = (string)reader["Name"];
                            }
                        }
                    }
                }
            }
            return name;
        }

        public List<Review> SearchReviewsByRating(int rating, string search)
		{
			List<Review> reviews = new List<Review>();
			string query = "SELECT * From REVIEW where Company_Id = (Select Id from [Company] where Name LIKE '%" + search + "%') AND Overall = '" + rating + "'";
			using (var conn = new SqlConnection(ConnectionString))
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
								int id = (int)reader["Company_Id"];
								Review review = new Review()
								{
									Function = (string)reader["Function"],
									StartDate = (DateTime)reader["StartDate"],
									EndDate = (DateTime)reader["EndDate"],
									Overall = (int)reader["Overall"],
									Explanation = (string)reader["Explanation"],
                                    Categories = GetCategories((int)reader["Id"])
                                   
								};

								CompanyContext _companyContext = new CompanyContext();
							    Company company = _companyContext.GetCompanyById(id);

								review.Company = company;

								reviews.Add(review);
							}
						}
					}
				}
				conn.Close();




				return reviews;
			}
		}
	

		public int CountInvitesOfUser(int userId)
        {
            string query = "SELECT COUNT(Id) AS 'InviteCount' FROM Review WHERE User_Id=@UserId AND IsInvite='true'";
            int countInvites = 0;
            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        countInvites = (int) reader["InviteCount"];
                    }

                    return countInvites;
                }
            }
        }

        public List<Review> GetInvitesOfUser(int userId)
        {
            string query = "SELECT * FROM Review WHERE User_Id=@UserId AND IsInvite='true'";
            var InvitesOfUser = new List<Review>();

            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var review = new Review()
                            {
                                Id = (int) reader["Id"],
                                StartDate = (DateTime) reader["StartDate"],
                                EndDate = (DateTime) reader["EndDate"],
                                Function = (string) reader["Function"],
                                CompanyId = (int)reader["Company_Id"]
                            };
                            InvitesOfUser.Add(review);
                        }

                        return InvitesOfUser;
                    }
                }
            }
        }
    }
}
