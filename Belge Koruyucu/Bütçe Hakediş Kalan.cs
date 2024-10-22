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
    public partial class Gelir_Gider_Kalan : Form
    {
        public Gelir_Gider_Kalan()
        {
            InitializeComponent();
            comboBox1.Text = "Transferler";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            datagridler();
            transfer();
        }

        private void transfer()
        {
            dbtransfer();
            transferbütçe();
            transferhakediş();
            transferkalan();
        }

        private void datagridler()
        {
            DataGridViewDataSettings(dataGridView1);
            DataGridViewDataSettings(dataGridView2);
            DataGridViewDataSettings(dataGridView3);
            DataGridViewDataSettings(dataGridView4);
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

        private void dbtransfer()
        {
            SqlDataAdapter data = new SqlDataAdapter("Select ID,Tarih,Saat,[Adı Soyadı],Nereden,Nereye,Şoför,Bütçe,Hakediş,Kalan from Transfer", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void dbturlar()
        {
            SqlDataAdapter data = new SqlDataAdapter("Select ID,[Başlangıç Tarihi],[Bitiş Tarihi],[Başlangıç Saati],[Bitiş Saati],[Adı Soyadı],Şoför,Bütçe,Hakediş,Kalan from Turlar",connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void transferbütçe()
        {
            SqlDataAdapter data = new SqlDataAdapter("Select Sum(Bütçe) from Transfer", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void transferhakediş()
        {
            SqlDataAdapter data = new SqlDataAdapter("Select Sum(Hakediş) from Transfer",connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView3.DataSource = dt;
        }

        private void transferkalan()
        {
            SqlDataAdapter data = new SqlDataAdapter("Select Sum(Kalan) from Transfer",connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView4.DataSource = dt;
        }

        private void turlarbütçe()
        {
            SqlDataAdapter data = new SqlDataAdapter("Select Sum(Bütçe) from Turlar", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void turlarhakediş()
        {
            SqlDataAdapter data = new SqlDataAdapter("Select Sum(Hakediş) from Turlar", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView3.DataSource = dt;
        }

        private void turlarkalan()
        {
            SqlDataAdapter data = new SqlDataAdapter("Select Sum(Hakediş) from Turlar", connection);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dataGridView4.DataSource = dt;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void turlar()
        {
            dbturlar();
            turlarbütçe();
            turlarhakediş();
            turlarkalan();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Transferler")
            {
                dbtransfer();
                transferbütçe(); transferhakediş(); transferkalan();
                tabPage1.Text = "Transferler";
                button3.Text = "Transfer Detayları";
            }
            if (comboBox1.SelectedItem == "Turlar")
            {
                turlar();
                tabPage1.Text = "Turlar";
                button3.Text = "Tur Detayları";
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void labelGelir_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Müşteri Kartviziti - Hatalı!
            try
            {
                int seçilialan = dataGridView2.SelectedCells[0].RowIndex;
                int id = Convert.ToInt32(dataGridView2.Rows[seçilialan].Cells[0].Value);
                connection.Open();
                SqlCommand cmd = new SqlCommand("Select [Müşteri ID] from Transferler where ID='" + id + "'", connection);
                SqlDataReader read = cmd.ExecuteReader();
                Müşteri_Kartviziti mk = new Müşteri_Kartviziti();

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
