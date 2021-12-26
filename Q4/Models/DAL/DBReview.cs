using ExeBeni1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

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

        


        public List<Review> Read(string UserName)
        {
            SqlConnection con = null;

            List<Review> RList = new List<Review>();
            try
            {
                con = Connect("REVIEW_2022");

                SqlCommand selectcommand = Createselect(UserName, con);

                SqlDataReader dr = selectcommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read() == true)
                {
                    Review A = new Review();
                    A.ArticleId = (int)dr["articleId"];
                    A.CriticName = (string)dr["criticName"];
                    A.Email = (string)dr["email"];
                    //A.Date = (DateTime)dr["date"];
                    A.Rate = (int)dr["rate"];
                    A.ReviewS = (string)dr["reviewS"];


                    RList.Add(A);
                }
                return RList;

            }
            catch (Exception ex)
            {
                throw new Exception("failed to find a user", ex);
            }
            finally
            {
                con.Close();
            }
        }

        SqlCommand Createselect(string UserName, SqlConnection con)
        {
            string sqlString = "select* from  REVIEW_2022 where Mail = @mail";
            SqlCommand cmd = createCommand(con, sqlString);

            cmd.Parameters.AddWithValue("@mail", UserName);

            return cmd;
        }

        SqlCommand createCommand(SqlConnection con, string CommandSTR)
        {
            SqlCommand cmd = new SqlCommand();  // create the command object
            cmd.Connection = con;               // assign the connection to the command object
            cmd.CommandText = CommandSTR;       // can be Select, Insert, Update, Delete
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandTimeout = 5; // seconds
            return cmd;
        }
    }
}
