using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frameworks.Model.Subject
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture_path { get; set; }

        public Food(string name, string description, string picture_path)
        {
            this.Name = name;
            this.Description = description;
            this.Picture_path = picture_path;
        }
    }
}