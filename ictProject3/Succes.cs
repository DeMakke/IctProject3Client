using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ictProject3
{
    public class Succes
    {

        [JsonProperty("Value")]
        public bool value { get; set; }

        [JsonProperty("Title")]
        public string title { get; set; }

        [JsonProperty("Message")]
        public string message { get; set; }

    }
}
