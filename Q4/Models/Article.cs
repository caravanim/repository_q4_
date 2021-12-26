using Q1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Q1.Models
{
    public class Article
    {
        int seriesId;
        string seriesName;
        string seriesHeader;
        string seriesShort;
        string seriesFound;
        DateTime date;
        string image;
        string link;

        public int SeriesId { get => seriesId; set => seriesId = value; }
        public string SeriesName { get => seriesName; set => seriesName = value; }
        public string SeriesHeader { get => seriesHeader; set => seriesHeader = value; }
        public string SeriesShort { get => seriesShort; set => seriesShort = value; }
        public string SeriesFound { get => seriesFound; set => seriesFound = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Image { get => image; set => image = value; }
        public string Link { get => link; set => link = value; }

        public Article() { }
        public Article(int SeriesId, string SeriesName, string SeriesHeader, string SeriesShort, string SeriesFound, DateTime Date, string Image, string Link)
        {
            this.SeriesId = SeriesId;
            this.SeriesName = SeriesName;
            this.SeriesHeader = SeriesHeader;
            this.SeriesShort = SeriesShort;
            this.SeriesFound = SeriesFound;
            this.Date = Date;
            this.Image = Image;
            this.Link = Link;
        }


        public int Insert(string Userid)
        {
            DBArticle dbs = new DBArticle();
            return dbs.Insert(Userid, this);
        }

        public List<Article> Read()
        {
            DBArticle dbs = new DBArticle();
            return dbs.Read();
        }


        public List<Article> Read(string Sname)
        {
            DBArticle dbs = new DBArticle();
            return dbs.Read(Sname);
        }
    }
}