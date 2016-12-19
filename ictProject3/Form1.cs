using System;
using System.Collections;
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
            logoutButton.Enabled = false;
            btnAdmin.Enabled = false;
            btnAdmin.Visible = false;
            btnDelen.Enabled = false;
            //btnDelen.Visible = false; //maak de delen knop ook onzichtbaar
            btnDeleteItem.Enabled = false;
            //btnDeleteItem.Visible = false; //maak de knop ook onzichtbaar
            btnUpload.Enabled = false;
            //btnUpload.Visible = false; //maak de knop ook onzichtbaar
            btnWijzigen.Enabled = false;
            btnWijzigen.Visible = false; //maak de knop ook onzichtbaar

            if (Properties.Settings.Default.Token == Convert.ToString(new Guid()))
            {
                this.Text = "LAN-FileShare @Guest";
            }
        }

        public WebCom servercom = new WebCom();
        private CancellationTokenSource cts;     

        JsonCode jsoncode = new JsonCode();
        Base64Code base64code = new Base64Code();
        Data data = new Data();
        public FileHandling fileHandler = new FileHandling();

        private static string _loggedInUserName;
        public static string loggedInUserName
        {
            get // this makes you to access value in form2
            {
                return _loggedInUserName;
            }
            set // this makes you to change value in form2
            {
                _loggedInUserName = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ReportProgress(int value)
        {
            //progressreport
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //FIRST set the user-token to GUEST login aka 0000
            //ONLY then load the files list for user GUEST (= alle openbare bestanden)
            //Properties.Settings.Default.Token = "8500";
            getdata(); //loads the files list for the logged in user
            loggedInUserName = "Guest";

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
            //addToSessionHistory("save File:","File saved To", false);
            
            Data response = jsoncode.JsonDeCoding(result); 
            Tuple<byte[], string> file = base64code.DeSerializeBase64(response);
            string filelocation = base64code.saveFile(file.Item1, file.Item2);
            addToSessionHistory("save File:", "File saved To " + filelocation, false);
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


                List<string> base64data = base64code.SplitEvery(data.base64, data.base64.Length / Convert.ToInt16(splitted[1]), Convert.ToInt16(splitted[1]));
                
                for (int i = 1; i <= Convert.ToInt16(splitted[1]) ; i++)
                {

                        resultb = await servercom.ReceiveDataAsync("SaveFile/" + splitted[0] + "/" + splitted[1] + "/" + Convert.ToString(i), base64data.ElementAt(i - 1) , progressindicator, cts.Token);

                }
                addToSessionHistory("Upload File:", "File succesfully uploaded. Name = " + name, false);
                //MessageBox.Show(resultb);
                //Debug.WriteLine(resultb);
                getdata();

            }
        }

        private async void btnDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                Item item = new Item();
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
                    addToSessionHistory("Delete:", "File hes been succesfully deleted", false);
                    getdata();
                }
                else
                {
                    addToSessionHistory("Delete:", "File could not be deleted", true);
                }
                lstFiles.ClearSelected();
            }
            catch (Exception ex)
            {
                addToSessionHistory("Delete:", "Server Unreachable", true);
            }
        }

        private async Task<string> GetFiles()
        {
            var progressindicator = new Progress<int>(ReportProgress);
            cts = new CancellationTokenSource();
            string json = "for later implementation of users";

            string result = await servercom.ReceiveDataAsync("GetFileNames", json, progressindicator, cts.Token);
            //MessageBox.Show(result);

            return result;
        }


        private void loginButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            if (Properties.Settings.Default.Token != Convert.ToString(new Guid()))
            {
                loginButton.Enabled = false;
                logoutButton.Enabled = true;

                btnDelen.Enabled = true;
                //btnDelen.Visible = true; //maak de delen knop ook onzichtbaar
                btnDeleteItem.Enabled = true;
                //btnDeleteItem.Visible = true; //maak de knop ook onzichtbaar
                btnUpload.Enabled = true;
                //btnUpload.Visible = true; //maak de knop ook onzichtbaar
                btnWijzigen.Enabled = true;
                btnWijzigen.Visible = true; //maak de knop ook onzichtbaar
                addToSessionHistory("Login:", "Logged in succesfully.", false);
                getdata();

                if (LoginForm.CurrentUser.name == "Admin")
                {
                    btnAdmin.Enabled = true;
                    btnAdmin.Visible = true;
                }
            }
            else { addToSessionHistory("Login:", "Could not login", true); }
            if (loggedInUserName != null)
            {

                this.Text = "LAN-FileShare @" + loggedInUserName;
            }
        }

        private async void logoutButton_Click(object sender, EventArgs e)
        {
            var progressindicator = new Progress<int>(ReportProgress);
            cts = new CancellationTokenSource();
            
            string result = await servercom.ReceiveDataAsync("Logout", "", progressindicator, cts.Token);
            result = jsoncode.cropString(result);
            User userResponse = jsoncode.JsonDeCodingUser(result);
            Properties.Settings.Default.Token = userResponse.token.ToString();
            if (Properties.Settings.Default.Token != "00000000-0000-0000-0000-000000000000")
            {

                addToSessionHistory("Logout:", "Something went wrong while you were trying to log out.", true);
                
            }
            else
            {
                addToSessionHistory("Logout:", "Logged out succesfully.", false);
                getdata();
                loginButton.Enabled = true;
                logoutButton.Enabled = false;
                btnAdmin.Enabled = false;
                btnAdmin.Visible = false;
                btnDelen.Enabled = false;
                //btnDelen.Visible = false; //maak de delen knop ook onzichtbaar
                btnDeleteItem.Enabled = false;
                //btnDeleteItem.Visible = false; //maak de knop ook onzichtbaar
                btnUpload.Enabled = false;
                //btnUpload.Visible = false; //maak de knop ook onzichtbaar
                btnWijzigen.Enabled = false;
                btnWijzigen.Visible = false; //maak de knop ook onzichtbaar
                loggedInUserName = "Guest";
                this.Text = "LAN-FileShare @" + loggedInUserName;
            }
        }

        private static string _fileId;
        public static string fileId
        {
            get // this makes you to access value in form2
            {
                return _fileId;
            }
            set // this makes you to change value in form2
            {
                _fileId = value;
            }
        }

        private void btnDelen_Click(object sender, EventArgs e)
        {
            if (lstFiles.SelectedItem != null)
            {
                var item = Convert.ToString(lstFiles.SelectedValue);
                DeelVenster delen = new DeelVenster(item);
                fileId = Convert.ToString(lstFiles.SelectedValue);
                delen.ShowDialog();
            }else
            {
                MessageBox.Show("Je moet een bestand selecteren!", "Delen");
            }
            

        }

        private void addToSessionHistory(string action, string description, bool error) {
            Color titleColor = new Color();
            if (error == false)
            {
                titleColor = Color.Green;
            }
            else
            {
                titleColor = Color.Red;
            }
            rtxtHistory.SelectionStart = 0;
            rtxtHistory.SelectionLength = 0;
            rtxtHistory.SelectionColor = titleColor;
            rtxtHistory.SelectionFont = new Font(rtxtHistory.Font, FontStyle.Bold);
            rtxtHistory.SelectedText = action + Environment.NewLine;
            rtxtHistory.SelectionColor = Color.Black;
            rtxtHistory.SelectionFont = new Font(rtxtHistory.Font, FontStyle.Regular);
            rtxtHistory.SelectedText = description + Environment.NewLine + Environment.NewLine;
        }

        private void btnWijzigen_Click(object sender, EventArgs e)
        {
            GebruikerWijzigen wijzigen = new GebruikerWijzigen();
            wijzigen.ShowDialog();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            AdminPage admin = new AdminPage();
            admin.ShowDialog();
        }

    }
}
