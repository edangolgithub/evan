using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace console
{
    static class  Dynamodb
    {
        static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        static string tablename = "ProductCatalog";
        public static async Task RunAsync()
        {
           // await ListTables();
          // await PutItem();
          await GetItem();
        }
        public static async Task ListTables()
        {
            var tables = await client.ListTablesAsync();
            foreach (var item in tables.TableNames)
            {
                Console.WriteLine(item);
            }
        }
        public static async Task PutItem()
        {
            var request = new PutItemRequest
            {
                TableName = "ProductCatalog",
                Item = new Dictionary<string, AttributeValue>{
                { "Id", new AttributeValue { N = "100" }},
                { "Title", new AttributeValue { S = "Rom Jul" }},
                { "ISBN", new AttributeValue { S = "123456789" }},
                { "Price", new AttributeValue { S = "900.00" }},
                {
                    "Authors",
                    new AttributeValue
                    { SS = new List<string>{"Evan", "akshay"}}
                }
                }
            };
            await client.PutItemAsync(request);
        }
        public static async Task GetItem()
        {
            var request = new GetItemRequest
            {
                TableName = tablename,
                Key = new Dictionary<string, AttributeValue>()
                 { { "Id", new AttributeValue { N = "100" } } },
            };
            var response = await client.GetItemAsync(request);

            // Check the response.
            var result = response.Item;
            foreach (var item in result)
            {
               
                Console.WriteLine(item.Key+"---"+ item.Value.Resolveattributevalue());
               
            }
        }
        public static async Task getTransaction()
        {


            var request = new QueryRequest
            {
                TableName = "Transaction",
                KeyConditionExpression = "accountid = :v_Id",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                 {":v_Id", new AttributeValue { S =  "123" }}}
            };

            var response = await client.QueryAsync(request);

            // Check the response.
            var result = response.Items;
            Console.WriteLine(result);
            foreach (var item in result)
            {
                foreach (KeyValuePair<string, AttributeValue> kvp in item)
                {
                    string attributeName = kvp.Key;
                    AttributeValue value = kvp.Value;

                    Console.WriteLine(
                        attributeName + " " +
                      (value.S == null ? "" : value.S) +
               (value.N == null ? "" : value.N.ToString()) +
               (value.B == null ? "" : value.B.ToString()) +
               (value.SS == null ? "" : string.Join(",", value.SS.ToArray())) +
               (value.NS == null ? "" : string.Join(",", value.NS.ToArray()))
             +
            (value.BS == null ? "" : string.Join(",", (object[])value.BS.ToArray()))
           );
                }
                Console.WriteLine("************************************************");
            }
        }
        public static object Resolveattributevalue(this AttributeValue value)
        {
           return  (value.S == null ? "" : value.S) +
               (value.N == null ? "" : value.N.ToString()) +
               (value.B == null ? "" : value.B.ToString()) +
               (value.SS == null ? "" : string.Join(",", value.SS.ToArray())) +
               (value.NS == null ? "" : string.Join(",", value.NS.ToArray()))
             +
            (value.BS == null ? "" : string.Join(",", (object[])value.BS.ToArray()));
        }
    }
}


