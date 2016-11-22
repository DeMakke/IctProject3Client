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
                User user = new User();
                user.name = userNameTextBox.Text;
                string password = passwordTextBox.Text;
                using (MD5 md5Hash = MD5.Create())
                {
                    user.hash = hashing.GetMd5Hash(md5Hash, password);
                }
                string json = JSONData.JsonCoding(user);

                var progressindicator = new Progress<int>(ReportProgress);
                cts = new CancellationTokenSource();

                string result = "";
                result = await servercom.ReceiveDataAsync("ValidateUser", json, progressindicator, cts.Token);

                result = JSONData.cropString(result);
                User userResponse = JSONData.JsonDeCodingUser(result);
                if (userResponse.token != 9999)
                {
                    MessageBox.Show("De gebruiker is met succes ingelogd. ID= " + userResponse.token.ToString(), "Inloggen gebruiker");
                    Properties.Settings.Default.Token = userResponse.token.ToString();
                }
                else
                {
                    MessageBox.Show("De gebruiker is niet ingelogd!", "Inloggen gebruiker");
                    //else save token for authentication processes and close loginForm & append Form1's name with [userName]
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("system error on function: Inloggen gebruiker" + ex.ToString());
            }

            //make AuthenticateUser function that also sets the Form1's name for 'logged in user' with their name and deletes the token from local system


            this.Close();
        }

 





    }
}
