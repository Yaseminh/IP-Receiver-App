using FoxLearn.License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Janus_IP_Merkezi_Alıcı_Yazılımı
{
    public partial class LicenseKey : Form
    {
        //Lisans Doğrulama ve Lisans anahtarı kontrollerini yapar.
        public ErrorLog allerror = new ErrorLog();
        public LicenseKey()
        {
            InitializeComponent();
        }
        //lisansı doğrulamak için mail gönderen buton
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var count = 0;
                //lisans anahtarı üretilmesi      
                SqlConnection conn = new SqlConnection("Data Source=78.189.235.113; Initial Catalog=ESENSE;User ID=yasemin;Password=1236987;");
                SqlDataAdapter daF = new SqlDataAdapter("Select * from UserLicenses", conn);
                DataTable dtF = new DataTable();
                daF.Fill(dtF);
                var length = dtF.Rows.Count;
                if (dtF.Rows.Count > 0)
                {
                    SqlDataAdapter userlicence = new SqlDataAdapter("Select * from UserLicense", conn);
                    DataTable dtFuserlicence = new DataTable();
                    userlicence.Fill(dtFuserlicence);
                    for (int k = length - 1; k >= 0; k--)
                    {
                        if (dtF.Rows[k]["Invoice_Number"].ToString() == textBox2invoice.Text && Convert.ToBoolean(dtF.Rows[k]["Receiver"]) == true)
                        {
                            count++;
                            //lisans anahtarı üretilmesi
                            var key = ComputerInfo.GetComputerId();
                            var atıldımı = MailGonder(mailAddress.Text, key, "Ürün Anahtarı");
                            button1.Enabled = false;
                            mailAddress.Enabled = false;
                            textBox2invoice.Enabled = false;
                            button2.Enabled = true;
                            MessageBox.Show("Lisans anahtarı için mailinizi kontrol ediniz.", "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                if (count == 0)
                {
                    MessageBox.Show("Lisansınız bulunmamaktadır. Lisans  satın alınız.", "Kayıt İptal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("exmassage" + ex.Message + "stacktrc" + ex.StackTrace + "Olay zamanı:" + DateTime.Now, "Details_Load hata");
                allerror.TxtKaydetErrorLog("Lisans doğrula Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
        }

        //Mail Gönderen Fonksiyon  esense.com'dan atıyor.
        bool MailGonder(string Kime, string Icerik, string Konu)
        {
            bool Gitti = false;
            try
            {
                MailMessage mm = new MailMessage(new MailAddress("esense@esense.com.tr"), new MailAddress(Kime));
                string tesekkurmesajı ="Uygulamamızı kullandığınız için teşekkür ederiz.";
                mm.Body = Icerik+ "<br />"+tesekkurmesajı;
                mm.BodyEncoding = System.Text.Encoding.UTF8;
                mm.IsBodyHtml = true;
                mm.Subject = Konu;
                SmtpClient sCle = new SmtpClient("mail.esense.com.tr", 587);
                // sCle.Credentials = new NetworkCredential("esense@esense.com.tr", "GMgj74B0HI");
                sCle.Credentials = new NetworkCredential("esense@esense.com.tr", "!MBx?x16.W3!");
                sCle.Send(mm);
                Gitti = true;
            }
            catch (Exception ex)
            {
                string msf = ex.Message;
                // HataKayit(ex.Message, "Mail Gönderimi");
                Gitti = false;
                allerror.TxtKaydetErrorLog("Mail Gönder Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
            return Gitti;
        }
//Maile gelen anahtarı girdikten sonra doğrulama yapan fonksiyon
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (ComputerInfo.GetComputerId() == textBox1key.Text)
                {
                    var licencekey = ComputerInfo.GetComputerId();
                    if (MessageBox.Show("Kaydı onaylıyormusunuz?", "Onay Verin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var table = "UserLicense ";
                        var table2 = "UserLicenses ";
                        // MessageBox butonları geriye DialogResult değeri döndürür.
                        // MessageBoxtaki Evet'e tıklarsa buradaki kodlar çalışır.
                        // kayıt işlemleri kod bloğu
                        // DialogResult.Yes kullanabilmek için butonları MessageBoxButtons.YesNo olarak ayarlıyoruz.
                        // MessageBoxButtons.OKCancel yapsaydık  DialogResult.OK dememiz gerekirdi.
                        //veritabanına bilgi kayıtları
                        SqlConnection conn = new SqlConnection("Data Source=78.189.235.113; Initial Catalog=ESENSE;User ID=yasemin;Password=1236987;");
                        SqlDataAdapter daF = new SqlDataAdapter("Select * from UserLicense", conn);

                        DataTable dtFuserlicence = new DataTable();
                        daF.Fill(dtFuserlicence);

                        bool Itemstatu = dtFuserlicence.AsEnumerable().Where(c => c.Field<bool>("statu") == false).Any();

                        if (Itemstatu)
                        {
                            using (SqlCommand command = new SqlCommand("UPDATE " + table + "SET statu=1" + " WHERE " + "licenseKey" + " = '" + licencekey + "'", conn))
                            {
                                conn.Open();
                                command.ExecuteNonQuery();
                            }
                            //string querystr = "UPDATE UserLicense SET statu=False  WHERE licenseKey=licencekey";
                            conn.Close();
                            this.Close();
                        }
                        else
                        {
                            SqlCommand cmdEkle = new SqlCommand("INSERT INTO UserLicense (mailAddress, invoice_Number, licenseKey, app_name, AddedTime, statu ) VALUES(@mailAddress, @invoice_Number, @licenseKey, @app_name, @AddedTime, @statu)", conn);
                            //cmdU.Parameters.AddWithValue("@FIRID", Request.QueryString["FID"]);
                            cmdEkle.Parameters.AddWithValue("@mailAddress", mailAddress.Text);
                            cmdEkle.Parameters.AddWithValue("@invoice_Number", textBox2invoice.Text);
                            cmdEkle.Parameters.AddWithValue("@licenseKey", textBox1key.Text);
                            cmdEkle.Parameters.AddWithValue("@app_name", "Receiver");
                            cmdEkle.Parameters.AddWithValue("@AddedTime", DateTime.Now);
                            cmdEkle.Parameters.AddWithValue("@statu", 1);
                            SqlCommand command = cmdEkle;
                            conn.Close();
                            conn.Open();
                            cmdEkle.ExecuteNonQuery();
                            conn.Close();
                            Form1.form2ABS.Form1_Load(sender, e);
                            SqlDataAdapter daFli = new SqlDataAdapter("Select * from UserLicenses", conn);
                            DataTable dtFuserlicenceli = new DataTable();
                            daFli.Fill(dtFuserlicenceli);
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kayıt işlemi tarafınızca iptal edilmiştir.", "Kayıt İptal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Hayır dediğimiz için bu alana girdi ve bir bilgi mesajı gösterdik.
                        // MessageBoxButtons.OK eklemeden MessageBoxIcon.Information ekleyemiyoruz. yani önce buton sonra ikon
                    }
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("Hatalı veya eksik lisans anahtarı girdiniz. Lütfen size gelen mailde olan lisans anahtarını kontrol ediniz.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                allerror.TxtKaydetErrorLog("Lisans aktive Exception Hatası:" + "Mesaj hatası:" + ex.Message + "stackTrace Hatası:" + ex.StackTrace + "Olay zamanı:" + DateTime.Now);
            }
        }

        private void LicenseKey_Load(object sender, EventArgs e)
        {

        }
    }
}
