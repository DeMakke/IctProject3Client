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

namespace ictProject3
{
    public partial class AdminPage : Form
    {
        public AdminPage()
        {
            InitializeComponent();
            getUsers();
        }

        private void AdminPage_Load(object sender, EventArgs e)
        {

        }
        private void ReportProgress(int value)
        {
            //progressreport
        }
        public WebCom servercom = new WebCom();
        private CancellationTokenSource cts;
        JsonCode jsoncode = new JsonCode();
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
            lstGebruikers.DataSource = userList;
            lstGebruikers.DisplayMember = "name";
            lstGebruikers.ValueMember = "id";
            lstGebruikers.Refresh();
            lstGebruikers.Update();
        }

        private void btnGebrToevoegen_Click(object sender, EventArgs e)
        {

        }
    }
}
