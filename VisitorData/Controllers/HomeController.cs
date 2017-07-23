using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisitorData.Analytics;

namespace VisitorData.Controllers
{
    [AnalyticsFilter]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           // IPHostGenerator gen = new IPHostGenerator();
            string ipaddress = IPHostGenerator.GetVisitorDetails();
            ipaddress = "59.94.121.148";
            //string ip = ipaddress.Split(new char[] { ':' })[1];
            FreeGeoIpResponse location = IPHostGenerator.GetLocation(ipaddress);
            return View(location);


            //string res = "http://ipinfo.io/" + ipaddress + "/city";
            //string ipResponse = gen.IPRequestHelper(res);
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}