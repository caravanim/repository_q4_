using ExeBeni1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using Q4.Models;

namespace ExeBeni1.DAL
{
    public class DBReview
    {
        public int Insert(int userid_R, string user_M, string user_NF, string user_NL, Review review)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("REVIEW_2022");

                SqlCommand command = Createinsert(userid_R, user_M, user_NF, user_NL, review, con);

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

        SqlCommand Createinsert(int userid_R, string user_M, string user_NF, string user_NL, Review review, SqlConnection con)
        {
            string sqlString = "INSERT INTO [REVIEW_2022] ([userId],[articleId],[criticName],[email],[date],[rate],[reviewS]) " +
                "VALUES (@userId,@articleId,@criticName,@email,@date,@rate,@reviewS)";
            SqlCommand command = new SqlCommand(sqlString, con);
            command.Parameters.AddWithValue("@userId", userid_R);
            command.Parameters.AddWithValue("@articleId", review.ArticleId);
            command.Parameters.AddWithValue("@criticName", user_NF + " " + user_NL);
            command.Parameters.AddWithValue("@email", user_M);
            command.Parameters.AddWithValue("@date", review.Date);
            command.Parameters.AddWithValue("@rate", review.Rate);
            command.Parameters.AddWithValue("@reviewS", review.ReviewS);
            return command;
        }

        public List<Review> Read(int UserName)
        {
            SqlConnection con = null;

            List<Review> RList = new List<Review>();
            try
            {
                con = Connect("REVIEW_2022");

                SqlCommand selectcommand = Createselect(UserName, con);

                SqlDataReader dr = selectcommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    Review A = new Review();
                    A.ArticleId = (int)dr["articleId"];
                    A.Date = Convert.ToDateTime(dr["date"]);
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

       SqlCommand Createselect(int UserName, SqlConnection con)
        {
            string sqlString = "select* from  REVIEW_2022 where userId = @id";
            SqlCommand cmd = createCommand(con, sqlString);

            cmd.Parameters.AddWithValue("@id", UserName);

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
