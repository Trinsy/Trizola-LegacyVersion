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
    public partial class Müşteri_Ekleme : Form
    {
        public Müşteri_Ekleme()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=.;Initial Catalog=Belge Koruyucu;Integrated Security=True");

        private void dbyenile()
        {
            SqlDataAdapter data = new SqlDataAdapter("Select * From mbilgiler", connection);
            DataSet ds = new DataSet();
            connection.Open();
            data.Fill(ds, "mbilgiler");
            connection.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength < 2)
            {
                MessageBox.Show("Müşteri eklemeden önce müşterinizin \"Ad Soyad\" bölümünü doldurunuz","Belge Koruyucu - Müşteri Ekleme",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            if (textBox3.TextLength < 1 && maskedTextBox1.TextLength < 12 && textBox6.TextLength < 2 && (textBox5.TextLength < 6 && textBox5.TextLength > 30))
            {
                MessageBox.Show("Lütfen müşterinizin bilgilerinin en az birini doldurun..");
            }
            if (textBox6.Text.Length > 30)
                MessageBox.Show("Ülke bölümüne maksimum 30 karakter girebilirsiniz !", "Belge Koruyucu - Müşteri Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (textBox5.Text.Length > 30)
                MessageBox.Show("E-Mail bölümüne maksimum 30 karater girebilirsiniz !", "Belge Koruyucu - Müşteri Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO mbilgiler([Adı Soyadı],İnstagram,Numara,Ülkesi,EMail,[Kişi Bilgisi])values('" + textBox1.Text + "','" + textBox3.Text + "','" + maskedTextBox1.Text + "','" + textBox6.Text + "','" + textBox5.Text + "','" + richTextBox1.Text +"')", connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                dbyenile();
                textBox1.Text = ""; textBox3.Text = ""; maskedTextBox1.Text = ""; textBox5.Text = ""; textBox6.Text = "";
                MessageBox.Show("Kullanıcı Başarılı Bir Şekilde Eklendi !","Belge Koruyucu - Müşteri Ekleme",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.MaxLength = 200;
            if (richTextBox1.MaxLength == 200)
            {
                label2.Visible = true;
            }
            else
            {
                label2.Visible = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.MaxLength = 30;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.MaxLength = 30;
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox1_TextAlignChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.MaxLength = 30;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox6.MaxLength = 30;
        }
    }
}
