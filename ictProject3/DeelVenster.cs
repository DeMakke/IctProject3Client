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
        }

        private void btnVerwijderen_Click(object sender, EventArgs e)
        {
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            int id = Form1.fileId;
            List<Gebruiker> SelectedUserList = new List<Gebruiker>();            
            SelectedUserList = lstGeselecteerdeGebruikers.Items.Cast<Gebruiker>().ToList();//nog testen of dit werkt
            //JsonCoding()
            //lijst met gebruikers en de file id naar json omzetten en naar server sturen

            //indien succesvol
            this.Close();
            //anders foutmelding

        }
    }
}
