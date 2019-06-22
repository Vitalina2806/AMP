using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Лабораторна_робота_2___АМП
{
    public class Parcel
    {
        [Key]
        public int Id { get; set; }

        // 3.Реалізувати поля даних за допомогою властивостей (get, set).,1,2,4,11,13,14
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int price { get; set; }
        public long TTN { get; set; }
        public string type { get; set; }
        public string mistovidpr { get; set; }
        public string mistootr { get; set; }
     
        public string client { get; set; }
        public string otrymuvach { get; set; }

        public int? PostId { get; set; }
        public Post Post { get; set; }
        
        public virtual int getPrice(int weightofdelivery)
        {
            this.price = weightofdelivery * 15;
            return this.price;
        }

        public Parcel() {  }

        
      
        //public Parcel(string d1, string d2, Person ob,string cityfrom,string typefrom,int vaga,Label displayprice,Label displayTTN,string otrymuvach)
        //      {
        //      this.startDate = Convert.ToString(d1);
        //      this.endDate = Convert.ToString(d2);
        //      this.client = ob.getname() +" "+ ob.getsurname();
        //      this.otrymuvach = otrymuvach;

        //      this.weight = vaga;
        //      this.price = this.getPrice(this.weight);
        //      displayprice.Text = Convert.ToString(this.price);
        //      this.type = typefrom;
        //      this.mistovidpr = ob.getcity();
        //      this.mistootr = cityfrom;

        //      Random rand = new Random();
        //      this.TTN = rand.Next(1000, 5000);
        //      displayTTN.Text = Convert.ToString(this.TTN);
        //      }

        //public virtual void writeTofile()
        //{
        //    string[] lines = { this.startDate, this.endDate, this.client, this.type, Convert.ToString(this.weight), Convert.ToString(this.price),this.mistovidpr, this.mistootr, Convert.ToString(this.TTN) };
        //    System.IO.File.AppendAllLines(@"Parcels.txt", lines);

        //}

        //public virtual void readINFOaboutparcel(List<Parcel> anyparcel)
        //{

        //    string line;
        //    System.IO.StreamReader file = new System.IO.StreamReader(@"Parcels.txt");
        //    while ((line = file.ReadLine()) != null)
        //    {
        //            Parcel parcel = new Parcel();
        //            parcel.startDate = line;

        //            line = file.ReadLine();
        //            parcel.endDate = line;

        //            line = file.ReadLine();
        //            parcel.type = line;

        //            line = file.ReadLine();
        //            parcel.client = line;

        //            line = file.ReadLine();
        //            parcel.otrymuvach = line;

        //            line = file.ReadLine();
        //            parcel.price = System.Convert.ToInt32(line);

        //            line = file.ReadLine();
        //            parcel.mistovidpr = line;

        //            line = file.ReadLine();
        //            parcel.mistootr = line;

        //            line = file.ReadLine();
        //            parcel.TTN = System.Convert.ToInt64(line);

        //            anyparcel.Add(parcel);
        //        }
        //    file.Close();    
        //}

        //public void writeMAINtofile()
        //{
        //    string[] lines = { this.startDate, this.endDate,this.type, this.client, this.otrymuvach, Convert.ToString(this.price), this.mistovidpr, this.mistootr, Convert.ToString(this.TTN) };
        //    System.IO.File.AppendAllLines(@"Parcels.txt", lines);

        //}


    }

   public  class DocParcel : Parcel
    {
        [Key]
        public int IdParcelDoc { get; set; }

       

        public DocParcel() { }

        public DocParcel(string d1, string d2, Person ob, string cityfrom, string typefrom, Label displayprice, Label displayTTN, string otrymuvach , Post postN)
        {
            this.startDate = Convert.ToString(d1);
            this.endDate = Convert.ToString(d2);
            this.client = ob.getname() + " " + ob.getsurname();
            this.otrymuvach = otrymuvach;
            this.price = 46;
            displayprice.Text = Convert.ToString(this.price);
            this.type = typefrom;
            this.mistovidpr = ob.getcity();
            this.mistootr = cityfrom;

            Random rand = new Random();
            this.TTN = rand.Next(1000, 5000);
            displayTTN.Text = Convert.ToString(this.TTN);
            this.Post = postN;

        }

        //public override void writeTofile()
        //{
        //    string[] lines = { this.startDate, this.endDate, this.client,this.otrymuvach, this.type, Convert.ToString(this.weight), Convert.ToString(this.price), this.mistovidpr, this.mistootr, Convert.ToString(this.TTN) };
        //    System.IO.File.AppendAllLines(@"DocParcels.txt", lines);

        //}

        // public override void readINFOaboutparcel(List<Parcel> anydocparcel)
        //{

        //    string line;
        //    System.IO.StreamReader file = new System.IO.StreamReader(@"DocParcels.txt");
        //    while ((line = file.ReadLine()) != null)
        //    {
        //        DocParcel docparcel = new DocParcel();
        //        docparcel.startDate = line;

        //        line = file.ReadLine();
        //        docparcel.endDate = line;

        //        line = file.ReadLine();
        //        docparcel.client = line;

        //        line = file.ReadLine();
        //        docparcel.otrymuvach = line;

        //        line = file.ReadLine();
        //        docparcel.price = System.Convert.ToInt32(line);

        //        line = file.ReadLine();
        //        docparcel.mistovidpr = line;

        //        line = file.ReadLine();
        //        docparcel.mistootr = line;

        //        line = file.ReadLine();
        //        docparcel.TTN = System.Convert.ToInt64(line);

        //        anydocparcel.Add(docparcel);
        //    }
        //    file.Close();

        //}

    }

   public  class MoneyParcel : Parcel
    {
        [Key]
        public int IdParcelMoney { get; set; }

        public int suma { get; set; }

        public MoneyParcel() { }

        public override int getPrice(int suma)
        {
            price = (suma / 10) + 15;
            return this.price;
        }
        public int getsum(int sum)
        {
            this.suma += sum;
            return this.suma;
        }
        public MoneyParcel(string d1, string d2, Person ob, string cityfrom, string typefrom, int suma, Label displayprice, Label displayTTN, string otrymuvach,Post postN)
        {
            this.startDate = Convert.ToString(d1);
            this.endDate = Convert.ToString(d2);
            this.client = ob.getname() + " " + ob.getsurname();
            this.otrymuvach = otrymuvach;
            this.suma = suma;
            this.price = this.getPrice(this.suma);
            displayprice.Text = Convert.ToString(this.price);
            this.type = typefrom;
            this.mistovidpr = ob.getcity();
            this.mistootr = cityfrom;

            Random rand = new Random();
            this.TTN = rand.Next(1000, 5000);
            displayTTN.Text = Convert.ToString(this.TTN);

            this.Post = postN;
        }

        //    public override void writeTofile()
        //    {
        //        string[] lines = { this.startDate, this.endDate, this.client, this.otrymuvach, this.type, Convert.ToString(this.suma), Convert.ToString(this.price), this.mistovidpr, this.mistootr, Convert.ToString(this.TTN) };
        //        System.IO.File.AppendAllLines(@"MoneyParcels.txt", lines);

        //    }

        //    public sealed override void readINFOaboutparcel(List<Parcel> anymoneyparcel)
        //    {

        //        string line;
        //        System.IO.StreamReader file = new System.IO.StreamReader(@"MoneyParcels.txt");
        //        while ((line = file.ReadLine()) != null)
        //        {
        //            MoneyParcel moneyparcel = new MoneyParcel();
        //            moneyparcel.startDate = line;

        //            line = file.ReadLine();
        //            moneyparcel.endDate = line;

        //            line = file.ReadLine();
        //            moneyparcel.client = line;

        //            line = file.ReadLine();
        //            moneyparcel.otrymuvach = line;


        //            line = file.ReadLine();
        //            moneyparcel.type = line;

        //            line = file.ReadLine();
        //            moneyparcel.suma = System.Convert.ToInt32(line);

        //            line = file.ReadLine();
        //            moneyparcel.price = System.Convert.ToInt32(line);

        //            line = file.ReadLine();
        //            moneyparcel.mistovidpr = line;

        //            line = file.ReadLine();
        //            moneyparcel.mistootr = line;

        //            line = file.ReadLine();
        //            moneyparcel.TTN = System.Convert.ToInt64(line);

        //            anymoneyparcel.Add(moneyparcel);
        //        }
        //        file.Close();

        //    }
        //}  
    }

   public class VantazhParcel : Parcel
        {
            [Key]
            public int IdParcelVantazh { get; set; }

            public int weight { get; set; }
            public VantazhParcel() { }



            public VantazhParcel(string d1, string d2, Person ob, string cityfrom, string typefrom, int vaga, Label displayprice, Label displayTTN, string otrymuvach, Post postN)
            {
                this.startDate = Convert.ToString(d1);
                this.endDate = Convert.ToString(d2);
                this.client = ob.getname() + " " + ob.getsurname();
                this.otrymuvach = otrymuvach;
                this.weight = vaga;
                this.price = base.getPrice(this.weight);//6
                displayprice.Text = Convert.ToString(this.price);
                this.type = typefrom;
                this.mistovidpr = ob.getcity();
                this.mistootr = cityfrom;

                Random rand = new Random();
                this.TTN = rand.Next(1000, 5000);
                displayTTN.Text = Convert.ToString(this.TTN);
                this.Post = postN;
        }

            //public override void writeTofile()
            //{
            //    string[] lines = { this.startDate, this.endDate, this.client,this.otrymuvach, this.type, Convert.ToString(this.weight), Convert.ToString(this.price), this.mistovidpr, this.mistootr, Convert.ToString(this.TTN) };
            //    System.IO.File.AppendAllLines(@"VantazhParcels.txt", lines);

            //}



            //public override void readINFOaboutparcel(List<Parcel> anyWparcel)
            //{

            //    string line;
            //    System.IO.StreamReader file = new System.IO.StreamReader(@"VantazhParcels.txt");
            //    while ((line = file.ReadLine()) != null)
            //    {
            //        VantazhParcel vantazhparcel = new VantazhParcel();
            //        vantazhparcel.startDate = line;

            //        line = file.ReadLine();
            //        vantazhparcel.endDate = line;

            //        line = file.ReadLine();
            //        vantazhparcel.client = line;

            //        line = file.ReadLine();
            //        vantazhparcel.otrymuvach = line;

            //        line = file.ReadLine();
            //        vantazhparcel.type = line;

            //        line = file.ReadLine();
            //        vantazhparcel.weight = System.Convert.ToInt32(line);

            //        line = file.ReadLine();
            //        vantazhparcel.price = System.Convert.ToInt32(line);

            //        line = file.ReadLine();
            //        vantazhparcel.mistovidpr = line;

            //        line = file.ReadLine();
            //        vantazhparcel.mistootr = line;

            //        line = file.ReadLine();
            //        vantazhparcel.TTN = System.Convert.ToInt64(line);

            //        anyWparcel.Add(vantazhparcel);
            //    }
            //    file.Close();


            //}

        }
    }

