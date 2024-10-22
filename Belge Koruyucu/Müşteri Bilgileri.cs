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
    public partial class Müşteri_Bilgileri : Form
    {
        public Müşteri_Bilgileri()
        {
            InitializeComponent();
            dataGridView1.Size = new Size(982, 664);
            DataGridViewDataSettings(dataGridView1);
            dbyenile();
        }

        private void DataGridViewDataSettings(DataGridView datagridview)
        {
            datagridview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            datagridview.BackgroundColor = Color.FromArgb(32, 30, 45);
            datagridview.BorderStyle = BorderStyle.None;
            datagridview.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            datagridview.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            datagridview.RowHeadersVisible = false;
            datagridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datagridview.DefaultCellStyle.BackColor = Color.FromArgb(32, 30, 45);
            datagridview.DefaultCellStyle.ForeColor = Color.Gainsboro;
            datagridview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            datagridview.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 30, 45);
            datagridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        SqlConnection connection = new SqlConnection(@"Data Source=.;Initial Catalog=Belge Koruyucu;Integrated Security=True");

        private void dbyenile()
        {
            SqlDataAdapter data = new SqlDataAdapter("Select ID,[Adı Soyadı],İnstagram,Numara,Ülkesi,EMail From mbilgiler", connection);
            DataSet ds = new DataSet();
            connection.Open();
            data.Fill(ds, "mbilgiler");
            dataGridView1.DataSource = ds.Tables["mbilgiler"];
            connection.Close();
        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            groupBox1.Size = new Size(288, 194);
            button2.Enabled = true;
            button2.Visible = true;
            if (textBox1.Text == "")
            {
                dbyenile();
                groupBox1.Size = new Size(310, 176);
                button2.Enabled = false;
                button2.Visible = false;
            }
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select ID,[Adı Soyadı],İnstagram,Numara,Ülkesi,EMail from mbilgiler where [Adı Soyadı] like '" + textBox1.Text.Trim() +"%'", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            groupBox1.Size = new Size(288, 194);
            button2.Enabled = true;
            button2.Visible = true;
            if (textBox3.Text == "")
            {
                dbyenile();
                groupBox1.Size = new Size(310, 176);
                button2.Enabled = false;
                button2.Visible = false;
            }
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select ID,[Adı Soyadı],İnstagram,Numara,Ülkesi,EMail from mbilgiler where İnstagram like'" + textBox3.Text.Trim() + "%'", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                dataGridView1.Width -= 290;
                groupBox1.Enabled = true;
                groupBox1.Visible = true;
                groupBox2.Enabled = true;
                groupBox2.Visible = true;
                textBox1.Select();
            }
            else if (checkBox1.Checked == false)
            {
                dataGridView1.Width += 290;
                groupBox1.Enabled = false;
                groupBox1.Visible = false;
                groupBox2.Enabled = false;
                groupBox2.Visible = false;
            }
        }


        private void button11_Click(object sender, EventArgs e)
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex; //Kullanıcının seçtiği alan
            connection.Open();
            SqlCommand update = new SqlCommand("update mbilgiler set [Adı Soyadı]='" + textBox8.Text + "',İnstagram='" +textBox11.Text + "',Numara='" + maskedTextBox2.Text + "',Ülkesi='" + textBox5.Text + "',EMail='" + textBox6.Text +"'where ID='" + 
             dataGridView1.Rows[seçilialan].Cells[0].Value.ToString() + "'",connection);
            update.ExecuteNonQuery();
            connection.Close();
            dbyenile();

            textBox8.Text = "";
            textBox11.Text = "";
            maskedTextBox2.Text = "";
            textBox5.Text = "";
            textBox6.Text = ""; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Müşterinizi kaldırmak istiyorsanız, kaldırmak istediğiniz müşteriyi tablodan seçin ve \"Kişi Sil\" Butonuna basın." +

                "\n\nMüşterilerinizin bilgilerini düzenlemek istiyorsanız," +
                "\n\"Müşteri Düzenle\" kutucuğunu açtıktan sonra tablodan düzenlemek istediğiniz müşteriye tıklayın , Müşteri Düzenle karesinde bulunan kısımlara yeni bilgileri girin ve \"Güncelle\" butonuna tıklayın" +

                "\n\nMüşterilerinizi aramak istiyorsanız, \"Müşteri Ara\" kutucuğunu açıktıktan sonra açılan kareye müşterilerinizin Adı , Soyadı veya Telefon Numarasını yazarak ulaşabilirsiniz.",
                "Belge Koruyucu - Yardım",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex; //Kullanıcının seçtiği alan

            string adsoyad = dataGridView1.Rows[seçilialan].Cells[1].Value.ToString();
            string instagram = dataGridView1.Rows[seçilialan].Cells[2].Value.ToString();
            string numara = dataGridView1.Rows[seçilialan].Cells[3].Value.ToString();
            string ülkesi = dataGridView1.Rows[seçilialan].Cells[4].Value.ToString();
            string email = dataGridView1.Rows[seçilialan].Cells[5].Value.ToString();

            textBox8.Text = adsoyad;
            textBox11.Text = instagram;
            maskedTextBox2.Text = numara;
            textBox5.Text = ülkesi;
            textBox6.Text = email;
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            groupBox1.Size = new Size(288, 194);
            button2.Enabled = true;
            button2.Visible = true;
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select ID,[Adı Soyadı],İnstagram,Numara,Ülkesi,EMail from mbilgiler where Numara like'" + maskedTextBox1.Text.Trim() + "%'", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

            textBox8.MaxLength = 20;
            if (textBox8.TextLength == 20)
            {
                MessageBox.Show("Müşterinizin Ad kısmında 20 karakter bulunabilir !","Belge Koruyucu",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            textBox11.MaxLength = 50;
            if (textBox11.TextLength == 50)
            {
                MessageBox.Show("Müşterinizin İnstagram kullanıcı adı kısmında 50 karakter bulunabilir !", "Belge Koruyucu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {
            textBox5.MaxLength = 35;
            if (textBox5.TextLength == 35)
            {
                MessageBox.Show("Müşterinizin Ülke kısmında 35 karakter bulunabilir !", "Belge Koruyucu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox6.MaxLength = 50;
            if (textBox6.TextLength == 50)
            {
                MessageBox.Show("Müşterinizin E-Mail kısmında 50 karakter bulunabilir !", "Belge Koruyucu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            groupBox1.Size = new Size(288, 164);
            textBox1.Text = ""; textBox3.Text = ""; maskedTextBox1.Text = "";
            dbyenile();
            button2.Enabled = false;
            button2.Visible = false;
            textBox1.Select();
        }

        private void KayıtSil(int id)
        {
            connection.Open();
            SqlCommand delete = new SqlCommand("DELETE from mbilgiler where ID=@id", connection);
            delete.Parameters.AddWithValue("@id", id);
            delete.ExecuteNonQuery();
            connection.Close();
        }


        private void button12_Click_1(object sender, EventArgs e)
        {
            DialogResult soru = MessageBox.Show("Müşteriyi silmek istediğinize emin misiniz ?", "Belge Koruyucu - Müşteri Bilgileri", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (soru == DialogResult.Yes)
            {
                foreach (DataGridViewRow item in dataGridView1.SelectedRows)
                {
                    int id = Convert.ToInt32(item.Cells[0].Value);
                    KayıtSil(id);
                    if (id > 1)
                    {
                        MessageBox.Show("Müşteriler Başarılı Bir Şekilde Silinmiştir.", "Belge Koruyucu - Müşteri Bilgileri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (id == 1)
                    {
                        MessageBox.Show("Müşteri Başarılı Bir Şekilde Silinmiştir.", "Belge Koruyucu - Müşteri Bilgileri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                dbyenile();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
                int ID = Convert.ToInt32(dataGridView1.Rows[seçilialan].Cells[0].Value);
                Müşteri_Kartviziti mk = new Müşteri_Kartviziti();

                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from mbilgiler where ID='" + ID + "'", connection);
                SqlDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    mk.label8.Text = read["ID"].ToString();
                    mk.label2.Text = read["Adı Soyadı"].ToString();
                    mk.label7.Text = read["Adı Soyadı"].ToString();
                    mk.label3.Text = read["İnstagram"].ToString();
                    mk.label6.Text = read["Numara"].ToString();
                    mk.label5.Text = read["Ülkesi"].ToString();
                    mk.label4.Text = read["EMail"].ToString();
                }
                read.Close();
                connection.Close();
                mk.turlar();
                mk.transferler();
                mk.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen sadece 1 müşteri seçiniz..", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
