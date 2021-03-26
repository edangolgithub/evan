using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace console
{
    class Dynamodbdocument
    {
        public static async Task RunAsync()
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();

           Table table = await Task.Run(()=> Table.LoadTable(client, "Transaction"));
          // Document document = await table.GetItemAsync("123"); // Primary key 101.
         //   DateTime twoWeeksAgoDate = DateTime.UtcNow - TimeSpan.FromDays(15);
          //  RangeFilter filter = new RangeFilter(QueryOperator.GreaterThan, twoWeeksAgoDate);
         //   Search search = table.Query("DynamoDB Thread 2", filter); 
          
        }
    }
}


