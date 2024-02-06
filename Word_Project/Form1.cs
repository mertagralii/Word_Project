using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Word_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\my_me\OneDrive\Masaüstü\dbSozluk.accdb");
        Random random = new Random();
        int süre = 90;
        int kelime = 0;
        void getir()
        {
            int sayi;
            sayi = random.Next(1, 2490);
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("SELECT * from sozluk where id =@P1", baglanti);
            komut.Parameters.AddWithValue("@P1", sayi);
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Txtİngilizce.Text = dr[1].ToString();
                LblCevap.Text = dr[2].ToString();
                LblCevap.Text =LblCevap.Text.ToLower();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            getir();
            timer1.Start();

        }

        private void TxtTurkce_TextChanged(object sender, EventArgs e)
        {
            if (TxtTurkce.Text == LblCevap.Text)
            {
                kelime++;
                LblKelime.Text = kelime.ToString();
                getir();
                TxtTurkce.Clear();
                MessageBox.Show("Doğru cevap", "Bilgi");

            }
            else
            {
                MessageBox.Show("Yanlış Cevap","Bilgi");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            süre--;
            LblSure.Text = süre.ToString();
            if (süre ==0)
            {
                TxtTurkce.Enabled = false;
                Txtİngilizce.Enabled = false;
                timer1.Stop();
            }
        }
    }
}
