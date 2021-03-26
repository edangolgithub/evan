
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
using Amazon.Runtime;

namespace console
{
    class HighLevelQueryAndScan
    {
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();

      public  static async Task RunAsync()
        {
            try
            {
                DynamoDBContext context = new DynamoDBContext(client);
               
          //   await GetBook(context, 101);
               

                // Sample forum and thread to test queries.
                string forumName = "Amazon DynamoDB";
                string threadSubject = "DynamoDB Thread 1";
                // Sample queries.
                await FindRepliesInLast15DaysAsync(context, forumName, threadSubject);
              //  await FindRepliesPostedWithinTimePeriodAsync(context, forumName, threadSubject);
 
                // Scan table.
             //await FindProductsPricedLessThanZeroAsync(context);
               
            }
            catch (AmazonDynamoDBException e) { Console.WriteLine(e.Message); }
            catch (AmazonServiceException e) { Console.WriteLine(e.Message); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        private static async Task<Book> GetBook(DynamoDBContext context, int productId)
        {
            Book bookItem = await context.LoadAsync<Book>(productId);

            Console.WriteLine("\nGetBook: Printing result.....");
            Console.WriteLine("Title: {0} \n No.Of threads:{1} \n No. of messages: {2}",
                      bookItem.Title, bookItem.ISBN, bookItem.PageCount);
                      return bookItem;
        }

        private static async Task FindRepliesInLast15DaysAsync(DynamoDBContext context,
                                string forumName,
                                string threadSubject)
        {
            string replyId = forumName + "#" + threadSubject;
            DateTime twoWeeksAgoDate = new DateTime(2015,09,21) - TimeSpan.FromDays(2);
     
            var latestRepliesserch =
                context.QueryAsync<Reply>(replyId, QueryOperator.GreaterThan,new List<object>{twoWeeksAgoDate});
            Console.WriteLine("\nFindRepliesInLast15Days: Printing result.....");
            var latestReplies = await latestRepliesserch.GetNextSetAsync();
            foreach (Reply r in latestReplies)
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", r.Id, r.PostedBy, r.Message, r.ReplyDateTime);
        }

        private static async Task FindRepliesPostedWithinTimePeriodAsync(DynamoDBContext context,
                                      string forumName,
                                      string threadSubject)
        {
            string forumId = forumName + "#" + threadSubject;
            Console.WriteLine("\nFindRepliesPostedWithinTimePeriod: Printing result.....");

            DateTime startDate =new DateTime(2015,09,01); // DateTime.UtcNow - TimeSpan.FromDays(30);
            DateTime endDate =new DateTime(2015,09,21);// DateTime.UtcNow - TimeSpan.FromDays(1);
            // QueryFilter sc= new QueryFilter();
            // sc.AddCondition("forumId", QueryOperator.Between, startDate, endDate );
            // DynamoDBOperationConfig config = new DynamoDBOperationConfig();
           // config.QueryFilter.Add(sc);
            var repliesInAPeriodsearch =context.QueryAsync<Reply>(forumId,QueryOperator.Between,new List<object>{startDate,endDate});
            var repliesInAPeriod = await repliesInAPeriodsearch.GetNextSetAsync();
            foreach (Reply r in repliesInAPeriod)
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", r.Id, r.PostedBy, r.Message, r.ReplyDateTime);
        }

        private static async Task FindProductsPricedLessThanZeroAsync(DynamoDBContext context)
        {
            int price = 200;
            var scanConditions = new List<ScanCondition>() {
                 new ScanCondition("Price", ScanOperator.LessThan, price),
                new ScanCondition("ProductCategory", ScanOperator.Equal, "Book")
              };
            var searchResults = context.ScanAsync<Book>(scanConditions);
            var itemsWithWrongPrice = await searchResults.GetNextSetAsync();
            Console.WriteLine("\nFindProductsPricedLessThanZero: Printing result.....");
            foreach (Book r in itemsWithWrongPrice)
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", r.Id, r.Title, r.Price, r.ISBN);
        }
    }

    [DynamoDBTable("Reply")]
    public class Reply
    {
        [DynamoDBHashKey] //Partition key
        public string Id
        {
            get; set;
        }

        [DynamoDBRangeKey] //Sort key
        public DateTime ReplyDateTime
        {
            get; set;
        }

        // Properties included implicitly.
        public string Message
        {
            get; set;
        }
        // Explicit property mapping with object persistence model attributes.
        [DynamoDBProperty("LastPostedBy")]
        public string PostedBy
        {
            get; set;
        }
        // Property to store version number for optimistic locking.
        [DynamoDBVersion]
        public int? Version
        {
            get; set;
        }
    }

    [DynamoDBTable("Thread")]
    public class Thread
    {
        // Partition key mapping.
        [DynamoDBHashKey] //Partition key
        public string ForumName
        {
            get; set;
        }
        [DynamoDBRangeKey] //Sort key
        public DateTime Subject
        {
            get; set;
        }
        // Implicit mapping.
        public string Message
        {
            get; set;
        }
        public string LastPostedBy
        {
            get; set;
        }
        public int Views
        {
            get; set;
        }
        public int Replies
        {
            get; set;
        }
        public bool Answered
        {
            get; set;
        }
        public DateTime LastPostedDateTime
        {
            get; set;
        }
        // Explicit mapping (property and table attribute names are different).
        [DynamoDBProperty("Tags")]
        public List<string> KeywordTags
        {
            get; set;
        }
        // Property to store version number for optimistic locking.
        [DynamoDBVersion]
        public int? Version
        {
            get; set;
        }
    }

    [DynamoDBTable("Forum")]
    public class Forum
    {
        [DynamoDBHashKey]
        public string Name
        {
            get; set;
        }
        // All the following properties are explicitly mapped
        // to show how to provide mapping.
        [DynamoDBProperty]
        public int Threads
        {
            get; set;
        }
        [DynamoDBProperty]
        public int Views
        {
            get; set;
        }
        [DynamoDBProperty]
        public string LastPostBy
        {
            get; set;
        }
        [DynamoDBProperty]
        public DateTime LastPostDateTime
        {
            get; set;
        }
        [DynamoDBProperty]
        public int Messages
        {
            get; set;
        }
    }

    [DynamoDBTable("ProductCatalog")]
    public class Book
    {
        [DynamoDBHashKey] //Partition key
        public int Id
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }
        public string ISBN
        {
            get; set;
        }
        public int Price
        {
            get; set;
        }
        public string PageCount
        {
            get; set;
        }
        public string ProductCategory
        {
            get; set;
        }
        public bool InPublication
        {
            get; set;
        }
    }
}
