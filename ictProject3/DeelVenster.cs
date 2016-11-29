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
            lstGeselecteerdeGebruikers.Items.Add(lstGebruikers.SelectedItems);
            lstGebruikers.SelectedItems.Clear();
        }

        private void btnVerwijderen_Click(object sender, EventArgs e)
        {
            lstGebruikers.Items.Add(lstGeselecteerdeGebruikers.SelectedItems);
            lstGeselecteerdeGebruikers.SelectedItems.Clear();
            
        }

        public async void getUsers()
        {
            var progressindicator = new Progress<int>(ReportProgress);
            cts = new CancellationTokenSource();            
            string json = "for later implementation of users";
            string result = await servercom.ReceiveDataAsync("users", json, progressindicator, cts.Token);
            List<Gebruiker> UserList = new List<Gebruiker>();
            UserList = jsoncode.Deserialize<List<Gebruiker>>(result);
            lstGebruikers.DataSource = UserList;
            lstGebruikers.DisplayMember = "name";
            lstGebruikers.ValueMember = "id";
            lstGebruikers.Refresh();
            lstGebruikers.Update();           
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Form1.fileId;
                List<Gebruiker> selectedUserList = new List<Gebruiker>();
                selectedUserList = lstGeselecteerdeGebruikers.Items.Cast<Gebruiker>().ToList();//nog testen of dit werkt

                UserList userList = new UserList();
                JsonCode jsonCode = new JsonCode();

                userList.users = selectedUserList;
                string json = jsonCode.JsonCodingUserList(userList);

                var progressindicator = new Progress<int>(ReportProgress);
                cts = new CancellationTokenSource();
                string result = "";
                result = await servercom.ReceiveDataAsync("SetUsers/" + id, json, progressindicator, cts.Token);

                result = jsoncode.cropString(result);
                Succes succes = jsoncode.JsonDeCodingSucces(result);
                if (succes.value)
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
                MessageBox.Show("system error on function: Delete File");
            }

            this.Close();
        }
    }
}
