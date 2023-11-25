using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frameworks.Model.Subject
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture_Path { get; set; }

        public Restaurant(string name, string description, string picture_path)
        {
            Name = name;
            Description = description;
            Picture_Path = picture_path;
        }
    }
}
