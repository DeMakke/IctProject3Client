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
    public class UserList
    {
        [DataMember]
        public List<Gebruiker> users { get; set; }
    }
}
