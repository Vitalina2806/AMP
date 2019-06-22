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
using System.Data.Entity;

namespace Лабораторна_робота_2___АМП
{
    public partial class Menu : MaterialSkin.Controls.MaterialForm
    {
        public string gettxtbox(string fromtb)
        {
            return fromtb;
        }

        LogInfo current = new LogInfo();
        delegate string showcash(string currcash, int elemofsubs);
        static string showmoneyintextbox(string fromtxtbox, int b)
        {
            fromtxtbox = Convert.ToString(Convert.ToInt32(fromtxtbox) - b);
            return fromtxtbox;
        }

        delegate void showcashformp();

        delegate int liambda(int txtbox, int price);

        liambda liambdaviraz = (txtbox, price) => txtbox - price;
        liambda addToCurrentCash = (txtbox, suma) => txtbox + suma;

        private LogInfo logInfo;


        public Menu(LogInfo ob)
        {
            InitializeComponent();
            var skinmanager = MaterialSkinManager.Instance;
            skinmanager.AddFormToManage(this);
            skinmanager.ColorScheme = new ColorScheme(Primary.Brown700, Primary.Brown600, Primary.Brown700, Accent.DeepOrange100, TextShade.WHITE);
            current = ob;
            current.Person = ob.Person;


            dataGridView2.Rows.Add("Ім'я", current.Person.name);
            dataGridView2.Rows.Add("Прізвище", current.Person.surname);
            dataGridView2.Rows.Add("Місто", current.Person.city);
            dataGridView2.Rows.Add("Адреса", current.Person.address);
            dataGridView2.Rows.Add("Телефон", current.login);
            dataGridView2.Rows.Add("Пароль", current.password);



            materialSingleLineTextField1.Text = Convert.ToString(current.Person.money);


            using (PostContext db = new PostContext())
            {
                //використання  оператора join, об'єднання двох таблиць
                var postho4ki = from p in db.Posts
                             join c in db.Cities on p.CityId equals c.Id
                             select new { City = c.name, Address = p.address, Number = p.numberOFpost,id=p.Id };

                foreach (var postinka in postho4ki)
                {

                    if (current.Person.city == postinka.City) 
                   {
                        materialLabel45.Text = " № " + Convert.ToString(postinka.Number);
                        materialLabel25.Text = postinka.Address;
                        materialLabel29.Text = postinka.City;

                    }

                }
            }



        }
        private void Form3_Load(object sender, EventArgs e)
        {

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            List<Parcel> pp = new List<Parcel>();

            using (PostContext db = new PostContext())
            {
                string otr = current.Person.name + " " + current.Person.surname;
                //sort by method OrderBy(), operator group, .Include()-4,5, Intersect

                var parcelsForCurr = from f in db.Parcels.OrderBy(p => p.startDate).Include("Post").ToList()
                                    .Intersect(db.Parcels.Where(p => p.mistootr == current.Person.city))
                                     group f by f.startDate;



                //Include + group

                foreach (var g in parcelsForCurr)
                {
                    foreach (var p in g)
                    {
                        if (p.otrymuvach == otr)
                        {
                            pp.Add(p);
                        }
                    }
                }
                if (pp.Count > 0)
                {


                    foreach (Parcel count in pp)
                    {

                        dataGridView1.Rows.Add(count.startDate, count.endDate, count.TTN, count.type, count.client);


                    }
                }
                else
                {
                    dataGridView1.Visible = false;
                }


            }
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(3);


        }

        private void button1_Click(object sender, EventArgs e)
        {



            string date = dateTimePicker1.Value.ToShortDateString();
            string data = dateTimePicker2.Value.ToShortDateString();

            DialogResult result = MessageBox.Show(
         " \nПосилка буде доставлена " + Convert.ToString(data) + " до міста " + comboBox2.Text + ".\nОтримувач: " + materialSingleLineTextField2.Text + "\nЯкщо інформація введена правильно, натисніть кнопку <ТАК>.",
         "Перевірка введеної інформації",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1,
        MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
            {

                int vaga = 0;
                if (comboBox1.SelectedIndex == 0)
                    vaga = 1;
                else if (comboBox1.SelectedIndex == 1)
                    vaga = 5;
                else if (comboBox1.SelectedIndex == 2)
                    vaga = 10;
                else if (comboBox1.SelectedIndex == 3)
                    vaga = 15;
                else if (comboBox1.SelectedIndex == 4)
                    vaga = 20;


                List<DocParcel> documents = new List<DocParcel>();
                List<VantazhParcel> vantazh = new List<VantazhParcel>();
                List<MoneyParcel> money = new List<MoneyParcel>();

                using (PostContext db = new PostContext())
                {
                    var otrymuvach = db.People.Where(c => c.name + " " + c.surname == materialSingleLineTextField2.Text).Include(t => t.post).FirstOrDefault();
                    if (otrymuvach != null)
                    {
                        if (checkedListBox1.SelectedIndex == 0)
                        {
                            if ((Convert.ToInt32(materialSingleLineTextField1.Text) - 15) == 0)
                            {
                                MessageBox.Show("Недостатньо коштів на рахунку для відправлення посилки.", "Помилка відправлення", MessageBoxButtons.OK,
          MessageBoxIcon.Information,
          MessageBoxDefaultButton.Button1,
          MessageBoxOptions.DefaultDesktopOnly);
                            }
                            else
                            {

                                DocParcel dp = new DocParcel(date, data, current.Person, comboBox2.Text, checkedListBox1.Text, materialLabel6, materialLabel9, materialSingleLineTextField2.Text, otrymuvach.post);
                                documents.Add(dp);
                                db.DocParcels.Add(dp);

                                db.Parcels.Add(dp);
                                db.SaveChanges();

                                materialSingleLineTextField1.Text = Convert.ToString(liambdaviraz(Convert.ToInt32(materialSingleLineTextField1.Text), dp.price));

                            }
                        }

                        else if (checkedListBox1.SelectedIndex == 1)
                        {
                            if ((Convert.ToInt32(materialSingleLineTextField1.Text) - Convert.ToInt32(materialSingleLineTextField3.Text)) < 50)
                            {

                                MessageBox.Show("Недостатньо коштів на рахунку для відправлення посилки.", "Помилка відправлення", MessageBoxButtons.OK,
          MessageBoxIcon.Information,
          MessageBoxDefaultButton.Button1,
          MessageBoxOptions.DefaultDesktopOnly);

                            }
                            else
                            {
                                    MoneyParcel mp = new MoneyParcel(date, data, current.Person, comboBox2.Text, checkedListBox1.Text, Convert.ToInt32(materialSingleLineTextField3.Text), materialLabel6, materialLabel9, materialSingleLineTextField2.Text, otrymuvach.post);

                                    money.Add(mp);
                                    db.Parcels.Add(mp);
                                    db.MoneyParcels.Add(mp);
                                    db.SaveChanges();
                                    showcashformp scash = delegate
                                     {

                                         materialSingleLineTextField1.Text = Convert.ToString(Convert.ToInt32(materialSingleLineTextField1.Text) - mp.price - mp.suma);

                                     };

                                    scash();
                                
                            }


                        }
                        else if (checkedListBox1.SelectedIndex == 2)
                        {
                            if (Convert.ToInt32(materialSingleLineTextField1.Text) < vaga * 15)
                            {

                                MessageBox.Show("Недостатньо коштів на рахунку для відправлення посилки.", "Помилка відправлення", MessageBoxButtons.OK,
          MessageBoxIcon.Information,
          MessageBoxDefaultButton.Button1,
          MessageBoxOptions.DefaultDesktopOnly);
                            }
                            else
                            {
                               

                                    VantazhParcel vp = new VantazhParcel(date, data, current.Person, comboBox2.Text, checkedListBox1.Text, vaga, materialLabel6, materialLabel9, materialSingleLineTextField2.Text, otrymuvach.post);
                                    vantazh.Add(vp);


                                    showcash mon = new showcash(showmoneyintextbox);
                                    materialSingleLineTextField1.Text = mon(gettxtbox(materialSingleLineTextField1.Text), vp.price);
                                    db.Parcels.Add(vp);
                                    db.VantazhParcels.Add(vp);
                                    db.SaveChanges();
                                
                            }
                        }
                    }
                    else
                    {
                        DialogResult err = MessageBox.Show(
            "Отримувача з введеними даними немає в базі даних нашої пошти.",
            "Посилка не відправлена",
           MessageBoxButtons.OK,
           MessageBoxIcon.Error,
           MessageBoxDefaultButton.Button1,
           MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            current.Person.updateInfoAboutCurrentCash(Convert.ToInt32(materialSingleLineTextField1.Text));
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            current.Person.updateInfoAboutCurrentCash(Convert.ToInt32(materialSingleLineTextField1.Text));
            this.Close();

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel20_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel21_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Parcel p1 = new Parcel();
            using (PostContext db = new PostContext())
            {

                var parcels = db.Parcels.ToList();

                foreach (Parcel incr in parcels)
                {
                    if (materialSingleLineTextField4.Text == Convert.ToString(incr.TTN))
                    {
                        p1 = incr;
                    }
                }

                if (Convert.ToString(p1.TTN) == materialSingleLineTextField4.Text)
                {
                    dateTimePicker3.Value = Convert.ToDateTime(p1.startDate);
                    dateTimePicker4.Value = Convert.ToDateTime(p1.endDate);
                    materialLabel13.Text = Convert.ToString(p1.price) + " гривень";
                    materialLabel21.Text = p1.client;
                    materialLabel16.Text = p1.mistovidpr;
                    materialLabel17.Text = p1.mistootr;
                }

                else
                {
                    DialogResult res = MessageBox.Show("Перевірте правильність ТТН.\nПосилка з таким номером не була знайдена.", "Помилка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);

                    if (res == DialogResult.OK)
                    {
                        materialSingleLineTextField4.Clear();
                    }
                    else materialSingleLineTextField4.Clear();
                }
            }

        }


        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    checkedListBox1.SetItemChecked(i, false);
                checkedListBox1.SetItemChecked(checkedListBox1.SelectedIndex, true);
            }
        }
        private void checkedListBox1_DoubleClick(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == 0)
            {
                label12.Visible = false;
                materialLabel4.Visible = false;
                materialSingleLineTextField3.Visible = false;
                comboBox1.Visible = false;


            }

            else if (checkedListBox1.SelectedIndex == 1)
            {
                label12.Visible = true;
                materialLabel4.Visible = true;
                materialLabel4.Text = "Сума";
                materialSingleLineTextField3.Visible = true;
                comboBox1.Visible = false;



            }
            else if (checkedListBox1.SelectedIndex == 2)
            {
                label12.Visible = true;
                materialLabel4.Visible = true;
                materialLabel4.Text = "Вага";
                materialSingleLineTextField3.Visible = false;
                comboBox1.Visible = true;

            }
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void checkedListBox1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == 0)
            {
                label12.Visible = false;
                materialLabel4.Visible = false;
                materialSingleLineTextField3.Visible = false;
                comboBox1.Visible = false;


            }

            else if (checkedListBox1.SelectedIndex == 1)
            {
                label12.Visible = true;
                materialLabel4.Visible = true;
                materialLabel4.Text = "Сума";
                materialSingleLineTextField3.Visible = true;
                comboBox1.Visible = false;



            }
            else if (checkedListBox1.SelectedIndex == 2)
            {
                label12.Visible = true;
                materialLabel4.Visible = true;
                materialLabel4.Text = "Вага";
                materialSingleLineTextField3.Visible = false;
                comboBox1.Visible = true;

            }

        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            List<MoneyParcel> posilki = new List<MoneyParcel>();
            PostContext db = new PostContext();
            //Операція з множинами
            var vant = db.VantazhParcels.Where(c => (current.Person.name + " " + current.Person.surname) == c.otrymuvach);
            var docs = db.DocParcels.Where(c => (current.Person.name + " " + current.Person.surname) == c.otrymuvach);
            //Агрегатна операція Count
            int neotr = vant.Count() + docs.Count();
            var money = db.MoneyParcels.Where(p => p.suma < 5000)
                .Intersect(db.MoneyParcels.Where(p => p.mistootr == current.Person.city));

            foreach (MoneyParcel m in money)
            {
                if (m.otrymuvach == (current.Person.name + " " + current.Person.surname))
                {
                    posilki.Add(m);
                }
            }
            if (posilki.Count > 0)
            {
                dataGridView1.Visible = true;

                int suma = 0;

                foreach (MoneyParcel counter in posilki)
                {
                    suma += counter.suma;
                }
              
                MessageBox.Show("Загальна сума грошових відправлень на\nВаш рахунок складає " + Convert.ToString(suma) + " гривень.\n" + "У вас неотримано " + neotr + " відправлення(-ь).\nЇх можна отримати за адресою\nВашого поштового відділення.", "Отримання відправлення", MessageBoxButtons.OK,
                   MessageBoxIcon.Information,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.DefaultDesktopOnly);
                materialSingleLineTextField1.Text = Convert.ToString(addToCurrentCash(Convert.ToInt32(materialSingleLineTextField1.Text), suma));


                foreach (MoneyParcel count in posilki)
                {
                    db.MoneyParcels.Remove(count);
                    db.SaveChanges();
                }

                //refresh datagridview
               


                List<Parcel> pp = new List<Parcel>();
                if (dataGridView1.Rows.Count != null)
                {
                    
                    dataGridView1.Rows.Clear();
                    string otr = current.Person.name + " " + current.Person.surname;
                    var parcelsForCurr = db.Parcels;


                    foreach (Parcel p in parcelsForCurr)
                    {
                        if (p.otrymuvach == otr)
                        {
                            pp.Add(p);
                        }
                    }

                    foreach (Parcel count in pp)
                    {

                        dataGridView1.Rows.Add(count.startDate, count.endDate, count.TTN, count.type, count.client);

                    }
                }
                else
                {
                    MessageBox.Show("Неотриманих відправлень немає", "Інформаційне повідомлення");
                    dataGridView1.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Посилки можна отримати за адресою \nнайближчого поштового відділення.", "Отримання відправлення", MessageBoxButtons.OK,
                  MessageBoxIcon.Information,
                  MessageBoxDefaultButton.Button1,
                  MessageBoxOptions.DefaultDesktopOnly);



            }



        }

        private void materialTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void button10_Click(object sender, EventArgs e)
        {
            current.Person.updateInfoAboutCurrentCash(Convert.ToInt32(materialSingleLineTextField1.Text));
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            current.Person.updateInfoAboutCurrentCash(Convert.ToInt32(materialSingleLineTextField1.Text));
            this.Close();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button11_Click(object sender, EventArgs e)
        {
            Form changepass = new ChangePassword(current.Person);
            changepass.Show();
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
           // current.Person.updateInfoAboutCurrentCash(Convert.ToInt32(materialSingleLineTextField1.Text));
        }

        private void DataGridView1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Label44_Click(object sender, EventArgs e)
        {

        }

        private void MaterialSingleLineTextField1_Click(object sender, EventArgs e)
        {

        }

        private void MaterialSingleLineTextField1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void MaterialSingleLineTextField2_KeyPress(object sender, KeyPressEventArgs e)
        {



        }

        private void MaterialSingleLineTextField3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            { 
                    e.Handled = true;
            }
           
        }

        private void MaterialSingleLineTextField4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
                e.Handled = true;


        }

        private void ComboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void ComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void MaterialSingleLineTextField2_Click(object sender, EventArgs e)
        {

        }

        private void MaterialSingleLineTextField3_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void MaterialSingleLineTextField3_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void Button12_Click(object sender, EventArgs e)
        {
            Form cash = new Cashdown();
            cash.Show();
        }

        private void MaterialLabel34_Click(object sender, EventArgs e)
        {

        }

        private void TextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!Char.IsDigit(e.KeyChar))
            {
                  e.Handled = true;
            }
        }

        private void TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Button24_Click(object sender, EventArgs e)
        {
           
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button14_Click(object sender, EventArgs e)
        {
                   }

        private void Button15_Click(object sender, EventArgs e)
        {
            
        }
    }
}


