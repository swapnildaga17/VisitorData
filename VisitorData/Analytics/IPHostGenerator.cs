using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Xml;

namespace VisitorData.Analytics
{
    public static class IPHostGenerator
    {
        internal static string GetCurrentPageUrl()
        {
            return HttpContext.Current.Request.Url.AbsoluteUri;
        }
        internal static string GetVisitorDetails()
        {
            //string host = HttpContext.Current.Request.Url.Host.ToLower();
            if (HttpContext.Current.Request.IsLocal)
                return "127.0.0.1";
            string varIPAddress = string.Empty;
            string varVisitorCountry = string.Empty;
            string varIpAddress = string.Empty;
            string source = string.Empty;
            //varIpAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(varIpAddress))
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    source = "HTTP_X_FORWARDED_FOR";
                    varIpAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
            }
   
            if (string.IsNullOrEmpty(varIPAddress))
            {
                if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                {
                    source = "REMOTE_ADDR";
                    varIpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
            }
            //varIPAddress = Request.ServerVariables["REMOTE_ADDR"];    
            return varIpAddress;
        }



        //internal DataTable GetLocation(string varIPAddress)
        //{
        //    WebRequest varWebRequest = WebRequest.Create("http://freegeoip.net/json/" + varIPAddress);
        //    WebProxy px = new WebProxy("http://freegeoip.net/json/" + varIPAddress, true);
        //    varWebRequest.Proxy = px;
        //    varWebRequest.Timeout = 2000;
        //    try
        //    {
        //        WebResponse rep = varWebRequest.GetResponse();
        //        XmlTextReader xtr = new XmlTextReader(rep.GetResponseStream());
        //        DataSet ds = new DataSet();
        //        ds.ReadXml(xtr);
        //        return ds.Tables[0];
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        internal static FreeGeoIpResponse GetLocation(string ipAddress)
        {
            try
            {
                if (ipAddress.Equals("127.0.0.1"))
                {
                    FreeGeoIpResponse response = new FreeGeoIpResponse();
                    response.ip = ipAddress;
                    return response;
                }

                //string response = "";
                string url = "http://freegeoip.net/json/" + ipAddress;
                using (var syncClient = new WebClient())
                {
                    var response = syncClient.DownloadString(url);
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(FreeGeoIpResponse));
                    using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(response)))
                    {
                        var locationResponse = (FreeGeoIpResponse)serializer.ReadObject(ms);
                        return locationResponse;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }

            //return null;
        }

        internal static string GetMachineNameUsingIPAddress(string varIpAdress)
        {
            string machineName = string.Empty;
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(varIpAdress);

                machineName = hostEntry.HostName;
            }
            catch (Exception ex)
            {
                // Machine not found...    
            }
            return machineName;
        }

        internal static string IPRequestHelper(string url)
        {

            string checkURL = url;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            StreamReader responseStream = new StreamReader(objResponse.GetResponseStream());
            string responseRead = responseStream.ReadToEnd();
            responseRead = responseRead.Replace("\n", String.Empty);
            responseStream.Close();
            responseStream.Dispose();
            return responseRead;
        }
    }
}