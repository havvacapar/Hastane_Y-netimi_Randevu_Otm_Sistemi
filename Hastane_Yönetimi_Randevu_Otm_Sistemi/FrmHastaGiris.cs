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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl=new SqlBaglantisi();

        private void lnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayıt frh= new FrmHastaKayıt();
            frh.Show();
            
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from Tbl_Hastalar where HastaTc=@p1 and HastaSifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTc.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti();
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read()) //dr komutu doğru şekilde okursa;
            {
                FrmHastaDetay frd = new FrmHastaDetay();
                frd.tc = mskTc.Text; 
                //Hasta detay sayfasında public string tc tanımladık. Hasta giriş ekranında yazdığımız tcyi hasta detay design ekranına kaydetmek için bu işlemi yaptık
                frd.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tc veya şifre hatalı!");
            }
            bgl.baglanti().Close();
        }
    }
}
