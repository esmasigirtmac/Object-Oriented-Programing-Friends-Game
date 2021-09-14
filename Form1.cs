/****************************************************************************
**					SAKARYA ÜNİVERSİTESİ
**				BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
**				    BİLGİSAYAR MÜHENDİSLİĞİ BÖLÜMÜ
**				   NESNEYE DAYALI PROGRAMLAMA DERSİ
**					2014-2015 BAHAR DÖNEMİ
**	
**				ÖDEV NUMARASI..........: Proje
**				ÖĞRENCİ ADI............: Esmaülhüsna Sığırtmaç
**				ÖĞRENCİ NUMARASI.......: G171210032
**              DERSİN ALINDIĞI GRUP...: 1A
****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    pictureBox1.BackgroundImage = Image.FromFile(@"Resimler\Phoebe.png");
                    break;
                case 1:
                    pictureBox1.BackgroundImage = Image.FromFile(@"Resimler\Joey.png");
                    break;
                case 2:
                    pictureBox1.BackgroundImage = Image.FromFile(@"Resimler\Chandler.png");
                    break;
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    pictureBox2.BackgroundImage = Image.FromFile(@"Resimler\Janes.png");
                    break;
                case 1:
                    pictureBox2.BackgroundImage = Image.FromFile(@"Resimler\Gunther.png");
                    break;
                case 2:
                    pictureBox2.BackgroundImage = Image.FromFile(@"Resimler\Richard.png");
                    break;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    pictureBox3.BackColor = Color.FromArgb(255, 192, 192);
                    break;
                case 1:
                    pictureBox3.BackColor = Color.RosyBrown;
                    break;
                case 2:
                    pictureBox3.BackColor = Color.PeachPuff;
                    break;
                default:
                    pictureBox3.BackColor = Color.FromArgb(255, 192, 192);
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Your information has been successfully saved.", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Form2 form2sec = new Form2();
            form2sec.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string file = "File.txt";
            FileStream stream = File.Create(file);
            StreamWriter sw = new StreamWriter(stream);
            sw.WriteLine(comboBox1.SelectedIndex + " " + comboBox2.SelectedIndex + " " + comboBox3.SelectedIndex);
            sw.Close();
            stream.Close();
            file = "Game.txt";
            stream = File.Create(file);
            sw = new StreamWriter(stream);
            sw.WriteLine(0 + " " + 1);
            sw.Close();
            stream.Close();
            //MessageBox.Show("Your information has been successfully saved.", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Form2 form2sec = new Form2();
            form2sec.Show();
            this.Hide();
        }
    }

}
