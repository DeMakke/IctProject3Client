using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
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

        public WebCom servercom = new WebCom();
        private CancellationTokenSource cts;

        JsonCode jsoncode = new JsonCode();
        Base64Code base64code = new Base64Code();

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ReportProgress(int value)
        {
            //progressreport
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

        private async void btnDownloadFile_Click(object sender, EventArgs e)
        {

            var progressindicator = new Progress<int>(ReportProgress);
            cts = new CancellationTokenSource();

            string result = "";
            result = await servercom.ReceiveDataAsync("GetFile", Convert.ToString(lstFiles.SelectedItem), progressindicator, cts.Token);

            result = jsoncode.cropString(result);
            MessageBox.Show(result); //achteraf hier nog een try catch aan toevoegen zodat het prog nie crashed in geval van foute response;

            
            Data response = jsoncode.JsonDeCoding(result); // vult data object niet
            Tuple<byte[], string> file = base64code.DeSerializeBase64(response);
            base64code.saveFile(file.Item1, file.Item2);
        }

        private void btnUpdateList_Click(object sender, EventArgs e)
        {
            //kijk welke geselecteerd is
            //maak een object van het geselecteerde item

            var selectedFromListBox = lstFiles.SelectedItem;
        }

        

    }
}
