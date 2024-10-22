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
    public partial class Transfer_Ekleme : Form
    {
        public Transfer_Ekleme()
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
            connection.Open();
            SqlDataAdapter mdata = new SqlDataAdapter("Select [Adı Soyadı],Numara,İnstagram,Ülkesi,ID from mbilgiler", connection);
            DataSet ds = new DataSet();
            mdata.Fill(ds, "mbilgiler");
            dataGridView1.DataSource = ds.Tables["mbilgiler"];
            connection.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            
            if (textBox3.TextLength < 2 || textBox4.TextLength < 2)
            {
                MessageBox.Show("Lütfen Transferinizin \"Nereden , Nereye\" olduğunu belirtiniz !", "Belge Koruyucu - Transfer Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (textBox5.TextLength < 2)
            {
                MessageBox.Show("Lütfen Transferi yapan şoförü belirtiniz !","Belge Koruyucu - Transfer Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (textBox7.Text.Trim() == "" || textBox8.Text.Trim() == "" || textBox9.Text.Trim() == "")
            {
                MessageBox.Show("Lütfen Transferinizin Bütçe , Hakediş ve Kalan bilgilerini boş geçmeyiniz !", "Belge Koruyucu - Transfer Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (checkBox1.Checked == true)
                {
                    if (textBox6.Text.Trim() == "")
                    {
                        DialogResult msg = MessageBox.Show("Transferin yapıldığı Aracı girmek istemediğinize emin misiniz ?", "Belge Koruyucu - Transfer Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (msg == DialogResult.No)
                        {
                            return;
                        }
                    }
                    if (maskedTextBox1.Text.Trim() == "(   )     ")
                    {
                        DialogResult msg = MessageBox.Show("Transferi yapan şoförün telefon numarasını girmek istemediğinize emin misiniz ?", "Belge Koruyucu - Transfer Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (msg == DialogResult.No)
                        {
                            return;
                        }
                    }

                    int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
                    string adsoyad = dataGridView1.Rows[seçilialan].Cells[0].Value.ToString();
                    string mnumara = dataGridView1.Rows[seçilialan].Cells[1].Value.ToString();
                    string insta = dataGridView1.Rows[seçilialan].Cells[2].Value.ToString();
                    string ülkesi = dataGridView1.Rows[seçilialan].Cells[3].Value.ToString();
                    int id = Convert.ToInt32(dataGridView1.Rows[seçilialan].Cells[4].Value);
                    textBox1.Text = adsoyad;

                    connection.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Transfer([Müşteri ID],Tarih,Saat,[Adı Soyadı],İnstagram,[Müşteri Numarası],Ülkesi,Nereden,Nereye,Şoför,[Şoför Numarası],Araba,Bütçe,Hakediş,Kalan)values('"+ id + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + "" + "','" + adsoyad + "','" + insta + "','" + mnumara + "','" + ülkesi + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + maskedTextBox2.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "')", connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    dbyenile();
                    textBox1.Text = ""; textBox3.Text = ""; textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = ""; textBox7.Text = ""; textBox8.Text = ""; textBox9.Text = ""; maskedTextBox2.Text = "";
                }
                else if (checkBox1.Checked == false)
                {
                    if (textBox6.Text.Trim() == "")
                    {
                        DialogResult msg = MessageBox.Show("Transferin yapıldığı Aracı girmek istemediğinize emin misiniz ?", "Belge Koruyucu - Transfer Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (msg == DialogResult.No)
                        {
                            return;
                        }
                    }
                    if (maskedTextBox1.Text.Trim() == "(   )     ")
                    {
                        DialogResult msg = MessageBox.Show("Transferi yapan şoförün telefon numarasını girmek istemediğinize emin misiniz ?", "Belge Koruyucu - Transfer Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (msg == DialogResult.No)
                        {
                            return;
                        }
                    }
                    if (maskedTextBox1.Text.Trim() == "" || maskedTextBox1.Text.Trim() == "")
                    {
                        MessageBox.Show("Lütfen Transfer saatinizi girin eğer saat bilinmiyor veya yoksa \"Saat Bilimiyor\" kutucuğunu işaretleyin", "Belge Koruyucu - Transfer Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        int saat = Convert.ToInt32(maskedTextBox1.Text);
                        int saniye = Convert.ToInt32(maskedTextBox3.Text);
                        if (saat > 23 || saniye > 59)
                        {
                            MessageBox.Show("Girdiğiniz Saat Dilimi Hatalı");
                            maskedTextBox1.Text = ""; maskedTextBox3.Text = "";
                        }
                        else if (saat <= 23 || saniye <= 59)
                        {
                            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
                            string adsoyad = dataGridView1.Rows[seçilialan].Cells[0].Value.ToString();
                            string insta = dataGridView1.Rows[seçilialan].Cells[1].Value.ToString();
                            string mnumara = dataGridView1.Rows[seçilialan].Cells[2].Value.ToString();
                            string ülkesi = dataGridView1.Rows[seçilialan].Cells[3].Value.ToString();
                            int id = Convert.ToInt32(dataGridView1.Rows[seçilialan].Cells[4].Value);
                            textBox1.Text = adsoyad;

                            connection.Open();
                            SqlCommand cmd = new SqlCommand("INSERT INTO Transfer([Müşteri ID],Tarih,Saat,[Adı Soyadı],İnstagram,[Müşteri Numarası],Ülkesi,Nereden,Nereye,Şoför,[Şoför Numarası],Araba,Bütçe,Hakediş,Kalan)values('"+ id + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + (maskedTextBox1.Text + ":" + maskedTextBox3.Text) + "','" + adsoyad + "','" + insta + "','" + mnumara + "','" + ülkesi + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + maskedTextBox2.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "')", connection);
                            cmd.ExecuteNonQuery();
                            connection.Close();
                            dbyenile();
                            textBox1.Text = ""; textBox3.Text = ""; textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = ""; textBox7.Text = ""; textBox8.Text = ""; textBox9.Text = ""; maskedTextBox2.Text = "";

                            MessageBox.Show("Transfer başarılı bir şekilde eklenmiştir !", "Belge Koruyucu - Transfer Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void Transfer_Ekleme_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.MinDate = DateTime.UtcNow;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                maskedTextBox1.Enabled = false;
                maskedTextBox3.Enabled = false;
                maskedTextBox1.Text = "";
                maskedTextBox3.Text = "";
            }
            if (checkBox1.Checked == false)
            {
                maskedTextBox1.Enabled = true;
                maskedTextBox3.Enabled = true;
                maskedTextBox1.Text = "";
                maskedTextBox3.Text = "";
            }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select [Adı Soyadı],İnstagram,Numara,Ülkesi,ID from mbilgiler where [Adı Soyadı] like'" + textBox1.Text.Trim() + "%'", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
            string adsoyad = dataGridView1.Rows[seçilialan].Cells[0].Value.ToString();

            textBox1.Text = adsoyad;
        }
    }
}
