using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ictProject3
{
    public class UserList
    {
        [JsonProperty("users")]
        public List<Gebruiker> users { get; set; }
    }
}
