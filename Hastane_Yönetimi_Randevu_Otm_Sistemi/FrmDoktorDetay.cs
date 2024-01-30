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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl=new SqlBaglantisi();
        public string Tc;
        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = Tc;

            //DOKTOR AD SOYAD ÇEKME
            SqlCommand komut = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorTc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTc.Text);
            SqlDataReader dr= komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            //GİRİŞ YAPAN DOKTORA AİT RANDEVULARI ÇEKME

            DataTable dt=new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevu where RandevuDoktor='" + lblAdSoyad.Text + "'", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;  
        }

        private void btnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle frb=new FrmDoktorBilgiDuzenle();
            frb.TcNo = lblTc.Text;
            frb.Show();
        }

        private void btnDuyuru_Click(object sender, EventArgs e)
        {
            FrmDuyurular frd=new FrmDuyurular();
            frd.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            rchSikayet.Text = dataGridView2.Rows[secilen].Cells[7].Value.ToString(); //SQLDE HASTASİKAYET 7.İNDEKSTE  
        }
    }
}
