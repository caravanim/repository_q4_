using ExeBeni1.Models;
using Q4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExeBeni1.Controllers
{
    public class ReviewController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Review> Get(int UserName)
        {
            Review article1 = new Review();
            return article1.Read(UserName);
        }


        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        public int Post(int userid_R, string user_M, string user_NF, string user_NL, [FromBody] Review review)
        {
            return review.Insert(userid_R, user_M, user_NF, user_NL);
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