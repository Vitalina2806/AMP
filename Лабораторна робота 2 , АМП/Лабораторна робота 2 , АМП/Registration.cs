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
    public partial class Registration : MaterialSkin.Controls.MaterialForm
    {
        public Registration()
        {
            InitializeComponent();
            var skinmanager = MaterialSkinManager.Instance;
            skinmanager.AddFormToManage(this);
            skinmanager.ColorScheme = new ColorScheme(Primary.Brown700, Primary.Brown600, Primary.Brown700, Accent.DeepOrange700, TextShade.WHITE);
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((materialSingleLineTextField1.Text != String.Empty) && (materialSingleLineTextField2.Text != String.Empty) && (materialSingleLineTextField3.Text != String.Empty) && (materialSingleLineTextField4.Text != String.Empty) && (comboBox1.Text.Length > 0))
            {


                using (PostContext db = new PostContext())
                {


                    LogInfo clientinfo = new LogInfo(materialSingleLineTextField4.Text);
                    db.LogInfo.Add(clientinfo);
                    Random rand = new Random();
                    var post = db.Posts.Where(c => c.City.name == comboBox1.Text).ToList();
                    var mypost = post[rand.Next(0, post.Count())];


                    Person client = new Person (comboBox1.Text, materialSingleLineTextField1.Text, materialSingleLineTextField2.Text, materialSingleLineTextField3.Text, clientinfo, mypost);
                    MessageBox.Show("Реєстрація пройшла успішно.\nВаш пароль:  " + clientinfo.password+"\nВаше поштове відділення\nзнаходиться за адресою\n"+mypost.address);

                    db.People.Add(client);
                    db.SaveChanges();
                }
            }
            else MessageBox.Show("Введіть інформацію у всі поля!");
            materialSingleLineTextField1.Clear();
            materialSingleLineTextField2.Clear();
            materialSingleLineTextField3.Clear();
            materialSingleLineTextField4.Clear();
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialSingleLineTextField4_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }
    }
}
