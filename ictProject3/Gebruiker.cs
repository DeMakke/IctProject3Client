using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ictProject3
{
    public class Gebruiker
    {
        [JsonProperty("id")]
        public Guid id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
    }
}
