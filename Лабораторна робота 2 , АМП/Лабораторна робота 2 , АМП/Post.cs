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

    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string address { get; set; }
        public int numberOFpost { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; }
       
        public string getadddr()
        {
            return this.address;

        }
        public ICollection<Parcel> Parcels { get; set; }
        public Post()
        {
            Parcels = new List<Parcel>();
        }
       

        public Post(City city, string addr, int num)
        {
            Parcels = new List<Parcel>();
            this.City = city;
            this.address = addr;
            this.numberOFpost=num;
        }

         public void getparcel(string address)
        {
            throw new NotImplementedException();
        }
    }
}
