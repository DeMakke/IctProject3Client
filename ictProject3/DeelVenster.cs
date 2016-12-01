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
        public DeelVenster()
        {
            InitializeComponent();
            getUsers();

        }

        public WebCom servercom = new WebCom();
        private CancellationTokenSource cts;

        JsonCode jsoncode = new JsonCode();
        Base64Code base64code = new Base64Code();
        Data data = new Data();
        public FileHandling fileHandler = new FileHandling();

        private void ReportProgress(int value)
        {
            //progressreport
        }

        private void btnToevoegen_Click(object sender, EventArgs e)
        {
            List<Gebruiker> templist = new List<Gebruiker>();
            
            foreach (Gebruiker item in lstGebruikers.SelectedItems)
            {
                templist.Add(item);

                lstGeselecteerdeGebruikers.DataSource = templist;
                lstGeselecteerdeGebruikers.DisplayMember = "name";
                lstGeselecteerdeGebruikers.ValueMember = "id";
                lstGeselecteerdeGebruikers.Refresh();
                lstGeselecteerdeGebruikers.Update();
                
                
            }
            lstGebruikers.ClearSelected();
        }

        private void btnVerwijderen_Click(object sender, EventArgs e)
        {
            foreach (Gebruiker item in lstGeselecteerdeGebruikers.SelectedItems)
            {
                lstGebruikers.Items.Add(item.name);
            }
            lstGeselecteerdeGebruikers.ClearSelected();
        }

        public async void getUsers()
        {
            var progressindicator = new Progress<int>(ReportProgress);
            cts = new CancellationTokenSource();            
            string json = "for later implementation of users";
            string result = await servercom.ReceiveDataAsync("GetUsers", json, progressindicator, cts.Token);
            List<Gebruiker> userList = new List<Gebruiker>();
            //GetUserResult userList = new GetUserResult();
            //result = result.Replace("\\\"", "");
            //userList = jsoncode.JsonDeCodingUserList(result);

            Gebruiker test = new Gebruiker();
            test.id = "4EC5E708-5817-4DE1-B507-98A44301CDC1";
            test.name = "joris";
            userList.Add(test);
            lstGebruikers.DataSource = userList;
            lstGebruikers.DisplayMember = "name";
            lstGebruikers.ValueMember = "id";
            lstGebruikers.Refresh();
            lstGebruikers.Update();           
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
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
                Succes succes = jsoncode.JsonDeCodingSucces(result);

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

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
