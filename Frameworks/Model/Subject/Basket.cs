using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frameworks.Model.Subject
{
    public class Basket
    {
        public int Id { get; set; }
        public int Id_Food { get; set; }
        public int Id_Client { get; set; }
        public int Amount { get; set; }
        public int Id_Restaurant { get; set; }

        public Basket(string arg1, string arg2, string arg3, string arg4, string arg5)
        {
            this.Id = Convert.ToInt32(arg1);
            this.Id_Food = Convert.ToInt32(arg2);
            this.Id_Client = Convert.ToInt32(arg3);
            this.Amount = Convert.ToInt32(arg4);
            this.Id_Restaurant = Convert.ToInt32(arg5);
        }
        public Basket(string arg2, string arg3, string arg4, string arg5)
        {
            this.Id_Food = Convert.ToInt32(arg2);
            this.Id_Client = Convert.ToInt32(arg3);
            this.Amount = Convert.ToInt32(arg4);
            this.Id_Restaurant = Convert.ToInt32(arg5);
        }
    }
}
