using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using VisitorData.Controllers;
using VisitorData.StorageHelper;

namespace VisitorData.Analytics
{
    public class AnalyticsFilter : ActionFilterAttribute
    {
        public TableStorageHelper tableStorageHelper = new TableStorageHelper(string.Empty);
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            AnalyticsData tableData = new AnalyticsData("1","1");
            tableData.DiseaseId = "1";
            tableData.DiseaseName = "Acne";
            //string diseaseId="1";
            if (filterContext.RouteData.Values.ContainsKey("diseaseId"))
                tableData.DiseaseId = (string)filterContext.RouteData.Values["diseaseId"];
          
            tableData.ProductId = "1";
            tableData.ProductName = "Abalone";
            if (filterContext.RouteData.Values.ContainsKey("productId"))
                tableData.ProductId = (string)filterContext.RouteData.Values["productId"];

           tableData.AbsoluteUri= filterContext.HttpContext.Request.Url.AbsoluteUri;
            //String time = DateTime.UtcNow.ToString();
            string ipaddress = IPHostGenerator.GetVisitorDetails();
            tableData.IpAddress = "59.94.121.148";
            ipaddress = "59.94.121.148";
            //string ip = ipaddress.Split(new char[] { ':' })[1];
            FreeGeoIpResponse location = IPHostGenerator.GetLocation(ipaddress);
            tableData.City = location.city;
            tableData.Country = location.country_name;
            tableData.CountryCode = location.country_code;
            tableData.State = location.region_name;
            tableData.StateCode = location.region_code;
            tableData.Latitude = location.latitude;
            tableData.Longitute = location.longitude;
            tableData.Timestamp=tableData.Date = DateTime.UtcNow;
            tableStorageHelper.AddEntity(tableData);


          

            //filterContext.HttpContext.Request.Browser.Crawler
            Debug.WriteLine(filterContext.HttpContext.Request.Url.AbsoluteUri);
            Debug.WriteLine("SomeText");
        }
    }
}