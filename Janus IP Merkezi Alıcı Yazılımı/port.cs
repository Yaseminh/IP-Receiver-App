using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Janus_IP_Merkezi_Alıcı_Yazılımı
{
    public partial class port : Form
    {
        public static string SetValueForport = "";
        private Form1 _firstForm;
        public ErrorLog allerror = new ErrorLog();
        public port(Form1 firstForm)
        {
            InitializeComponent();
            try
            {
                _firstForm = firstForm;
            }

            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "form portda hata var");
                allerror.TxtKaydetErrorLog("Panel load list Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }

        }

        private void port_Load(object sender, EventArgs e)
        {

        }
        public static string getText()
        {
            return SetValueForport;
        }

        private void findport_Click(object sender, EventArgs e)
        {
            try
            {
                _firstForm.label1.Text = textBoxport.Text;
                int myport = Convert.ToInt32(textBoxport.Text);
                _firstForm.control = true;
                _firstForm.label5.Text = "Açık";
                _firstForm.StartServer(myport);
                _firstForm.closeport.Visible = true;
                this.Close();
            }
            catch (SocketException se)
            {
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace + "Olay zamanı:" + DateTime.Now, "findport hatası");
                allerror.TxtKaydetErrorLog("findport  Hatası:" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);

            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "findport hata");
                allerror.TxtKaydetErrorLog("findport  Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Girilen Porta Bağlanır.
        private void findport_Click_1(object sender, EventArgs e)
        {
            try
            {

                if (_firstForm.label1.Text != "Bağlantı Yok")
                {
                    MessageBox.Show("Bağlantıyı Kapatmadan porta bağlanamazsınız");
                }
                else if((_firstForm.label1.Text == "Bağlantı Yok"))
                {
                    _firstForm.label1.Text = textBoxport.Text;
                    int myport = Convert.ToInt32(textBoxport.Text);
                    _firstForm.control = true;
                    _firstForm.label5.Text = "Açık";
                    _firstForm.StartServer(myport);
                    _firstForm.closeport.Visible = true;
                    this.Close();
                } 
            }
            catch (SocketException se)
            { 
                allerror.TxtKaydetErrorLog("findport  Hatası:" + "Mesaj hatası:" + se.Message + "stackTrace Hatası:" + se.StackTrace + "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + se.Message + "stacktrc" + se.StackTrace + "Olay zamanı:" + DateTime.Now, "findport hatası");

            }
            catch (Exception ex)
            {
                allerror.TxtKaydetErrorLog("findport  Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "findport hata");
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
