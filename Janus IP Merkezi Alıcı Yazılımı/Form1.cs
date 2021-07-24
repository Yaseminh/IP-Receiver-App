
using FoxLearn.License;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Janus_IP_Merkezi_Alıcı_Yazılımı
{
    public partial class Form1 : Form
    {
        //System.Timers.Timer timer;
        //System.Timers.Timer timer2;
        private Socket _clientSocket = new Socket(AddressFamily.InterNetwork,
        SocketType.Stream, ProtocolType.Tcp);
      
        
        public Socket _serverSocket;
        public port port2;
        public StringBuilder hexxencrypted;
        public StringBuilder qissignaltypelikeatraq;
        public StreamWriter swactivepanelFile;
        public TcpClient tcp;
        public bool Itemstatu;
        public int setport;
        public StringBuilder receivernumberwithzero;
        public StringBuilder substring;
        public StringBuilder plaintext;
        public byte[] encrypted;
        public StringBuilder isinternet = new StringBuilder();
        public StringBuilder isreceiver = new StringBuilder();
        public TcpListener serverforatraq = null;
        public int crc = 0x0000;
        public int folderlength;
        public bool controlmsg;
        public IPAddress ip;
        public List<ReceiverData> distinctReceiverDataList;
        public List<string> last4;
        public ReceiverData distinctReceiverData;
        string destinationFile;
        //public static Form myformvalue;
        public static int countserver = 0;
        public static bool durum = false;
        public static int counterline=0;
        public bool control = true;
        public static int countforwrite = 0;
        List<ReceiverData> distinctReceiverDataList2;
        public StringBuilder myipadres;
        public static bool stopwrite = false;
        public static bool serverbaslat = false;
        public ManualResetEvent allDone = new ManualResetEvent(false);
        static object locker = new object();
        public ErrorLog allerror = new ErrorLog();
        public static Form1 form2ABS = new Form1();
        //Auto resent eventler asenkron metodları senkron gibi çalıştırır. Birbirlerinin işlemlerini beklerler.
        private static AutoResetEvent event_1 = new AutoResetEvent(true);
        private static AutoResetEvent event_2 = new AutoResetEvent(false);
        private static AutoResetEvent event_3 = new AutoResetEvent(false);
        private static AutoResetEvent event_4 = new AutoResetEvent(false);
       

        public static string json;

        public Form1()
        {
            InitializeComponent();
            

           
            try
            {
                form2ABS = this;


                //MessageBox.Show(location);
                //Var olmayan klasörler oluşturulur.
               
                Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

                string FilePathss = "IPReceiver_Log\\" + "ipreceiverlog" + ".txt";
                if (File.Exists(FilePathss))
                {
                    var lines = System.IO.File.ReadAllLines("IPReceiver_Log\\" + "ipreceiverlog" + ".txt");
                    folderlength = lines.Length;
                }
                //create folder
                string path = "IPReceiver_Log//";
                //string path = @"C:\MP_Upload";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    var ddssd = 3;
                }

                string path2 = "IPReceiverError_Log//";
                //string path = @"C:\MP_Upload";
                if (!Directory.Exists(path2))
                {
                    Directory.CreateDirectory(path2);
                    var ddssd = 3;
                }

                string path3 = "IPReceiversendToAtraq_Log//";
                //string path = @"C:\MP_Upload";
                if (!Directory.Exists(path3))
                {
                    Directory.CreateDirectory(path3);
                    var ddssd = 3;
                }    
                    port2 = new port(this);   
                //Atraqa bağlanır.
                    Thread threadatraq = new Thread(new ThreadStart(ListenForAtraq));
            }
            catch (SocketException se)
            {
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace + "Olay zamanı:" + DateTime.Now, "Atraq bağlanma socket hatası");
                allerror.TxtKaydetErrorLog("Atraq bağlanma Socket Hatası:" + "Atraq Bağlantı" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace+"Olay zamanı:" + DateTime.Now);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "Atraq bağlanma hata");
                allerror.TxtKaydetErrorLog("Atraq bağlanma Exception Hatası:" + "Atraq Bağlantı" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
        }
        private string FindByDisplayName(RegistryKey parentKey, string name)
        {
            string[] nameList = parentKey.GetSubKeyNames();
            for (int i = 0; i < nameList.Length; i++)
            {
                RegistryKey regKey = parentKey.OpenSubKey(nameList[i]);
                try
                {
                    if (regKey.GetValue("DisplayName").ToString() == name)
                    {
                        return regKey.GetValue("InstallLocation").ToString();
                    }
                }
                catch { }
            }
            return "";
        }
        //Atraq bilgileri ile dinlenmeye başlar.
        private void ListenForAtraq()
        {
            try
            {
                Int32 port3 = 9997;   
                //IPAddress localAddr2 = IPAddress.Parse("192.168.92.170");
                var  strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
                var addr = ipEntry.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork);              
                var firstInList = addr.First();
                //MessageBox.Show(firstInList.ToString(), "your connected ipadress");
                IPAddress localAddr2 = firstInList;  
                serverforatraq = new TcpListener(localAddr2, port3);
                serverforatraq.Start(); 
                // Start listening for client requests.
            }
            catch (SocketException se)
            {
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace + "Olay zamanı:" + DateTime.Now, "Atraq dinleme socket hatası");
                allerror.TxtKaydetErrorLog("Atraq dinleme Socket Hatası:" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "Atraq dinleme hata");
                allerror.TxtKaydetErrorLog("Atraq dinleme Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
        }
        //Receiver ip'si
        public void SetIPReceiver(string receiver)
        {
            isreceiver = new StringBuilder(receiver);
        }
        public string GetIPReceiver()
        {
            return isreceiver.ToString();
        }
      
        private void OnTimerEvent(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void ConnectShowPort()
        {
            try
            {
                listBox1.Items.Add("Connected Port:" + port.SetValueForport);
                listBox1.Update();
                listBox1.Refresh();
            }
            catch (SocketException se)
            {
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace + "Olay zamanı:" + DateTime.Now, "Porta Bağlantı socket hatası");
                allerror.TxtKaydetErrorLog("Porta Bağlantı Socket Hatası:" + port.SetValueForport + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "Porta Bağlantı hata");
                allerror.TxtKaydetErrorLog("Porta Bağlantı Exception Hatası:" + port.SetValueForport + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);

            }
        }
        private void notifyIcon1_DoubleClick(object Sender, EventArgs e)
        {
          
            if (this.WindowState == FormWindowState.Minimized)
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }
        private void OnApplicationExit(object sender, EventArgs e)
        {
            notifyIcon2.Dispose();
            notifyIcon2.Visible = true;

        }

        //Port'a bağlanıp server'ı başlatma
        private async Task AsyncMessageStartServer(int myport) //async keyword’ü ile yeni bir Task türetiyoruz…
        {
            await Task.Run(() => Thread.Sleep(0));
            try
            {
                setport = myport;
                //bellek sızıntısı var
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //_serverSocket.ReceiveTimeout = 5000;
                //SetKeepAliveValues(_serverSocket, true, 36000000, 1000);
                MessageBox.Show("Bağlantı ayarlarınız başarıyla tamamlandı.");
                //bellek sızıntısı var
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    //ip de var bellek sızıntısı
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        //bellek sızıntısı var
                        myipadres = new StringBuilder(ip.ToString());
                    }
                }
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, myport));
                _serverSocket.Listen(100);
                allDone.Reset();
                // Start an asynchronous socket to listen for connections and receive data from the client.    
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), _serverSocket);
            }

            catch (SocketException se)
            {                                         
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace + "Olay zamanı:" + DateTime.Now, "StartServer socket hatası");
                allerror.TxtKaydetErrorLog("StartServer Socket Hatası:" + myport + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            catch (Exception ex)                                     
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "StartServer hata");
                allerror.TxtKaydetErrorLog("StartServer Exception Hatası:" + myport + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
        }
        public async Task  StartServer(int port)
        {
            await Task.Run(() => Thread.Sleep(0));
            await AsyncMessageStartServer(port);
        }
        private bool ConnectServer()
        {
            try
            {
                //_clientSocket = new System.Net.Sockets.TcpClient();
                //// clientSocket.Connect("127.0.0.1", 8888)
                //_clientSocket.Connect("127.0.0.1", 8888);

                _clientSocket = new Socket(AddressFamily.InterNetwork,
              SocketType.Stream,
              ProtocolType.Tcp);
                _clientSocket.ReceiveTimeout = 5000;

                var strHostName = System.Net.Dns.GetHostName();

                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

                var addr = ipEntry.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork);
                var firstInList = addr.First();
                var ddd = 3;

                _clientSocket.Connect(firstInList, Convert.ToInt32(label1));

                var sdss = 3;


                // Label1.Text = "Client Socket Program - Server Connected ..."
                //serverStream = clientSocket.GetStream();

                //byte[] outStream = System.Text.Encoding.ASCII.GetBytes(TextBox3.Text + "$");
                //serverStream.Write(outStream, 0, outStream.Length);
                //serverStream.Flush();

                //System.Threading.Thread ctThread = new System.Threading.Thread(getMessage);
                //ctThread.Start();
                return true;
            }
            catch (Exception ex)
            {
                _clientSocket.Close();

                return false;
            }
        }

            //Burada server'a clientler bağlanabilir. Clientlerin bağlanabilmesini başlatan asenkron metot
        private async Task AsyncMessageAccceptCallback(IAsyncResult AR) //async keyword’ü ile yeni bir Task türetiyoruz…
        {
            StateObject state = new StateObject();
            try
            {

                await Task.Run(() => Thread.Sleep(0));
                allDone.Set();
                Thread.Sleep(250);
                if (control == true)
                {
                  
                  _clientSocket = new Socket(AddressFamily.InterNetwork,
                 SocketType.Stream, ProtocolType.Tcp);
                    _clientSocket.ReceiveTimeout = 5000;
                    _clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                    _clientSocket.SetIPProtectionLevel(IPProtectionLevel.Unrestricted);
                    if (_clientSocket.ReceiveTimeout == 5000)
                    {
                        allerror.TxtKaydetErrorLog("_Client Socket1:" + _clientSocket.Connected + "Olay zamanı:" + DateTime.Now);
                        _clientSocket = _serverSocket.EndAccept(AR);
                        allerror.TxtKaydetErrorLog("_Client Socket2:" + _clientSocket.Connected + "Olay zamanı:" + DateTime.Now);
                        state.workSocket = _clientSocket;
                        allDone.WaitOne();
                        event_2.Reset();                      
                            if (_clientSocket.Connected)
                            {
                                Socket client = new Socket(AddressFamily.InterNetwork,
                             SocketType.Stream, ProtocolType.Tcp);
                                _clientSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(SomeReceiveCallback), state);        
                        }
                        else if (!_clientSocket.Connected)
                            {
                                Socket client = new Socket(AddressFamily.InterNetwork,
                                SocketType.Stream, ProtocolType.Tcp);
                            }
                        //}
                        //else
                        //{
                            if (_clientSocket.Connected)
                            {          
                            }
                            else if (!_clientSocket.Connected)
                            {
                                Socket client = new Socket(AddressFamily.InterNetwork,
                              SocketType.Stream, ProtocolType.Tcp);
                            }
                           
                        }
                    //}

                    else
                    {
                        _clientSocket.Close();
                        _clientSocket = new Socket(AddressFamily.InterNetwork,
               SocketType.Stream, ProtocolType.Tcp);
                    }
                }
                else
                {
                }
            }
            //catch'e düşerse socketler kapatılmalı..
            catch (SocketException se)
            {
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace, "AsyncMessageAccceptCallback socket hatası");
                allerror.TxtKaydetErrorLog("AsyncMessageAccceptCallback Socket Hatası:" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);
                //allerror.TxtKaydetErrorLog("_Client Socket:" + _clientSocket.Connected + "Olay zamanı:" + DateTime.Now);
                _clientSocket.Close();
               
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace, "AsyncMessageAccceptCallback hata");
                allerror.TxtKaydetErrorLog("AsyncMessageAccceptCallback Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
                //allerror.TxtKaydetErrorLog("_Client Socket:" + _clientSocket.Connected + "Olay zamanı:" + DateTime.Now);
                _clientSocket.Close();
            }
            finally
            {
                //catche düşen kopan bağlantıyı normal'e getirir.
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), state);

            }
        }
        private async void AcceptCallback(IAsyncResult AR)
        {
            try
            {
                //// Get the socket that handles the client request.
                await AsyncMessageAccceptCallback(AR);
            }

            catch (SocketException se)
            {
                //MessageBox.Show("Socket hata"+se.Message +"stacktrc" + se.StackTrace + "exmassage", "AcceptCallback socket hatası");
                allerror.TxtKaydetErrorLog("AcceptCallback Socket Hatası:" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage"+ex.Message + "stacktrc" + ex.StackTrace,"AcceptCallback hata");
                allerror.TxtKaydetErrorLog("AcceptCallback Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace+"Olay zamanı:" + DateTime.Now);
            }
        }

        private async void SomeReceiveCallback(IAsyncResult AR)
        {
            try
            {
                //// Get the socket that handles the client request.
                await ReceiveCallback(AR);
            }

            catch (SocketException se)
            {
                //MessageBox.Show("Socket hata" + se.Message + "stacktrc" + se.StackTrace + "exmassage", "AcceptCallback socket hatası");
                allerror.TxtKaydetErrorLog("AcceptCallback Socket Hatası:" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace, "AcceptCallback hata");
                allerror.TxtKaydetErrorLog("AcceptCallback Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
        }
        //Socket verilerini list box'a yazar.
        private void AppendToTextBox(string text)
        {
            try
            {
                MethodInvoker invoker = new MethodInvoker(delegate
                {
                    //textBox.Text += text + "\r\n" + "\r\n";
                    listBox1.Items.Add(text);
                });
                this.Invoke(invoker);
            }

            catch (SocketException se)
            {
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace+"Olay zamanı:" + DateTime.Now, "AppendToTextBox  socket hatası");
                allerror.TxtKaydetErrorLog("AppendToTextBox Socket Hatası:" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace+"Olay zamanı:" + DateTime.Now, "AppendToTextBox hata");
                allerror.TxtKaydetErrorLog("AppendToTextBox Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
        }
        public class StateObject
        {
            // Client socket.  
            public Socket workSocket = null;
            // Size of receive buffer.  
            public const int BufferSize = 256;
            // Receive buffer.  
            public byte[] buffer = new byte[BufferSize];
            // Received data string.  
            public StringBuilder sb = new StringBuilder();
        }
     
        public List<ReceiverData> GetListReceiverDataactive(string activeaccount)
        {
            try
            {
                List<ReceiverData> listrecdata = new List<ReceiverData>();
                string[] lines = System.IO.File.ReadAllLines("ipreceiverlog.txt");
                // Display the file contents by using a foreach loop.
                System.Console.WriteLine("Contents of WriteLines2.txt = ");
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
                distinctReceiverDataList2 = listrecdata.Where(c => c.Account == activeaccount)
       .GroupBy(m => new { m.toport, m.IpAdress, m.Account })
       .Select(group => group.First())  // instead of First you can also apply your logic here what you want to take, for example an OrderBy
       .ToList();
            }
            catch (SocketException se)
            {
                //MessageBox.Show("Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now,"GetListReceiverDataactive  socket hatası");
                allerror.TxtKaydetErrorLog("GetListReceiverDataactive Socket Hatası:"+"Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now,"GetListReceiverDataactive hata");
                allerror.TxtKaydetErrorLog("GetListReceiverDataactive Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace+"Olay zamanı:" + DateTime.Now);
            }
            return distinctReceiverDataList2;


        }
        //Atraq log dosyalarını son bir hafta olacak şekilde kaydeder.
        public void deletefileafter7()
        {
            var dirName = "IPReceiversendToAtraq_Log//";
            string[] files = Directory.GetFiles(dirName);  
            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);      
                if (fi.LastAccessTime < DateTime.Now.AddDays(-7))
                    fi.Delete();
            }
        }
// Client'lerden gelen verileri alır.
        private async Task ReceiveCallback(IAsyncResult AR)
        {
            try
            {
                SocketError errorCode;
                await Task.Run(() => Thread.Sleep(0));
                event_2.Set();
                Thread.Sleep(250);
                deletefileafter7();
                StateObject state = (StateObject)AR.AsyncState;
                Socket handler = state.workSocket; 
                // Read data from the client socket.                
                    int bytesRead = handler.EndReceive(AR, out errorCode);
                    if(errorCode!=SocketError.Success)
                {
                    bytesRead = 0;
                }
                    //buralarda şifre çözme işlemleri yapılır.
                    if (bytesRead > 0)
                    {
                        // There  might be more data, so store the data received so far.
                        state.sb.Append(Encoding.ASCII.GetString(
                        state.buffer, 0, bytesRead));
                        int position = state.sb.ToString().IndexOf("\r");
                        StringBuilder crcwithquote = new StringBuilder('\"' + state.sb.ToString().Substring(0, position).Remove(0, 10).ToString());
                        string seqRrcvrLprefmsg = GetbeetweenIndexwithLenth(state.sb, 19, 13);
                          string deviceid = GetbeetweenIndexwithLenth(state.sb, 11, 4);
                    substring.Clear();
                        substring = null;
                        byte[] msgcrcData = System.Text.Encoding.ASCII.GetBytes(crcwithquote.ToString());
                        if (state.sb[0] == '\n')
                        {
                            string hex_value = $"{ state.sb[state.sb.ToString().Split(new string[] { "\n" }, StringSplitOptions.None)[1].Split('*')[0].Trim().IndexOf('"') - 1]}{state.sb[state.sb.ToString().Split(new string[] { "\n" }, StringSplitOptions.None)[1].Split('*')[0].Trim().IndexOf('"')]}";
                            int int_value = Convert.ToInt32(hex_value, 16);
                            //crc for secret
                            string forcrc = state.sb.ToString().Split(new string[] { "[" }, StringSplitOptions.None)[1].Split('\r')[0].Trim();
                            byte[] crcforencrptbytes = StringToByteArray(forcrc);
                            unsafe
                            {
                                fixed (char* str = state.sb.ToString().Substring(0, position).Remove(0, 10).ToString())
                                {
                                    int ch = 0;
                                    int crc = 0x0000;
                                    foreach (char i2 in msgcrcData)
                                    {
                                        ch = i2;
                                        if (ch != 10.5)
                                        {
                                            var mycrc = crc;
                                            crc = calcCRC((int)crc, ch);
                                        }
                                    }
                                    if (crc == (decimal)Int64.Parse(GetbeetweenIndexwithLenth(state.sb, 1, 4), System.Globalization.NumberStyles.HexNumber) && System.Text.Encoding.ASCII.GetBytes(state.sb.ToString()).Length >= int_value - 3)
                                    {
                                        string hour = DateTime.Now.ToString("HH:mm:ss");
                                        string trh = DateTime.Now.ToString("yyyy-MM-dd");
                                        string strformatdate = $"_{hour},{trh}";
                                        byte[] iv2 = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                                        byte[] enckey = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                                        //add padding   ppp
                                        string plainText = "pppppppppppp" + strformatdate;
                                        // Check arguments.
                                        if (plainText == null || plainText.Length <= 0)
                                            throw new ArgumentNullException("plainText");
                                        if (enckey == null || enckey.Length <= 0)
                                            throw new ArgumentNullException("Key");
                                        if (iv2 == null || iv2.Length <= 0)
                                            throw new ArgumentNullException("Key");
                                        byte[] encrypted = new byte[32768];
                                        // Create an RijndaelManaged object
                                        // with the specified key and IV.
                                        using (RijndaelManaged rijAlg = new RijndaelManaged())
                                        {
                                            rijAlg.Key = enckey;
                                            rijAlg.IV = iv2;
                                            rijAlg.Mode = CipherMode.CBC;
                                            rijAlg.Padding = PaddingMode.Zeros;
                                            // Create a decrytor to perform the stream transform.
                                            ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
                                            MemoryStream msEncrypt;
                                            CryptoStream csEncrypt;
                                            StreamWriter swEncrypt;
                                            // Create the streams used for encryption.
                                            using (msEncrypt = new MemoryStream())
                                            {
                                                using (csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                                                {
                                                    using (swEncrypt = new StreamWriter(csEncrypt))
                                                    {

                                                        //Write all data to the stream.
                                                        swEncrypt.Write(plainText);
                                                        swEncrypt.Close();
                                                        swEncrypt.Dispose();
                                                        swEncrypt = null;
                                                    }
                                                    encrypted = msEncrypt.ToArray();
                                                    hexxencrypted = new StringBuilder(BitConverter.ToString(encrypted).Replace("-", ""));
                                                    encrypted = null;
                                                    msEncrypt = null;
                                                    csEncrypt = null;
                                                }
                                            }
                                        }
                                        unsafe
                                        {
                                            string crcData2 = "*ACK" + '\"' + seqRrcvrLprefmsg + "[" + hexxencrypted;
                                            //hexxencrypted.Clear();
                                            //hexxencrypted = null;
                                            string newStringwithzero = "";
                                            string newStringwithzero2 = "";
                                            string newstr = "";
                                            fixed (char* str3 = crcData2)
                                            {
                                                //var crcData2 = "*ACK" + '\"' + seqRrcvrLprefmsg + "[" + hexxencrypted;
                                                //var crcwithquote2 = '\"' + crcData2;
                                                //message length calculate
                                                int lengthmsg = crcData2.Length - 1;
                                                string hexValuenew = lengthmsg.ToString("X");
                                                char pad = '0';
                                                newstr = hexValuenew.PadLeft(4, pad);
                                                //newStringwithzero2 = hexValuenew.PadLeft(4, '0');
                                                byte[] cnvbytecrcdata2 = new byte[32768];
                                                cnvbytecrcdata2 = System.Text.Encoding.ASCII.GetBytes(crcData2);
                                                //calculate hexadecimal
                                                string conhexxencrypted = BitConverter.ToString(cnvbytecrcdata2).Replace("-", "");
                                                cnvbytecrcdata2 = null;
                                                newStringwithzero2 = conhexxencrypted.PadLeft(4, '0');
                                                byte[] msgcrcData2 = new byte[32768];
                                                msgcrcData2 = System.Text.Encoding.ASCII.GetBytes(crcData2);
                                                int crcCalcResult2 = Pointercrc(str3, msgcrcData2);
                                                msgcrcData2 = null;
                                                string hexValue2 = crcCalcResult2.ToString("X");
                                                newStringwithzero = hexValue2.PadLeft(4, '0');
                                            }
                                            var sendackmsgforjanus = "\n" + newStringwithzero + newstr + '"' + "*ACK" + '"' + seqRrcvrLprefmsg + "[" + hexxencrypted + "\r";
                                            byte[] msgtojanus = System.Text.Encoding.ASCII.GetBytes(sendackmsgforjanus);
                                            NetworkStream stream;

                                        event_2.WaitOne();
                                        event_3.Reset();
                                        //_clientSocket.Disconnect(true);
                                        if (!_clientSocket.Connected)
                                        {
                                            Socket client = new Socket(AddressFamily.InterNetwork,
                                   SocketType.Stream, ProtocolType.Tcp);   
                                        }
                                        using (stream = new NetworkStream(_clientSocket))
                                            {
                                                stream.Write(msgtojanus, 0, msgtojanus.Length);
                                                stream.Close();
                                                stream.Dispose();
                                                stream = null;
                                        }
                                        event_3.Set();
                                        Thread.Sleep(250);
                                    }
                                    }
                                }
                            }
                        }
                        //byte[] key = Encoding.ASCII.GetBytes(mykey);
                        byte[] key = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
                        using (Aes aes = Aes.Create())
                        {
                            aes.BlockSize = 128;
                            // aes.IV = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0xa, 0xb, 0xc, 0xd, 0xe, 0xf }; // blocksize / 8 = 16 long
                            aes.KeySize = 256;
                            aes.Key = key;
                            aes.Mode = CipherMode.CBC;
                            aes.Padding = PaddingMode.None;
                            //var encriptdata = $"a62f97f742daf8baeb12fe2e5927b99b¦/.÷BÚøºë.þ.Y'¹.";
                            string mysecretdata = state.sb.ToString().Split(new string[] { "[" }, StringSplitOptions.None)[1].Split('\r')[0].Trim();
                            string encriptdata = mysecretdata;
                            byte[] encrptbytes = new byte[32768];
                            //var encriptdata = "bda8aff5f745943d7fe0fd96ef619c6fa170ec116916cf5126decca1d7fbebfa8b708654f4e9cf79b336171b3dcca193";
                            encrptbytes = StringToByteArray(encriptdata);
                            //byte[] utf8Bytes = utf8.GetBytes(inUTF8Chars);
                            byte[] iv = new byte[aes.IV.Length];
                            Stream st = new MemoryStream(encrptbytes);
                            encrptbytes = null;
                            CryptoStream cryptStream;
                            event_4.Reset();
                            using (cryptStream = new CryptoStream(st,
                       aes.CreateDecryptor(key, iv),
                       CryptoStreamMode.Read)
        )
                            {
                            event_4.Set();
                            Thread.Sleep(250);
                            st = null;
                                StreamReader sReader;
                            event_4.WaitOne();
                            using (sReader = new StreamReader(cryptStream))
                                {
                                    
                                    cryptStream = null;
                                    string decrypeddata = sReader.ReadToEnd();
                                    sReader = null;
                                    //discreate receiver  data
                                    int toport = setport;
                                event_3.WaitOne();
                                IPAddress IpAdress = IPAddress.Parse(((IPEndPoint)_clientSocket.RemoteEndPoint).Address.ToString());
                                //allerror.TxtKaydetErrorLog("_Client Socket ip adress:" + _clientSocket.Connected + "Olay zamanı:" + DateTime.Now);


                                string Identifierst = state.sb.ToString().Substring(state.sb.ToString().LastIndexOf('#') + -8);
                                    int pos = Identifierst.IndexOf("#");
                                    string Identifier = Identifierst.Remove(pos);
                                    string Account = decrypeddata.Split(new string[] { "#" }, StringSplitOptions.None)[1].Split('|')[0].Trim();
                                    string ReportingTime = decrypeddata.Split(new string[] { "_" }, StringSplitOptions.None)[1].Split('/')[0].Trim();
                                    CultureInfo outputCulture = CultureInfo.CreateSpecificCulture("es-es");
                                    CultureInfo inputCulture = CultureInfo.CreateSpecificCulture("en-us");
                                    Thread.CurrentThread.CurrentCulture = outputCulture;
                                    Thread.CurrentThread.CurrentUICulture = outputCulture;
                                    DateTime formattedDate = DateTime.Parse(ReportingTime, inputCulture);
                                    ReportingTime = null;
                                    var frmdate = formattedDate.ToString();
                                    Console.WriteLine(formattedDate.ToShortDateString()); //this returns 09/05/2014
                                    Console.WriteLine(DateTime.Today.ToShortDateString());

                                    var mydescratedata = '/' + "Data:" + decrypeddata + '/' + "Port:" + toport + '/' + "Host:" + getInternalIP() + '/' + "ipadresi:" + IpAdress + '/' + "identifier:" + Identifier + '/' + "Account:" + Account + '/' + "ReportingTime:" + frmdate + '/';
                                    AppendToTextBox("Gelen Veri" + mydescratedata);
                                string FilePathss = "IPReceiver_Log\\" + "ipreceiverlog" + ".txt";
                                if (File.Exists(FilePathss))
                                {

                                    using (FileStream fs = new FileStream(FilePathss, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                                    using (StreamWriter sw = new StreamWriter(fs))
                                    {
                                        if (folderlength < 300)
                                        {
                                            sw.WriteLine('*' + "Data:" + decrypeddata + '*' + "Port:" + toport + '*' + "Host:" + getInternalIP() + '*' + "ipadresi:" + IpAdress + '*' + "identifier:" + Identifier + '*' + "Account:" + Account + '*' + "ReportingTime:" + frmdate + '*');
                                        }   
                                    }
                                }
                                if (!File.Exists(FilePathss))
                                {
                                    if (!File.Exists(FilePathss))
                                    {
 
                                        using (StreamWriter sw = File.CreateText(FilePathss))
                                        {
                                            if (folderlength<=300)
                                            {

                                                sw.WriteLine('*' + "Data:" + decrypeddata + '*' + "Port:" + toport + '*' + "Host:" + getInternalIP() + '*' + "ipadresi:" + IpAdress + '*' + "identifier:" + Identifier + '*' + "Account:" + Account + '*' + "ReportingTime:" + frmdate + '*');
                                            }
                                        }
                                    }
                                }
                                var lines = System.IO.File.ReadAllLines("IPReceiver_Log\\" + "ipreceiverlog" + ".txt");
                                folderlength = lines.Length;
                                lines[counterline] = '*' + "Data:" + decrypeddata + '*' + "Port:" + toport + '*' + "Host:" + getInternalIP() + '*' + "ipadresi:" + IpAdress + '*' + "identifier:" + Identifier + '*' + "Account:" + Account + '*' + "ReportingTime:" + frmdate + '*';
                                File.WriteAllLines("IPReceiver_Log\\" + "ipreceiverlog" + ".txt", lines);
                                counterline++;
                                if (counterline == 300)
                                {
                                    counterline = 0;
                                    durum = true;
                                }

                                string getreceivernumber = state.sb.ToString().Split(new string[] { "R" }, StringSplitOptions.None)[1].Split('L')[0].Trim();
                                    receivernumberwithzero = new StringBuilder(getreceivernumber.PadLeft(2, '0'));
                                    getreceivernumber = null;
                                    StringBuilder getserialnumber = new StringBuilder(state.sb.ToString().Split(new string[] { "L" }, StringSplitOptions.None)[1].Split('#')[0].Trim());
                                    string account_number_for_hex = state.sb.ToString().Split(new string[] { "#" }, StringSplitOptions.None)[1].Split('[')[0].Trim();
                                    //int value = Convert.ToInt32(account_number_for_hex);
                                    //int toBase = 16;
                                    //string hex = ";
                                    //veriler atraq'a gidecek şekilde hazırlanır.
                                    state.sb.Clear();
                                    state.sb = null;
                                    string hex_account_number = account_number_for_hex.PadLeft(4, '0');
                                    account_number_for_hex = null;
                                    string decretadata = decrypeddata.Split(new string[] { "#" }, StringSplitOptions.None)[1].Split(']')[0].Trim();
                                    //get event code values 1,3,6 and to integrate E,R, P for atraq
                                    string forgeteventcodeandstate = decretadata.Remove(0, 5);
                                    string getalleventcodeandstate = forgeteventcodeandstate.Substring(0, 4);
                                    //signal type for atraq
                                    string qissignaltype = getalleventcodeandstate.Substring(0, 1);
                                    if (qissignaltype == "1")
                                    {
                                        qissignaltypelikeatraq = new StringBuilder("E");

                                    }
                                    else if (qissignaltype == "3")
                                    {
                                        qissignaltypelikeatraq = new StringBuilder("R");
                                    }
                                    else if (qissignaltype == "6")
                                    {
                                        qissignaltypelikeatraq = new StringBuilder("P");
                                    }
                                    //event code for atraq
                                    string eventcode = getalleventcodeandstate.Substring(1, 3);
                                    string allgroupcodeandareacode = decretadata.Remove(0, 10);
                                    decretadata = null;
                                    string groupcode = allgroupcodeandareacode.Substring(0, 2);
                                    string areacode = allgroupcodeandareacode.Substring(allgroupcodeandareacode.Length - 3);
                                    string sendtoatraqdata = "5" + receivernumberwithzero + getserialnumber + " " + "18" + hex_account_number + qissignaltypelikeatraq + eventcode + groupcode + areacode;                                  
                                    List<byte> list2 = new List<byte>();
                                    char[] values = sendtoatraqdata.ToCharArray();
                                    foreach (char letter in values)
                                    {
                                        // Get the integral value of the character.
                                        int valuer = Convert.ToInt32(letter);
                                        // Convert the decimal value to a hexadecimal value in string form.
                                        string hexOutput = String.Format("{0:X}", valuer);
                                        string hexstr = "0" + 'x' + hexOutput;
                                        byte b = Convert.ToByte(hexstr, 16);
                                        list2.Add(b);
                                        //dbArray = addByteToArray(datasendingarray, b);
                                        Console.WriteLine("Hexadecimal value of {0} is {1}", letter, hexOutput);
                                    }
                                    //var hexsnsdata = "353031312031383030393945333536303030373914";
                                    // Translate the passed message into ASCII and store it as a Byte array.
                                    //Byte[] mysendingdata = System.Text.Encoding.ASCII.GetBytes(hexsnsdata);
                                    byte[] datasending = { 0x35, 0x30, 0x31, 0x31, 0x20, 0x31, 0x38, 0x30, 0x30, 0x39, 0x39, 0x45, 0x33, 0x35, 0x36, 0x30, 0x30, 0x30, 0x37, 0x39, 0x14 };
                                    string hexstr2 = "0x14";
                                    byte b2 = Convert.ToByte(hexstr2, 16);
                                    //add cr4 character on last
                                    list2.Add(b2);
                                    byte[] sendjns;
                                    sendjns = list2.ToArray();
                                    list2.Clear();
                                    // Instantiate an ASCII encoding object.
                                    //ASCIIEncoding ascii = new ASCIIEncoding();
                                    //String decoded = ascii.GetString(bytes);
                                    //Byte[] mysendingdata2 = System.Text.Encoding.ASCII.GetBytes(decoded);
                                    try
                                    {
                                        //client2 = new TcpClient("192.168.92.170", 9997);
                                        tcp = new TcpClient("192.168.92.127", 9997);
                                        //Connect()
                                        //tcp.Connect("192.168.92.127", 9997);
                                    }
                                    catch (Exception ex)
                                    {
                                      
                                    }

                                    //MessageBox.Show("geldi");
                                    if (sendjns != null)
                                    {
                                    //Atraq'a mesaj gider ve list boxlarda gösterilir.
                                        if (tcp.Client.Send(sendjns) > 0)
                                        {
                                            AppendToTextBox("Giden Veri:" + sendtoatraqdata);
                                            TxtKaydetAtraq("Atraq Verisi:" + sendtoatraqdata + "Tarih:" + DateTime.Now.ToString());
                                            //MessageBox.Show("atraq giden veri:", sendtoatraqdata);
                                            //sendjns = null;
                                            tcp.Close();
                                            tcp.Dispose();
                                            //tcp = null;
                                        }
                                        else
                                        {
                                            MessageBox.Show("No data:", sendtoatraqdata);
                                        }
                                    }
                                }
                            }
                        }
                    }
                
            }

            catch (SocketException se)
            {
                allerror.TxtKaydetErrorLog("ReceiveCallback Socket Hatası:" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace+"Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace+ "Olay zamanı:" + DateTime.Now, "ReceiveCallback socket hatası");

            }
            catch (Exception ex)
            {
                allerror.TxtKaydetErrorLog("ReceiveCallback Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace+ "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace+ "Olay zamanı:" + DateTime.Now, "ReceiveCallback hata");
            }
        }
        public void SetInternet(string internet)
        {
            isinternet = new StringBuilder(internet);
        }
        public string GetInternet()
        {
            return isinternet.ToString();
        }
        private string getInternalIP()
        {
            try
            {
                //  string hostName = Dns.GetHostName(); // Retrive the Name of HOST
                //                                       //HostNameTextLabel.Text = hostName;
                //  IPHostEntry hostEntry = Dns.GetHostEntry(hostName);                
                //  IPAddress[] addr = hostEntry.AddressList;
                //ip = addr.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                //               .FirstOrDefault();      
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now,"getInternalIp hata");
                allerror.TxtKaydetErrorLog("getInternalIp Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            return ip.ToString() ?? "";                               

        }  // End private string getInternalIP()    
        //private async Task AsyncMessageConnectCallback(IAsyncResult AR) //async keyword’ü ile yeni bir Task türetiyoruz…
        //{
        //    try
        //    {
        //        await Task.Run(() => Thread.Sleep(0));
        //        byte[] buffer = Encoding.ASCII.GetBytes(port.SetValueForport);
        //        _clientSocket.Send(buffer);
        //        _clientSocket.EndConnect(AR);
        //        UpdateControlStates(true);
        //        _clientSocket.Close();
        //        _clientSocket.Dispose();
        //        _clientSocket = null;
        //    }
        //    catch (SocketException se)
        //    {
        //        MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace + "Olay zamanı:" + DateTime.Now, "AsyncMessageConnectCallback socket hatası");
        //        allerror.TxtKaydetErrorLog("AsyncMessageConnectCallback Socket Hatası:" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);
        //    }
        //    catch (Exception ex)
        //    {
        //        allerror.TxtKaydetErrorLog("AsyncMessageConnectCallback Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace);
        //        MessageBox.Show("AsyncMessageConnectCallback hata", "exmassage" + ex.Message + "stacktrc" + ex.StackTrace);
        //    }
        //}
        //private async void ConnectCallback(IAsyncResult AR)
        //{
        //    try
        //    {
        //        await AsyncMessageConnectCallback(AR);
        //    }
        //    catch (SocketException se)
        //    {
        //        MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace + "Olay zamanı:" + DateTime.Now, "ConnectCallback socket hatası");
        //        allerror.TxtKaydetErrorLog("ConnectCallback Socket Hatası:" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);
        //    }
        //    catch (Exception ex)
        //    {
        //        allerror.TxtKaydetErrorLog("ConnectCallback Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
        //        MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now,"Connectcallback hata");
        //    }
        //}
        private void UpdateControlStates(bool toggle)
        {
            try
            {
                MethodInvoker invoker = new MethodInvoker(delegate {

                });
                this.Invoke(invoker);
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now,"uPdate control states hata");
                allerror.TxtKaydetErrorLog("UpdateControlStates Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
        }
        //Hata loglarını kaydeden fonksiyondur.
        public void TxtKaydetErrorLog(string GelenVeri)
        {
            try
            {
                if (GelenVeri != null || GelenVeri != "")
                {
                    ArrayList arrRawData = new ArrayList(85190);
                    string rdata = GelenVeri;
                    arrRawData.Add(rdata);
                    if (arrRawData.Count > 0)
                    {
                        string DateFileNamess = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                                                           
                        string FilePathss = "IPReceiverError_Log\\" + DateFileNamess;

                        if (File.Exists(FilePathss))
                        {
                            FileStream fs = new FileStream(FilePathss, FileMode.Append, FileAccess.Write);
                            StreamWriter sw = new StreamWriter(fs);
                            string lineData = "";
                            for (int i = 0; i < arrRawData.Count; i++)
                            {
                                lineData += arrRawData[i].ToString();
                                sw.WriteLine(arrRawData[i].ToString());
                            }
                            arrRawData.Clear();
                            arrRawData = null;
                            sw.WriteLine(lineData);
                            sw.Close();
                            sw.Dispose();
                            fs.Close();
                            fs.Dispose();
                        }
                        else
                        {
                            FileStream fs = new FileStream(FilePathss, FileMode.Create, FileAccess.Write);
                            StreamWriter sw = new StreamWriter(fs);
                            string lineData = string.Empty;
                            for (int i = 0; i < arrRawData.Count; i++)
                            {
                                lineData += arrRawData[i].ToString();
                                sw.WriteLine(arrRawData[i].ToString());
                            }
                            arrRawData.Clear();
                            arrRawData = null;
                            sw.WriteLine(lineData);
                            sw.Close();
                            sw.Dispose();
                            fs.Close();
                            fs.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace, "TxtKaydetErrorLog hata"+ "Olay zamanı:" + DateTime.Now);
                allerror.TxtKaydetErrorLog("TxtKaydetErrorLog Exception Hatası:" + GelenVeri + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            finally
            {

            }
        }
        //Atraq loglarını kaydeder.
        public void TxtKaydetAtraq(string GelenVeri)
        {
            try
            {
                if (GelenVeri != null || GelenVeri != "")                          
                {
                    ArrayList arrRawData = new ArrayList(85190);
                    string rdata = GelenVeri;
                    arrRawData.Add(rdata);
                    if (arrRawData.Count > 0)
                    {
                        string DateFileNamess = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();

                        string FilePathss = "IPReceiversendToAtraq_Log\\" + DateFileNamess;

                        if (File.Exists(FilePathss))
                        {
                         

                            using (FileStream fs = new FileStream(FilePathss, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                            using (StreamWriter sw = new StreamWriter(fs))
                            {
                                string lineData = "";
                                for (int i = 0; i < arrRawData.Count; i++)
                                {

                                    lineData += arrRawData[i].ToString();
                                    sw.WriteLine(arrRawData[i].ToString());
                                }
                                arrRawData.Clear();
                                arrRawData = null;
                                //sw.WriteLine(lineData);
                                sw.Close();
                                sw.Dispose();
                                fs.Close();
                                fs.Dispose();
                            }
                        }
                        else
                        {
                           
                            using (FileStream fs = new FileStream(FilePathss, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                            using (StreamWriter sw = new StreamWriter(fs))
                            {
                                string lineData = "";
                                for (int i = 0; i < arrRawData.Count; i++)
                                {

                                    lineData += arrRawData[i].ToString();
                                    sw.WriteLine(arrRawData[i].ToString());
                                }
                                arrRawData.Clear();
                                arrRawData = null;
                                //sw.WriteLine(lineData);
                                sw.Close();
                                sw.Dispose();
                                fs.Close();
                                fs.Dispose();

                            }
                        }
                    }
                    }
                
            }
            catch (SocketException se)
            {
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace + "Olay zamanı:" + DateTime.Now, "TxtKaydetAtraq socket hatası");
                allerror.TxtKaydetErrorLog("Janus Socket Hatası:" + GelenVeri + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "TxtKaydetAtraq hata");
                allerror.TxtKaydetErrorLog("Janus Exception Hatası:" + GelenVeri + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            finally
            {

            }
        }      
        private delegate void ListeyeEkle(object item);
        public byte[] StringToByteArray(string hex)
        {
            try
            {
                return Enumerable.Range(0, hex.Length)
                                           .Where(x => x % 2 == 0)
                                           .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                                           .ToArray();
            }

            catch (SocketException se)
            {
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace + "Olay zamanı:" + DateTime.Now, "CStringtobytearray hata");
                allerror.TxtKaydetErrorLog("StringToByteArray Socket Hatası:" + "hexadecimal deger" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "CStringtobytearray hata");
                allerror.TxtKaydetErrorLog("StringToByteArray Hatası:" + "hexadecimal deger" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            return Enumerable.Range(0, hex.Length)
                                       .Where(x => x % 2 == 0)
                                       .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                                       .ToArray();
        }
        //byte dataları crc hesabı için karakter karakter ayrıştırır. Pointer ile yapar.
        unsafe int Pointercrc(char* mydata, byte[] hdata)
        {
            try
            {
                int ch = 0;
                char* ptr = mydata;
                //  mydata = new char[mydata.Length];                                       
                foreach (char i in hdata)
                {
                    ch = i;
                    if (ch != 10.5)
                    {
                        crc = calcCRC((int)crc, ch);                                                  //                    printf("\nChar %c [%2.2x] CRC is %4.4x, %2.2x count",
                    }
                }
            }
            catch (SocketException se)
            {
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace + "Olay zamanı:" + DateTime.Now, "Pointercrc socket hatası");
                allerror.TxtKaydetErrorLog("Pointercrc Socket Hatası:" + "Pointercrc" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);

            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "Pointercrc hata");
                allerror.TxtKaydetErrorLog("Pointercrc Exception Hatası:" + "Pointercrc" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }

            return crc;
        }
        //CRC hesabı yapar.
        public int calcCRC(int crc, int ch)
        {
            try
            {
                int i;
                //crc = 0x0000;
                sbyte temp;
                temp = (sbyte)ch; /* TREAT LOCALLY AS UNSIGNED */
                for (i = 0; i < 8; i++) /* DO 8 BITS */
                {
                    temp = (sbyte)(temp ^ (byte)(crc & 1));
                    /* PROCESS LSB */
                    /* SHIFT RIGHT */
                    crc >>= 1;
                    if (Convert.ToBoolean((sbyte)(temp & 1)))
                    {
                        //CRC = (byte)((CRC) ^ 0xA001);
                        crc ^= 0xA001;
                    }
                    /*  CRC ^= 0xA001;*/ /* IF LSB SET,ADD FEEDBACK */
                    temp >>= 1; /* GO TO NEXT BIT */
                }
            }
            catch (SocketException se)
            {
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace + "Olay zamanı:" + DateTime.Now, "Calccrc hata hata");
                allerror.TxtKaydetErrorLog("Calccrc Socket Hatası:" + "crc deger" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "Calccrc hata hata");
                allerror.TxtKaydetErrorLog("Calccrc  Hatası:" + "crc deger" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            return crc;
        }
        //Verileri ayırmayı sağlar.
        public string GetbeetweenIndexwithLenth(StringBuilder data, int index, int dtlength)
        {
            try
            {
                int startIndex = index;
                int length = dtlength;
                substring = new StringBuilder(data.ToString().Substring(startIndex, length));
            }

            catch (SocketException se)
            {
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace + "Olay zamanı:" + DateTime.Now, "GetbeetweenIndexwithLenth hata");
                allerror.TxtKaydetErrorLog("GetbeetweenIndexwithLenth  Hatası:" + "GetbeetweenIndexwithLenth deger" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "GetbeetweenIndexwithLenth hata");
                allerror.TxtKaydetErrorLog("GetbeetweenIndexwithLenth hata  Hatası:" + "GetbeetweenIndexwithLenth deger" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            return substring.ToString();
        }
        //Mesajı şifrelemeyi sağlar.
        byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            try
            {
                // Check arguments.
                if (plainText == null || plainText.Length <= 0)
                    throw new ArgumentNullException("plainText");
                if (Key == null || Key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (IV == null || IV.Length <= 0)
                    throw new ArgumentNullException("IV");

                // Create an Aes object
                // with the specified key and IV.
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;
                    // Create an encryptor to perform the stream transform.
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                    // Create the streams used for encryption.
                    MemoryStream msEncrypt;
                    StreamWriter swEncrypt;
                    using (msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.
                                swEncrypt.Write(plainText);
                            }
                            encrypted = msEncrypt.ToArray();
                            msEncrypt.Close();
                            msEncrypt.Dispose();
                            msEncrypt = null;
                            swEncrypt.Close();
                            swEncrypt.Dispose();
                            swEncrypt = null;
                        }
                    }
                }
            }

            catch (SocketException se)
            {
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace + "Olay zamanı:" + DateTime.Now, "EncryptStringToBytes_Aes hata");
                allerror.TxtKaydetErrorLog("EncryptStringToBytes_Aes  Hatası:" + "EncryptStringToBytes_Aes deger" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "EncryptStringToBytes_Aes hata");
                allerror.TxtKaydetErrorLog("EncryptStringToBytes_Aes  Hatası:" + "EncryptStringToBytes_Aes deger" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        //mesajın şifresini çözer
        string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            try
            {
                // Check arguments.
                if (cipherText == null || cipherText.Length <= 0)
                    throw new ArgumentNullException("cipherText");
                if (Key == null || Key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (IV == null || IV.Length <= 0)
                    throw new ArgumentNullException("IV");
                // Declare the string used to hold
                // the decrypted text.
                // Create an Aes object
                // with the specified key and IV.
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;
                    // Create a decryptor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    MemoryStream msDecrypt;
                    CryptoStream csDecrypt;
                    StreamReader srDecrypt;
                    // Create the streams used for decryption.
                    using (msDecrypt = new MemoryStream(cipherText))
                    {
                        using (csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = new StringBuilder(srDecrypt.ReadToEnd());
                            }
                            msDecrypt.Close();
                            msDecrypt.Dispose();
                            msDecrypt = null;
                            csDecrypt.Close();
                            csDecrypt.Clear();
                            csDecrypt.Dispose();
                            csDecrypt = null;
                            srDecrypt.Close();
                            srDecrypt.DiscardBufferedData();
                            srDecrypt.Dispose();
                        }
                    }
                }
            }
            catch (SocketException se)
            {
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace + "Olay zamanı:" + DateTime.Now, "DecryptStringFromBytes_Aes hata");
                allerror.TxtKaydetErrorLog("DecryptStringFromBytes_Aes  Hatası:" + "DecryptStringFromBytes_Aes deger" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);

            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "EncryptStringToBytes_Aes hata");
                allerror.TxtKaydetErrorLog("DecryptStringFromBytes_Aes  Hatası:" + "DecryptStringFromBytes_Aes deger" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);

            }
            return plaintext.ToString();
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            /*      Deletefile();*/ // This Method for Deleting files in DeleteData after 7 days.

            //CheckForIllegalCrossThreadCalls = false;
            //lisanslama işlemlerini yapar.
            //notifyIcon2.Icon = null;
            //notifyIcon2.Dispose();
            




            var licencekey = ComputerInfo.GetComputerId();
            SqlConnection conn = new SqlConnection("Data Source=78.189.235.113; Initial Catalog=ESENSE;User ID=yasemin;Password=1236987;");
            SqlDataAdapter daF = new SqlDataAdapter("Select * from UserLicense", conn);
            SqlDataAdapter daFusers = new SqlDataAdapter("Select * from UserLicenses", conn);
            DataTable dtFuserlicences = new DataTable();
            daFusers.Fill(dtFuserlicences);
            DataTable dtFuserlicence = new DataTable();
            daF.Fill(dtFuserlicence);
            Itemstatu = dtFuserlicence.AsEnumerable().Where(c => c.Field<string>("licenseKey") == licencekey && c.Field<bool>("statu") == true && c.Field<string>("app_name") == "Receiver").Any();
            if (Itemstatu)
            {

                lisansbToolStripMenuItem.Text = "Lisansı Sil";
                toolStripMenuItem1.Enabled = true;
                panelToolStripMenuItem.Enabled = true;
                yardımToolStripMenuItem.Enabled = true;
                hakkındaToolStripMenuItem.Enabled = true;
                controlmsg = false;

            }
            else
            {

                lisansbToolStripMenuItem.Text = "Lisansı Aktive Et";

                //MessageBox.Show("Programı Aktive Ediniz!", "Lisanlama Bilgisi",
                //  MessageBoxButtons.YesNo,
                //  MessageBoxIcon.Question);
                controlmsg = true;
                toolStripMenuItem1.Enabled = false;
                panelToolStripMenuItem.Enabled = false;
                yardımToolStripMenuItem.Enabled = false;
                hakkındaToolStripMenuItem.Enabled = false;

            }
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }
        private void networkPortsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
               
                port2.ShowDialog();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "networkPorts hata");
                allerror.TxtKaydetErrorLog("networkPorts  Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void verileriSorgulaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Panel panel = new Panel();
                panel.ShowDialog();
            }
            catch (Exception ex)
            {
              
                allerror.TxtKaydetErrorLog("verileriSorgula  Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace+"Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "verileriSorgula hata");
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
//portu kapatan fonksiyon
        private void closeport_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Port bağlantısını kapattınız.");
                if (_serverSocket != null)
                {
                    _serverSocket.Close();
                    _serverSocket.Dispose();
                    control = false;
                    label5.Text = "Kapalı";
                }
                //await AsyncMessageCloseCallback();
            }
            catch (SocketException se)
            {
                
                allerror.TxtKaydetErrorLog("closeport_Click  Hatası:" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace+"Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace+"Olay zamanı:" + DateTime.Now, "closeport_Click hata");

            }
            catch (Exception ex)
            {
             
                allerror.TxtKaydetErrorLog("closeport_Click  Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "closeport_Click hata");
            }
        }

        private void networkPortsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                port2 = new port(this);
                port2.ShowDialog();
            }
            catch (Exception ex)
            {
               
                allerror.TxtKaydetErrorLog("networkPorts  Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "networkPorts hata");
            }
        }

        private void closeport_Click_1(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Port bağlantısını kapattınız.");
                if (_serverSocket != null)
                {
                    _serverSocket.Close();
                    _serverSocket.Dispose();
                    control = false;
                    label5.Text = "Kapalı";
                    label1.Text = "Bağlantı Yok";
                }
                //await AsyncMessageCloseCallback();
            }
            catch (SocketException se)
            {
                allerror.TxtKaydetErrorLog("closeport_Click  Hatası:" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace + "Olay zamanı:" + DateTime.Now, "closeport_Click hata");
               

            }
            catch (Exception ex)
            {
               
                allerror.TxtKaydetErrorLog("closeport_Click  Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "closeport_Click hata");
            }
        }

        private void verileriSorgulaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                Panel panel = new Panel();
                panel.ShowDialog();
            }
            catch (Exception ex)
            {
               
                allerror.TxtKaydetErrorLog("verileriSorgula  Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "verileriSorgula hata");
            }
        }

        private void yardımToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string HelpFilePath = Application.StartupPath + "\\Yardim.pdf";
            //System.Diagnostics.Process.Start(HelpFilePath);
           help help = new help();
           help.ShowDialog();
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Hakkında hakkında= new Hakkında();
           hakkında.ShowDialog();
        }

        private void lisansBilgileriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lisans lisansform = new Lisans();
            lisansform.ShowDialog();
        }

        public void showfirstMethod()
        {
            if (controlmsg == true)
            {
                //Thread.Sleep(1000);
                MessageBox.Show("Lisansınızı aktive ediniz!", "Lisanlama Bilgisi",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Question);
            }
            else
            {
                //do anything
            }

        }
         //lisans bilgilerini gösterir.
        private void lisansbToolStripMenuItem_Click(object sender, EventArgs e)
        {

           try
            {
                if (Itemstatu == true)
                {
                    var result = MessageBox.Show("Lisansı silmek istediğinize emin misiniz?", "Lisans",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        // Closes the parent form.

                        var licencekey = ComputerInfo.GetComputerId();
                        SqlConnection conn = new SqlConnection("Data Source=78.189.235.113; Initial Catalog=ESENSE;User ID=yasemin;Password=1236987;");
                        SqlDataAdapter daF = new SqlDataAdapter("Select * from UserLicense", conn);
                        SqlDataAdapter daFusers = new SqlDataAdapter("Select * from UserLicenses", conn);
                        DataTable dtFuserlicences = new DataTable();
                        daFusers.Fill(dtFuserlicences);
                        DataTable dtFuserlicence = new DataTable();
                        daF.Fill(dtFuserlicence);

                        bool Itemstatu = dtFuserlicence.AsEnumerable().Where(c => c.Field<string>("licenseKey") == licencekey).Any();
                        string myinvoicenumber = dtFuserlicence.AsEnumerable().Where(c => c.Field<string>("licenseKey") == licencekey && c.Field<string>("app_name") == "Receiver").Select(c => c.Field<string>("invoice_Number")).SingleOrDefault();

                        if (Itemstatu)
                        {
                            conn.Open();
                            var table = "UserLicense ";


                            using (SqlCommand command = new SqlCommand("UPDATE " + table + "SET statu=0" + " WHERE " + "licenseKey" + " = '" + licencekey + "'", conn))
                            {


                                command.ExecuteNonQuery();
                            }

                            //string querystr = "UPDATE UserLicense SET statu=False  WHERE licenseKey=licencekey";


                            conn.Close();


                            conn.Close();

                            serverbaslat = true;

                            if (_serverSocket != null)
                            {
                                _serverSocket.Close();
                                _serverSocket.Dispose();
                                control = false;
                                label5.Text = "Kapalı";
                                label1.Text = "Bağlantı Yok";
                            }
                            listBox1.Items.Clear();
                            closeport.Enabled = false;

                            Form1_Load(sender, e);
                            //this.Close();
                        }


                    }
                }
                else
                {
                    LicenseKey lisansheygonder = new LicenseKey();
                    lisansheygonder.ShowDialog();
                    closeport.Enabled = true;
                    Form1_Load(sender, e);
                }
            }
            catch (Exception ex)
            {

                allerror.TxtKaydetErrorLog("lisansbToolStripMenuItem_Click  Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "closeport_Click hata");
            }

           
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
          

        }

        private void appexit(object sender, FormClosedEventArgs e)
        {
           
            notifyIcon2.Dispose();
          
        }

    
     

        private void clickfunc(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)

                this.WindowState = FormWindowState.Normal;
        }

        private void activated(object sender, EventArgs e)
        {
            notifyIcon2.Visible = true;
        }

        private void closed(object sender, EventArgs e)
        {
            var thisIcon = (NotifyIcon)sender;
            thisIcon.Visible = false;
            thisIcon.Dispose();
        }
    }
}
