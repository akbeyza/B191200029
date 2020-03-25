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

/****************************************************************************
** SAKARYA ÜNİVERSİTESİ
** BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
** BİLİŞİM SİSTEMLERİ MÜHENDİSLİĞİ BÖLÜMÜ
** NESNEYE DAYALI PROGRAMLAMA DERSİ
** 2019-2020 BAHAR DÖNEMİ
**
** ÖDEV NUMARASI...1.......:
** ÖĞRENCİ ADI..Beyza AK..........:
** ÖĞRENCİ NUMARASI..B191200029.....:
** DERSİN ALINDIĞI GRUP..1.:
****************************************************************************/ 

namespace KütüphaneOtomasyonu
{
    public partial class Sıralamafrm : Form
    {
        public Sıralamafrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-66RF25L;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();
        private void Sıralamafrm_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from Uye order by okukitapsayisi desc ", baglanti);        //okunulan kitap sayısına göre azalan sıralama yapılıyor
            adtr.Fill(daset, "Uye");                                //kayıtları geçici tabloya aktarıyoruz
            dataGridView1.DataSource = daset.Tables["Uye"];         //veritabanındaki tabloyu yazdırıyoruz
            baglanti.Close();
            label2.Text = "";
            label2.Text = daset.Tables["Uye"].Rows[0]["adsoyad"].ToString()+" = ";    //en çok kitap okuyan kişinin ismi yazılıyor
            label2.Text += daset.Tables["Uye"].Rows[0]["okukitapsayisi"].ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
