
/**
 * Copyright 2010-2019 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 * This file is licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License. A copy of
 * the License is located at
 *
 * http://aws.amazon.com/apache2.0/
 *
 * This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 * CONDITIONS OF ANY KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using System.Linq;
using System.Diagnostics;


namespace console
{
    class DynoContext
    {
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();

        public static async Task RunAsync()
        {
            try
            {
                DynamoDBContext context = new DynamoDBContext(client);
                  await gettransactionq(context);
               // await gobalsecondarindex(context);
               


            }
            catch (Exception)
            {
               throw ;
            }
        }
        public async static Task gettransactionq(DynamoDBContext context, string accountid = "123")
        {
            try
            {
                DateTime twoWeeksAgoDate = DateTime.UtcNow - TimeSpan.FromDays(15);
                var latestRepliesserch =
                                context.QueryAsync<Transaction>(accountid, QueryOperator.GreaterThan, new List<object> { twoWeeksAgoDate });
                Console.WriteLine("\nFindRepliesInLast15Days: Printing result.....");
                var latestReplies = await latestRepliesserch.GetNextSetAsync();
                foreach (Transaction r in latestReplies)
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", r.accountid, r.created, r.balance, r.interest, r.accountname);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async static Task gobalsecondarindex(DynamoDBContext context)
        {
            try
            {
                QueryRequest queryRequest = new QueryRequest
                {
                    TableName = "Transaction",
                    IndexName = "latestindex",
                    KeyConditionExpression = "#dt = :p",
                    ExpressionAttributeNames = new Dictionary<String, String> {
        {"#dt", "islatest"}
    },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
        {":p", new AttributeValue { S =  "1" }}}
      ,
                    ScanIndexForward = true
                };

                var result = await client.QueryAsync(queryRequest);

                var items = result.Items;
                foreach (var currentItem in items)
                {
                    foreach (string attr in currentItem.Keys)
                    {
                        Console.Write(attr + "---> ");
                        if (attr == "Precipitation")
                        {
                            Console.WriteLine(currentItem[attr].N);
                        }
                        else
                        {
                            Console.WriteLine(currentItem[attr].S);
                        }

                    }
                    Console.WriteLine(items.Count);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public class Transaction
        {
            public string accountid { get; set; }
            public DateTime created { get; set; }
            public string accounttypeid { get; set; }
            public string type { get; set; }
            public string entry { get; set; }
            public double amount { get; set; }
            public double interest { get; set; }
            public double balance { get; set; }
            public string id { get; set; }
            public string accounttype { get; set; }
            public string islatest { get; set; }
            public string accountname { get; set; }
            public string address { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string isenabled { get; set; }

        }
    }
}