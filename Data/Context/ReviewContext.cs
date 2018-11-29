using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Models;

namespace Data.Context
{
    public class ReviewContext
    {
        private string ConnectionString { get; set; } =
            "Server=tcp:joshhq.database.windows.net,1433;Initial Catalog=Rockstar;Persist Security Info=False;User ID=joshhq;Password=Admin1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            string query = "SELECT * FROM Category";
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
                                Category category = new Category
                                {
                                    Id = (int) reader["Id"],
                                    Name = (string) reader["Name"]
                                };
                                categories.Add(category);
                            }
                        }
                    }
                }

                return categories;
            }
        }

        public void AddReview(Review review, int userId)
        {
            string query =
                "UPDATE Review SET [User_Id] = @UserId, [Overall] = @Overall, [Explanation] = @Explanation, [IsInvite] = @IsInvite" +
                " WHERE Id = @Id;";


            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Overall", review.Overall);
                    cmd.Parameters.AddWithValue("@Explanation", review.Explanation);
                    cmd.Parameters.AddWithValue("@Id", review.Id);
                    cmd.Parameters.AddWithValue("@Isinvite", false);

                    try
                    {
                        conn.Open();

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception errorException)
                    {
                        throw errorException;
                    }
                }
            }
        }

        public void AddRatingToReview(Category category, int reviewId)
        {
            string query = "INSERT INTO Review_Category ([Review_Id], [Category_Id], [Rating], [Explanation])" +
                           "VALUES (@ReviewId, @CategoryId, @Rating, @Explanation)";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ReviewId", reviewId);
                    cmd.Parameters.AddWithValue("@CategoryId", category.Id);
                    cmd.Parameters.AddWithValue("@Rating", category.Rating);
                    cmd.Parameters.AddWithValue("@Explanation", category.Explanation);

                    cmd.ExecuteReader();
                }
            }
        }


        public Review GetNewReviewById(int id)
        {
            string query = "SELECT [Function], StartDate, EndDate, Company_Id FROM Review WHERE Id= @Id";
            var review = new Review();
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
                            review = new Review()
                            {
                                Id = id,
                                Function = (string)reader["Function"],
                                StartDate = (DateTime)reader["StartDate"],
                                EndDate = (DateTime)reader["EndDate"],
                                CompanyId = (int)reader["Company_Id"]
                            };
                        }

                        return review;
                    }
                }
            }
        }

        public List<Review> GetReviewsById(int id)
        {
            List<Review> reviews = new List<Review>();
            string query = "SELECT * FROM Review where User_Id = " + id + "";
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
                                int userid = (int) reader["User_Id"];
                                Review review = new Review()
                                {
                                    Function = (string)reader["Function"],
                                    StartDate = (DateTime)reader["StartDate"],
                                    EndDate = (DateTime)reader["EndDate"],
                                    Overall = (int)reader["Overall"],
                                    Explanation = (string)reader["Explanation"],
                                
                                };

                                AccountContext _accountContext = new AccountContext();
                                Account account = _accountContext.GetAccountById(userid);

                                review.Account = account;

                                reviews.Add(review);
                            }
                        }
                    }
                }




                return reviews;
            }
        }

       


        public List<Review> GetReviewInvitesByUserId(int id)
        {
            CompanyContext cc = new CompanyContext();
            
            List<Review> reviews = new List<Review>();
            string query = "SELECT * FROM Review where User_Id = " + id + " AND IsInvite = true";
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
                                int userid = (int) reader["User_Id"];
                                Review review = new Review()
                                {
                                    Function = (string) reader["Function"],
                                    StartDate = (DateTime) reader["StartDate"],
                                    EndDate = (DateTime) reader["EndDate"],
                                    Company = cc.GetCompanyById((int)reader["Company_Id"]),
                                };

                                AccountContext _accountContext = new AccountContext();
                                Account account = _accountContext.GetAccountById(userid);

                                review.Account = account;

                                reviews.Add(review);
                            }
                        }
                    }
                }




                return reviews;
            }
        }
    }
}
