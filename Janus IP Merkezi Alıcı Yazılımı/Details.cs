using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Janus_IP_Merkezi_Alıcı_Yazılımı
{
    public partial class Details : Form
    {
        //Receiver'daki listedeki detayları gösterir.
        List<ReceiverData> distinctReceiverDataList;
        public ErrorLog allerror = new ErrorLog();
        public List<ReceiverData> GetListReceiverData()
        {
            try
            {
                List<ReceiverData> listrecdata = new List<ReceiverData>();

                string[] lines = System.IO.File.ReadAllLines("IPReceiver_Log\\" + "ipreceiverlog" + ".txt");
                foreach (string line in lines)
                {
                    //data
                    string mydata = line.Split(new string[] { "*" }, StringSplitOptions.None)[1].Split('*')[0].Trim();
                    string orjremovedatachar = mydata.Substring(5, mydata.Length - 5);
                    int removecountdata = mydata.Length + 1;
                    string removedatafromarray = line.Remove(0, removecountdata);
                    //port
                    string myport = removedatafromarray.Split(new string[] { "*" }, StringSplitOptions.None)[1].Split('*')[0].Trim();
                    string orjremoveportchar = myport.Substring(5, myport.Length - 5);
                    int removecountport = myport.Length + 1;
                    string removeportfromarray = removedatafromarray.Remove(0, removecountport);
                    //host
                    string myhost = removeportfromarray.Split(new string[] { "*" }, StringSplitOptions.None)[1].Split('*')[0].Trim();
                    string orjremovehostchar = myhost.Substring(5, myhost.Length - 5);
                    int removecounthost = myhost.Length + 1;
                    string removehostfromarray = removeportfromarray.Remove(0, removecounthost);
                    //ip adresi
                    string ipadress = removehostfromarray.Split(new string[] { "*" }, StringSplitOptions.None)[1].Split('*')[0].Trim();
                    string orjremoveipadresschar = ipadress.Substring(9, ipadress.Length - 9);
                    int removecountipadress = ipadress.Length + 1;
                    string removeipadressfromarray = removehostfromarray.Remove(0, removecountipadress);
                    //identifier
                    string identifier = removeipadressfromarray.Split(new string[] { "*" }, StringSplitOptions.None)[1].Split('*')[0].Trim();
                    string orjremoveidentifierchar = identifier.Substring(11, identifier.Length - 11);
                    int removecountidentifier = identifier.Length + 1;
                    string removeidentifierfromarray = removeipadressfromarray.Remove(0, removecountidentifier);
                    //account
                    string account = removeidentifierfromarray.Split(new string[] { "*" }, StringSplitOptions.None)[1].Split('*')[0].Trim();
                    string orjremoveaccountchar = account.Substring(8, account.Length - 8);
                    int removecountaccount = account.Length + 1;
                    string removeaccountfromarray = removeidentifierfromarray.Remove(0, removecountaccount);
                    //reporting time
                    string reporttime = removeaccountfromarray.Split(new string[] { "*" }, StringSplitOptions.None)[1].Split('*')[0].Trim();
                    string orjremovereportchar = reporttime.Substring(14, reporttime.Length - 14);                  
                    ReceiverData receiverdata = new ReceiverData();
                    receiverdata.data = orjremovedatachar;
                    receiverdata.toport = Convert.ToInt32(orjremoveportchar);
                    receiverdata.tohost = orjremovehostchar;
                    receiverdata.IpAdress = orjremoveipadresschar;
                    receiverdata.Identifier = orjremoveidentifierchar;
                    receiverdata.Account = orjremoveaccountchar;
                    receiverdata.ReportingTime = orjremovereportchar;
                    listrecdata.Add(receiverdata);
                }
                distinctReceiverDataList = listrecdata.Where(c => c.toport == Panel.portnumarası && c.Account == Panel.SetValueForText1.ToString())        
         .ToList();
                lines = null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "GetListReceiverData Details hata");
                allerror.TxtKaydetErrorLog("GetListReceiverData Details Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            return distinctReceiverDataList;
        }
        public Details()
        {
            InitializeComponent();
        }
        private void Details_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                //dataGridView1.Refresh();    
                dataGridView1.ColumnCount = 7;
                dataGridView1.Columns[0].Name = "Hesap";
                dataGridView1.Columns[1].Name = "Port";
                dataGridView1.Columns[2].Name = "Host";
                dataGridView1.Columns[3].Name = "Ip Adresi";
                dataGridView1.Columns[4].Name = "Kimlik";
                dataGridView1.Columns[5].Name = "Veri";
                dataGridView1.Columns[6].Name = "Raporlama Zamanı";
                addRows();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "Details_Load hata");
                allerror.TxtKaydetErrorLog("Details_Load Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
        }
        private void addRows()
        {
            try
            {
                foreach (var ipreceiverdata in GetListReceiverData())
                {
                    dataGridView1.Rows.Add(ipreceiverdata.Account, ipreceiverdata.toport, ipreceiverdata.tohost, ipreceiverdata.IpAdress, ipreceiverdata.Identifier, ipreceiverdata.ReportingTime, ipreceiverdata.data);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "addRows hata");
                allerror.TxtKaydetErrorLog("addRows Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
        }
        private void cloding(object sender, FormClosedEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
