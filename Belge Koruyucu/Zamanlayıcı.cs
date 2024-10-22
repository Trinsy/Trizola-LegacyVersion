using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Belge_Koruyucu
{
    public partial class Zamanlayıcı : Form
    {
        public Zamanlayıcı()
        {
            InitializeComponent();
            opaklık.Start();
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            KronometreStarter();
        }

        private void opaklık_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.1;
            if (this.Opacity == 1)
            {
                opaklık.Stop();
            }
        }

        private void Zamanlayıcı_FormClosing(object sender, FormClosingEventArgs e) { }


        private void KronometreStarter()
        {
            button4.Enabled = false;
            button4.Visible = false;
            button3.Enabled = false;
            button3.Visible = false;
            button5.Location = new Point(100, 184);
            button4.Location = new Point(100, 162);
            button3.Location = new Point(100, 206);
        }

        #region Kronometre
        int saat = 0;
        int dakika = 0;
        int saniye = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            saniye++;

            label1.Text = "0" + saniye.ToString();
            if (saniye >= 10)
            {
                label1.Text = saniye.ToString();
            }
            if (saniye >= 60)
            {
                label1.Text = "00";
                saniye = 0;
            }
            if (saat == 99 && dakika == 59 && saniye == 59)
            {
                timer1.Stop(); timer2.Stop(); timer3.Stop();
                DialogResult dia = MessageBox.Show("Zamanlayıcınız maksimum değer olan 100 saate ulaştı.\n\"Tamam\" a tıkladığınızda kronometre sıfırlanacaktır..", "Zamanlayıcı - Kronometre", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                if (dia == DialogResult.OK)
                {
                    label1.Text = "00";
                    label2.Text = "00";
                    label5.Text = "00";
                    saat = 0;
                    dakika = 0;
                    saniye = 0;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            button5.Visible = false;
            button4.Visible = true;
            button4.Enabled = true;
            button3.Enabled = true;
            button3.Visible = true;
            button4.Location = new Point(100, 162);
            timer1.Start(); timer2.Start(); timer3.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            dakika++;
            label2.Text = "0" + dakika.ToString();
            if (dakika >= 10)
            {
                label2.Text = dakika.ToString();
            }
            if (dakika >= 60)
            {
                label2.Text = "00";
                dakika = 0;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            saat++;
            label5.Text = "0" + saat.ToString();
            if (saat >= 10)
            {
                label5.Text = saat.ToString();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            button5.Enabled = true;
            button5.Visible = true;
            button4.Enabled = false;
            button4.Visible = false;
            button3.Enabled = true;
            button3.Visible = true;
            button5.Text = "Devam Et";
            button5.Location = new Point(100, 162);
            timer1.Stop(); timer2.Stop(); timer3.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button5.Enabled = true;
            button5.Visible = true;
            button4.Enabled = false;
            button4.Visible = false;
            button3.Enabled = false;
            button3.Visible = false;
            button5.Text = "Başla";
            button5.Location = new Point(100, 184);
            label1.Text = "00";
            label2.Text = "00";
            label5.Text = "00";
            timer1.Stop(); timer2.Stop(); timer3.Stop();
            saniye = 0;
            dakika = 0;
            saat = 0;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int ıParam);

        private void Mouse_Move()
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Mouse_Move();
        }

        private void label6_MouseDown(object sender, MouseEventArgs e)
        {
            Mouse_Move();
        }
    }
}