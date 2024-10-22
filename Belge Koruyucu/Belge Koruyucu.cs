using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml;

namespace Belge_Koruyucu
{
    public partial class BelgeKoruyucu : Form
    {
        public BelgeKoruyucu()
        {
            InitializeComponent();
            CustomizeDesign();
            subMenuHide();
            timer1.Start();
            döviz();
            timerReflesh.Start();
            openChildForm(new Müşteri_Bilgileri());
            panelBelgelerSubMenu.Height = 165;
            panelBelgelerSubMenu.Visible = true;
        }

        private void döviz()
        {
            string bugün = "https://tcmb.gov.tr/kurlar/today.xml";
            var xmldoc = new XmlDocument();
            xmldoc.Load(bugün);

            DateTime Tarih = Convert.ToDateTime(xmldoc.SelectSingleNode("//Tarih_Date").Attributes["Tarih"].Value);

            string USD = xmldoc.SelectSingleNode("Tarih_Date/Currency [@Kod='USD']/ForexSelling").InnerXml;
            string EUR = xmldoc.SelectSingleNode("Tarih_Date/Currency [@Kod='EUR']/ForexSelling").InnerXml;
            string GBP = xmldoc.SelectSingleNode("Tarih_Date/Currency [@Kod='GBP']/ForexSelling").InnerXml;


            label5.Text = string.Format("| {0} |", Tarih.ToShortDateString());
            label2.Text = "Dolar (USD) : " + USD;
            label3.Text = "Euro (EUR) : " + EUR;
            label6.Text = "Pound (GBP) : " + GBP;
        }

        private void CustomizeDesign()
        {
            panel2.Visible = false;
            panelBelgelerSubMenu.Visible = false;
            panelİşlemlerSubMenu.Visible = false;
        }

        private void subMenuHide()
        {
            panelBelgelerSubMenu.Height = 0;
            panelİşlemlerSubMenu.Height = 0;

            if (panelBelgelerSubMenu.Visible == true)
            {
                panelBelgelerSubMenu.Visible = false;
            }

            if (panelİşlemlerSubMenu.Visible == true)
            {
                panelİşlemlerSubMenu.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BelgeKoruyucu.ActiveForm.Text = "Belge Koruyucu - Turlar";
            openChildForm(new Turlar());
        }

        private void btnHesap_Click(object sender, EventArgs e)
        {
            subMenuHide();
            BelgeKoruyucu.ActiveForm.Text = "Belge Koruyucu - Hesap";
            openChildForm(new Kullanıcı_Ayarları());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            subMenuHide();
            
            panelBelgelerSubMenu.Visible = true;
            timerBelgelerSubMenu.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            subMenuHide();
            panelİşlemlerSubMenu.Visible = true;
            timerIslemlerSubMenu.Start();
        }

        public Form activeForm = null;
        public void openChildForm(Form Childform)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = Childform;
            Childform.TopLevel = false;
            Childform.FormBorderStyle = FormBorderStyle.None;
            Childform.Dock = DockStyle.Fill;
            panelChildFrom.Controls.Add(Childform);
            panelChildFrom.Tag = Childform;
            Childform.BringToFront();
            Childform.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button11.Enabled = false;
            button6.Enabled = false;
            button10.Enabled = false;
            panel2.Height = 0;
            panel2.Visible = true;
            timerÇıkış.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BelgeKoruyucu.ActiveForm.Text = "Belge Koruyucu - Müşteri Bilgileri";
            openChildForm(new Müşteri_Bilgileri());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BelgeKoruyucu.ActiveForm.Text = "Belge Koruyucu - Ekle";
            openChildForm(new Create());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            BelgeKoruyucu.ActiveForm.Text = "Belge Koruyucu - Transferler";
            openChildForm(new Transferler());
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.1;
        }
        private void button7_Click(object sender, EventArgs e)
        {
             Zamanlayıcı z = new Zamanlayıcı();
             z.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://mail.google.com/mail/u/0/#inbox?compose=CllgCJlLWncCvHtVkgZZSLNrjwSCVgXrgFgxCTQNNNtJcBcnVhsBcVBQCDZstzmVjTtbBxBwDlq");
        }

        private void timerBelgelerSubMenu_Tick_1(object sender, EventArgs e)
        {
            panelBelgelerSubMenu.Height += 5;
            if (panelBelgelerSubMenu.Height == 165)
            {
                timerBelgelerSubMenu.Stop();
            }
        }

        private void timerIslemlerSubMenu_Tick(object sender, EventArgs e)
        {
            panelİşlemlerSubMenu.Height += 2;
            if (panelİşlemlerSubMenu.Height == 69)
            {
                timerIslemlerSubMenu.Stop();
            }
        }

        /*private void timerBelgelerSubMenuGeriÇıkış_Tick(object sender, EventArgs e)
        {
            if (panelBelgelerSubMenu.Height == 138)
            {
                panelBelgelerSubMenu.Height -= 6;
                if (panelBelgelerSubMenu.Height == 0)
                {
                    timerBelgelerSubMenuGeriÇıkış.Stop();
                    panelBelgelerSubMenu.Visible = false;
                }
            }
        }*/

        /*private void timerIslemlerSubMenuGeriÇıkış_Tick(object sender, EventArgs e)
        {
            if (panelİşlemlerSubMenu.Height == 69)
            {
                panelİşlemlerSubMenu.Height -= 2;
                if (panelBelgelerSubMenu.Height == 0)
                {
                    timerIslemlerSubMenuGeriÇıkış.Stop();
                    panelİşlemlerSubMenu.Visible = false;
                }
            }
        }*/

        private void timerÇıkış_Tick(object sender, EventArgs e)
        {
            button10.Height -= 3;
            if (button10.Height == 0)
            {
                button10.Visible = false;
                panel2.Height += 3;
                if (panel2.Height == 78)
                {
                    timerÇıkış.Stop();
                    button11.Enabled = true;
                    button6.Enabled = true;
                }
            }
        }

        private void timerÇıkış2_Tick(object sender, EventArgs e)
        {
            panel2.Height -= 3;
            if (panel2.Height == 0)
            {
                panel2.Visible = false;
                button10.Visible = true;
                button10.Height += 3;
                if (button10.Height == 45)
                {
                    timerÇıkış2.Stop();
                    button10.Enabled = true;
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timerÇıkış2.Start();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (panel3.Visible == false)
            {
                panel3.Visible = true;
                button8.TextImageRelation = TextImageRelation.TextBeforeImage;
                button8.RightToLeft = RightToLeft.Yes;
                button8.TextAlign = ContentAlignment.MiddleCenter;

                döviz();
                timerReflesh.Stop();
                timerReflesh.Start();
            }
            else
            {
                panel3.Visible = false;
                timerReflesh.Stop();
                button8.TextImageRelation = TextImageRelation.ImageBeforeText;
                button8.RightToLeft = RightToLeft.No;
                button8.TextAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void timerReflesh_Tick(object sender, EventArgs e)
        {
            döviz();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            BelgeKoruyucu.ActiveForm.Text = "Belge Koruyucu - Gelir / Gider / Kalan";
            openChildForm(new Gelir_Gider_Kalan());
        }
    }
}
