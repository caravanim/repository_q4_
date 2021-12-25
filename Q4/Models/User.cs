using Q4.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Q4.Models
{
    public class User
    {
        string firstname;
        string lastname;
        DateTime birthday;
        string mail;
        string password;

        public User()
        {

        }

        public User(string firstname, string lastname, DateTime birthday, string mail, string password)
        {
            Firstname = firstname;
            Lastname = lastname;
            Birthday = birthday;
            Mail = mail;
            Password = password;
        }

        public string Firstname { get => firstname; set => firstname = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Password { get => password; set => password = value; }

        public int Insert()
        {
            DBUser dbs = new DBUser();
            return dbs.Insert(this);

        }

        public User Read(string mail, string password)
        {
            DBUser dbs = new DBUser();
            return dbs.Read(mail, password);
        }



    }
}