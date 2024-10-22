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
    public partial class Create : Form
    {
        public Create()
        {
            InitializeComponent();
            comboBox1.Text = "Müşteri Bilgi";
            label1.Text = "Müşteri Bilgileri";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Müşteri Bilgi")
            {
                label1.Text = "Müşteri Bilgileri";
                BelgeKoruyucu.ActiveForm.Text = "Belge Koruyucu - Ekle / Müşteri Bilgi";
                openChildForm(new Müşteri_Ekleme());
            }
            if (comboBox1.SelectedItem == "Transfer")
            {
                label1.Text = "Transferler";
                BelgeKoruyucu.ActiveForm.Text = "Belge Koruyucu - Ekle / Transfer";
                openChildForm(new Transfer_Ekleme());
            }
            if (comboBox1.SelectedItem == "Tur")
            {
                label1.Text = "Turlar";
                BelgeKoruyucu.ActiveForm.Text = "Belge Koruyucu - Ekle / Tur";
                openChildForm(new Tur_Ekleme());
            }
        }

        
        private Form activeForm = null;
        private void openChildForm(Form Childform)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = Childform;
            Childform.TopLevel = false;
            Childform.FormBorderStyle = FormBorderStyle.None;
            Childform.Dock = DockStyle.Fill;
            panelChildFrom.Controls.Add(Childform);
            panelChildFrom.Tag = Childform;
            Childform.BringToFront();
            Childform.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            
        }
    }
}
