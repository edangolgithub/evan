using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace console
{
    static class CreateDynamodb
    {
        static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        static string tablename = "movies";
        public static async Task RunAsync()
        {
              await CreateTable();
        }
        public static async Task CreateTable()
        {
            var request = new CreateTableRequest
            {
                TableName = tablename,
                ProvisionedThroughput = new ProvisionedThroughput(1, 1),
                AttributeDefinitions = new List<AttributeDefinition>{
                   new AttributeDefinition
      {
        AttributeName = "id",
        AttributeType = "S"
      },
      new AttributeDefinition
      {
        AttributeName = "created",
        AttributeType = "S"
      }
               },
                KeySchema = new List<KeySchemaElement>
    {
      new KeySchemaElement
      {
        AttributeName = "id",
        KeyType = "HASH"
      },
      new KeySchemaElement
      {
        AttributeName = "created",
        KeyType = "RANGE"
      }
    }
            };
            var ctable =await client.CreateTableAsync(request);
            Console.WriteLine(ctable);
        }
    }
}