using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ictProject3
{
    public class Base64Code
    {
        public string SerializeBase64(byte[] file) //geen static van maken ~M.V.G. Dries
        {
            // Serialize to a base 64 string
            //byte[] bytes;
            //long length = 0;
            //MemoryStream ws = new MemoryStream();
            //BinaryFormatter sf = new BinaryFormatter();
            //sf.Serialize(ws, file);
            //length = ws.Length;
            //bytes = ws.GetBuffer();
            //string encodedData = bytes.Length + ":" + Convert.ToBase64String(bytes, 0, bytes.Length, Base64FormattingOptions.None);
            //return encodedData;
            
            string encodedData = Convert.ToBase64String(file);
            return encodedData;
        }

        public Tuple<byte[], string> DeSerializeBase64(Data o)
        {
            string naam = o.name;

            byte[] bitarray = Convert.FromBase64String(o.base64);

            return Tuple.Create(bitarray, naam);
        }

        public string saveFile(byte[] tempBytes, string fileName)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + fileName;
            string fileNameOnly = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);
            string path = Path.GetDirectoryName(filePath);
            string newFullPath = filePath;
            int count = 1;
            while (File.Exists(newFullPath))
            {
                string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                newFullPath = Path.Combine(path, tempFileName + extension);
                
            }
            //MessageBox.Show(newFullPath);
            File.WriteAllBytes(newFullPath, tempBytes);

            return newFullPath;
        }
        public byte[] GetFile(string path)
        {
            byte[] file = File.ReadAllBytes(path);
            return file;
        }

        public List<string> SplitEvery(string s, int size, int packetcount)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < packetcount; i++)
            {
                if (i == packetcount -1)
                {
                    result.Add(s.Substring(size * i));
                }
                else
                {
                    result.Add(s.Substring(size * i, size));
                }
                
            }
            return result;
        }
    }
}
