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
using System.Net;
using System.Net.Mail;

namespace Belge_Koruyucu
{
    public partial class Şifremi_Unuttum : Form
    {
        public Şifremi_Unuttum()
        {
            InitializeComponent();
            useryenile();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=.;Initial Catalog=Belge Koruyucu Users;Integrated Security=True");

        private void useryenile()
        {
            connection.Open();
            SqlDataAdapter data = new SqlDataAdapter("Select KAd,KSifre From userb", connection);
            DataSet ds = new DataSet();
            data.Fill(ds, "userb");
            connection.Close();
        }
        
        private void button11_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select * from userb where EMail='" + textBox1.Text.Trim() + "'", connection);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    SmtpClient smtpserver = new SmtpClient();
                    MailMessage mail = new MailMessage();
                    string tarih = DateTime.Now.ToLongDateString();
                    string mailadresin = ("trinsyca@gmail.com");
                    #region ***SIFRE***
                    string sifre = ("xo_zIsx%5W%$1406//");
                    #endregion
                    string smtpsrvr = "smtp.gmail.com";
                    string kime = (read["EMail"].ToString());
                    string konu = ("Şifre Hatırlatma - TrinsyCa Belge Koruyucu");
                    string yaz = ("\nKullanıcı Adı: "+ read["KAd"].ToString() +"\nParolanız: " + read["KSifre"].ToString() + "\n\nSayın, " + read["İsim"].ToString() + " " + read["Soyisim"].ToString() + "\nBizden " + tarih + " Tarihinde Şifre Hatırlatma Talebinde Bulundunuz\n\nTrinsyCa - Belge Koruyucu");
                    smtpserver.Credentials = new NetworkCredential(mailadresin, sifre);
                    smtpserver.Port = 587; //Gmail port numarası
                    smtpserver.Host = smtpsrvr;
                    smtpserver.EnableSsl = true;
                    mail.From = new MailAddress(mailadresin);
                    mail.To.Add(kime);
                    mail.Subject = konu;
                    mail.Body = yaz;
                    smtpserver.Send(mail);
                    MessageBox.Show("Şifrenizi Mail Adresinizden Kontrol Edebilirsiniz..","trinsyca@gmail.com",MessageBoxButtons.OK);
                    this.Hide();
                    kg.Show();
                }
                catch (Exception)
                {
                    MessageBox.Show("Böyle Bir Mail Adresi Bulunmuyor!","TrinsyCa",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            connection.Close();
        }
        Kullanıcı_Girişi kg = new Kullanıcı_Girişi();
        private void button1_Click(object sender, EventArgs e)
        {
            kg.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Şifremi_Unuttum_FormClosing(object sender, FormClosingEventArgs e)
        {
            kg.Show();
        }
    }
}
