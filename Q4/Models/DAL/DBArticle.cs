using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Q1.Models
{
    public class DBArticle
    {
       
        static List<Article> ArticleList;
        public int Insert(string Userid, Article article)
        {
            ArticleList = new List<Article>();
            if (ArticleList == null)
            {
                ArticleList = new List<Article>();
            }
            ArticleList.Add(article);

            SqlConnection con = null;
            SqlConnection conUA = null;
            int numEffected = 0;
            int numEffectedUA = 0;

            try
            {
               
                con = Connect("ARTICAL_2022");
                conUA = ConnectUA("UsersArticles_2022");

                SqlCommand command = Createinsert(article, con);
                SqlCommand commandUA = CreateinsertUA(Userid,article, conUA);


                numEffected = command.ExecuteNonQuery();
                numEffectedUA = commandUA.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("failed to insert an article", ex);
            }
            finally
            {
                con.Close();
            }
            return numEffected+ numEffectedUA;
        }

        SqlConnection ConnectUA(string connectstringname)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings[connectstringname].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
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

        SqlCommand CreateinsertUA(string Userid,Article article, SqlConnection con)
        {

            string sqlString = "INSERT INTO UsersArticles_2022 ([Mail],[seriesId]) "
                + "VALUES ('" + Userid + "','" + article.SeriesId  + "')";
            SqlCommand command = new SqlCommand(sqlString, con);
            command.CommandTimeout = 5; // after 5 secoend it will return a timeout exception
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }


        public List<Article> Read()
        {
            return ArticleList;
        }

        public List<Article> Read(string Sname)
        {
            List<Article> ArticalNewsList = new List<Article>();
            foreach (Article f in ArticleList)
            {
                if (f.SeriesName == Sname)
                {
                    ArticalNewsList.Add(f);
                }
            }
            return ArticalNewsList;
        }


    }
}