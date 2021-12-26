using ExeBeni1.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExeBeni1.Models
{
    public class Review
    {
        int articleId;
        string criticName;
        string email;
        DateTime date;
        int rate;
        string reviewS;

        public Review(int articleId, string criticName, string email, DateTime date, int rate, string review)
        {
            this.articleId = articleId;
            this.criticName = criticName;
            this.email = email;
            this.date = date;
            this.rate = rate;
            this.reviewS = review;
        }
        public Review() { }
        public int ArticleId { get => articleId; set => articleId = value; }
        public string CriticName { get => criticName; set => criticName = value; }
        public string Email { get => email; set => email = value; }
        public DateTime Date { get => date; set => date = value; }
        public int Rate { get => rate; set => rate = value; }
        public string ReviewS { get => reviewS; set => reviewS = value; }

        public int Insert()
        {
            DBReview dbs = new DBReview();
            return dbs.Insert(this);

        }

        public List<Review> Read(string UserName)
        {
            DBReview dbs = new DBReview();
            return dbs.Read(UserName);
        }

    }
}