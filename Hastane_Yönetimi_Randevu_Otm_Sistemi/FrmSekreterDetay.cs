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
using System.Net.Http.Headers;

namespace Hastane_Yönetimi_Randevu_Otm_Sistemi
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        public string tcNumara;
        SqlBaglantisi bgl=new SqlBaglantisi();
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tcNumara;

            //Ad Soyad

            SqlCommand komut = new SqlCommand("select SekreterAdSoyad from Tbl_Sekreter where SekreterTc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTc.Text);
            SqlDataReader dr1 = komut.ExecuteReader();
            while (dr1.Read())
            {
                lblAdSoyad.Text = dr1[0].ToString();

            }
            bgl.baglanti().Close();


            //BRANŞLARI DATAGRIDE AKTARMA

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Branslar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //DOKTORLARI DATAGRIDE AKTARMA
            DataTable dt2= new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select (DoktorAd+ ' ' +DoktorSoyad) as 'Doktorlar', DoktorBrans from Tbl_Doktorlar ", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Bransı Comboboxa aktarma

            SqlCommand komut2 = new SqlCommand("select BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();  
            while(dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0].ToString());
            }
            bgl.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutKaydet = new SqlCommand("insert into Tbl_Randevu (RandevuTarih, RandevuSaat, RandevuBrans, RandevuDoktor) values (@r1,@r2,@r3,@r4)", bgl.baglanti());
            //buradan girilen değerleri sqle kaydetmek için insert to komutu kullandık.
            komutKaydet.Parameters.AddWithValue("@r1", mskTarih.Text);
            komutKaydet.Parameters.AddWithValue("@r2", mskSaat.Text);
            komutKaydet.Parameters.AddWithValue("@r3", cmbBrans.Text);
            komutKaydet.Parameters.AddWithValue("@r4", cmbDoktor.Text);
            komutKaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevunuz Oluşturuldu.");
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbDoktor.Items.Clear();

            SqlCommand komutDoktor = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komutDoktor.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr=komutDoktor.ExecuteReader();
            while (dr.Read()) {
                cmbDoktor.Items.Add(dr[0] + " " + dr[1]);
            }
            bgl.baglanti().Close();
        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", rchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu.");

        }

        private void btnDoktorPnl_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli drp=new FrmDoktorPaneli();
            drp.Show();

        }

        private void btnBransPnl_Click(object sender, EventArgs e)
        {
            FrmBrans frb=new FrmBrans();
            frb.Show(); 
        }

        private void btnRandevuList_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frr=new FrmRandevuListesi();
            frr.Show();
        }

        private void btnDuyuru_Click(object sender, EventArgs e)
        {
            FrmDuyurular frd=new FrmDuyurular();
            frd.Show();
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
