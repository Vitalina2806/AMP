using System;
using MaterialSkin;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лабораторна_робота_2___АМП
{
    public partial class LogIn : MaterialSkin.Controls.MaterialForm
    {

        public LogIn()
        {
            InitializeComponent();
            var skinmanager = MaterialSkinManager.Instance;
            skinmanager.AddFormToManage(this);
            skinmanager.ColorScheme = new ColorScheme(Primary.Brown700, Primary.Brown600, Primary.Brown700, Accent.DeepOrange700, TextShade.WHITE);
          

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            using (PostContext db = new PostContext())
            {


                City kyiv = new City("Київ");
                City zhytomyr = new City("Житомир");
                City khmel = new City("Хмельницький");
                City lviv = new City("Львів");
                City ternopil = new City("Тернопіль");
                City odesa = new City("Одеса");
                City kharkiv = new City("Харків");
                City chernivtsi = new City("Чернівці");
                City vinnitsa = new City("Вінниця");
                City ivanoFrank = new City("Івано-Франківськ");

                db.Cities.Add(ivanoFrank);
                db.Cities.Add(kyiv);
                db.Cities.Add(zhytomyr);
                db.Cities.Add(khmel);
                db.Cities.Add(lviv);
                db.Cities.Add(ternopil);
                db.Cities.Add(odesa);
                db.Cities.Add(kharkiv);
                db.Cities.Add(chernivtsi);
                db.Cities.Add(vinnitsa);




                Post kyiv1 = new Post(kyiv, "вул.Шевченківська 12", 1);
                Post kyiv2 = new Post(kyiv, "вул.Проспектна 45", 2);
                Post kyiv3 = new Post(kyiv, "вул.Івана Франка 87/Б", 3);
                Post kyiv4 = new Post(kyiv, "вул.Університетська 23", 4);

                db.Posts.AddRange(new List<Post> { kyiv1, kyiv2, kyiv3, kyiv4 });

                Post zhytomyr1 = new Post(zhytomyr, "вул.Степана Радченка 1а", 1);
                Post zhytomyr2 = new Post(zhytomyr, "вул.Кривоноса 2", 2);

                db.Posts.AddRange(new List<Post> { zhytomyr1, zhytomyr2 });

                Post khmel1 = new Post(khmel, "вул.Кам'янецька 45", 1);
                Post khmel2 = new Post(khmel, "вул.Степана Бандери 10", 2);
                Post khmel3 = new Post(khmel, "вул.Інститутська 11", 3);

                db.Posts.AddRange(new List<Post> { khmel1, khmel2, khmel3 });

                Post lviv1 = new Post(lviv, "вул.Ратушенська 67А", 1);
                Post lviv2 = new Post(lviv, "вул.Годинникова 6", 2);
                Post lviv3 = new Post(lviv, "вул.Західна 9", 3);

                db.Posts.AddRange(new List<Post> { lviv1, lviv2, lviv3 });

                Post ternopil1 = new Post(ternopil, "вул.Святого Іоана 33", 1);
                Post ternopil2 = new Post(ternopil, "вул.Пресвятоуспенська 333", 2);

                db.Posts.AddRange(new List<Post> { ternopil1, ternopil2 });

                Post odesa1 = new Post(odesa, "вул.Морська лагуна 5", 1);
                Post odesa2 = new Post(odesa, "вул.Дельфінкова 5А", 2);
                Post odesa3 = new Post(odesa, "вул.Ланжерон 7", 3);

                db.Posts.AddRange(new List<Post> { odesa1, odesa2, odesa3 });

                Post kharkiv1 = new Post(kharkiv, "вул.Олевандиста Казимира 44", 1);
                Post kharkiv2 = new Post(kharkiv, "вул.Базарна 98", 2);

                db.Posts.AddRange(new List<Post> { kharkiv1, kharkiv2 });

                Post chernivtsi1 = new Post(chernivtsi, "вул.Тернопільська 2", 1);
                Post chernivtsi2 = new Post(chernivtsi, "вул.Чернівецька 8", 2);

                db.Posts.AddRange(new List<Post> { chernivtsi1, chernivtsi2 });

                Post vinnitsa1 = new Post(vinnitsa, "вул.Гагаріна 34/2", 1);
                Post vinnitsa2 = new Post(vinnitsa, "вул.Столична 3", 2);

                db.Posts.AddRange(new List<Post> { vinnitsa1, vinnitsa2 });


                Post ivanoFrank1 = new Post(ivanoFrank, "вул.Соборна Площа 4А", 1);
                Post ivanoFrank2 = new Post(ivanoFrank, "вул.Гагаріна 12", 2);
                Post ivanoFrank3 = new Post(ivanoFrank, "вул.Грушевського 24", 3);

                db.Posts.AddRange(new List<Post> { ivanoFrank1, ivanoFrank2, ivanoFrank3 });

                db.SaveChanges();


            }

        }

            private void toolStripContainer1_BottomToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void materialDivider1_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((materialSingleLineTextField1.Text != String.Empty) && (materialSingleLineTextField2.Text != String.Empty))
            {
                using (PostContext db = new PostContext())
                {
                    LogInfo p = new LogInfo();
                    foreach (LogInfo person in db.LogInfo.Include("Person").ToList())
                    {
                        if ((Convert.ToString(person.password )== materialSingleLineTextField2.Text) && (Convert.ToString(person.login )== materialSingleLineTextField1.Text))
                        {
                              p = person;

                        }
                    }
                    if ((Convert.ToString(p.password) == materialSingleLineTextField2.Text) && (Convert.ToString(p.login) == materialSingleLineTextField1.Text))
                    {

                        Form menu = new Menu(p);
                        menu.Show();
                    }
                    else
                        MessageBox.Show("Невірний пароль або номер телефону.");

                }
            }

            else MessageBox.Show("Авторизація не пройдена у зв'язку з наявністю пустого поля.");
        }


        private void button2_Click(object sender, EventArgs e)

        {
            Form authorization = new Registration();
            authorization.Show();
        }
    }
}
