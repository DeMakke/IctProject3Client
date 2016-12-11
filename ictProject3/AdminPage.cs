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

        private void btnGebrToevoegen_Click(object sender, EventArgs e)
        {
            bool id = false;
            string userid = Convert.ToString(new Guid());
            Gebruiker nieuweGebruiker = new Gebruiker();
            if ((txtPasswordNew.Text==txtPasswordConfirm.Text) && (txtName.Text != null))
            {
                while (id == false)
                {
                    if ((userList.Find(x => x.id.Contains(userid)) != null))
                    {
                        userid = Convert.ToString(new Guid());
                        id = false;
                    }
                    else
                    {
                        id = true;
                        nieuweGebruiker.id = userid;
                    }
                }
                txtPasswordNew.ForeColor = Color.Black;
                txtPasswordConfirm.ForeColor = Color.Black;
                nieuweGebruiker.password = txtPasswordConfirm.Text;
                nieuweGebruiker.name = txtName.Text;
                //////////////////////////////////////////////////////////////////
                //////////HIER CODE VOOR INSERT TE ZENDEN NAAR SERVER/////////////
                //////////////////////////////////////////////////////////////////
            }
            else
            {
                txtPasswordNew.ForeColor = Color.Red;
                txtPasswordConfirm.ForeColor = Color.Red;
                MessageBox.Show("De Opgegeven gegevens zijn niet volledig.", "Gegevens incorrect");
            }
        }

        private void lstGebruikers_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var g in userList)
            {
                if (g.id == Convert.ToString(lstGebruikers.SelectedValue))
                {
                    txtName.Text = g.name;
                    txtId.Text = g.id;
                    //////////////////////////////////////////////////////////////////
                    //////////HIER CODE VOOR WW OP TEVRAGEN BIJ DE SERVER/////////////
                    //////////////////////////////////////////////////////////////////
                    txtPasswordOld.Text = g.password;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if ((txtName.Text!=null) || (txtPasswordNew.Text==txtPasswordConfirm.Text))
            {
                txtPasswordNew.ForeColor = Color.Black;
                txtPasswordConfirm.ForeColor = Color.Black;
                /////////////////////////////////////////////////////////
                //////////HIER CODE VOOR UPDATE OP DE SERVER/////////////
                /////////////////////////////////////////////////////////
            }
            else
            {
                txtPasswordNew.ForeColor = Color.Red;
                txtPasswordConfirm.ForeColor = Color.Red;
                MessageBox.Show("De Opgegeven gegevens zijn niet volledig.", "Gegevens incorrect");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string TeVerwijderenId = txtId.Text;
            //////////////////////////////////////////////////////////////////
            //////////HIER CODE VR HET VERWIJDEREN NAAR DE SERVER/////////////
            //////////////////////////////////////////////////////////////////
        }
    }
}
