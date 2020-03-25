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
    public partial class UyeListelemefrm : Form
    {
        public UyeListelemefrm()
        {
            InitializeComponent();
        }

        private void Sil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from uye where tc=@tc", baglanti);     //sil tuşunda basıldığında
            komut.Parameters.AddWithValue("@tc", dataGridView1.CurrentRow.Cells["tc"].Value.ToString());
            komut.ExecuteNonQuery();          //işlemler okutuluyor
            baglanti.Close();
            MessageBox.Show("Silme İşlemi Gerçekleşti");
            daset.Tables["uye"].Clear();
            uyelistele();
            foreach (Control item in Controls)   //işlemler gerçekleştikten sonra temizlesin
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();         //tc olan sütunu, tc'nin olduğu textboxda göster.
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-66RF25L;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from uye where tc like '" + txtTc.Text + "'", baglanti);      //tc ye göre arama yapılıyor.
            SqlDataReader read = komut.ExecuteReader();    //textlerde kayıtların görünmesini sağlamak için durumlar eşitleniyor.
            while (read.Read())                            //kayıtlar okunduğu sürece
            {

                txtAdSoyad.Text = read["adsoyad"].ToString();
                txtYas.Text = read["yas"].ToString();
                comboCinsiyet.Text = read["cinsiyet"].ToString();
                txtTelefon.Text = read["telefon"].ToString();
                txtAdres.Text = read["adres"].ToString();
                txtEmail.Text = read["email"].ToString();
                txtOkunanSayi.Text = read["okukitapsayisi"].ToString();
            }
            baglanti.Close();
        }
        DataSet daset = new DataSet();          //kayıtları geçici olarak tutacağımız bir tablo
        private void txtAraTc_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["uye"].Clear();        //kayıtların üst üste binmemesi için önce temizletiyoruz.
            baglanti.Open();                    //bağlantı açılıyor
            SqlDataAdapter adtr = new SqlDataAdapter("select *from uye where tc like '%" + txtAraTc.Text + "%'", baglanti);     //içinde yazılan değerlerin olduğu kaydı gösterecek.
            adtr.Fill(daset, "uye");
            dataGridView1.DataSource = daset.Tables["uye"];
            baglanti.Close();                   //bağlantı kapatılıyor

        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();                       //iptal tuşuna basildiğinda kapatılıyor
        }
        private void uyelistele()
        {
            baglanti.Open();                    //bağlantı açılıyor
            SqlDataAdapter adtr = new SqlDataAdapter("select *from uye", baglanti);
            adtr.Fill(daset, "uye");
            dataGridView1.DataSource = daset.Tables["uye"];
            baglanti.Close();
        }
        private void UyeListelemefrm_Load(object sender, EventArgs e)
        {
            uyelistele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update uye set adsoyad=@adsoyad,yas=@yas,cinsiyet=@cinsiyet,telefon=@telefon,adres=@adres,email=@email,okukitapsayisi=@okukitapsayisi where tc=@tc",baglanti);
            komut.Parameters.AddWithValue("@tc", txtTc.Text);
            komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@yas", txtYas.Text);
            komut.Parameters.AddWithValue("@cinsiyet", comboCinsiyet.Text);
            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@adres", txtAdres.Text);
            komut.Parameters.AddWithValue("@email", txtEmail.Text);
            komut.Parameters.AddWithValue("@okukitapsayisi", int.Parse(txtOkunanSayi.Text));
            komut.ExecuteNonQuery();                  //işlem okutuluyor
            baglanti.Close();                         //baglanti kapatılıyor
            MessageBox.Show("Güncelleme İşlemi Gerçekleşti");     //ekrana mesaj yazdırılıyor
            daset.Tables["uye"].Clear();
            uyelistele();
            foreach (Control item in Controls)        //formdaki bütün kontrolleri tara
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }
    }
}
