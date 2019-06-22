using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace Лабораторна_робота_2___АМП
{
    class PostContext : DbContext
    {
        public PostContext() : base("Db") { }

        public DbSet<Post> Posts { get; set; }//
        public DbSet<City> Cities { get; set; }//
        public DbSet<Person> People { get; set; }//
        public DbSet <LogInfo> LogInfo { get; set; }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<DocParcel> DocParcels { get; set; }
        public DbSet<MoneyParcel> MoneyParcels { get; set; }
        public DbSet<VantazhParcel> VantazhParcels { get; set; }
    }
}
