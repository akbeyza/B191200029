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
    public partial class EmanetKitapVerfrm : Form
    {
        public EmanetKitapVerfrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-66RF25L;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet where barkodno='"+dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString()+"'",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme İşlemi Gerçekleşti","Silme İşlemi");
            daset.Tables["sepet"].Clear();          //silsin ve yeni listelemeyi yapsın      
            sepetlistele();
            lblKitapSayi.Text = "";                 //önce kitap sayısı temizleniyor
            kitapsayisi();                          //metod çağırılıyor
        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();                           //bulunduğu sayfadan çıksın
        }
        private void sepetlistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from sepet", baglanti);
            adtr.Fill(daset, "sepet");
            dataGridView1.DataSource = daset.Tables["sepet"];
            baglanti.Close();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into sepet(barkodno,kitapadi,yazari,yayinevi,sayfasayisi,kitapsayisi,teslimtarihi,iadetarihi) values(@barkodno,@kitapadi,@yazari,@yayinevi,@sayfasayisi,@kitapsayisi,@teslimtarihi,@iadetarihi)", baglanti);
            komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
            komut.Parameters.AddWithValue("@kitapadi", txtKitapAdi.Text);
            komut.Parameters.AddWithValue("@yazari", txtYazari.Text);
            komut.Parameters.AddWithValue("@yayinevi", txtYayinevi.Text);
            komut.Parameters.AddWithValue("@sayfasayisi", txtSayfaSayisi.Text);
            komut.Parameters.AddWithValue("@kitapsayisi", int.Parse(txtKitapSayisi.Text));    //sadece sayısal değerler girilmesi için int.parse
            komut.Parameters.AddWithValue("@teslimtarihi", dateTimePicker1.Text);
            komut.Parameters.AddWithValue("@iadetarihi", dateTimePicker2.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap(lar) sepete eklendi", "Ekleme İşlemi");        //ekrana mesaj veriliyor
            daset.Tables["sepet"].Clear();                                        //temizletme işlemi
            sepetlistele();                                                       //kayıtların görülmesi işlemi
            lblKitapSayi.Text = "";                                               //önce kitap sayısı temizleniyor
            kitapsayisi();                                                        //metod çağırılıyor

            foreach (Control item in grpKitapBilgi.Controls)
            {
                if (item is TextBox)
                {
                    if (item != txtKitapSayisi)
                    {
                        item.Text = "";
                    }
                }
            }

        }
        private void kitapsayisi()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select sum(kitapsayisi) from sepet",baglanti);      //form açıldığı zaman sepetteki kitap sayısının çağırılıyor
            lblKitapSayi.Text = komut.ExecuteScalar().ToString();
            baglanti.Close();
        }
        private void EmanetKitapVerfrm_Load(object sender, EventArgs e)
        {
            sepetlistele();
            kitapsayisi();
        }

        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from uye where tc like'" + txtTcAra.Text + "'", baglanti);   //tc ile arama yaparken üye tablosunu çağıracağız
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())                           //kayıtlar okunduğu sürece
            {
                txtAdSoyad.Text = read["adsoyad"].ToString();
                txtYas.Text = read["yas"].ToString();
                txtTelefon.Text = read["telefon"].ToString();
            }
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select sum(kitapsayisi) from emanetkitaplar where tc='"+txtTcAra.Text+"'",baglanti);       //kayıtlı kitap sayısı tc ara ile çağırılıyor
            lblKayitliKitapSayi.Text = komut2.ExecuteScalar().ToString();
            baglanti.Close();

            if (txtTcAra.Text == "")                      //işlem bittikten sonra yazılı bütün değerler siliniyor
            {
                foreach (Control item in grpUyeBilgi.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                       
                    }
                    
                }
                lblKayitliKitapSayi.Text = "";        //kayıtlı kitap sayısı da temizleniyor
            }
        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();                              //bağlantı açılıyor
            SqlCommand komut = new SqlCommand("select *from kitap where barkodno like '" + txtBarkodNo.Text + "'", baglanti);   //barkod no ile arama yaparken kitap tablosunu çağıracağız
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtKitapAdi.Text = read["kitapadi"].ToString();
                txtYazari.Text = read["yazari"].ToString();
                txtYayinevi.Text = read["yayinevi"].ToString();
                txtSayfaSayisi.Text = read["sayfasayisi"].ToString();                
            }
            baglanti.Close();
            if (txtBarkodNo.Text=="")
            {
                foreach (Control item in grpKitapBilgi.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != txtKitapSayisi)                    //kitap sayısı hariç temizleniyor
                        {
                            item.Text = "";
                        }
                    }
                }
            }
                
            }

        private void btnTeslimEt_Click(object sender, EventArgs e)
        {
            if (lblKitapSayi.Text!= "")                                              //sepetteki kitap sayısının boştan farklı bir değer alması gerekiyo
            {
                if (lblKayitliKitapSayi.Text=="" && int.Parse(lblKitapSayi.Text)<=3 || lblKayitliKitapSayi.Text!="" && int.Parse(lblKayitliKitapSayi.Text)+int.Parse(lblKitapSayi.Text)<=3)
                    //eğer kayıtlı kitap sayısı boşsa sepetteki kitap sayısı 3 den küçük olmalıdır, eğer kayıtlı kitap sayısı boş değilse kayıtlı kitap sayısı ve sepetteki kitap sayısının toplamı 3 den küçük olmalıdır.
                {
                    if (txtTcAra.Text!="" &&txtAdSoyad.Text!="" && txtYas.Text!="" && txtTelefon.Text!="")       //üye bilgileri boşsa
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count-1; i++)         //ekleyeceğimiz birden fazla satır olacağı için for döngüsü kurduk
                        {
                            baglanti.Open();
                            SqlCommand komut = new SqlCommand("insert into emanetkitaplar(tc,adsoyad,yas,telefon,barkodno,kitapadi,yazari,yayinevi,sayfasayisi,kitapsayisi,teslimtarihi,iadetarihi) values(@tc,@adsoyad,@yas,@telefon,@barkodno,@kitapadi,@yazari,@yayinevi,@sayfasayisi,@kitapsayisi,@teslimtarihi,@iadetarihi)", baglanti);
                            komut.Parameters.AddWithValue("@tc",txtTcAra.Text);
                            komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                            komut.Parameters.AddWithValue("@yas", txtYas.Text);
                            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                            komut.Parameters.AddWithValue("barkodno", dataGridView1.Rows[i].Cells["barkodno"].Value.ToString());
                            komut.Parameters.AddWithValue("kitapadi", dataGridView1.Rows[i].Cells["kitapadi"].Value.ToString());
                            komut.Parameters.AddWithValue("yazari", dataGridView1.Rows[i].Cells["yazari"].Value.ToString());
                            komut.Parameters.AddWithValue("yayinevi", dataGridView1.Rows[i].Cells["yayinevi"].Value.ToString());
                            komut.Parameters.AddWithValue("sayfasayisi", dataGridView1.Rows[i].Cells["sayfasayisi"].Value.ToString());
                            komut.Parameters.AddWithValue("kitapsayisi", int.Parse(dataGridView1.Rows[i].Cells["kitapsayisi"].Value.ToString()));
                            komut.Parameters.AddWithValue("teslimtarihi", dataGridView1.Rows[i].Cells["teslimtarihi"].Value.ToString());
                            komut.Parameters.AddWithValue("iadetarihi", dataGridView1.Rows[i].Cells["iadetarihi"].Value.ToString());
                            komut.ExecuteNonQuery();
                            SqlCommand komut2 = new SqlCommand("update uye set okukitapsayisi=okukitapsayisi+'"+int.Parse(dataGridView1.Rows[i].Cells["kitapsayisi"].Value.ToString())+"'where tc='"+txtTcAra.Text+"'",baglanti);          //okuduğu kitap sayısı değişiyor (tc ye göre)
                            komut2.ExecuteNonQuery();
                            SqlCommand komut3 = new SqlCommand("update kitap set stoksayisi=stoksayisi-'" + int.Parse(dataGridView1.Rows[i].Cells["kitapsayisi"].Value.ToString()) + "'where barkodno='" + dataGridView1.Rows[i].Cells["barkodno"].Value.ToString() + "'", baglanti);          //okuduğu kitap sayısı değişiyor (tc ye göre)
                            komut3.ExecuteNonQuery();
                            baglanti.Close();

                        }
                        baglanti.Open();
                        SqlCommand komut4 = new SqlCommand("delete from sepet",baglanti);     //sepetteki kitaplar temizleniyor
                        komut4.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Kitap(lar) emanet edildi");
                        daset.Tables["sepet"].Clear();
                        sepetlistele();
                        txtTcAra.Text = "";
                        lblKitapSayi.Text = "";                                 //önce kitap sayısı temizleniyor
                        kitapsayisi();                                          //metod çağırılıyor
                        lblKayitliKitapSayi.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Önce üye ismi seçmeniz gerekir!","Uyarı");     //eğer üye bilgileri boşsa ekrana uyarı mesajı yazdırır
                    }
                }
                else
                {
                    MessageBox.Show("Emanet kitap sayısı 3 den fazla olamaz!","Uyarı");
                }             
            }

            else
            {
                MessageBox.Show("Önce sepete kitap eklenmelidir!","Uyarı");      //label eğer boşsa ekrana mesaj yazdırılıyor
            }
            
            
            
        }
    }
    }
