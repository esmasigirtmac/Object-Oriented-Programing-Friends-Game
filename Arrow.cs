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
    class Arrow : PictureBox
    {
        public Image Arrow_Image;

        public bool Arrow_Move(int width)
        {
            if (Left + Height > width)
            {
                Left = Width;
                return true;
            }
            else
            {
                Left += Width / 2;
                return false;
            }
        }
    }
}
