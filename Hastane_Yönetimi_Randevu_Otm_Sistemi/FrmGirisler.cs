using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_Yönetimi_Randevu_Otm_Sistemi
{
    public partial class FrmGirisler : Form
    {
        public FrmGirisler()
        {
            InitializeComponent();
        }

        private void btnHastaGirisi_Click(object sender, EventArgs e)
        {
            FrmHastaGiris frh=new FrmHastaGiris();
            frh.Show();
            


        }

        private void btnDoktorGirisi_Click(object sender, EventArgs e)
        {
            FrmDoktorGiris frd=new FrmDoktorGiris();
            frd.Show();
            
        }

        private void btnSekreterGirisi_Click(object sender, EventArgs e)
        {
            FrmSekreterGiris frs=new FrmSekreterGiris();
            frs.Show();
            
        }

        private void FrmGirisler_Load(object sender, EventArgs e)
        {

        }
    }
}
