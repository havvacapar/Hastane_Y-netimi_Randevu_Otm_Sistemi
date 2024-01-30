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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl=new SqlBaglantisi();
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Tbl_Doktorlar ", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Bransı Comboboxa aktarma

            SqlCommand komut1 = new SqlCommand("select BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                cmbBrans.Items.Add(dr1[0].ToString());
            }
            bgl.baglanti().Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut=new SqlCommand("insert into Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTc,DoktorSifre) values (@d1,@d2,@d3,@d4,@d5)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", txtAd.Text);
            komut.Parameters.AddWithValue("@d2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@d3", cmbBrans.Text);
            komut.Parameters.AddWithValue("@d4", mskTc.Text);
            komut.Parameters.AddWithValue("@d5", txtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //HERHANGİ BİR HÜCREYE TIKLANDIĞINDA NE OLSUN;

            int secilen = dataGridView1.SelectedCells[0].RowIndex;//Hücrenin 0.sütununa göre satır indeksi alsın. 
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString(); //0.indeks id olduğu için 1den başladık
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskTc.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();


        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("delete from Tbl_Doktorlar where DoktorTc=@p1", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", mskTc.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti() .Close();
            MessageBox.Show("Doktor bilgileri silindi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@d1,DoktorSoyad=@d2,DoktorBrans=@d3,DoktorSifre=@d4 where DoktorTc=@d5", bgl.baglanti());
            komut3.Parameters.AddWithValue("@d1", txtAd.Text);
            komut3.Parameters.AddWithValue("@d2", txtSoyad.Text);
            komut3.Parameters.AddWithValue("@d3", cmbBrans.Text);
            komut3.Parameters.AddWithValue("@d4", txtSifre.Text);
            komut3.Parameters.AddWithValue("@d5", mskTc.Text);
            komut3.ExecuteNonQuery();
            bgl.baglanti() .Close();
            MessageBox.Show("Doktor bilgileri güncellendi!", "Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
