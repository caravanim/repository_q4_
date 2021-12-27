using ExeBeni1.Models;
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
        public IEnumerable<Review> Get(string UserName)
        {
            Review article1 = new Review();
            return article1.Read(UserName);
        }


        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public int Post(int userid_R, [FromBody] Review review)
        {
            return review.Insert(userid_R);
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