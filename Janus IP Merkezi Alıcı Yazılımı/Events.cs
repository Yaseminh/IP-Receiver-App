using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Janus_IP_Merkezi_Alıcı_Yazılımı
{
    public partial class Events : Form
    {
        //Olaykları Sorgulama bölümü
        public string SetValueForselectedevent = "";
        List<ReceiverData> distinctReceiverDataList;
        public ErrorLog allerror = new ErrorLog();
        string senddatawitheventcode;
        public Events()
        {
            InitializeComponent();
        }
        private void Events_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "Olay Seçimi";
        }
        public List<ReceiverData> GetListReceiverData(string eventcode)
        {
            try
            {
                string FilePathss = "IPReceiver_Log\\" + "ipreceiverlog" + ".txt";
                if (File.Exists(FilePathss))
                {
                    List<ReceiverData> listrecdata = new List<ReceiverData>();

                    string[] lines = System.IO.File.ReadAllLines("IPReceiver_Log\\" + "ipreceiverlog" + ".txt");
                    // Display the file contents by using a foreach loop.
                    System.Console.WriteLine("Contents of WriteLines2.txt = ");
                    foreach (string line in lines)
                    {
                        //data
                        string mydata = line.Split(new string[] { "*" }, StringSplitOptions.None)[1].Split('*')[0].Trim();
                        string orjremovedatachar = mydata.Substring(5, mydata.Length - 5);
                        int removecountdata = mydata.Length + 1;
                        string removedatafromarray = line.Remove(0, removecountdata);
                        //get event code
                        string mydataevent = line.Split(new string[] { "#" }, StringSplitOptions.None)[1].Split(']')[0].Trim();
                        string orjremoveeventdatachar = mydataevent.Substring(6, mydataevent.Length - 6);
                        string myeventcode = orjremoveeventdatachar.Substring(0, 3);
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
                        var removecountidentifier = identifier.Length + 1;
                        string removeidentifierfromarray = removeipadressfromarray.Remove(0, removecountidentifier);
                        //account
                        string account = removeidentifierfromarray.Split(new string[] { "*" }, StringSplitOptions.None)[1].Split('*')[0].Trim();
                        string orjremoveaccountchar = account.Substring(8, account.Length - 8);
                        int removecountaccount = account.Length + 1;
                        string removeaccountfromarray = removeidentifierfromarray.Remove(0, removecountaccount);
                        //reporting time
                        string reporttime = removeaccountfromarray.Split(new string[] { "*" }, StringSplitOptions.None)[1].Split('*')[0].Trim();
                        string orjremovereportchar = reporttime.Substring(14, reporttime.Length - 14);
                        //add object to list 
                        ReceiverData receiverdata = new ReceiverData();
                        receiverdata.data = myeventcode;
                        receiverdata.toport = Convert.ToInt32(orjremoveportchar);
                        receiverdata.tohost = orjremovehostchar;
                        receiverdata.IpAdress = orjremoveipadresschar;
                        receiverdata.Identifier = orjremoveidentifierchar;
                        receiverdata.Account = orjremoveaccountchar;
                        receiverdata.ReportingTime = orjremovereportchar;
                        listrecdata.Add(receiverdata);
                    }
                    distinctReceiverDataList = listrecdata.Where(c => c.data == eventcode).ToList();
                }
               
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "GetListReceiverData event hata");
                allerror.TxtKaydetErrorLog("GetListReceiverData event Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }

            return distinctReceiverDataList;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetValueForselectedevent = comboBox1.Text;
        }
      
    

        private void eventsearch_Click_1(object sender, EventArgs e)
        {
            try
            {
              
                var senddatawitheventcode = SetValueForselectedevent.Substring(0, 3);              
                //dataGridView1.DataSource = GetListReceiverData(portnumarası);

                if(GetListReceiverData(senddatawitheventcode)!=null)
                {
                        dataGridView1.DataSource = GetListReceiverData(senddatawitheventcode);                   
                        dataGridView1.Columns[0].HeaderText = "Olay Kodu";
                        dataGridView1.Columns[1].HeaderText = "Port";
                        dataGridView1.Columns[2].HeaderText = "Host";
                        dataGridView1.Columns[3].HeaderText = "IP Adresi";
                        dataGridView1.Columns[4].HeaderText = "Kimlik";
                        dataGridView1.Columns[5].HeaderText = "Hesap";
                        dataGridView1.Columns[6].HeaderText = "Raporlama Zamanı";                
                }        
                else if(GetListReceiverData(senddatawitheventcode) == null)
                {
                    MessageBox.Show("Aradığınız Olay Kodu bulunamadı");
                  
                }
                //dataGridView1.Dispose();
                //this.Dispose();
            }
            catch (Exception ex)
            {
            
                allerror.TxtKaydetErrorLog("eventsearch_Click Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "eventsearch_Click hata");
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SetValueForselectedevent = comboBox1.Text;
        }
    }
}
