using CircularProgressBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Janus_IP_Merkezi_Alıcı_Yazılımı
{
    public partial class Panel : Form
    {
        public static int portnumarası;
        public static string SetValueForText1;
        string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        StringBuilder indexrec;
        public static bool control;
        private readonly DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
        Details datadetail;
        List<ReceiverData> distinctReceiverDataList;
        public ErrorLog allerror = new ErrorLog();
        public Panel()
        {
            InitializeComponent();
        }
        private void Panel_Load(object sender, EventArgs e)
        {
            try
            {
                listView1.View = View.Tile;
                listView1.Alignment = ListViewAlignment.Left;
                listView1.OwnerDraw = true;
                listView1.DrawItem += listView1_DrawItem;
                listView1.TileSize = new Size(48,
                  listView1.ClientSize.Height - (SystemInformation.HorizontalScrollBarHeight + 4));
                var mygetlist = GetListPort();

                if (mygetlist != null)
                {
                    if (mygetlist.Count > 0)
                    {

                        foreach (var item in GetListPort())
                        {
                            listView1.Items.Add(new ListViewItem(item.toport.ToString()));
                        }

                        GetListPort();
                    }
                }
                else if (mygetlist == null)
                {

                }
            }
            catch (Exception ex)
            {
                allerror.TxtKaydetErrorLog("Panel load list Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "Panel load list hata");
            }

        }
        //Kutucuk halinde portları liste halinde çizer.
        void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            try
            {
                Color textColor = Color.Black;
                if ((e.State & ListViewItemStates.Selected) != 0)
                {
                    e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                    textColor = SystemColors.HighlightText;
                }
                else
                {
                    e.Graphics.FillRectangle(Brushes.Gainsboro, e.Bounds);
                }
                e.Graphics.DrawRectangle(Pens.White, e.Bounds);

                TextRenderer.DrawText(e.Graphics, e.Item.Text, listView1.Font, e.Bounds,
                                      textColor, Color.Empty,
                                      TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            catch (Exception ex)
            {

                allerror.TxtKaydetErrorLog("Panel color Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "Panel color hata");
            }

          
        }
        //Porta göre Panelleri Sorgular.
        public List<ReceiverData> GetListReceiverData(int portno)
        {
            try
            {
                string FilePathss = "IPReceiver_Log\\" + "ipreceiverlog" + ".txt";
                if (File.Exists(FilePathss))
                {
                    List<ReceiverData> listrecdata = new List<ReceiverData>();
                    string[] lines = System.IO.File.ReadAllLines("IPReceiver_Log\\" + "ipreceiverlog" + ".txt");
                    foreach (string line in lines)
                    {
                        //data
                        StringBuilder mydata = new StringBuilder(line.Split(new string[] { "*" }, StringSplitOptions.None)[1].Split('*')[0].Trim());
                        string orjremovedatachar = mydata.ToString().Substring(5, mydata.Length - 5);
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
                        var removeidentifierfromarray = removeipadressfromarray.Remove(0, removecountidentifier);
                        //account
                        string account = removeidentifierfromarray.Split(new string[] { "*" }, StringSplitOptions.None)[1].Split('*')[0].Trim();
                        string orjremoveaccountchar = account.Substring(8, account.Length - 8);
                        int removecountaccount = account.Length + 1;
                        string removeaccountfromarray = removeidentifierfromarray.Remove(0, removecountaccount);
                        //reporting time
                        string reporttime = removeaccountfromarray.Split(new string[] { "*" }, StringSplitOptions.None)[1].Split('*')[0].Trim();
                        string orjremovereportchar = reporttime.Substring(14, reporttime.Length - 14);
                        //var removecountreporttime = reporttime.Length + 1;
                        //var removereporttimefromarray = removeaccountfromarray.Remove(0, removecountreporttime);
                        //add object to list 
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
                    distinctReceiverDataList = listrecdata.Where(c => c.toport == portno)
            .GroupBy(m => new { m.Account })
            .Select(group => group.First())  // instead of First you can also apply your logic here what you want to take, for example an OrderBy
            .ToList();
                    lines = null;
                }
                else
                {
                    dataGridView1.Columns.Clear();
                    MessageBox.Show("Aktif panel kaydı bulunamamıştır.");
                }
                    
            }
            catch (Exception ex)
            {
               
                allerror.TxtKaydetErrorLog("Data details  Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
                MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "data hata");
            }
            return distinctReceiverDataList;
        }
        public List<ReceiverData> GetListPort()
        {
            try
            {
                string FilePathss = "IPReceiver_Log\\" + "ipreceiverlog" + ".txt";
                if (File.Exists(FilePathss))
                {
                    List<ReceiverData> listrecdata = new List<ReceiverData>();

                    string[] lines = System.IO.File.ReadAllLines("IPReceiver_Log\\" + "ipreceiverlog" + ".txt");
                    if (lines != null || lines.Length > 0)
                    {

                        foreach (string line in lines)
                        {
                            //data
                            StringBuilder mydata = new StringBuilder(line.Split(new string[] { "*" }, StringSplitOptions.None)[1].Split('*')[0].Trim());
                            string orjremovedatachar = mydata.ToString().Substring(5, mydata.Length - 5);
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
                            var removeidentifierfromarray = removeipadressfromarray.Remove(0, removecountidentifier);
                            //account
                            string account = removeidentifierfromarray.Split(new string[] { "*" }, StringSplitOptions.None)[1].Split('*')[0].Trim();
                            string orjremoveaccountchar = account.Substring(8, account.Length - 8);
                            int removecountaccount = account.Length + 1;
                            string removeaccountfromarray = removeidentifierfromarray.Remove(0, removecountaccount);
                            //reporting time
                            string reporttime = removeaccountfromarray.Split(new string[] { "*" }, StringSplitOptions.None)[1].Split('*')[0].Trim();
                            string orjremovereportchar = reporttime.Substring(14, reporttime.Length - 14);
                            //var removecountreporttime = reporttime.Length + 1;
                            //var removereporttimefromarray = removeaccountfromarray.Remove(0, removecountreporttime);
                            //add object to list 
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
                        distinctReceiverDataList = listrecdata
                .GroupBy(m => new { m.toport })
                .Select(group => group.First())  // instead of First you can also apply your logic here what you want to take, for example an OrderBy
                .ToList();
                        lines = null;
                    }
                }

               
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "data hata");
                allerror.TxtKaydetErrorLog("Data details  Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            return distinctReceiverDataList;
        }
        public void DoSomeDetails(StringBuilder Account)
        {
            try
            {
                SetValueForText1 = Account.ToString();
               
                datadetail = new Details();
                datadetail.ShowDialog();
            }
            catch (Exception ex)
            {
               
                allerror.TxtKaydetErrorLog("Data details Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "data hata");
            }
        }

        private void data(object sender, DataGridViewCellEventArgs e)
        {
            ReceiverData[] recdata = GetListReceiverData(portnumarası).ToArray();
            try
            {
                //string receiverdata = recdata[e.RowIndex].Account;
                //SetValueForText1 = receiverdata;
                ////MessageBox.Show(this, "Data " + receiverdata+"Detaylar","Veri", MessageBoxButtons.OK,MessageBoxIcon.Information);
                //Details datadetail = new Details();
                ////user.updateEventHandler += User_UpdateEventHandler1;
                //datadetail.ShowDialog();
                //Details frm = new Details(this);
                //frm.Show();
                var senderGrid = (DataGridView)sender;

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    //TODO - Button Clicked - Execute Code Here
                    string receiverdata = recdata[e.RowIndex].Account;
                    SetValueForText1 = receiverdata;
                    //MessageBox.Show(this, "Data " + receiverdata+"Detaylar","Veri", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    Details datadetail = new Details();
                    //user.updateEventHandler += User_UpdateEventHandler1;
                    datadetail.ShowDialog();
                }
            }
            catch (Exception ex)
            {

                allerror.TxtKaydetErrorLog("Data details  Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "data hata");
            }
        }
        private void addRows()
        {
            try
            {
                var mygetlist = GetListReceiverData(portnumarası);
                if (mygetlist != null)
                {
                    foreach (var ipreceiverdata in GetListReceiverData(portnumarası))
                    {
                        dataGridView1.Rows.Add(ipreceiverdata.Account, ipreceiverdata.toport, ipreceiverdata.tohost, ipreceiverdata.IpAdress, ipreceiverdata.Identifier, ipreceiverdata.ReportingTime, ipreceiverdata.data);
                    }
                }
                else if (mygetlist == null)
                {
                  
                }
            }
            catch (Exception ex)
            {  
                allerror.TxtKaydetErrorLog("Panel addRows Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "Panel addRows hata");
            }
        }
        //Porttaki aktif paneleri sorgulayan fonksiyon
        private void panelsearch_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Visible = true;             
                dataGridView1.Rows.Clear();
                portnumarası = Convert.ToInt32(textBoxportnumarasi.Text);
                dataGridView1.ColumnCount = 7;
                dataGridView1.Columns[0].Name = "Hesap";        
                dataGridView1.Columns[1].Name = "Port";
                dataGridView1.Columns[2].Name = "Host";
                dataGridView1.Columns[3].Name = "IP Adresi";
                dataGridView1.Columns[4].Name = "Kimlik";
                dataGridView1.Columns[5].Name = "Raporlama Zamanı";
                dataGridView1.Columns[6].Name = "Veri";
                addButtonColumn();
                dataGridView1.Columns.Add(btn);
                addRows();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "panelsearch_Click hata");
                allerror.TxtKaydetErrorLog("panelsearch_Click Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
        }
        //detay butonları ekler.
        private void addButtonColumn()
        {
            try
            {
                btn.HeaderText = @"Detay";
                btn.Name = "button";
                btn.Text = "Detaya Git";
                btn.UseColumnTextForButtonValue = true;
                //btn. += myButton_Click;

            }
            catch (Exception ex)
            {
             
                allerror.TxtKaydetErrorLog("addButtonColumn Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "addButtonColumn hata");
            }
        }

        private void olaySorgulaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
//Panellerdeki Olayları sorgular
        private void olaySorgulaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
                  try
            {
                Events events = new Events();
                events.ShowDialog();
            }
            catch (Exception ex)
            {
              
                allerror.TxtKaydetErrorLog("olaySorgula Hatası:"+"Mesaj hatası:"+ex.Message+"stackTrace Hatası:"+ex.StackTrace + "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "olaySorgula hata");
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var firstSelectedItem = listView1.SelectedItems[0].Text;
          
        }  
        private void myfunc(object sender, EventArgs e)
        {
            var firstSelectedItem = listView1.SelectedItems[0].Text;
            //MessageBox.Show(firstSelectedItem,"selected list view");
            try
            {
                dataGridView1.Visible = true;
                dataGridView1.Rows.Clear();
                portnumarası = Convert.ToInt32(firstSelectedItem);
                dataGridView1.ColumnCount = 7;
                dataGridView1.Columns[0].Name = "Hesap";
                dataGridView1.Columns[1].Name = "Port";
                dataGridView1.Columns[2].Name = "Host";
                dataGridView1.Columns[3].Name = "IP Adresi";
                dataGridView1.Columns[4].Name = "Kimlik";
                dataGridView1.Columns[5].Name = "Raporlama Zamanı";
                dataGridView1.Columns[6].Name = "Veri";
                addButtonColumn();
                dataGridView1.Columns.Add(btn);
                addRows();
            }
            catch (Exception ex)
            {
               
                allerror.TxtKaydetErrorLog("panelsearch_Click Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "panelsearch_Click hata");
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
