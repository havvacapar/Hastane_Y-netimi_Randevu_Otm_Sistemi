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


namespace Hastane_Yönetimi_Randevu_Otm_Sistemi
{
    public partial class FrmHastaKayıt : Form
    {
        public FrmHastaKayıt()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();  //SQl sınıfına eriştik.
        private void btnKayıtYap_Click(object sender, EventArgs e)
        {
            
            SqlCommand komut=new SqlCommand("insert into Tbl_Hastalar (HastaAd,HastaSoyad,HastaTc,HastaTelefon,HastaSifre,HastaCinsiyet) values (@p1, @p2, @p3,@p4,@p5,@p6)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTc.Text);
            komut.Parameters.AddWithValue("@p4", mskTel.Text);
            komut.Parameters.AddWithValue("@p5", txtSifre.Text);
            komut.Parameters.AddWithValue("@p6", cmbCinsiyet.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close(); //baglantıyı direkt kapatamıyoruz. sınıf içerisindeki metotu çağırıp kapatıyoruz.
            MessageBox.Show("Kaydınız Gerçekleşmiştir. Şifreniz: " + txtSifre.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //txtSifre'den sonra "bilgi" messagebox kutusu başlığı, messagebox butonu OK olsun, mesaagebox logosu "İ"nformation olsun


        }
    }
}
