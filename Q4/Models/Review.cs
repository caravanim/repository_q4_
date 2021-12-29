using ExeBeni1.DAL;
using Q4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExeBeni1.Models
{
    public class Review
    {   
        int articleId;
        DateTime date;
        int rate;
        string reviewS;

        public Review(int articleId, DateTime date, int rate, string review)
        {
            this.articleId = articleId;
            this.date = date;
            this.rate = rate;
            this.reviewS = review;
        }
        public Review() { }
        public int ArticleId { get => articleId; set => articleId = value; }
        public DateTime Date { get => date; set => date = value; }
        public int Rate { get => rate; set => rate = value; }
        public string ReviewS { get => reviewS; set => reviewS = value; }

        public int Insert(int userid_R, string user_M, string user_NF, string user_NL)
        {
            DBReview dbs = new DBReview();
            return dbs.Insert(userid_R, user_M, user_NF, user_NL, this);
        }

        public List<Review> Read(int UserName)
        {
            DBReview dbs = new DBReview();
            return dbs.Read(UserName);
        }

    }
}