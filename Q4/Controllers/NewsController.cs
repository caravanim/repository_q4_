using Q1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Q2.Controllers
{
    public class NewsController : ApiController
    {
        // GET api/<controller>
        //public IEnumerable<Article> Get()
        //{
        //    Article article1 = new Article();
        //    return article1.Read();
        //}

        // GET api/<controller>/5
        public IEnumerable<Article> Get(string Sname)
        {
            Article article3 = new Article();
            return article3.Read(Sname);
        }

        public IEnumerable<Article> Get(string Sname, string SRname)
        {
            Article article = new Article();
            return article.Read(Sname, SRname);
        }

        // POST api/<controller>
        public int Post(int Userid, [FromBody] Article article )
        {

           return article.Insert(Userid);


        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}