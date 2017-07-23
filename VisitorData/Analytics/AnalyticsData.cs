using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisitorData.Analytics
{
    public class AnalyticsData  : TableEntity
    {

        public AnalyticsData(string productId, string diseaseId)
        {
            this.PartitionKey = productId;
            this.RowKey = Guid.NewGuid().ToString();
            //this.Timestamp = DateTime.UtcNow;
        }

        public AnalyticsData() { }

        public string DiseaseId { get; set; }
        public string ProductId { get; set; }

        public string DiseaseName { get; set; }
        public string ProductName { get; set; }

        public string AbsoluteUri { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string StateCode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitute { get; set; }
        public DateTime Date { get; set; }


        //public Address Location { get; set; }

        public string IpAddress { get; set; }
    }

   
}