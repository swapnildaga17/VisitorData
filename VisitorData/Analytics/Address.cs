using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisitorData.Analytics
{
    public class Address
    {
        public string City { get; set; }
        public string State { get; set; }
        public string StateCode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitute { get; set; }
    }
}