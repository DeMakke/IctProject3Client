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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        WebCom servercom = new WebCom();
        Md5Class hashing = new Md5Class();
        private CancellationTokenSource cts;

        private void ReportProgress(int value)
        {
            //progressreport
        }

        private void loginCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void loginValidationButton_Click(object sender, EventArgs e)
        {
            try
            {
                JsonCode JSONData = new JsonCode();
                //User user = new User();
                _user = new User();
                CurrentUser.name = userNameTextBox.Text;
                string password = passwordTextBox.Text;

                if (password != "" && password != null && CurrentUser.name != "" && CurrentUser.name != null)
                {
                    using (MD5 md5Hash = MD5.Create())
                    {
                        CurrentUser.hash = hashing.GetMd5Hash(md5Hash, password);
                    }
                    string json = JSONData.JsonCoding(CurrentUser);

                    var progressindicator = new Progress<int>(ReportProgress);
                    cts = new CancellationTokenSource();

                    string result = "";
                    result = await servercom.ReceiveDataAsync("ValidateUser", json, progressindicator, cts.Token);

                    if (result == "An error has occured.")
                    {

                    }
                    else
                    {
                        result = JSONData.cropString(result);
                        User userResponse = JSONData.JsonDeCodingUser(result);
                        if (userResponse.token != new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"))
                        {
                            //MessageBox.Show("De gebruiker is met succes ingelogd. ID= " + userResponse.token.ToString(), "Inloggen gebruiker");
                            Properties.Settings.Default.Token = userResponse.token.ToString();
                            Form1.loggedInUserName = userResponse.name;
                        }
                        else
                        {
                            MessageBox.Show("De gebruiker is niet ingelogd!", "Inloggen gebruiker");
                            //else save token for authentication processes and close loginForm & append Form1's name with [userName]
                        }

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Ongeldige gebruikersnaam en/of paswoord!");
                    this.Close();
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("system error on function: Inloggen gebruiker" + ex.ToString());
                this.Close();
            }

            //make AuthenticateUser function that also sets the Form1's name for 'logged in user' with their name and deletes the token from local system


            
        }

        private static User _user;
        public static User CurrentUser
        {
            get // this makes you to access value in form2
            {
                return _user;
            }
            set // this makes you to change value in form2
            {
                _user = value;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
