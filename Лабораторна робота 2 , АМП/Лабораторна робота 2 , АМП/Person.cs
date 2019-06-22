using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Лабораторна_робота_2___АМП
{ 
    interface addTocash
    {
        
        int Put(int fromparcel,int fromtxtbox);
    }
   public class Person:addTocash
    { // 3.Реалізувати поля даних за допомогою властивостей (get, set).
        [Key]
        [ForeignKey("Log")]
        public int Id { get; set; }
        public int[] arr = new int[3];
        public int this[int index]
        {  get
            {
                
                return arr[index];
            }
            set
            { arr[index] = value; }
        }
        public string name { get; set; }
        public string surname { get; set; }
        public string address { get; set; }
        public string city { get; set; }

        public Post post { get; set; }
        public int money { get; set; }
   
     
    
        public string getname() { return this.name; }
        public string getsurname () { return this.surname; }
        public string getcity() { return this.city; }
        public string getaddr() { return this.address; }

        public LogInfo Log { get; set; }
        public Person()
        {

        }
   
        public int getmoney()
        {
            Person p = new Person();
            Random rand = new Random();

            for (int i = 0; i < 2; i++)
            {
                p[i] = rand.Next(100, 900);
            }
            this.money = p[0] + p[1] - (p[2] / 5);
            return this.money;
        }
        public Person(string cityfromkb,string namefromkb, string surnamefromkb, string contacts, LogInfo idshka,Post mypost)
        {
           this.city = cityfromkb;
            this.name = namefromkb;
            this.surname = surnamefromkb;
            this.address = contacts;

            this.money = this.getmoney();
            this.Id = idshka.Id;
            this.post = mypost;

           
           
        }

        public int Put(int fromparc,int fromtxt)
        {
            fromtxt=fromtxt+fromparc;
            return fromtxt;
        }
       
        public void readINFOaboutclient(List<Person> smth)
        {
                    
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"Clients.txt");
            while ((line = file.ReadLine()) != null)
            {
                Person ob = new Person();
                    ob.city = line;

                    line = file.ReadLine();
                    ob.name = line;

                    line = file.ReadLine();
                    ob.surname = line;

                    line = file.ReadLine();
                    ob.address = line;

                    line = file.ReadLine();
                    ob.money = Convert.ToInt32(line);


                    smth.Add(ob);
                
                         }



            file.Close();
        }
        public void rewriteinfo(Person ob,string subs)
        {
            string[] lines = System.IO.File.ReadAllLines(@"Clients.txt");
            int i = 0;
          
            foreach (string k in lines)
            {
          
                if ( k == Convert.ToString(ob.money))
                {
                    lines[i] = subs;
                }
                    
              i++;
            }
            System.IO.File.WriteAllLines(@"Clients.txt", lines);
            
        }

        public void updateInfoAboutCurrentCash(int newcash)
        {
            PostContext context = new PostContext();

            var customer = context.People

                .Where(c => c.money == money)
                .FirstOrDefault();

            customer.money = newcash;
            context.SaveChanges();
        }

    }

    public class LogInfo
    {
      [Key]
        public int Id { get; set; }

        public int password { get; set; }
        public string  login { get; set; }

        public LogInfo( string log)
        {
            Random rand = new Random();
            this.password = rand.Next(1000, 9999);
            this.login = log;
        }

        public LogInfo()
        {
        }

        public Person Person { get; set; }
    }
 }