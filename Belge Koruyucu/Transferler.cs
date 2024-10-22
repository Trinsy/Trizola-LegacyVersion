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
    public partial class Transferler : Form
    {
        public Transferler()
        {
            InitializeComponent();
            DataGridViewDataSettings(dataGridView1);
            dbyenile();
        }

        private void Transferler_Load(object sender, EventArgs e)
        {

        }

        SqlConnection connection = new SqlConnection(@"Data Source=.;Initial Catalog=Belge Koruyucu;Integrated Security=True");

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

        private void dbyenile()
        {
            SqlDataAdapter data = new SqlDataAdapter("Select ID,Tarih,Saat,[Müşteri ID],[Adı Soyadı],İnstagram,[Müşteri Numarası],Ülkesi,Nereden,Nereye,Şoför,[Şoför Numarası],Araba,Bütçe,Hakediş,Kalan From Transfer", connection);
            DataSet ds = new DataSet();
            connection.Open();
            data.Fill(ds, "Transfer");
            dataGridView1.DataSource = ds.Tables["Transfer"];
            connection.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();
        }

        private void KayıtSil(int id)
        {
            connection.Open();
            SqlCommand delete = new SqlCommand("DELETE From Transfer where ID=@id", connection);
            delete.Parameters.AddWithValue("@id", id);
            delete.ExecuteNonQuery();
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
            DateTime tarih = Convert.ToDateTime(dataGridView1.Rows[seçilialan].Cells[1].Value.ToString());
            string saat = dataGridView1.Rows[seçilialan].Cells[2].Value.ToString();
            string adsoyad = dataGridView1.Rows[seçilialan].Cells[3].Value.ToString();
            string şoför = dataGridView1.Rows[seçilialan].Cells[9].Value.ToString();
            string nereden = dataGridView1.Rows[seçilialan].Cells[7].Value.ToString();
            string nereye = dataGridView1.Rows[seçilialan].Cells[8].Value.ToString();
            if (saat.Trim() == "")
            {
                DialogResult msg = MessageBox.Show(tarih.ToShortDateString() + " Tarihindeki " + adsoyad + " Müşterisinin aldığı ve " + şoför + " adlı şoförünün yaptığı " + nereden + "'dan " + nereye + "'a transferini silmek istediğinize emin misiniz ?", "Belge Koruyucu - Transferler", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (msg == DialogResult.Yes)
                {
                    foreach (DataGridViewRow draw in dataGridView1.SelectedRows)
                    {
                        int id = Convert.ToInt32(draw.Cells[0].Value);
                        KayıtSil(id);
                    }
                    dbyenile();

                    MessageBox.Show("Transfer Başarılı Bir Şekilde Kaldırılmıştır.", "Belge Koruyucu - Transferler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                { return; }
            }
            else
            {
                DialogResult msg = MessageBox.Show(saat + "Saati" + tarih.ToShortDateString() + " Tarihindeki " + adsoyad + " Müşterisinin aldığı ve " + şoför + " adlı şoförünün yaptığı " + nereden + "'dan " + nereye + "'a transferini silmek istediğinize emin misiniz ?", "Belge Koruyucu - Transferler", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (msg == DialogResult.Yes)
                {
                    foreach (DataGridViewRow draw in dataGridView1.SelectedRows)
                    {
                        int id = Convert.ToInt32(draw.Cells[0].Value);
                        KayıtSil(id);
                    }
                    dbyenile();

                    MessageBox.Show("Transfer Başarılı Bir Şekilde Kaldırılmıştır.", "Belge Koruyucu - Transferler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                { return; }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panelMara.Visible = true;
            radioButton1.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panelMara.Visible = false;
            panel4.Visible = false;
            panel2.Visible=true;
            radioButton3.Checked = true;
            radioButton4.Checked = false;
            radioButton6.Checked = true;
            radioButton5.Checked = false;
            radioButton9.Checked = false;
            radioButton7.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = true;
            radioButton2.Checked =false;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select * from Transfer where [Adı Soyadı] like'" + textBox8.Text.Trim() + "%'", connection);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select * from Transfer where Şoför like'" + textBox1.Text.Trim() + "%'", connection);
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

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
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

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panelMara.Visible = false;
        }

        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select * from Transfer where [Müşteri Numarası] like'" + maskedTextBox2.Text.Trim() + "%'", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
            if (maskedTextBox2.TextLength >= 3)
            {
                button4.Visible = true;
            }
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select * from Transfer where [Şoför Numarası] like'" + maskedTextBox1.Text.Trim() + "%'", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
            if (maskedTextBox1.TextLength >= 3)
            {
                button5.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox8.Text = ""; maskedTextBox2.Text = "";
            dbyenile();
            button4.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = ""; maskedTextBox1.Text = "";
            dbyenile();
            button5.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Grubun tamamını seç kutucuğnu açtığınızda tablonuzda seçtiğiniz satırda bulunan bütün sütunları seçersiniz ve bilgileri kopyalamak istediğinizde hepsi birden gelir" +
                "\n\n\"Ara\" butonuna bastığınızda karşınıza 3 işlem çıkacaktır.\n\n\"Tarih\" kutucuğunu işaretlediğinizde seçtiğiniz tarihteki transferlerinizi görebilirsiniz." +
                "\n\n\"Müşteri\" kutucuğunu işaretlediğinizde size müşterilerinizin aldığı transferleri ve müşterinin bilgilerini girerek aldığı transferleri görebilirsiniz." +
                "\n\n\"Şoför\" kutucuğunu işaretlediğinizde size transfere yazılmış olan şoförün aldığı transferleri gösterir." +
                "\n\n\"X\" butonu ise yalnızca Tarih , Şoför ve Müşteri kutucuklarının bulunduğu paneli kapatır.","Belge Koruyucu - Yardım",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel4.Visible = true;
            radioButton7.Checked = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            button7.Visible = true;
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select * from Transfer where Tarih like'" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "%'", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
            button7.Visible = true;
            button5.Visible = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select * from Transfer where Araba like'" + textBox2.Text.Trim() + "%'",connection);
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

        private void button7_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            dbyenile();
            button7.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
            
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

        private void button8_Click_1(object sender, EventArgs e)
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
            int ID = Convert.ToInt32(dataGridView1.Rows[seçilialan].Cells[3].Value);
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
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
            DateTime tarih = Convert.ToDateTime(dataGridView1.Rows[seçilialan].Cells[1].Value.ToString());
            string saat = dataGridView1.Rows[seçilialan].Cells[2].Value.ToString();
            string adsoyad = dataGridView1.Rows[seçilialan].Cells[4].Value.ToString();
            string şoför = dataGridView1.Rows[seçilialan].Cells[10].Value.ToString();
            string nereden = dataGridView1.Rows[seçilialan].Cells[8].Value.ToString();
            string nereye = dataGridView1.Rows[seçilialan].Cells[9].Value.ToString();

            Clipboard.SetText("Saat: " + saat + "\nTarih: " + tarih + "\nAd Soyad: " + adsoyad + "\nŞoför: " + şoför + "\nNereden: " + nereden + "\nNereye: " + nereye);
        }
    }
}