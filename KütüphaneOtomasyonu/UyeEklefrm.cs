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
    public partial class UyeEklefrm : Form
    {
        public UyeEklefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-66RF25L;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();    //bulunduğu sayfa kapatılıyor
        }

        private void btnUyeEkle_Click_1(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into uye(tc,adsoyad,yas,cinsiyet,telefon,adres,email,okukitapsayisi) values(@tc,@adsoyad,@yas,@cinsiyet,@telefon,@adres,@email,@okukitapsayisi)", baglanti);
            komut.Parameters.AddWithValue("@tc", txtTc. Text);
            komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad. Text);
            komut.Parameters.AddWithValue("@yas", txtYas. Text);
            komut.Parameters.AddWithValue("@cinsiyet", comboCinsiyet. Text);
            komut.Parameters.AddWithValue("@telefon", txtTelefon. Text);
            komut.Parameters.AddWithValue("@adres", txtAdres. Text);
            komut.Parameters.AddWithValue("@email", txtEmail. Text);
            komut.Parameters.AddWithValue("@okukitapsayisi", txtOkunanSayi. Text);
            komut.ExecuteNonQuery();                  //işlem okutuluyor
            baglanti.Close();                         //baglanti kapatılıyor
            MessageBox.Show("Üye Kaydı Yapıldı");     //ekrana mesaj yazdırılıyor
            foreach (Control item in Controls)        //formdaki bütün kontrolleri tara
            {
                if (item is TextBox)
                {
                    if(item!=txtOkunanSayi)           //Okunan kitap sayısı hariç diğer değerler siliniyor
                    {
                        item.Text = "";
                    }
                }
            }
        }

        private void UyeEklefrm_Load(object sender, EventArgs e)
        {

        }
    }
}
