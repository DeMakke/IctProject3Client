using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ictProject3
{
    public class FileHandling
    {
        public Data data = new Data();

        public Data UploadFile(string name, string path)
        {
            data.path = path;
            data.name = name;
            data.id = 0;
            return data;
        }
    }
}
