using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Belge_Koruyucu
{
    public partial class TrinsyCa : Form
    {
        public TrinsyCa()
        {
            InitializeComponent();
            timer1.Start();
            timer2.Start();
        }

        bool formum = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
                this.Opacity += 0.025;
                if (this.Opacity == 1)
                {
                    timer1.Stop();
                }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            Kullanıcı_Girişi f = new Kullanıcı_Girişi();
            f.Show();
            this.Hide();
            formum = false;
            timer2.Stop();
        }

        private void TrinsyCa_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (formum == true)
            {
                e.Cancel = true;
            }
            if (formum == false)
            {
                e.Cancel = false;
            }
        }
    }
}
