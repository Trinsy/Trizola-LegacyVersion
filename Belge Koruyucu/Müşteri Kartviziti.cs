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
using System.Data.Sql;
using System.Data.SqlClient;

namespace Belge_Koruyucu
{
    public partial class Müşteri_Kartviziti : Form
    {
        public Müşteri_Kartviziti()
        {
            InitializeComponent();
            DataGridViewDataSettings(dataGridView1);
            DataGridViewDataSettings(dataGridView2);
            button3.Width = 0;
            timerOpacity.Start();
            this.Size = new Size(900, 320);
            timerTransferTur.Start();
        }

        private void DataGridViewDataSettings(DataGridView datagridview)
        {
            datagridview.RightToLeft = RightToLeft.No;

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

            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=.;Initial Catalog=Belge Koruyucu;Integrated Security=True");
        
        public void turlar()
        {
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select ID,[Başlangıç Tarihi],[Bitiş Tarihi],[Başlangıç Saati],[Bitiş Saati],[Adı Soyadı],İnstagram,[Müşteri Numarası],Ülkesi,Şoför,[Şoför Numarası],Araba,Bütçe,Hakediş,Kalan from Turlar where [Müşteri ID] like'" + label8.Text + "'", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        public void transferler()
        {
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("select ID,Tarih,Saat,[Adı Soyadı],İnstagram,[Müşteri Numarası],Ülkesi,Nereden,Nereye,Şoför,[Şoför Numarası],Araba,Bütçe,Hakediş,Kalan from Transfer where [Müşteri ID]='" + label8.Text + "'", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView2.DataSource = dt;
            connection.Close();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd,int wMsg,int wParam,int ıParam);

        private void Mouse_Move()
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (mouse == true)
            { Mouse_Move(); }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private void timerOpacity_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.1;
            if (this.Opacity == 1)
            {
                timerOpacity.Stop();
            }
        }

        bool mouse = false;

        private void timerTransferTur_Tick(object sender, EventArgs e)
        {
            this.Height += 5;
            if (this.Height == 550)
            {
                tabControl1.Visible = true;
                timerTransferTur.Stop();
                timerKişiBilgileri.Start();
            }
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            if (mouse == true)
            {
                Mouse_Move();
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select [Kişi Bilgisi] from mbilgiler where ID='" + label8.Text + "'", connection);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                string UserB = read["Kişi Bilgisi"].ToString();
                if (UserB.Trim() == "")
                {
                    MessageBox.Show("[ Müşterinin Ekstra Bilgileri Bulunmuyor. ]", (label7.Text + " Müşterisinin Ekstra Bilgileri"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(UserB, (label7.Text + " Müşterinin Ekstra Bilgileri"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            connection.Close();
        }

        private void timerKişiBilgileri_Tick(object sender, EventArgs e)
        {
            button3.Width += 3;
            if (button3.Width == 105)
            {
                timerKişiBilgileri.Stop();
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                mouse = true;
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (TurlarPage.Focus())
            {
                button11.Enabled = true;
                button11.Visible = true;
                button4.Text = "Tur Kaldır";
            }
            else
            {
                button11.Enabled = false;
                button11.Visible = false;
                button4.Text = "Transfer Kaldır";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
                connection.Open();
                SqlCommand cmd = new SqlCommand("Select [Tur Güzergahı] from Turlar where ID='" + dataGridView1.Rows[seçilialan].Cells[0].Value + "'", connection);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    string turgüzergahı = read["Tur Güzergahı"].ToString();
                    string adsoyad = label7.Text;
                    MessageBox.Show(turgüzergahı, (adsoyad + " / Müşterisinin Tur Güzergahı"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                read.Close();
                connection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Müşterinin Turu Bulunmuyor.","Belge Koruyucu / Müşteri Kartviziti",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void TurKayıtSil(int id)
        {
            connection.Open();
            SqlCommand delete = new SqlCommand("DELETE From Turlar where ID=@id", connection);
            delete.Parameters.AddWithValue("@id", id);
            delete.ExecuteNonQuery();
            connection.Close();
        }

        private void TransferKayıtSil(int id)
        {
            connection.Open();
            SqlCommand delete = new SqlCommand("DELETE From Transfer where ID=@id", connection);
            delete.Parameters.AddWithValue("@id", id);
            delete.ExecuteNonQuery();
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (TransferPage.Focus())
            {
                //Transfer
                try
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
                                TransferKayıtSil(id);
                            }
                            transferler();

                            MessageBox.Show("Transfer Başarılı Bir Şekilde Kaldırılmıştır.", "Belge Koruyucu - Transferler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        { return; }
                    }
                    else
                    {
                        DialogResult msg = MessageBox.Show(saat + " Saati " + tarih.ToShortDateString() + " Tarihindeki " + adsoyad + " Müşterisinin aldığı ve " + şoför + " adlı şoförünün yaptığı " + nereden + "'dan " + nereye + "'a transferini silmek istediğinize emin misiniz ?", "Belge Koruyucu - Transferler", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                        if (msg == DialogResult.Yes)
                        {
                            connection.Open();
                            SqlCommand delete = new SqlCommand("DELETE From Transfer where ID='" + dataGridView1.Rows[seçilialan].Cells[0].Value.ToString() + "' ", connection);
                            delete.ExecuteNonQuery();
                            connection.Close();
                            transferler();

                            MessageBox.Show("Transfer Başarılı Bir Şekilde Kaldırılmıştır.", "Belge Koruyucu - Transferler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        else
                        { return; }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Müşterinin Transferi Bulunmamaktadır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (TurlarPage.Focus())
            {
                //Turlar
                try
                {
                    int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
                    DateTime tarih = Convert.ToDateTime(dataGridView1.Rows[seçilialan].Cells[1].Value.ToString());
                    DateTime btarih = Convert.ToDateTime(dataGridView1.Rows[seçilialan].Cells[2].Value.ToString());
                    string saat = dataGridView1.Rows[seçilialan].Cells[3].Value.ToString();
                    string bsaat = dataGridView1.Rows[seçilialan].Cells[4].Value.ToString();
                    string adsoyad = dataGridView1.Rows[seçilialan].Cells[6].Value.ToString();
                    string şoför = dataGridView1.Rows[seçilialan].Cells[10].Value.ToString();

                    DialogResult msg = MessageBox.Show(saat + " Saatinde başlayıp " + bsaat + " Saatinde biten ve " + tarih.ToShortDateString() + " Tarihindeki başlayıp " + btarih.ToShortDateString() + " Tarihinde biten. " + adsoyad + " Müşterisinin aldığı ve " + şoför + " Şoförünün yaptığı turu silmek istediğinize emin misiniz ?", "Belge Koruyucu - Turlar", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                    if (msg == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow item in dataGridView1.SelectedRows)
                        {
                            int id = Convert.ToInt32(item.Cells[0].Value);
                            TurKayıtSil(id);
                        }
                        turlar();

                        MessageBox.Show("Tur Başarılı Bir Şekilde Kaldırılmıştır.", "Belge Koruyucu - Turlar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                    { return; }
                }
                catch (Exception)
                {
                    MessageBox.Show("Müşterinin Turu Bulunmamaktadır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select [Kişi Bilgisi] from mbilgiler where ID='" + label8.Text + "'", connection);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                string UserB = read["Kişi Bilgisi"].ToString();
                if (UserB.Trim() == "")
                {
                    Clipboard.SetText("Müşteri ID: " + label8.Text + "\nAdı Soyadı: " + label7.Text + "\nİnstagram: " + label3.Text + "\nNumara: " + label6.Text + "\nÜlkesi: " + label5.Text + "\nE-Mail: " + label4.Text );
                }
                else
                {
                    Clipboard.SetText("Müşteri ID: " + label8.Text + "\nAdı Soyadı: " + label7.Text + "\nİnstagram: " + label3.Text + "\nNumara: " + label6.Text + "\nÜlkesi: " + label5.Text + "\nE-Mail: " + label4.Text + "\n\nMüşteri Bilgisi: " + UserB);
                }
                MessageBox.Show("Bilgiler Kopyalandı",(label7.Text + " Müşterisinin Tüm Bilgileri"),MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            connection.Close();
        }
    }
}
