using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace console
{
    class loadjson
    {
        static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        static string tablename = "movies";
        public static async Task RunAsync()
        {
           // await readjsonasync();
           await insertjsonasync();
        }
        public static async Task readjsonasync(string filePath = "dta.json")
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = await r.ReadToEndAsync();
                // dynamic array = JsonConvert.DeserializeObject(json);
                // foreach (var item in array)
                // {
                //     Console.WriteLine("{0}",item);

                // }

                Item item = (Item)JsonConvert.DeserializeObject<Item>(json);
                Console.WriteLine(item.info.rank);

            }

        }
        public static async Task insertjsonasync(string filePath = "dta.json")
        {
            try
            {
                Table table = Table.LoadTable(client, "movies");
                using (StreamReader r = new StreamReader(filePath))
                {
                    //  var jtr = new JsonTextReader(r);
                    // var  array = (JArray)await JToken.ReadFromAsync(jtr);
                    string x = await r.ReadToEndAsync();
                    Document doc = Document.FromJson(x);
                    var putItem = await table.PutItemAsync(doc);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
    public class Info
    {
        public List<string> directors { get; set; }      
        public double rating { get; set; }
        public List<string> genres { get; set; }
        public string image_url { get; set; }
        public string plot { get; set; }
        public int rank { get; set; }
        public int running_time_secs { get; set; }
        public List<string> actors { get; set; }
    }

    public class Item
    {
          public DateTime created { get; set; }
        public int evanid { get; set; }
        public string title { get; set; }
        public Info info { get; set; }
    }
}