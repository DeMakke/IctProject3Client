using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ictProject3
{
    [DataContract]
    public class Item
    {
        [DataMember]
        public Guid id { get; set; }
        [DataMember]
        public string name { get; set; }
    }
}
