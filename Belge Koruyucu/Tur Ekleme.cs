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
    public partial class Tur_Ekleme : Form
    {
        public Tur_Ekleme()
        {
            InitializeComponent();
            DataGridViewDataSettings(dataGridView1);
            dbyenile();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=.;Initial Catalog=Belge Koruyucu;Integrated Security=True");

        private void dbyenile()
        {
            connection.Open();
            SqlDataAdapter mdata = new SqlDataAdapter("Select [Adı Soyadı],İnstagram,Numara,Ülkesi,ID from mbilgiler", connection);
            DataSet ds = new DataSet();
            mdata.Fill(ds, "mbilgiler");
            dataGridView1.DataSource = ds.Tables["mbilgiler"];
            connection.Close();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter data = new SqlDataAdapter("select [Adı Soyadı],İnstagram,Numara,Ülkesi,ID from mbilgiler where [Adı Soyadı] like'" + textBox1.Text.Trim() + "%'", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text.Trim() == "" || maskedTextBox3.Text.Trim() == "" || maskedTextBox4.Text.Trim() == "" || maskedTextBox5.Text.Trim() == "")
            {
                MessageBox.Show("Lütfen Turunuzun başlangıç ve bitiş saatini girin", "Belge Koruyucu - Tur Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (textBox5.Text.Trim() == "")
            {
                MessageBox.Show("Lütfen Turu Yapan Şoförü Belirtiniz !","Belge Koruyucu - Tur Ekleme",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            if (textBox7.Text.Trim() == "" || textBox8.Text.Trim() == "" || textBox9.Text.Trim() == "")
            {
                MessageBox.Show("Lütfen Turunuzun Bütçe , Hakediş ve Kalan bilgilerini boş geçmeyiniz !", "Belge Koruyucu - Tur Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (richTextBox1.TextLength < 5)
            {
                MessageBox.Show("Lütfen Turunuzun Güzergahını Uzun Bir Metin Halinde Belirtiniz !", "Belge Koruyucu - Tur Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (maskedTextBox1.Text.Trim() != "" || maskedTextBox3.Text.Trim() != "" || maskedTextBox4.Text.Trim() != "" || maskedTextBox5.Text.Trim() != "")
                {
                    int saat = Convert.ToInt32(maskedTextBox1.Text);
                    int saniye = Convert.ToInt32(maskedTextBox3.Text);
                    int bsaat = Convert.ToInt32(maskedTextBox5.Text);
                    int bsaniye = Convert.ToInt32(maskedTextBox4.Text);
                    if ((saat > 23 || saniye > 59) || (bsaat > 23 || bsaniye > 59))
                    {
                        MessageBox.Show("Girdiğiniz Saat Dilimi Hatalı", "Belge Koruyucu - Tur Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        maskedTextBox1.Text = ""; maskedTextBox3.Text = "";
                    }
                    else if ((saat <= 23 || saniye <= 59) || (bsaat <= 23 || bsaniye <= 59))
                    {
                        if (maskedTextBox2.Text.Trim() == "(   )     ")
                        {
                            DialogResult msg = MessageBox.Show("Turu yapan şoförün telefon numarasını girmek istemediğinize emin misiniz ?", "Belge Koruyucu - Tur Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (msg == DialogResult.No)
                            {
                                return;
                            }
                        }
                        if (textBox6.Text.Trim() == "")
                        {
                            DialogResult msg = MessageBox.Show("Turun yapıldığı Aracı girmek istemediğinize emin misiniz ?", "Belge Koruyucu - Tur Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (msg == DialogResult.No)
                            {
                                return;
                            }
                        }

                        int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
                        string adsoyad = dataGridView1.Rows[seçilialan].Cells[0].Value.ToString();
                        string instagram = dataGridView1.Rows[seçilialan].Cells[1].Value.ToString();
                        string numarası = dataGridView1.Rows[seçilialan].Cells[2].Value.ToString();
                        string ülkesi = dataGridView1.Rows[seçilialan].Cells[3].Value.ToString();
                        int id = Convert.ToInt32(dataGridView1.Rows[seçilialan].Cells[4].Value);
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO Turlar([Müşteri ID],[Başlangıç Tarihi],[Bitiş Tarihi],[Başlangıç Saati],[Bitiş Saati],[Adı Soyadı],İnstagram,[Müşteri Numarası],Ülkesi,Şoför,[Şoför Numarası],Araba,Bütçe,Hakediş,Kalan,[Tur Güzergahı])values('" + id + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "','" +(maskedTextBox1.Text.ToString() + ":" + maskedTextBox3.Text.ToString()) + "','" + (maskedTextBox5.Text.ToString() + ":" +maskedTextBox4.Text.ToString()) + "','" + adsoyad + "','" + instagram + "','" + numarası + "','" + ülkesi + "','" +textBox5.Text + "','" + maskedTextBox2.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" +textBox9.Text + "','" + richTextBox1.Text  + "')", connection);
                           cmd.ExecuteNonQuery();
                        connection.Close();
                        dbyenile();
                        textBox1.Text = ""; textBox5.Text = ""; maskedTextBox2.Text = ""; textBox6.Text = ""; textBox7.Text = "";
                        textBox8.Text = ""; textBox9.Text = ""; richTextBox1.Text = "";

                        MessageBox.Show("Tur başarılı bir şekilde eklenmiştir !", "Belge Koruyucu - Tur Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
            string adsoyad = dataGridView1.Rows[seçilialan].Cells[0].Value.ToString();
            textBox1.Text = adsoyad;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime Tarih1 = Convert.ToDateTime(dateTimePicker1.Text);
            DateTime Tarih2 = Convert.ToDateTime(dateTimePicker2.Text);

            TimeSpan sonuc = Tarih2 - Tarih1;
            label15.Text = "Tur Günü: "+sonuc.TotalDays.ToString();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.MaxLength = 1000;
        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void maskedTextBox5_ModifiedChanged(object sender, EventArgs e)
        {
            
        }

        private void maskedTextBox3_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
