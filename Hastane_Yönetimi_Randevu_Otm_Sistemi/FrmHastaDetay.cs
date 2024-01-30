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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        public string tc;
        
        SqlBaglantisi bgl=new SqlBaglantisi();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;

            //HASTA DETAYA GİRİŞ YAPAN KİŞİNİN ADI-SOYAD ÇEKME
            SqlCommand komut= new SqlCommand("select HastaAd, HastaSoyad from Tbl_Hastalar where HastaTc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTc.Text);
            SqlDataReader dr=komut.ExecuteReader();
            while (dr.Read()) 
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];   
            }
            bgl.baglanti().Close();

            //1-Using system.data.sqlclient ekledik.
            //2-sqlcommanddan nesne türettik, Tbl_Hastalar tablosundan HastaAd, HastaSoyadı seçtik.
            //3-Bu nesnede tcsi lbltc'ye eşit olan hasta ad soyadı lblAdSoyada yazdırdık.
            
            //DATAGRIDE RANDEVU GEÇMİŞİ ÇEKME
            DataTable dt = new DataTable(); //sana veri tablosu oluşturduk
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevu where HastaTc=" + tc, bgl.baglanti());
            //SqlDataAdapter: datagride veri çekme komutu
            //tcden gelen değeri randevular kısmındaki HastaTc ile eşleştirip randevular tablosunu datagride yazdırdık.
            da.Fill(dt); //datagridi sanal veri tablosundan(dt) gelen değer ile doldur.
            dataGridView1.DataSource = dt; //sanal veri tablosunu datagride aktardık.


            //BRANŞ ÇEKME
            SqlCommand komut2 = new SqlCommand("select BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear(); // cmbBransa her tıklandığında cmbDoktorlara doktor ilavesi olmaması için yazdık.
            SqlCommand komut3 = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text); //cmbBranstan gelen değeri @p1e atadık, üstte p1e gitti
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }


        //AKTİF RANDEVU ÇEKME
        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevu where RandevuBrans='"+cmbBrans.Text +"'" , bgl.baglanti());
            //RandevuBranstan sonraki ' Str ifade yazabiilmek için.
            //RandevuBrans alanının, cmbBrans.Text ile seçilen değere eşit olması gerektiğini belirtiyor.
            da.Fill(dt);
            dataGridView2.DataSource = dt;

        }

        private void BilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle frb=new FrmBilgiDuzenle();
            frb.tcno=lblTc.Text;
            frb.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();


        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Randevu set RandevuDurum=1,HastaTc=@p1,HastaSikayet=@p2 where RandevuId=@p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTc.Text);
            komut.Parameters.AddWithValue("@p2", rchSikayet.Text);
            komut.Parameters.AddWithValue("@p3", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu oluşturuldu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
