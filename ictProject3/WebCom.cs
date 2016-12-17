using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ictProject3
{
    public class WebCom
    {
        string jsonDataToSend = "";
        string prevresult = "";
        string newresult = "";

        public async Task<string> ReceiveDataAsync(string connectionstring, string json, IProgress<int> progress, CancellationToken ct)
        {
            string result = await Task.Run<string>(() =>
            {
            jsonDataToSend = json;
            prevresult = "";
            newresult = "";
            //Debug.WriteLine(json);
            Uri defury = new Uri("http://localhost:8000/WebService.svc/Json/" + connectionstring + "/" + Properties.Settings.Default.Token);
            //HttpClient httpclient = new HttpClient();
            //httpclient.BaseAddress = defury;
            //StringContent content = new StringContent("test");

            //    httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            //    httpclient.DefaultRequestHeaders.Accept.Clear();
            //httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue();
            //var x = httpclient.PostAsync("Json / " + connectionstring + " / " + Properties.Settings.Default.Token,content);
            //    var y = x;  
            //    while (x == y)
            //    {

            //    }
                HttpWebRequest defRequest = (HttpWebRequest)HttpWebRequest.Create(defury);
                defRequest.Method = "POST";
                //var task = httpclient.PostAsXmlAsync<DeviceRequest>("api/SaveData", request);
                IAsyncResult resultOverUsed = (IAsyncResult)defRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), defRequest); // hier json meegeven ?


                while (prevresult == newresult)
                {

                }

                return newresult;
            }, ct);
            return result;
        }

        private void GetRequestStreamCallback(IAsyncResult callbackResult)
        {
            HttpWebRequest defRequest = (HttpWebRequest)callbackResult.AsyncState;

            Stream postStream = defRequest.EndGetRequestStream(callbackResult);

            byte[] byteArray = Encoding.UTF8.GetBytes(jsonDataToSend);

            postStream.Write(byteArray, 0, byteArray.Length);
            postStream.Dispose();


            defRequest.BeginGetResponse(new AsyncCallback(getResponseStreamCallback), defRequest);

        }

        private void getResponseStreamCallback(IAsyncResult callbackResultaat)
        {
            //string result = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)callbackResultaat.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(callbackResultaat);

                using (StreamReader httpstreamreader = new StreamReader(response.GetResponseStream()))
                {
                    newresult = httpstreamreader.ReadToEnd();



                    // Debug.WriteLine(result);
                }

            }
            catch (Exception e)
            {
                // throw;
            }


        }
    }
}
