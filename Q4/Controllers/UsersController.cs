using Q4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Q4.Controllers
{
    public class UsersController : ApiController
    {


        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        public string Get(string mail , string password)
        {
            User user = new User();
            return user.Read(mail, password);
        }


        // POST api/<controller>
        public int Post([FromBody] User user)
        {
            return user.Insert();
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