using FoxLearn.License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Janus_IP_Merkezi_Alıcı_Yazılımı
{
    public partial class Lisans : Form
    {
        public ErrorLog allerror = new ErrorLog();
        public Lisans()
        {
            InitializeComponent();
        }

        //Lisans bilgilerini veri tabanından sorgular.
        private void Lisans_Load(object sender, EventArgs e)
        {
            try { 
            //kullanıcı key'i
            var licencekey = ComputerInfo.GetComputerId();
            SqlConnection conn = new SqlConnection("Data Source=78.189.235.113; Initial Catalog=ESENSE;User ID=yasemin;Password=1236987;");
            SqlDataAdapter daF = new SqlDataAdapter("Select * from UserLicense", conn);
            SqlDataAdapter daFusers = new SqlDataAdapter("Select * from UserLicenses", conn);
            DataTable dtFuserlicences = new DataTable();
            daFusers.Fill(dtFuserlicences);
            DataTable dtFuserlicence = new DataTable();
            daF.Fill(dtFuserlicence);
            bool Itemstatu = dtFuserlicence.AsEnumerable().Where(c => c.Field<string>("licenseKey") == licencekey && c.Field<bool>("statu")!=false).Any();
            if (Itemstatu)
            {

                var useremail = dtFuserlicence.AsEnumerable().Where(c => c.Field<string>("licenseKey") == licencekey && c.Field<string>("app_name") == "Receiver").Select(r => r.Field<string>("mailAddress")).ToArray().SingleOrDefault();
                var usernameandsurname = dtFuserlicences.AsEnumerable().Where(c => c.Field<string>("email") == useremail).Select(r => r.Field<string>("userName") + "\u0020" + r.Field<string>("lastname")).ToArray().SingleOrDefault();
                if (useremail != null)
                {
                    var ddd = 3;
                    label2.Text = "Lisanslı";
                    label3.Text = usernameandsurname;
                    label4.Text = licencekey;
                    label1.Text = "1";
                }
                else
                {
                    label2.Text = "--";
                    label3.Text = "--";
                    label4.Text = "--";
                    label1.Text = "--";
                }
            }
            else
            {
                label2.Text = "--";
                label3.Text = "--";
                label4.Text = "--";
                label1.Text = "--";
            }
        }
               catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "Details_Load hata");
                allerror.TxtKaydetErrorLog("Lisans_Load Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
        }
    }
}
