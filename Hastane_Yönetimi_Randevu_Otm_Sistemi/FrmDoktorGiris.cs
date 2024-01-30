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
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl=new SqlBaglantisi();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from Tbl_Doktorlar where DoktorTc=@p1 and DoktorSifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTc.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr= komut.ExecuteReader();
            if (dr.Read()) //İF SADECE GİRİŞLERDE PARAMETRELERİN BİRBİRYLE UYUMLU OLUP OLAMADIĞINI ÖĞRENMEK İÇİN KULLANIYORUZ.
            {
                FrmDoktorDetay frmdoktorDetay = new FrmDoktorDetay();
                frmdoktorDetay.Tc = mskTc.Text;
                frmdoktorDetay.Show();                
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış!");
            }
            bgl.baglanti().Close();

        }

        private void FrmDoktorGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
