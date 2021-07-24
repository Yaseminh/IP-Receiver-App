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
    //Hakkında Bölümü
    public partial class Hakkında : Form
    {
        public Hakkında()
        {
            InitializeComponent();
        }

        private void Hakkında_Load(object sender, EventArgs e)
        {
            linkLabel1.LinkArea = new LinkArea(0, 22);
            linkLabel1.Links.Add(24, 9, "http://www.eds.com.tr");
            linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkedLabelClicked);
        }
        private void LinkedLabelClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("http://www.eds.com.tr");
        }
    }
}
