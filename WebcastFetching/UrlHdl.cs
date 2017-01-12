using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WebcastFetching
{
    class UrlHdl
    {
        public string Url;

        public string Result;

        public UrlHdl(string OriginalUrl)
        {
            Url = OriginalUrl;
            Result = String.Empty;
        }

        public void HttpGetText()
        {
            StreamReader myStreamReader = new StreamReader(HttpGetRaw(Url), Encoding.GetEncoding("utf-8"));
            Result = myStreamReader.ReadToEnd();
            myStreamReader.Close();
        }

        public static Stream HttpGetRaw(string OriginalUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(OriginalUrl);
            request.Method = "Get";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.GetResponseStream();
        }
    }
}
