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

namespace Belge_Koruyucu
{
    public partial class Kullanıcı_Girişi : Form
    {
        public Kullanıcı_Girişi()
        {
            InitializeComponent();
            useryenile();
            button3.Location = new Point(494, 274);
            timer1.Start();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=.;Initial Catalog=Belge Koruyucu Users;Integrated Security=True");

        private void useryenile()
        {
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("Select * From userb", connection);
            DataSet ds = new DataSet();
            data.Fill(ds, "userb");
            connection.Close();
        }

        private void Kullanıcı_Girişi_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }




        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.1;
            if (this.Opacity == 1)
            {
                timer1.Stop();
;           }
        }

        private void linkLabel1_LinkClicked_2(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Şifremi_Unuttum s = new Şifremi_Unuttum();
            s.Show();
            this.Hide();
        }
        private void button11_Click_2(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("select * from userb where KAd='" + textBox1.Text + "'AND KSifre='" + textBox2.Text + "'", connection);
            SqlDataReader read = cmd.ExecuteReader();
            if(read.Read())
            {
                BelgeKoruyucu bk = new BelgeKoruyucu();
                bk.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
            }
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '\0';
            button3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            button3.Visible = false;
        }
    }
}
