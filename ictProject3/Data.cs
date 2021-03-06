﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ictProject3
{
    public class Data
    {

        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("base64")]
        public string base64 { get; set; }
        [JsonProperty("path")]
        public string path { get; set; }
        [JsonProperty("size")]
        public int size { get; set; }
    }
}
