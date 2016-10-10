using System;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace ictProject3
{
    public class JsonCode
    {
        CRespons rp;
        public Data JsonDeCoding(String json)
        {
            Data data = JsonConvert.DeserializeObject<Data>(json);
            return data;
        }

        public String JsonCoding(Data data)
        {
            string json = JsonConvert.SerializeObject(data);
            return json;
        }

        public string cropString(string input)
        {
            string output;
            input = input.Remove(0,18);
            output = input.Remove(input.Length-2);
            return output;
        }

        public CRespons DecodeClientJson(string json)
        {
            JsonCode jc = new JsonCode();
            if (json != null)
            {
                rp = jc.Deserialize<CRespons>(json);
                MessageBox.Show("BestandsNaam =" + rp.naam + Environment.NewLine + "Base64Code = " + rp.respons64);
                return rp;
            }
            else
            {
                return null;
            }
        }
        //-----------------------------------------------//

        public T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }
    }
    //------------------------------------------------------------//
    public class CRespons
    {
        public CRespons() { }
        public CRespons(string Base64, string Name) { respons64 = Base64; naam = Name; }
        public string respons64 { get; set; }
        public string naam { get; set; }
    }
    //------------------------------------------------------------//

}
