using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        Data data = new Data();
        public FileHandling fileHandler = new FileHandling();

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
            //MessageBox.Show(result); //achteraf hier nog een try catch aan toevoegen zodat het prog nie crashed in geval van foute response;

            
            Data response = jsoncode.JsonDeCoding(result); 
            Tuple<byte[], string> file = base64code.DeSerializeBase64(response);
            base64code.saveFile(file.Item1, file.Item2);
        }

        private void btnUpdateList_Click(object sender, EventArgs e)
        {
            //kijk welke geselecteerd is
            //maak een object van het geselecteerde item

            var selectedFromListBox = lstFiles.SelectedItem;
        }

        private async void btnUpload_Click(object sender, EventArgs e)
        {

            DialogResult resultaat = OpenFileDialog.ShowDialog();
            if (resultaat != DialogResult.Cancel)
            {
                string filePath = OpenFileDialog.FileName;
                string name = OpenFileDialog.SafeFileName;
                data = fileHandler.UploadFile(name, filePath);
                lblFilename.Text += name;


                
                var progressindicator = new Progress<int>(ReportProgress);
                cts = new CancellationTokenSource();
                string json = "";
                string resulta = "";
                string resultb = "";
                //Get data object from object met base64 coded bestand inclusief.
                string tempfilebase64 = base64code.SerializeBase64(base64code.GetFile(data.path));
                int filesize = fileHandler.Filesize(tempfilebase64);
                data.size = filesize;
                data.base64 = "";

                
                json = jsoncode.Serialize<Data>(data);
                //json = jsoncode.cropString(json);
                //json = jsoncode.cropString(json);
                Data datatest = new Data();
                //datatest = jsoncode.JsonDeCoding(json);


                resulta = await servercom.ReceiveDataAsync("Checkdiv", json, progressindicator, cts.Token);
                //resulta.Replace("\"", "");
                resulta = jsoncode.cropStringMore(resulta);
                string[] splitted = resulta.Split(':');

                string jsontosend = "";

                data.base64 = tempfilebase64;

                Debug.WriteLine(data.base64.Length / (Convert.ToInt16(splitted[1])- 1));
                Debug.WriteLine(data.base64.Length % (Convert.ToInt16(splitted[1]) - 1));

                List<string> base64data = base64code.SplitEvery(data.base64, data.base64.Length / (Convert.ToInt16(splitted[1]) - 1));
                
                for (int i = 1; i <= Convert.ToInt16(splitted[1]) ; i++)
                {
                    resultb = await servercom.ReceiveDataAsync("SaveFile/" + splitted[0] + "/" + splitted[1] + "/" + Convert.ToString(i), base64data.ElementAt(i-1), progressindicator, cts.Token);
                }
                
                MessageBox.Show(resultb);
                Debug.WriteLine(resultb);

            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            string item = Convert.ToString(lstFiles.SelectedItem);
            data.name = (item.Substring(10,item.Length-10)).Trim();
            
        }

        private void BerichtVerwijderen()
        {
            Succes succes = new Succes();

            if (succes.value)
            {
                MessageBox.Show("Het bestand is succesvol verwijderd.", "Bestand verwijderen");
            }
            else {
                MessageBox.Show("Het bestand kan niet verwijderd worden!", "Bestand verwijderen");
            }
            
        }
    }
}
