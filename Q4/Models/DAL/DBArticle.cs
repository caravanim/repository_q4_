using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

namespace Q1.Models
{
    public class DBArticle
    {
       
        public int Insert(int Userid, Article article)
        {
            SqlConnection con = null;
            SqlConnection conAdd = null;
            con = Connect("ARTICAL_2022");
            conAdd = Connect("UsersArticles_2022");
            int numEffected = 0;
            int numEffectedUA = 0;

            try // בודק האם הכתבה קיימת בדאטה
            {
                SqlCommand commandExistArticle = CreateSelectExistArticle(article.SeriesId, con);
                SqlDataReader drExistArticle = commandExistArticle.ExecuteReader(CommandBehavior.CloseConnection);
                
                if (drExistArticle.Read()) //קיים בדאטה של הכתבות נבדוק אם קיים בדאטה של כתבות משתמש
                {
                    try { 
                         SqlCommand commandExistInUserA = CreateSelectExistInUserA(Userid, article.SeriesId, conAdd);
                         SqlDataReader drExistInUserA = commandExistInUserA.ExecuteReader(CommandBehavior.CloseConnection);

                         if (drExistInUserA.Read())//קיים בדאטה של הכתבות וגם בדאטה של המתשתמש
                         {
                           return 0;
                         }
                         else //קיים בדאטה של הכתבות ולא קיים בדאטה של המשתמש
                         {
                            //drExistInUserA.Close();
                            drExistArticle.Close();
                           
                            try
                             {
                               SqlCommand commandUA = CreateinsertUA(Userid, article, conAdd);
                               numEffectedUA = commandUA.ExecuteNonQuery();
                               return numEffectedUA;
                             }
                             catch (Exception ex)
                             {
                                  throw new Exception("failed to insert an article", ex);
                               }
                              finally
                               {
                                 conAdd.Close();
                                 con.Close();
                               }
                          }
                     }
                    catch (Exception ex)
                    {
                        throw new Exception("failed to insert an article", ex);
                    }
                    finally
                    {
                        conAdd.Close();
                        con.Close();
                    }
                }
                else // לא קיים בדאטה של הכתבות נכניס לדאטה של הכתובת ולדאטה של המשתמש
                {
                    try
                    {
                        SqlCommand command = Createinsert(article, con);
                        SqlCommand commandUAF = CreateinsertUA(Userid, article, conAdd);

                        numEffected = command.ExecuteNonQuery();
                        numEffectedUA = commandUAF.ExecuteNonQuery();
                        return numEffected + numEffectedUA;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("failed to insert an article", ex);
                    }
                    finally
                    {
                        conAdd.Close();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The User is already exist", ex);
            }
            finally
            {
                conAdd.Close();
            }

        }

        SqlConnection Connect ( string connectstringname)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings[connectstringname].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }

        SqlCommand Createinsert(Article article, SqlConnection con)
        {
            article.SeriesShort = article.SeriesShort.Replace("'", "''");
            article.SeriesHeader = article.SeriesHeader.Replace("'", "''");
            article.SeriesFound = article.SeriesFound.Replace("'", "''");
            article.SeriesName = article.SeriesName.Replace("'", "''");
            article.Image = article.Image.Replace("'", "''");

            string sqlString = "INSERT INTO ARTICAL_2022 ([seriesId],[seriesName],[seriesHeader],[seriesShort],[seriesFound],[date],[imageUrl], [link] ) "
                + "VALUES (" + article.SeriesId + ",'" + article.SeriesName + "','" + article.SeriesHeader + "','" + article.SeriesShort + "','" + article.SeriesFound + "','" + article.Date + "','" + article.Image + "','" + article.Link+"')";
            SqlCommand command = new SqlCommand(sqlString, con);
            command.CommandTimeout = 5; // after 5 secoend it will return a timeout exception
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }

        SqlCommand CreateinsertUA(int Userid,Article article, SqlConnection con)
        {

            string sqlString = "INSERT INTO UsersArticles_2022 ([id],[seriesId]) "
                + "VALUES ('" + Userid + "','" + article.SeriesId  + "')";
            SqlCommand command = new SqlCommand(sqlString, con);
            command.CommandTimeout = 5; // after 5 secoend it will return a timeout exception
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }

        public List<Article> Read(string Sname)
        {
            SqlConnection con = null;
            SqlConnection conA = null;
            List<Article> AList = new List<Article>();
            try
            {
                con = Connect("UsersArticles_2022");

                SqlCommand selectcommand = Createselect(Sname, con);

                SqlDataReader dr = selectcommand.ExecuteReader(CommandBehavior.CloseConnection);


                while (dr.Read() == true)
                {
                    int S = (int)dr["seriesId"];


                    try
                    {
                        conA = Connect("ARTICAL_2022");

                        SqlCommand selectcommandA = CreateselectA(S, conA);

                        SqlDataReader drA = selectcommandA.ExecuteReader(CommandBehavior.CloseConnection);

                        while (drA.Read() == true)
                        {
                            Article A = new Article();
                            A.SeriesName= (string)drA["seriesName"];
                            AList.Add(A);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("failed to find a user", ex);
                    }
                    finally
                    {
                        conA.Close();
                    }
                }
                return AList;
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
       
        SqlCommand Createselect(string Sname, SqlConnection con)
        {
            string sqlString = "select* from  UsersArticles_2022 where id = @id";
            SqlCommand cmd = createCommand(con, sqlString);

            cmd.Parameters.AddWithValue("@id", Sname);

            return cmd;
        }

        SqlCommand CreateselectA(int S, SqlConnection con)
        {
            string sqlString = "select* from  ARTICAL_2022 where seriesId = @S";
            SqlCommand cmd = createCommand(con, sqlString);

            cmd.Parameters.AddWithValue("@S", S);

            return cmd;
        }



        SqlCommand createCommand(SqlConnection con, string CommandSTR)
        {
            SqlCommand cmd = new SqlCommand();  // create the command object
            cmd.Connection = con;               // assign the connection to the command object
            cmd.CommandText = CommandSTR;       // can be Select, Insert, Update, Delete
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandTimeout = 20; // seconds
            return cmd;
        }


        public List<Article> Read(string Sname, string SRname)
        {
            SqlConnection con = null;
            SqlConnection conA = null;
            List<Article> AList = new List<Article>();
            try
            {
                con = Connect("UsersArticles_2022");

                SqlCommand selectcommand = Createselect(Sname, con);

                SqlDataReader drF = selectcommand.ExecuteReader(CommandBehavior.CloseConnection);


                while (drF.Read())
                {
                    int S = (int)drF["seriesId"];
                    try
                    {
                        conA = Connect("ARTICAL_2022");

                        SqlCommand selectcommandSR = CreateselectSR(S, SRname, conA);

                        SqlDataReader drSR = selectcommandSR.ExecuteReader(CommandBehavior.CloseConnection);

                        while (drSR.Read())
                        {
                            Article A = new Article();
                            A.SeriesId = (int)drSR["seriesId"];
                            A.SeriesName = (string)drSR["seriesName"];
                            A.SeriesHeader = (string)drSR["seriesHeader"];
                            A.SeriesShort = (string)drSR["SeriesShort"];
                            A.SeriesFound = (string)drSR["SeriesFound"];
                            A.Date = Convert.ToDateTime(drSR["date"]);
                            A.Image = (string)drSR["imageUrl"];
                            A.Link = (string)drSR["link"];

                            AList.Add(A);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("failed to find a user", ex);
                    }
                    finally
                    {
                        conA.Close();
                    }

                }

                return AList;

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

        SqlCommand CreateselectSR(int S, string SRname,SqlConnection conA)
        {
            string sqlString = "select* from  ARTICAL_2022 where seriesId = @S and seriesName = @SRname";
            SqlCommand cmd = createCommand(conA, sqlString);

            cmd.Parameters.AddWithValue("@S", S);
            cmd.Parameters.AddWithValue("@SRname", SRname);

            return cmd;
        }

        SqlCommand CreateSelectAdd(int Userid, int ArticleId, SqlConnection conAdd)
        {
            string sqlString = "select * from  UsersArticles_2022 where seriesId = @AI and id = @UI";
            SqlCommand cmd = createCommand(conAdd, sqlString);

            cmd.Parameters.AddWithValue("@AI", ArticleId);
            cmd.Parameters.AddWithValue("@UI", Userid);

            return cmd;
        }

        SqlCommand CreateSelectExistArticle(int ArticleId, SqlConnection con)
        {
            string sqlString = "select * from  ARTICAL_2022 where seriesId = @SI";
            SqlCommand cmd = createCommand(con, sqlString);

            cmd.Parameters.AddWithValue("@SI", ArticleId);

            return cmd;
        }

        SqlCommand CreateSelectExistInUserA(int Userid, int ArticleId, SqlConnection con)
        {
            string sqlString = "select * from  UsersArticles_2022 where seriesId = @SI and id = @userId";
            SqlCommand cmd = createCommand(con, sqlString);

            cmd.Parameters.AddWithValue("@SI", ArticleId);
            cmd.Parameters.AddWithValue("@userId", Userid);

            return cmd;
        }

        
    }
}