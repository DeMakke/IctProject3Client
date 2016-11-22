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
            InitializeComponent(); //init and init again and again and.. :p
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
            getdata(); 

        }
        private void getdata() //
        {
            string fileString = Task.Run(GetFiles).Result;

            List<Item> itemList = new List<Item>();
            fileString = fileString.Remove(0, 23);
            fileString = fileString.Remove(fileString.Length - 2);
            fileString = fileString.Replace("\\\"", "\"");
            try
            {
                itemList = jsoncode.Deserialize<List<Item>>(fileString);

                lstFiles.DataSource = itemList;
                lstFiles.DisplayMember = "name";
                lstFiles.ValueMember = "id";

                lstFiles.Refresh();
                lstFiles.Update();
            }
            catch (Exception)
            {

            }
            
        }

        private async void btnDownloadFile_Click(object sender, EventArgs e)
        {

            var progressindicator = new Progress<int>(ReportProgress);
            cts = new CancellationTokenSource();

            string result = "";
            result = await servercom.ReceiveDataAsync("GetFile", Convert.ToString(lstFiles.SelectedValue), progressindicator, cts.Token);

            result = jsoncode.cropString(result);
            //MessageBox.Show(result); //achteraf hier nog een try catch aan toevoegen zodat het prog nie crashed in geval van foute response;

            
            Data response = jsoncode.JsonDeCoding(result); 
            Tuple<byte[], string> file = base64code.DeSerializeBase64(response);
            base64code.saveFile(file.Item1, file.Item2);
        }

        private void btnUpdateList_Click(object sender, EventArgs e)
        {
            getdata();
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

                List<string> base64data = base64code.SplitEvery(data.base64, data.base64.Length / Convert.ToInt16(splitted[1]), Convert.ToInt16(splitted[1]));
                
                for (int i = 1; i <= Convert.ToInt16(splitted[1]) ; i++)
                {

                        resultb = await servercom.ReceiveDataAsync("SaveFile/" + splitted[0] + "/" + splitted[1] + "/" + Convert.ToString(i), base64data.ElementAt(i - 1) , progressindicator, cts.Token);

                }

                //MessageBox.Show(resultb);
                //Debug.WriteLine(resultb);
                getdata();

            }
        }

        private async void btnDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                Item item = new Item();                     //dit werkt dus wel integenstelling met wat er stond
                var selectedItem = lstFiles.SelectedItem;
                item.id = (System.Guid)lstFiles.SelectedValue;     
                item.name = lstFiles.GetItemText(selectedItem);
                string json = jsoncode.JsonCoding(item);

                var progressindicator = new Progress<int>(ReportProgress);
                cts = new CancellationTokenSource();

                string result = "";
                result = await servercom.ReceiveDataAsync("DeleteFile", json, progressindicator, cts.Token);

                result = jsoncode.cropString(result);
                Succes succes = jsoncode.JsonDeCodingSucces(result);
                if (succes.value)
                {
                    MessageBox.Show("Het bestand is succesvol verwijderd.", "Bestand verwijderen");
                    getdata();
                }
                else
                {
                    MessageBox.Show("Het bestand kan niet verwijderd worden!", "Bestand verwijderen");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("system error on function: Delete File" + ex.ToString());
            }
            

        }

        private async Task<string> GetFiles()
        {
            var progressindicator = new Progress<int>(ReportProgress);
            cts = new CancellationTokenSource();
            string json = "for later implementation of users";

            string result = await servercom.ReceiveDataAsync("GetFileNames", json, progressindicator, cts.Token);
            MessageBox.Show(result);

            return result;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
        }
    }
}
