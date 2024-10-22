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
    public partial class Turlar : Form
    {
        public Turlar()
        {
            InitializeComponent();
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
            SqlDataAdapter data = new SqlDataAdapter("Select ID,[Başlangıç Tarihi],[Bitiş Tarihi],[Başlangıç Saati],[Bitiş Saati],[Müşteri ID],[Adı Soyadı],İnstagram,[Müşteri Numarası],Ülkesi,Şoför,[Şoför Numarası],Araba,Bütçe,Hakediş,Kalan From Turlar", connection);
            DataSet ds = new DataSet();
            connection.Open();
            data.Fill(ds , "Turlar");
            dataGridView1.DataSource = ds.Tables["Turlar"];
            connection.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select [Tur Güzergahı] from Turlar where ID ='" + dataGridView1.Rows[seçilialan].Cells[0].Value.ToString()+"'",connection);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                string turgüzergahı = read["Tur Güzergahı"].ToString();
                MessageBox.Show(turgüzergahı, ("Belge Koruyucu - Turlar / Müşteri: " + dataGridView1.Rows[seçilialan].Cells[5].Value.ToString() + " Tur Güzergahı"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            read.Close();
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();
        }

        private void KayıtSil(int id)
        {
            connection.Open();
            SqlCommand delete = new SqlCommand("DELETE From Turlar where ID=@id", connection);
            delete.Parameters.AddWithValue("@id", id);
            delete.ExecuteNonQuery();
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
            DateTime tarih = Convert.ToDateTime(dataGridView1.Rows[seçilialan].Cells[1].Value.ToString());
            DateTime btarih = Convert.ToDateTime(dataGridView1.Rows[seçilialan].Cells[2].Value.ToString());
            string saat = dataGridView1.Rows[seçilialan].Cells[3].Value.ToString();
            string bsaat = dataGridView1.Rows[seçilialan].Cells[4].Value.ToString();
            string adsoyad = dataGridView1.Rows[seçilialan].Cells[6].Value.ToString();
            string şoför = dataGridView1.Rows[seçilialan].Cells[10].Value.ToString();

            DialogResult msg = MessageBox.Show(saat + " Saatinde başlayıp " + bsaat + " Saatinde biten ve " + tarih.ToShortDateString() + " Tarihindeki başlayıp "+ btarih.ToShortDateString() + " Tarihinde biten. " + adsoyad + " Müşterisinin aldığı ve " + şoför + " Şoförünün yaptığı turu silmek istediğinize emin misiniz ?", "Belge Koruyucu - Turlar", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

            if (msg == DialogResult.Yes)
            {
                foreach (DataGridViewRow item in dataGridView1.SelectedRows)
                {
                    int id = Convert.ToInt32(item.Cells[0].Value);
                    KayıtSil(id);
                }
                dbyenile();

                MessageBox.Show("Tur Başarılı Bir Şekilde Kaldırılmıştır.", "Belge Koruyucu - Turlar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            { return; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panelMara.Visible = false;
            panel4.Visible = false;
            panel2.Visible = true;
            radioButton3.Checked = true;
            radioButton4.Checked = false;
            radioButton6.Checked = true;
            radioButton5.Checked = false;
            radioButton9.Checked = false;
            radioButton7.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel4.Visible = true;
            radioButton7.Checked = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panelMara.Visible = true;
            radioButton1.Checked = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = true;
            radioButton2.Checked = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                textBox8.Enabled = true;
            }
            else
            {
                textBox8.Enabled = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                maskedTextBox2.Enabled = true;
            }
            else
            {
                maskedTextBox2.Enabled = false;
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select ID,[Başlangıç Tarihi],[Bitiş Tarihi],[Başlangıç Saati],[Bitiş Saati],[Adı Soyadı],İnstagram,[Müşteri Numarası],Ülkesi,Şoför,[Şoför Numarası],Araba,Bütçe,Hakediş,Kalan from Turlar where [Adı Soyadı] like'" + textBox8.Text.Trim() + "%'", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
            button4.Visible = true;
            if (textBox8.TextLength == 0)
            {
                button4.Visible = false;
            }
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select ID,[Başlangıç Tarihi],[Bitiş Tarihi],[Başlangıç Saati],[Bitiş Saati],[Adı Soyadı],İnstagram,[Müşteri Numarası],Ülkesi,Şoför,[Şoför Numarası],Araba,Bütçe,Hakediş,Kalan from Turlar where [Müşteri Numarası] like'" + maskedTextBox2.Text.Trim() + "%'", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
            if (maskedTextBox2.TextLength >= 3)
            {
                button4.Visible = true;
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked == true)
            {
                textBox1.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true)
            {
                maskedTextBox1.Enabled = true;
            }
            else
            {
                maskedTextBox1.Enabled = false;
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked == true)
            {
                textBox2.Enabled = true;
            }
            else
            {
                textBox2.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select ID,[Başlangıç Tarihi],[Bitiş Tarihi],[Başlangıç Saati],[Bitiş Saati],[Adı Soyadı],İnstagram,[Müşteri Numarası],Ülkesi,Şoför,[Şoför Numarası],Araba,Bütçe,Hakediş,Kalan from Turlar where Şoför like'" + textBox1.Text.Trim() + "%'", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
            button5.Visible = true;
            if (textBox1.TextLength == 0)
            {
                button5.Visible = false;
            }
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select ID,[Başlangıç Tarihi],[Bitiş Tarihi],[Başlangıç Saati],[Bitiş Saati],[Adı Soyadı],İnstagram,[Müşteri Numarası],Ülkesi,Şoför,[Şoför Numarası],Araba,Bütçe,Hakediş,Kalan from Turlar where [Şoför Numarası] like'" + maskedTextBox1.Text.Trim() + "%'", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
            if (maskedTextBox1.TextLength >= 3)
            {
                button5.Visible = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select ID,[Başlangıç Tarihi],[Bitiş Tarihi],[Başlangıç Saati],[Bitiş Saati],[Adı Soyadı],İnstagram,[Müşteri Numarası],Ülkesi,Şoför,[Şoför Numarası],Araba,Bütçe,Hakediş,Kalan from Turlar where Araba like'" + textBox2.Text.Trim() + "%'", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
            button5.Visible = true;
            if (textBox2.TextLength == 0)
            {
                button5.Visible = false;
            }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panelMara.Visible = false;

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton7.Checked = false;
        }
    
        private void button4_Click(object sender, EventArgs e)
        {
            textBox8.Text = ""; maskedTextBox2.Text = "";
            dbyenile();
            button4.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = ""; maskedTextBox1.Text = ""; textBox2.Text = "";
            dbyenile();
            button5.Visible = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            button7.Visible = true;
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select ID,[Başlangıç Tarihi],[Bitiş Tarihi],[Başlangıç Saati],[Bitiş Saati],[Adı Soyadı],İnstagram,[Müşteri Numarası],Ülkesi,Şoför,[Şoför Numarası],Araba,Bütçe,Hakediş,Kalan from Turlar where [Başlangıç Tarihi] like'" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "%'", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            dbyenile();
            button7.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
             int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
             int ID = Convert.ToInt32(dataGridView1.Rows[seçilialan].Cells[5].Value);
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

        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panelMara.Visible = false;

            radioButton4.Checked = false;
            radioButton3.Checked = false;

            textBox8.Text = ""; maskedTextBox2.Text = "";
            dbyenile();
            button4.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panelMara.Visible = false;

            radioButton6.Checked = false;
            radioButton5.Checked = false;
            radioButton9.Checked = false;

            textBox1.Text = ""; maskedTextBox1.Text = ""; textBox2.Text = "";
            dbyenile();
            button5.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panelMara.Visible = false;
        }
    }
}
