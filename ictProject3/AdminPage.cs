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

        Base64Code base64code = new Base64Code();
        Data data = new Data();
        public FileHandling fileHandler = new FileHandling();

        public WebCom servercom = new WebCom();
        private CancellationTokenSource cts;
        JsonCode jsoncode = new JsonCode();
        List<Gebruiker> userList = new List<Gebruiker>();

        private void AdminPage_Load(object sender, EventArgs e)
        {

        }
        private void ReportProgress(int value)
        {
            //progressreport
        }
        public async void getUsers()
        {
            var progressindicator = new Progress<int>(ReportProgress);
            cts = new CancellationTokenSource();
            string json = "for later implementation of users";
            string result = await servercom.ReceiveDataAsync("GetUsers", json, progressindicator, cts.Token);
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

        private async void btnGebrToevoegen_Click(object sender, EventArgs e)
        {
            bool id = false;
            string userid = Convert.ToString(Guid.NewGuid());
            User user = new User();
            if ((txtPasswordNew.Text == txtPasswordConfirm.Text) && (txtName.Text != null))
            {
                while (id == false)
                {
                    if ((userList.Find(x => x.id.Contains(userid)) != null))
                    {
                        userid = Convert.ToString(Guid.NewGuid());
                        id = false;
                    }
                    else
                    {
                        id = true;
                        user.id = userid;
                    }
                }
                txtPasswordNew.ForeColor = Color.Black;
                txtPasswordConfirm.ForeColor = Color.Black;

                user.password = txtPasswordConfirm.Text;
                user.name = txtName.Text;

                JsonCode jsonCode = new JsonCode();
                var progressindicator = new Progress<int>(ReportProgress);
                cts = new CancellationTokenSource();
                
                string json = jsonCode.JsonCoding(user);
                string result = "";

                result = await servercom.ReceiveDataAsync("AddUser", json, progressindicator, cts.Token);

                result = jsoncode.cropStringMore(result);
                result = result.Remove(0, 17);
                result = result.Remove(result.Length - 1);
                Succes succes = jsoncode.JsonDeCodingSucces(result);

                getUsers();
                if (succes.value)
                {
                    MessageBox.Show("De gebruiker is succesvol toegevoegd.", "Gebruiker Toevoegen");
                }
                else
                {
                    MessageBox.Show("De gebruiker kan niet toegevoegd worden!", "Gebruiker Toevoegen");
                }

            }
            else
            {
                txtPasswordNew.ForeColor = Color.Red;
                txtPasswordConfirm.ForeColor = Color.Red;
                MessageBox.Show("De Opgegeven gegevens zijn niet volledig.", "Gegevens incorrect");
            }
        }

     

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            JsonCode jsonCode = new JsonCode();
            User user = new User();

            if ((txtName.Text != null) || (txtPasswordNew.Text == txtPasswordConfirm.Text))
            {

                user.id = txtId.Text;
                user.name = txtName.Text;
                user.password = txtPasswordConfirm.Text;

                txtPasswordNew.ForeColor = Color.Black;
                txtPasswordConfirm.ForeColor = Color.Black;
                var progressindicator = new Progress<int>(ReportProgress);
                cts = new CancellationTokenSource();

                string result = "";
                string json = jsonCode.JsonCoding(user);

                result = await servercom.ReceiveDataAsync("UpdateUser", json, progressindicator, cts.Token);

                result = jsoncode.cropStringMore(result);
                result = result.Remove(0, 20);
                result = result.Remove(result.Length - 1);
                Succes succes = jsoncode.JsonDeCodingSucces(result);

                getUsers();
                if (succes.value)
                {
                    MessageBox.Show("De gebruiker is succesvol aangepast.", "Gebruiker Toevoegen");
                }
                else
                {
                    MessageBox.Show("De gebruiker kan niet aangepast worden!", "Gebruiker Toevoegen");
                }

            }
            else
            {
                txtPasswordNew.ForeColor = Color.Red;
                txtPasswordConfirm.ForeColor = Color.Red;
                MessageBox.Show("De Opgegeven gegevens zijn niet volledig.", "Gegevens incorrect");
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            JsonCode jsonCode = new JsonCode();
            User user = new User();
            user.id = txtId.Text;

            var progressindicator = new Progress<int>(ReportProgress);
            cts = new CancellationTokenSource();

            string json = jsonCode.JsonCoding(user);
            string result = "";

            result = await servercom.ReceiveDataAsync("DeleteUser", json, progressindicator, cts.Token);

            result = jsoncode.cropStringMore(result);
            result = result.Remove(0, 20);
            result = result.Remove(result.Length - 1);
            Succes succes = jsoncode.JsonDeCodingSucces(result);

            getUsers();
            if (succes.value)
            {
                MessageBox.Show("De gebruiker is succesvol verwijderd.", "Gebruiker Verwijderen");
            }
            else
            {
                MessageBox.Show("De gebruiker kan niet verwijderd worden!", "Gebruiker Verwijderen");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstGebruikers_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            foreach (var g in userList)
            {
                if (g.id == Convert.ToString(lstGebruikers.SelectedValue))
                {
                    txtName.Text = g.name;
                    txtId.Text = g.id;
                }
            }
        }
    }
}
