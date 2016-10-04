using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ictProject3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        public static string SerializeBase64(object o)
        {
            // Serialize to a base 64 string
            byte[] bytes;
            long length = 0;
            MemoryStream ws = new MemoryStream();
            BinaryFormatter sf = new BinaryFormatter();
            sf.Serialize(ws, o);
            length = ws.Length;
            bytes = ws.GetBuffer();
            string encodedData = bytes.Length + ":" + Convert.ToBase64String(bytes, 0, bytes.Length, Base64FormattingOptions.None);
            return encodedData;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Item> itemList = Item.GetItems();
            lstFiles.Items.Clear();

            foreach (Item item in itemList)
            {
                lstFiles.Items.Add((Convert.ToString(item.Id)).PadRight(15) + (Convert.ToString(item.Naam)));
            }
        }

        private void btnDownloadFile_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateList_Click(object sender, EventArgs e)
        {
            //kijk welke geselecteerd is
            //maak een object van het geselecteerde item

            var selectedFromListBox = lstFiles.SelectedItem;
        }
    }
}
