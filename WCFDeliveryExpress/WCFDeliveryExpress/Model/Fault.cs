using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DeliveryExpress.Model
{
    [DataContract]
     public class Fault
    {
        [DataMember]
        public int code { get; set; }
        [DataMember]
        public String Error_reason { get; set; }
        [DataMember]
        public String Error_detail { get; set; }
    }
}
