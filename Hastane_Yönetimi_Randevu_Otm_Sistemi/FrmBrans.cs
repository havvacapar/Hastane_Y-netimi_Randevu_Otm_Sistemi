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
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl=new SqlBaglantisi();

        private void FrmBrans_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable(); 
            SqlDataAdapter da=new SqlDataAdapter("select * from Tbl_Branslar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;


        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut=new SqlCommand("insert into Tbl_Branslar (BransAd) values(@b1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@b1", txtBransAd.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş kaydedildi.", "Kaydet", MessageBoxButtons.OK,MessageBoxIcon.Information);


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;//Hücrenin 0.sütununa göre satır indeksi alsın. 
            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtBransAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("delete from Tbl_Branslar where BransAd=@b1", bgl.baglanti());
            komut2.Parameters.AddWithValue("@b1", txtBransAd.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş bilgileri silindi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("update Tbl_Branslar set BransAd=@b1 where BransId=@b2", bgl.baglanti());
            komut3.Parameters.AddWithValue("@b1", txtBransAd.Text);
            komut3.Parameters.AddWithValue("@b2", txtId.Text);
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş bilgileri güncellendi.", "Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
