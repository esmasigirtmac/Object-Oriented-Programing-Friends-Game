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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    class Balloon : PictureBox
    {
        public Image Balloon_Image;
        public int point { get; set; }

        public Balloon()
        {
            point = 5;
        }
        public bool Balloon_Move(int height)
        {
            if (Top + 2 * Width > height)
            {
                Top = height - (Width / 2);
                return true;
            }
            else
                Top += Width / 2;

            return false;
        }
    }
}
