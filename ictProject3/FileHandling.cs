using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ictProject3
{
    public class FileHandling
    {
        
        public Data UploadFile(string name, string path)
        {
            Data data = new Data();

            data.path = path;
            data.name = name;
            data.id = 0;
            return data;
        }

        public int Filesize (string Json)
        {
            return Encoding.UTF8.GetByteCount(Json);
        }
    }
}
