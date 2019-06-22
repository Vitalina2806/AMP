using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;

namespace Лабораторна_робота_2___АМП
{
    public partial class ChangePassword : MaterialSkin.Controls.MaterialForm
    {
       public Person person = new Person();
        
        public ChangePassword( Person ob)
        {
            InitializeComponent();
            var skinmanager = MaterialSkinManager.Instance;
            skinmanager.AddFormToManage(this);
            skinmanager.ColorScheme = new ColorScheme(Primary.Brown700, Primary.Brown600, Primary.Brown700, Accent.DeepOrange100, TextShade.WHITE);
            person = ob;
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if ((materialSingleLineTextField1.Text != String.Empty) && (materialSingleLineTextField2.Text != String.Empty))
            {
                PostContext context = new PostContext();


                var newpass = context.LogInfo.Include("Person").ToList()

                    .Where(c => c.Person.name == person.name && c.Person.surname == person.surname)
                    .FirstOrDefault();



                if (Convert.ToString(newpass.password) == materialSingleLineTextField1.Text)
                {
                    newpass.password = Convert.ToInt32(materialSingleLineTextField2.Text);
                    context.SaveChanges();

                    DialogResult result = MessageBox.Show("Ваш пароль оновлено.", "Операція успішна", MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);


                    if (result == DialogResult.OK)
                        this.Close();
                    else this.Close();
                }
                else
                {

                    DialogResult result = MessageBox.Show("Поточний пароль неправильний.", "Операція не виконана", MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);

                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Поточний чи бажаний пароль не введено.", "Помилка", MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
