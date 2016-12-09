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
    public partial class GebruikerWijzigen : Form
    {
        private CancellationTokenSource cts;
        public WebCom servercom = new WebCom();

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
            try
            {
                JsonCode jsonCode = new JsonCode();
                Gebruiker gebruiker = new Gebruiker();
                gebruiker.name = userNameTextBox.Text;
                gebruiker.password = passwordTextBox.Text;
                string json = jsonCode.Serialize(gebruiker);

                var progressindicator = new Progress<int>(ReportProgress);
                cts = new CancellationTokenSource();
                string result = "";
                result = await servercom.ReceiveDataAsync("ChangeUserData", json, progressindicator, cts.Token);

                result = jsonCode.cropString(result);
                //result = result.Remove(result.Length-1);
                //result = result.Remove(0, 24);
                Succes succes = new Succes();
                succes = jsonCode.JsonDeCodingSucces(result);

                if (succes.value == true)
                {
                    MessageBox.Show("De gebruikersnaam werd succesvol aangepast", "Gegevens wijzigen");
                }
                else
                {
                    MessageBox.Show("De veranderingen konden niet opgeslagen worden!", "Gegevens wijzigen");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("system error on function: Change user settings");
            }

            this.Close();
        }
    }
}
