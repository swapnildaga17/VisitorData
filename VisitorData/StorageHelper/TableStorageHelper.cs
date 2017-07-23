using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
namespace VisitorData.StorageHelper
{
    
    public class TableStorageHelper
    {
        public static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
        public static CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
        public string tableName;
        public static CloudTable table;
        public TableStorageHelper(string tableName) {
            if (!string.IsNullOrEmpty(tableName))
                this.tableName = tableName;
            else
                this.tableName = "curesdecodedAnalyticsData";
            table = tableClient.GetTableReference(this.tableName);
            table.CreateIfNotExists();
        }
        public void AddEntity<T>(T entity) where T: TableEntity{
            try
            {
                TableOperation insertOperation = TableOperation.Insert(entity);

                // Execute the insert operation.
                table.ExecuteAsync(insertOperation);
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        
    }
}