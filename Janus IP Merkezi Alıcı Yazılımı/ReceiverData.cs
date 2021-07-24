using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus_IP_Merkezi_Alıcı_Yazılımı
{
 public   struct ReceiverData
    {
        public string data { get; set; }
        public int toport { get; set; }
        public string tohost { get; set; }
        public string IpAdress { get; set; }
        public string Identifier { get; set; }
        public string Account { get; set; }
        public string ReportingTime { get; set; }
    }
}
