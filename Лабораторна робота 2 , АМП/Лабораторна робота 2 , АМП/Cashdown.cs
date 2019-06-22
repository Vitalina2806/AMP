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
    public partial class Cashdown : MaterialSkin.Controls.MaterialForm
    {
        public Cashdown()
        {
            InitializeComponent();
            var skinmanager = MaterialSkinManager.Instance;
            skinmanager.AddFormToManage(this);
            skinmanager.ColorScheme = new ColorScheme(Primary.Brown700, Primary.Brown600, Primary.Brown700, Accent.DeepOrange100, TextShade.WHITE);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button24_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text != String.Empty) && (textBox2.Text != String.Empty) && (textBox3.Text != String.Empty) && (textBox4.Text != String.Empty) && (textBox5.Text != String.Empty) && (textBox6.Text != String.Empty))
            {
              

                    DialogResult d = MessageBox.Show("На Вашу банківську карту зараховано " + textBox3.Text + " гривень.", "Успішна операція", MessageBoxButtons.OK,
                 MessageBoxIcon.Information,
                 MessageBoxDefaultButton.Button1,
                 MessageBoxOptions.DefaultDesktopOnly);
                    if (d == DialogResult.OK)
                    {
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        textBox5.Clear();
                        textBox6.Clear();
                        comboBox3.SelectedIndex = 0;
                        comboBox4.SelectedIndex = 0;
                     
                    }
                    else
                    {
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        textBox5.Clear();
                        textBox6.Clear();
                        comboBox3.SelectedIndex = 0;
                        comboBox4.SelectedIndex = 0;
                        
                    }


                }
              
            else
            {
                MessageBox.Show("Інформація введена не повністю\nЗаповність всі поля.", "Помилка зняття коштів", MessageBoxButtons.OK,
                 MessageBoxIcon.Information,
                 MessageBoxDefaultButton.Button1,
                 MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
          
        }
    }
}
