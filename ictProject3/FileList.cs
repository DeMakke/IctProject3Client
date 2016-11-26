using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ictProject3
{
    public class FileList
    {
        [JsonProperty("files")]
        public List<Item> files { get; set; }

        [JsonProperty("users")]
        public List<Gebruiker> users { get; set; }
    }
}
