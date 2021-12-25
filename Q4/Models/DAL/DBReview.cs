using ExeBeni1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ExeBeni1.DAL
{
    public class DBReview
    {
        static List<Review> ReviewList;
        public int Insert(Review review)
        {
            if (ReviewList == null)
            {
                ReviewList = new List<Review>();
            }
            ReviewList.Add(review);
            SqlConnection con = null;


            try
            {
                con = Connect("REVIEW_2022");

                SqlCommand command = Createinsert(review, con);

                int numEffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("failed to insert an review", ex);
            }
            finally
            {
                con.Close();
            }

            return 1;
        }
        SqlConnection Connect(string connectstringname)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings[connectstringname].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }

        SqlCommand Createinsert(Review review, SqlConnection con)
        {


            string sqlString = "INSERT INTO REVIEW_2022 ([articleId],[criticName],[email],[date],[rate],[reviewS]) "
            + "VALUES (" + review.ArticleId + ",'" + review.CriticName + "','" + review.Email + "','" + review.Date + "'," + review.Rate + ",'" + review.ReviewS + "')";
            SqlCommand command = new SqlCommand(sqlString, con);
            command.CommandTimeout = 5; // after 5 secoend it will return a timeout exception
            command.CommandType = System.Data.CommandType.Text;
            return command;

           
        }

        


        public List<Review> Read()
        {
            return ReviewList;
        }
    }
}
