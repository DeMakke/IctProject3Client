using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
                Uri defury = new Uri("http://localhost:8000/WebService.svc/Json/" + connectionstring);
                HttpWebRequest defRequest = (HttpWebRequest)HttpWebRequest.Create(defury);
                defRequest.Method = "POST";
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
