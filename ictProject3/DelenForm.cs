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
    public partial class DelenForm : Form
    {
        private string selectedItem;
        public DelenForm(string item)
        {
            InitializeComponent();
            selectedItem = item;
        }
        public WebCom servercom = new WebCom();
        private CancellationTokenSource cts;

        JsonCode jsoncode = new JsonCode();
        Base64Code base64code = new Base64Code();
        Data data = new Data();
        public FileHandling fileHandler = new FileHandling();

        private void DelenForm_Load(object sender, EventArgs e)
        {
            checkBoxPubliekDelen.Checked = true;
        }

        private async void buttonAccept_Click(object sender, EventArgs e)
        {
            try
            {
                bool test = checkBoxPubliekDelen.Checked;
                if (test == true)
                {
                    var progressindicator = new Progress<int>(ReportProgress);
                    cts = new CancellationTokenSource();
                    string result = "";
                    result = await servercom.ReceiveDataAsync("PublicShare", Convert.ToString(selectedItem), progressindicator, cts.Token);

                    result = jsoncode.cropString(result);
                    Succes succes = jsoncode.JsonDeCodingSucces(result);
                    if (succes.value)
                    {
                        MessageBox.Show("Het bestand is succesvol gedeeld.", "publiek delen");

                    }
                    else
                    {
                        MessageBox.Show("Het bestand kan niet gedeeld worden!", "publiek delen");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("fout bij delen");
            }
            finally
            {
                this.Close();
            }
        }

        private void ReportProgress(int value)
        {
            //progressreport
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
