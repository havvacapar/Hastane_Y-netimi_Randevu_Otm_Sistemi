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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl=new SqlBaglantisi();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut=new SqlCommand("select * from Tbl_Sekreter where SekreterTc=@p1 and SekreterSifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTc.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr=komut.ExecuteReader();
            if (dr.Read())
            {
                FrmSekreterDetay frms=new FrmSekreterDetay();
                frms.tcNumara = mskTc.Text;
                frms.Show();
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
