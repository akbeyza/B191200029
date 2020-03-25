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
    public partial class EmanetKitapİadelefrm : Form
    {
        public EmanetKitapİadelefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-66RF25L;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();
        private void EmanetKitapListelefrm_Load(object sender, EventArgs e)
        {
            EmanetListele();
            comboBox1.SelectedIndex = 0;                   //form açıldığı zaman combo box ın 1. nesnesi seçili olarak gelsin
        }

        private void EmanetListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from EmanetKitaplar", baglanti);        //form açıldığı zaman kayıtlar görünüyor
            adtr.Fill(daset, "EmanetKitaplar");                                //kayıtları geçici tabloya aktarıyoruz
            dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];         //veritabanındaki tabloyu yazıyoruz
            baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            daset.Tables["EmanetKitaplar"].Clear();
            if (comboBox1.SelectedIndex==0)
            {
                EmanetListele();
            }
            else if (comboBox1.SelectedIndex==1)                     //geciken kitaplar için
            {
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from EmanetKitaplar where'"+DateTime.Now.ToShortDateString()+"'>iadetarihi", baglanti);    //eğer bugünün tarihi iade tarihinden büyükse    
                adtr.Fill(daset, "EmanetKitaplar");            
                dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];         
                baglanti.Close();
            }
            else if (comboBox1.SelectedIndex == 2)                   //gecikmeyen kitaplar için
            {
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from EmanetKitaplar where'" + DateTime.Now.ToShortDateString() + "'<= iadetarihi", baglanti);    //eğer bugünün tarihi iade tarihinden küçük eşite    
                adtr.Fill(daset, "EmanetKitaplar");
                dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];
                baglanti.Close();
            }
        }
    }
}
