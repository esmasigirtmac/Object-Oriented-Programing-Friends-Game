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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form2 : Form
    {
        Random rand = new Random();
        List<Balloon> balloons = new List<Balloon>();
        List<Arrow> arrows = new List<Arrow>();
        bool gameOver = false;
        public int balloon_size { get; set; }
        public int red_balloon_creat_size { get; set; }
        public int yellow_balloon_creat_size { get; set; }
        public int yellow_balloon_creat_size_level2 { get; set; }
        public int remaining_arrow { get; set; }
        public int tick { get; set; }

        static Random random          = new Random();
        Gamer          gamer          = new Gamer();
        Archer         archer         = new Archer();
        Arrow          arrow          = new Arrow();
        Balloon        balloon        = new Balloon();
        Red_Balloon    red_balloon    = new Red_Balloon();
        Yellow_Balloon yellow_balloon = new Yellow_Balloon();

        public Form2()
        {
            InitializeComponent();
        }
        private void Start()
        {
            red_balloon_creat_size = 6;
            yellow_balloon_creat_size = 5;
            yellow_balloon_creat_size_level2 = 4;
            remaining_arrow = 50;
            tick = 0;
            File_Read();
            Archer_Creat();
            Arrow_Timer.Start();
            Balloon_Creat_Timer.Start();
            Balloon_Move_Timer.Start();
            GameTimer.Start();
            if (gamer.Level == 3)
                label3.Visible = true;
            else
                label3.Visible = false;
        }
        private void Stop()
        {
            if (!gameOver) return;
            GameTimer.Stop();
            Arrow_Timer.Stop();
            Balloon_Creat_Timer.Stop();
            Balloon_Move_Timer.Stop();
        }
        private void Archer_Move(char direction)
        {
            if (gameOver) return;

            if (direction == 'W')
            {
                if (pictureBox2.Top - pictureBox2.Width < label1.Height)
                    pictureBox2.Top = label1.Height;
                else
                    pictureBox2.Top -= pictureBox2.Width / 2;
            }
            else
            {
                if (pictureBox2.Top + 2 * pictureBox2.Width > panel1.Height)
                    pictureBox2.Top = panel1.Height - pictureBox2.Width;
                else
                    pictureBox2.Top += pictureBox2.Width / 2;
            }
        }
        private void Archer_Creat()
        {
            pictureBox2.BackgroundImage = archer.Archer_Image;
        }
        private void Arrow_Creat()
        {
            if (gameOver) return;
            if (gamer.Level == 3)
            {
                remaining_arrow--;
                label3.Text = "REMAINING ARROW: " + remaining_arrow;
            }
            Image image = arrow.Arrow_Image;
            arrow = new Arrow();
            arrow.Arrow_Image = image;
            arrow.Image = arrow.Arrow_Image;
            arrow.Location = new Point(pictureBox2.Width, (pictureBox2.Location.Y + (pictureBox2.Height- arrow.Height) / 2));
            panel1.Controls.Add(arrow);
            arrows.Add(arrow);
            if (remaining_arrow == 0)
            {
                gameOver = true;
                Stop();
            }
        }
        private void Arrow_Move()
        {
            for (int i = arrows.Count - 1; i >= 0; i--)
            {
                var _arrow = arrows[i];
                if (_arrow.Arrow_Move(panel1.Width))
                {
                    arrows.Remove(_arrow);
                    panel1.Controls.Remove(_arrow);
                }
            }
        }
        private void Balloon_Creat()
        {
            Image image = balloon.Balloon_Image;
            balloon = new Balloon();
            balloon.Balloon_Image = image;
            balloon.BackgroundImage = image;
            balloon.BackgroundImageLayout = ImageLayout.Stretch;
            balloon.Width = 100;
            balloon.Height = 100;
            balloon.Location = new Point(random.Next(pictureBox2.Width, panel1.Width - balloon.Width), -50);
            balloons.Add(balloon);
            panel1.Controls.Add(balloon);
        }
        private void Yellow_Balloon_Creat()
        {
            Image image = yellow_balloon.Balloon_Image;
            yellow_balloon = new Yellow_Balloon();
            yellow_balloon.Balloon_Image = image;
            yellow_balloon.BackgroundImage = image;
            yellow_balloon.BackgroundImageLayout = ImageLayout.Stretch;
            yellow_balloon.Width = 100;
            yellow_balloon.Height = 100;
            yellow_balloon.Location = new Point(random.Next(pictureBox2.Width, panel1.Width - yellow_balloon.Width), -50);
            balloons.Add(yellow_balloon);
            panel1.Controls.Add(yellow_balloon);
        }
        private void Red_Balloon_Creat()
        {
            Image image = red_balloon.Balloon_Image;
            red_balloon = new Red_Balloon();
            red_balloon.Balloon_Image = image;
            red_balloon.BackgroundImage = image;
            red_balloon.BackgroundImageLayout = ImageLayout.Stretch;
            red_balloon.Width = 100;
            red_balloon.Height = 100;
            red_balloon.Location = new Point(random.Next(pictureBox2.Width, panel1.Width - red_balloon.Width), -50);
            balloons.Add(red_balloon);
            panel1.Controls.Add(red_balloon);
        }
        private void Ballon_Move()
        {
            for (int i = balloons.Count - 1; i >= 0; i--)
            {
                var _balloon = balloons[i];
                if (_balloon.Balloon_Move(panel1.Height))
                {
                    if (_balloon.GetType().Name == "Red_Balloon")
                    {
                        balloons.Remove(_balloon);
                        panel1.Controls.Remove(_balloon);
                        continue;
                    }
                    gameOver = true;
                    Stop();
                }
            }
        }
        private void Collision_Control()
        {
            for (int i = arrows.Count - 1; i >= 0; i--)
            {
                var _arrow = arrows[i];
                for (int j = balloons.Count - 1; j >= 0; j--)
                {
                    var _balloon = balloons[j];
                    if (_arrow.Right > _balloon.Left && _arrow.Left < _balloon.Right)
                    {
                        if (_arrow.Top < _balloon.Bottom && _arrow.Bottom > _balloon.Top)
                        {
                            balloon_size++;
                            balloons.Remove(_balloon);
                            panel1.Controls.Remove(_balloon);
                            if(_balloon.point != 0)
                                gamer.Score += _balloon.point;
                            else
                                gamer.Score = 0;
                            if (balloon_size == 20 && gamer.Level == 1)
                            {
                                gamer.Level = 2;
                                balloon_size = 0;
                            }
                            else if (balloon_size == 26 && gamer.Level == 2)
                            {
                                gamer.Level = 3;
                                balloon_size = 0;
                                label3.Visible = true;
                            }
                            else if (balloon_size == 28 && gamer.Level == 3)
                            {
                                gameOver = true;
                                Stop();
                                MessageBox.Show("You Win", "Congratulations");
                            }
                        }
                    }
                }
            }
        }
        private void File_Read()
        {
            string dosya_yolu;
            FileStream fs;
            StreamReader sw;
            try
            {
                dosya_yolu = "File.txt";
                fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
                sw = new StreamReader(fs);
                string[] yazi = sw.ReadLine().Split(' ');
                switch (yazi[0])
                {
                    case "0":
                        archer.Archer_Image = Image.FromFile(@"Resimler\Phoebe.png");
                        arrow.Arrow_Image = Image.FromFile(@"Resimler\cat.png");
                        break;
                    case "1":
                        archer.Archer_Image = Image.FromFile(@"Resimler\Joey.png");
                        arrow.Arrow_Image = Image.FromFile(@"Resimler\pizza.png");
                        break;
                    case "2":
                        archer.Archer_Image = Image.FromFile(@"Resimler\Chandler.png");
                        arrow.Arrow_Image = Image.FromFile(@"Resimler\carrot.png");
                        break;
                    default:
                        archer.Archer_Image = Image.FromFile(@"Resimler\Phoebe.png");
                        arrow.Arrow_Image = Image.FromFile(@"Resimler\cat.png");
                        break;
                }
                switch (yazi[1])
                {
                    case "0":
                        balloon.Balloon_Image = Image.FromFile(@"Resimler\Janes.png");
                        yellow_balloon.Balloon_Image = Image.FromFile(@"Resimler\JanesYellow.png");
                        red_balloon.Balloon_Image = Image.FromFile(@"Resimler\JanesRed.png");
                        break;
                    case "1":
                        balloon.Balloon_Image = Image.FromFile(@"Resimler\Gunther.png");
                        yellow_balloon.Balloon_Image = Image.FromFile(@"Resimler\GuntherYellow.png");
                        red_balloon.Balloon_Image = Image.FromFile(@"Resimler\GuntherRed.png");
                        break;
                    case "2":
                        balloon.Balloon_Image = Image.FromFile(@"Resimler\Richard.png");
                        yellow_balloon.Balloon_Image = Image.FromFile(@"Resimler\RichardYellow.png");
                        red_balloon.Balloon_Image = Image.FromFile(@"Resimler\RichardRed.png");
                        break;
                    default:
                        balloon.Balloon_Image = Image.FromFile(@"Resimler\Janes.png");
                        yellow_balloon.Balloon_Image = Image.FromFile(@"Resimler\JanesYellow.png");
                        red_balloon.Balloon_Image = Image.FromFile(@"Resimler\JanesRed.png");
                        break;
                }
                switch (yazi[2])
                {
                    case "0":
                        panel1.BackColor = Color.FromArgb(255, 192, 192);
                        break;
                    case "1":
                        panel1.BackColor = Color.RosyBrown;
                        break;
                    case "2":
                        panel1.BackColor = Color.PeachPuff;
                        break;
                    default:
                        panel1.BackColor = Color.FromArgb(255, 192, 192);
                        break;
                }
                sw.Close();
                fs.Close();
            }
            catch
            {
                archer.Archer_Image = Image.FromFile(@"Resimler\Phoebe.png");
                arrow.Arrow_Image = Image.FromFile(@"Resimler\cat.png");
                balloon.Balloon_Image = Image.FromFile(@"Resimler\Janes.png");
                yellow_balloon.Balloon_Image = Image.FromFile(@"Resimler\JanesYellow.png");
                red_balloon.Balloon_Image = Image.FromFile(@"Resimler\JanesRed.png");
                panel1.BackColor = Color.FromArgb(255, 192, 192);
            }
            
            try
            {
                dosya_yolu = "Game.txt";
                fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
                sw = new StreamReader(fs);
                string[] yazi = sw.ReadLine().Split(' ');
                gamer.Score = Int32.Parse(yazi[0]);
                gamer.Level = Int32.Parse(yazi[1]);
                sw.Close();
                fs.Close();

            }
            catch (Exception)
            {
                gamer.Score = 0;
                gamer.Level = 1;
            }
            

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            Start();
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            string file = "Game.txt";
            FileStream stream = File.Create(file);
            StreamWriter sw = new StreamWriter(stream);
            sw.WriteLine(gamer.Score + " " + gamer.Level);
            sw.Close();
            stream.Close();
            Application.Exit();
        }
        private void Arrow_Timer_Tick(object sender, EventArgs e)
        {
            Arrow_Move();
            Collision_Control();
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            label1.Text = "SCORE: " + gamer.Score;
            label2.Text = "LEVEL: " + gamer.Level;
        }
        private void Balloon_Creat_Timer_Tick(object sender, EventArgs e)
        {
            if(gamer.Level == 1)
                Balloon_Creat();
            else if(gamer.Level == 2)
            {
                tick++;
                if (tick == yellow_balloon_creat_size_level2)
                {
                    Yellow_Balloon_Creat();
                    tick = 0;
                }
                else
                    Balloon_Creat();
            }
            else if (gamer.Level == 3)
            {
                tick++;
                if (tick == yellow_balloon_creat_size)
                {
                    Yellow_Balloon_Creat();
                }
                else if(tick == red_balloon_creat_size)
                {
                    Red_Balloon_Creat();
                    tick = 0;
                }
                else
                    Balloon_Creat();
            }

            /*
            if (tick == yellow_balloon_creat_size)
            {
                Yellow_Balloon_Creat();
            }
            else if (tick == red_balloon_creat_size)
            {
                Red_Balloon_Creat();
                tick = 0;
            }
            else
                Balloon_Creat();
            tick++;*/
        }
        private void Balloon_Move_Timer_Tick(object sender, EventArgs e)
        {
            Ballon_Move();
        }
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameOver) return;
            switch (e.KeyCode)
            {
                case Keys.W:
                    Archer_Move('W');
                    break;
                case Keys.S:
                    Archer_Move('S');
                    break;
                case Keys.Space:
                    Arrow_Creat();
                    break;
                default:
                    break;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}