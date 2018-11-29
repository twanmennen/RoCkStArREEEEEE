using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace Data.Context
{
    public class SendReviewContext
    {
        private string ConnectionString = "Server=tcp:joshhq.database.windows.net,1433;Initial Catalog=Rockstar;Persist Security Info=False;User ID=joshhq;Password=Admin1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public void SendReview(Review review)
        {
            string query = "INSERT INTO Review([Function], [StartDate], [EndDate], [User_Id], [Company_Id], [IsInvite])" +
                           "VALUES(@Function, @StartDate, @EndDate, @UserId, @CompanyId, @IsInvite)";

            using (var conn = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Function", review.Function);
                    cmd.Parameters.AddWithValue("@StartDate", review.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", review.EndDate);
                    cmd.Parameters.AddWithValue("@UserId", review.UserId);
                    cmd.Parameters.AddWithValue("@CompanyId", review.Company.Id);
                    cmd.Parameters.AddWithValue("@IsInvite", true);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
