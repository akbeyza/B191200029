using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
    public partial class AnaSayfafrm : Form
    {
        private Button currentBtn;
        private Panel LeftBorderBtn;
        private Form CurrentChildForm;
        public AnaSayfafrm()
        {
            CustomizeDesign();
            LeftBorderBtn = new Panel();
            LeftBorderBtn.Size = new Size(7, 60);
            //PanelMenu.Controls.Add(LeftBorderBtn);
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            InitializeComponent();
        }
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (Button)senderBtn;
                currentBtn.BackColor = Color.FromArgb(31, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;              
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Left Button
                LeftBorderBtn.BackColor = color;
                LeftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                LeftBorderBtn.Visible = true;
                LeftBorderBtn.BringToFront();
                //Icon Current Child Form
                
                

            }
        }
        private struct RGBColors
        {
            public static Color Color1 = Color.FromArgb(172, 126, 241);
            public static Color Color2 = Color.FromArgb(249, 118, 176);
            public static Color Color3 = Color.FromArgb(253, 138, 114);
            public static Color Color4 = Color.FromArgb(95, 77, 221);
            public static Color Color5 = Color.FromArgb(249, 88, 155);
            public static Color Color6 = Color.FromArgb(24, 161, 251);
        }
        private void OpenChildForm(Form childForm)
        {
            if (CurrentChildForm != null)
            {
                //open only form
                CurrentChildForm.Close();
            }
            CurrentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            PanelDesktop.Controls.Add(childForm);
            PanelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            LblTitleChildForm.Text = childForm.Text;
        }
        private void Reset()
        {
            DisableButton();
            LeftBorderBtn.Visible = false;
            
            LblTitleChildForm.Text = "Anasayfa";
            CustomizeDesign();
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void CustomizeDesign()
        {
            /*button2.Visible = false;
            button3.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button12.Visible = false;
            button13.Visible = false;*/
        }

        private void btnUyeEkle_Click(object sender, EventArgs e)
        {
            UyeEklefrm uyeekle = new UyeEklefrm();             //buttona basıldığında üye ekleme sayfasına gider
            uyeekle.ShowDialog();
        }

        private void btnUyeListele_Click(object sender, EventArgs e)
        {
            UyeListelemefrm uyeliste = new UyeListelemefrm();  //buttona basıldığına üye listeleme sayfasına gider
            uyeliste.ShowDialog();
        }

        private void btnKitapEkle_Click(object sender, EventArgs e)
        {
            KitapEklefrm kitapekle = new KitapEklefrm();      //buttona basıldığında kitap ekleme sayfasına gider
            kitapekle.ShowDialog();
        }

        private void AnaSayfafrm_Load(object sender, EventArgs e)
        {
            CustomizeDesign();
            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button12.Visible = false;
            button13.Visible = false;
        }

        private void btnKitapListele_Click(object sender, EventArgs e)
        {
            KitapListelefrm kitaplistele = new KitapListelefrm();    //buttona basıldığında kitap listeleme sayfasına gider
            kitaplistele.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            button5.Visible = false;
            button6.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button12.Visible = false;
            button13.Visible = false;
            button2.Visible = true;
            button3.Visible = true;
            ActivateButton(sender, RGBColors.Color1);
            LblTitleChildForm.Text = button1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            button5.Visible = false;
            button6.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button12.Visible = false;
            button13.Visible = false;
            button2.Visible = true;
            button3.Visible = true;
            OpenChildForm(new UyeEklefrm());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            button5.Visible = false;
            button6.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button12.Visible = false;
            button13.Visible = false;
            button2.Visible = true;
            button3.Visible = true;
            
            OpenChildForm(new UyeListelemefrm());
        }

        private void btnEmanetListele_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = true;
            button6.Visible = true;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button12.Visible = false;
            button13.Visible = false;
            
            OpenChildForm(new KitapEklefrm());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = true;
            button6.Visible = true;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button12.Visible = false;
            button13.Visible = false;
            
            OpenChildForm(new KitapListelefrm());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button8.Visible = true;
            button9.Visible = true;
            button10.Visible = true;
            button12.Visible = false;
            button13.Visible = false;
            
            OpenChildForm(new EmanetKitapVerfrm());
        }

        private void btnEmanetVer_Click(object sender, EventArgs e)
        {
            
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button12.Visible = false;
            button13.Visible = false;
            button5.Visible = true;
            button6.Visible = true;
            ActivateButton(sender, RGBColors.Color2);
            LblTitleChildForm.Text = button4.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            
            button12.Visible = false;
            button13.Visible = false;
            button8.Visible = true;
            button9.Visible = true;
            button10.Visible = true;
            ActivateButton(sender, RGBColors.Color3);
            LblTitleChildForm.Text = button7.Text;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            
            button12.Visible = true;
            button13.Visible = true;            
            ActivateButton(sender, RGBColors.Color4);
            LblTitleChildForm.Text = button11.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button8.Visible = true;
            button9.Visible = true;
            button10.Visible = true;
            button12.Visible = false;
            button13.Visible = false;
            OpenChildForm(new EmanetKitapİadelefrm());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button8.Visible = true;
            button9.Visible = true;
            button10.Visible = true;
            button12.Visible = false;
            button13.Visible = false;
            OpenChildForm(new EmanetKitapİadefrm());
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button12.Visible = true;
            button13.Visible = true;
            OpenChildForm(new Sıralamafrm());
        }

        private void button13_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button12.Visible = true;
            button13.Visible = true;
            OpenChildForm(new Grafikfrm());
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
