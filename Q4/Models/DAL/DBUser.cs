using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace Q4.Models.DAL
{
    public class DBUser
    {

        public int Insert(User user)
        {
            SqlConnection con = null;
            try
            {
                con = Connect("Users_2022");

                SqlCommand command = Createinsert(user, con);

                int numEffected = command.ExecuteNonQuery();
                return 1;

            }
            catch (Exception ex)
            {
                throw new Exception("failed to create an user", ex);
            }
            finally
            {
                con.Close();
            }
           
        }

        SqlConnection Connect(string connectstringname)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings[connectstringname].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }

        SqlCommand Createinsert(User user, SqlConnection con)
        {
            string sqlString = "INSERT INTO Users_2022 ([Firstname],[Lastname],[Birthday],[Mail],[Password]) "
               + "VALUES (@Firstname,@Lastname,@Birthday ,@Mail ,@Password)";
            SqlCommand cmd = createCommand(con ,sqlString);

            cmd.Parameters.AddWithValue("@Firstname", user.Firstname);
            cmd.Parameters.AddWithValue("@Lastname", user.Lastname);
            cmd.Parameters.AddWithValue("@Birthday", user.Birthday);
            cmd.Parameters.AddWithValue("@Mail", user.Mail);
            cmd.Parameters.AddWithValue("@Password", user.Password);

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


        public string Read(string mail, string password)
        {
            SqlConnection con = null;

            try
            {
                con = Connect("Users_2022");

                SqlCommand selectcommand = Createselect(mail, password, con);

                SqlDataReader dr = selectcommand.ExecuteReader(CommandBehavior.CloseConnection);
                //User U = new User();
                string str = "";
               
                if (dr.Read() == true)
                {    
                   str = (string)dr["Firstname"];
                }

                return str;
               
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

        SqlCommand Createselect(string mail, string password, SqlConnection con)
        {
            string sqlString = "select* from  Users_2022 where Mail = @mail and Password = @password ";
            SqlCommand cmd = createCommand(con, sqlString);

            cmd.Parameters.AddWithValue("@mail", mail);
            cmd.Parameters.AddWithValue("@password", password);

            return cmd;
        }



    }
}