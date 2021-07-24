using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Janus_IP_Merkezi_Alıcı_Yazılımı
{
   public class ErrorLog
    {
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
                        //    if (File.Exists(FilePathss))
                        //    {

                        //        FileStream fs = new FileStream(FilePathss, FileMode.Append, FileAccess.Write);
                        //        StreamWriter sw = new StreamWriter(fs);
                        //        string lineData = "";
                        //        for (int i = 0; i < arrRawData.Count; i++)
                        //        {
                        //            lineData += arrRawData[i].ToString();
                        //            sw.WriteLine(arrRawData[i].ToString());
                        //        }
                        //        arrRawData.Clear();
                        //        arrRawData = null;
                        //        sw.WriteLine(lineData);
                        //        sw.Close();
                        //        sw.Dispose();
                        //        fs.Close();
                        //        fs.Dispose();
                        //    }
                        //    else
                        //    {
                        //        FileStream fs = new FileStream(FilePathss, FileMode.Create, FileAccess.Write);
                        //        StreamWriter sw = new StreamWriter(fs);
                        //        string lineData = string.Empty;
                        //        for (int i = 0; i < arrRawData.Count; i++)
                        //        {
                        //            lineData += arrRawData[i].ToString();
                        //            sw.WriteLine(arrRawData[i].ToString());
                        //        }
                        //        arrRawData.Clear();
                        //        arrRawData = null;
                        //        sw.WriteLine(lineData);
                        //        sw.Close();
                        //        sw.Dispose();
                        //        fs.Close();
                        //        fs.Dispose();
                        //    }
                        //}

                        //if (File.Exists(FilePathss))
                        //{
                        //    FileStream fs = new FileStream(FilePathss, FileMode.Append, FileAccess.Write);
                        //    StreamWriter sw = new StreamWriter(fs);
                        //    string lineData = "";
                        //    for (int i = 0; i < arrRawData.Count; i++)
                        //    {
                        //        lineData += arrRawData[i].ToString();
                        //        sw.WriteLine(arrRawData[i].ToString());
                        //    }
                        //    arrRawData.Clear();
                        //    arrRawData = null;
                        //    sw.WriteLine(lineData);
                        //    sw.Close();
                        //    sw.Dispose();
                        //    fs.Close();
                        //    fs.Dispose();
                        //}
                        //else
                        //{
                        //    FileStream fs = new FileStream(FilePathss, FileMode.Create, FileAccess.Write);
                        //    StreamWriter sw = new StreamWriter(fs);
                        //    string lineData = string.Empty;
                        //    for (int i = 0; i < arrRawData.Count; i++)
                        //    {
                        //        lineData += arrRawData[i].ToString();
                        //        sw.WriteLine(arrRawData[i].ToString());
                        //    }
                        //    arrRawData.Clear();
                        //    arrRawData = null;
                        //    sw.WriteLine(lineData);
                        //    sw.Close();
                        //    sw.Dispose();
                        //    fs.Close();
                        //    fs.Dispose();
                        //}


                        if (File.Exists(FilePathss))
                        {

                            //FileStream fs = new FileStream(FilePathss, FileMode.Append, FileAccess.Write);

                            //StreamWriter sw = new StreamWriter(fs);
                            //string lineData = "";
                            //for (int i = 0; i < arrRawData.Count; i++)
                            //{

                            //    lineData += arrRawData[i].ToString();
                            //    sw.WriteLine(arrRawData[i].ToString());
                            //}
                            //arrRawData.Clear();
                            //arrRawData = null;
                            ////sw.WriteLine(lineData);
                            //sw.Close();
                            //sw.Dispose();
                            //fs.Close();
                            //fs.Dispose();


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
                            //FileStream fs = new FileStream(FilePathss, FileMode.Create, FileAccess.Write);
                            //StreamWriter sw = new StreamWriter(fs);
                            //string lineData = string.Empty;
                            //for (int i = 0; i < arrRawData.Count; i++)
                            //{
                            //    lineData += arrRawData[i].ToString();
                            //    sw.WriteLine(arrRawData[i].ToString());
                            //}
                            //arrRawData.Clear();
                            //arrRawData = null;
                            //sw.WriteLine(lineData);
                            //sw.Close();
                            //sw.Dispose();
                            //fs.Close();
                            //fs.Dispose();
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
                MessageBox.Show("Error Log Socket hatası" + se.Message + "stacktrc" + se.StackTrace);
                TxtKaydetErrorLog("Error Log Socket hatası:" + se.Message + "Stacktrace hatası:" + se.StackTrace);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "stacktrc" + ex.StackTrace, "TxtKaydet hata");
                TxtKaydetErrorLog("Error log Mesaj hatası:" + ex.Message + "stacktrace Hatası:" + ex.StackTrace);
            }
            finally
            {

            }

        }
    }
}
