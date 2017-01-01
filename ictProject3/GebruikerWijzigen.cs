using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ictProject3
{
    public partial class GebruikerWijzigen : Form
    {
        private CancellationTokenSource cts;
        public WebCom servercom = new WebCom();
        public JsonCode jsonCode = new JsonCode();
        public User gebruiker = new User();
        public Succes succes = new Succes();
        public Md5Class hashing = new Md5Class();
        private string result = "";

        public GebruikerWijzigen()
        {
            InitializeComponent();
            
        }

        private void ReportProgress(int value)
        {
            //progressreport
        }

        private void btnAnnuleren_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnOpslaan_Click(object sender, EventArgs e)
        {
            var progressindicator = new Progress<int>(ReportProgress);
            cts = new CancellationTokenSource();
            bool passwordChangeSet = false;
            bool nameChangeSet = false;

            if (passwordTextBox.Text == RepeatPasswordTextBox.Text)
            {
                if (userNameTextBox.Text != "Admin")
                {
                    if(oldPasswordTextBox.Text != "" && passwordTextBox.Text != "" && RepeatPasswordTextBox.Text != "")
                    {
                        try
                        {
                            //validate user
                            using (MD5 md5Hash = MD5.Create())
                            {
                                LoginForm.CurrentUser.hash = hashing.GetMd5Hash(md5Hash, oldPasswordTextBox.Text);
                            }
                            string json = jsonCode.JsonCoding(LoginForm.CurrentUser);
                            result = await servercom.ReceiveDataAsync("ValidateUser", json, progressindicator, cts.Token);
                            result = jsonCode.cropString(result);
                            User userResponse = jsonCode.JsonDeCodingUser(result);

                            if (userResponse.token != new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"))//if password is correct
                            {
                                passwordChangeSet = true;
                            }
                            else
                            {
                                MessageBox.Show("Please enter the correct password to update", "Warning Password");
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else if(oldPasswordTextBox.Text == "" && passwordTextBox.Text == "" && RepeatPasswordTextBox.Text == "")//enkel naam wijzigen
                    {
                        nameChangeSet = true;
                    }
                    else
                    {
                        MessageBox.Show("You must enter all password fields to change your password", "Warning Password");
                    }
                }
                else
                {
                    MessageBox.Show("You can't change name to Admin", "Warning Admin Namechange");
                }
            }
            else
            {
                MessageBox.Show("Passwords must match!", "Warning Passwords");
            }

            try
            {
                if(nameChangeSet == true && passwordChangeSet == true)
                {
                    ChangeUserData();
                }
                else if(nameChangeSet == true)
                {
                    ChangeUserData();
                }
                else if(passwordChangeSet == true)
                {
                    ChangeUserData();
                }
                else
                {
                    MessageBox.Show("No changes were saved", "Warning");
                }
            }
            catch (Exception)
            {

            }
        }

        private async void ChangeUserData()
        {
            var progressindicator = new Progress<int>(ReportProgress);
            gebruiker.name = userNameTextBox.Text;
            gebruiker.password = passwordTextBox.Text;
            string json2 = jsonCode.Serialize(gebruiker);

            result = await servercom.ReceiveDataAsync("ChangeUserData", json2, progressindicator, cts.Token);

            result = jsonCode.cropString(result);
            succes = jsonCode.JsonDeCodingSucces(result);

            if (succes.value == true)
            {
                MessageBox.Show("Your changes were saved", "Change Login Data");
                Form1.loggedInUserName = userNameTextBox.Text;
                this.Close();
            }
            //else
            //{
            //    MessageBox.Show("The changes could not be saved!", "Change Login Data");
            //}
        }


    }
}
