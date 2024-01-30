using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Hastane_Yönetimi_Randevu_Otm_Sistemi
{
    internal class SqlBaglantisi
    {
        public SqlConnection baglanti() //baglanti adında metot oluşturduk.
        {
            SqlConnection baglan = new SqlConnection("Data Source=LAPTOP-KRTI2VK1\\WINCCFLEX2014;Initial Catalog=HastaneProje;Integrated Security=True"); 
            //baglan adında nesne 
            baglan.Open();  
            return baglan;
        }
    }
}
