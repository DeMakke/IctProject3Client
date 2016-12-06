using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ictProject3
{
    public partial class DeelVenster : Form
    {
        private string selectedItem;
        private List<Gebruiker> lijstToevoegen = new List<Gebruiker>();

        public DeelVenster(string item)
        {
            InitializeComponent();
            getUsers();
            selectedItem = item;

        }

        public WebCom servercom = new WebCom();
        private CancellationTokenSource cts;

        JsonCode jsoncode = new JsonCode();
        Base64Code base64code = new Base64Code();
        Data data = new Data();
        public FileHandling fileHandler = new FileHandling();

        private void DeelVenster_Load(object sender, EventArgs e)
        {
            //checkBoxPubliekDelen.Checked = true;
        }

        private void ReportProgress(int value)
        {
            //progressreport
        }

        private void btnToevoegen_Click(object sender, EventArgs e)
        {
            foreach (Gebruiker user in lstGebruikers.SelectedItems)
            {
                lijstToevoegen.Add(user);
                lstGeselecteerdeGebruikers.Items.Add(user.name);
            }
        }

        private void btnVerwijderen_Click(object sender, EventArgs e)
        {
            if (lstGeselecteerdeGebruikers.Items.Count != 0)
            {
                lijstToevoegen.RemoveAt(lstGeselecteerdeGebruikers.SelectedIndex);
                lstGeselecteerdeGebruikers.Items.Clear();
                foreach (Gebruiker user in lijstToevoegen)
                {
                    lstGeselecteerdeGebruikers.Items.Add(user.name);
                }
            }
        }

        public async void getUsers()
        {
            var progressindicator = new Progress<int>(ReportProgress);
            cts = new CancellationTokenSource();            
            string json = "for later implementation of users";
            string result = await servercom.ReceiveDataAsync("GetUsers", json, progressindicator, cts.Token);
            List<Gebruiker> userList = new List<Gebruiker>();
            result = jsoncode.cropString(result);
            result = result.Remove(result.Length - 1);
            result = result.Remove(0, 18);
            userList = jsoncode.Deserialize<List<Gebruiker>>(result);
            string username = LoginForm.CurrentUser.name;
            if ((userList.Find(x => x.name.Contains(username)) != null))
            {
                userList.Remove(userList.Find(x => x.name.Contains(username)));
            }
            lstGebruikers.DataSource = userList;
            lstGebruikers.DisplayMember = "name";
            lstGebruikers.ValueMember = "id";
            lstGebruikers.Refresh();
            lstGebruikers.Update();           
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            if (checkBoxPubliekDelen.Checked == true)
            {
                try
                {
                    string fileid = Form1.fileId;

                    var progressindicator = new Progress<int>(ReportProgress);
                    cts = new CancellationTokenSource();
                    string result = "";
                    string json = "";
                    result = await servercom.ReceiveDataAsync("PublicShare/" + fileid, json, progressindicator, cts.Token);

                    result = jsoncode.cropStringMore(result);
                    result = result.Remove(0, 21);
                    result = result.Remove(result.Length - 1);
                    Succes succes = jsoncode.JsonDeCodingSucces(result);
                    if (succes.value)
                    {
                        MessageBox.Show("Het bestand is succesvol gedeeld.", "publiek delen");

                    }
                    else
                    {
                        MessageBox.Show("Het bestand kan niet gedeeld worden!", "publiek delen");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("fout bij delen", "database");
                }
            }
            else
            {
                lstGeselecteerdeGebruikers.DataSource = lijstToevoegen;
                lstGeselecteerdeGebruikers.DisplayMember = "name";
                lstGeselecteerdeGebruikers.ValueMember = "id";
                lstGeselecteerdeGebruikers.Refresh();
                lstGeselecteerdeGebruikers.Update();
                try
                {

                    string fileid = Form1.fileId;
                    List<Gebruiker> selectedUserList = new List<Gebruiker>();
                    selectedUserList = lstGeselecteerdeGebruikers.Items.Cast<Gebruiker>().ToList();//nog testen of dit werkt

                    JsonCode jsonCode = new JsonCode();

                    string json = jsonCode.Serialize(selectedUserList);

                    var progressindicator = new Progress<int>(ReportProgress);
                    cts = new CancellationTokenSource();
                    string result = "";
                    result = await servercom.ReceiveDataAsync("SetUsers/" + fileid, json, progressindicator, cts.Token);

                    result = jsoncode.cropString(result);
                    result = result.Remove(result.Length - 1);
                    result = result.Remove(0, 18);
                    Succes succes = new Succes();
                    succes = jsoncode.Deserialize<Succes>(result);

                    //result = jsoncode.cropString(result);
                    //Succes succes2 = jsoncode.Deserialize<Succes>(result);

                    if (succes.value == true)
                    {
                        MessageBox.Show("De gebruikers met toegang tot het bestand werden succesvol opgeslagen.", "Bestandsrechten");
                    }
                    else
                    {
                        MessageBox.Show("De veranderingen konden niet opgeslagen worden!", "Bestandsrechten");
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("system error on function: Share file");
                }
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxPubliekDelen_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPubliekDelen.Checked == true)
            {
                lstGebruikers.Enabled = false;
                lstGeselecteerdeGebruikers.Enabled = false;
                btnToevoegen.Enabled = false;
                btnVerwijderen.Enabled = false;
            }
            else
            {
                lstGebruikers.Enabled = true;
                lstGeselecteerdeGebruikers.Enabled = true;
                btnToevoegen.Enabled = true;
                btnVerwijderen.Enabled = true;
            }
        }
    }
}
