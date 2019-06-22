using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Лабораторна_робота_2___АМП
{
   public class City
    {[Key]
        public int Id { get; set; }
        public string name { get; set; }
        public ICollection<Post> Posts { get; set; }
        public City() { Posts = new List<Post>(); }
 
        public City(string name)
        {
            Posts = new List<Post>();
            this.name = name;
        }
        
    }
}
