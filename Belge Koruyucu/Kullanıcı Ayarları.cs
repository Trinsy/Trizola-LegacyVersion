using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Belge_Koruyucu
{
    public partial class Kullanıcı_Ayarları : Form
    {
        public Kullanıcı_Ayarları()
        {
            InitializeComponent();
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

        private void button1_Click(object sender, EventArgs e)
        {
            ksifre.PasswordChar = '\0';
            button1.Visible = false;
            button2.Visible = true;
            button2.Location = new Point(915, 135);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ksifre.PasswordChar = '*';
            button2.Visible = false;
            button1.Visible = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;

            button4.Location = new Point(865, 135);
            button3.Visible = false;
            button4.Visible = true;
            kad.Enabled = true;
            ksifre.Enabled = true;
            ktelefon.Enabled = true;
            kad.BorderStyle = BorderStyle.FixedSingle;
            ksifre.BorderStyle = BorderStyle.FixedSingle;
            ktelefon.BorderStyle = BorderStyle.FixedSingle;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            DialogResult msg = MessageBox.Show("Bilgilerinizi değiştirmek istediğinize emin misiniz ?", "TrinsyCa - Belge Koruyucu", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (msg == DialogResult.Yes)
            {
                button3.Visible = true;
                button4.Visible = false;
                kad.Enabled = false;
                ksifre.Enabled = false;
                ktelefon.Enabled = false;
                kad.BorderStyle = BorderStyle.None;
                ksifre.BorderStyle = BorderStyle.None;
                ktelefon.BorderStyle = BorderStyle.None;
                connection.Open();
                SqlCommand cmd = new SqlCommand("update userb set KAd=" + kad.Text + "',KSifre='" + ksifre.Text + "',Telefon='" + ktelefon.Text + "'where İsim='" + kisim.Text + "'where Soyisim='" + ksoyisim.Text + "'", connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                button1.Enabled = true;
                button2.Enabled = true;
            }
        }
    }
}
