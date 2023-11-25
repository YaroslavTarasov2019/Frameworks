using System.Data;
using ZstdSharp.Unsafe;

namespace Frameworks.Model.Subject
{
    public class Person
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string second_name { get; set; }
        public string sur_name { get; set; }
        public string photo_path { get; set; }
        public string phone_number { get; set; }
        public string password { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;

        public Person(int id, string fn, string sn, string surn, string pp, string pn)
        {
            this.id = id;
            this.first_name = fn;
            this.second_name = sn;
            this.sur_name = surn;
            this.photo_path = pp;
            this.phone_number = pn;
        }
        public Person(string fn, string sn, string surn, string pp, string pn, string pass)
        {
            this.first_name = fn;
            this.second_name = sn;
            this.sur_name = surn;
            this.photo_path = pp;
            this.phone_number = pn;
            this.password = pass;
        }
        public Person(int id, string fn, string sn, string surn, string pp, string pn, string pass)
        {
            this.id = id;
            this.first_name = fn;
            this.second_name = sn;
            this.sur_name = surn;
            this.photo_path = pp;
            this.phone_number = pn;
            this.password = pass;
        }
    }
    public class Courier : Person
    {
        public new string status { get; set; }
        public new string role { get; set; }

        public Courier(string fn, string sn, string surn, string pp, string pn, string pass) : base(fn, sn, surn, pp, pn, pass)
        {
            this.role = Constants.ROLE_COURIER;
            this.status = "FREE";
        }
    }
    public class Client : Person
    {
        public new string status { get; set; }
        public new string role { get; set; }

        public Client(string fn, string sn, string surn, string pp, string pn, string pass) : base(fn, sn, surn, pp, pn, pass)
        {
            this.role = Constants.ROLE_CLIENT;
            this.status = "(NULL)";
        }
    }
    public class Admin : Person
    {
        public new string status { get; set; }
        public new string role { get; set; }

        public Admin(string fn, string sn, string surn, string pp, string pn, string pass) : base(fn, sn, surn, pp, pn, pass)
        {
            this.role = Constants.ROLE_ADMIN;
            this.status = "(NULL)";
        }
    }
    public class Entry
    {
        public string phone_number { get; set; }
        public string password { get; set; }
    }
}