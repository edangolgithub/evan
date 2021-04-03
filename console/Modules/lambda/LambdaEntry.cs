using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Model;
using Newtonsoft.Json;

namespace console
{
    public class LambdaEntry
    {
        public static async Task RunAsync()
        {
            AmazonLambdaClient client = new AmazonLambdaClient(RegionEndpoint.USEast1);
            InvokeRequest ir = new InvokeRequest
            {
                FunctionName = "lambdaonly",
                InvocationType = InvocationType.RequestResponse,
                Payload = "\"hello\""
            };

            InvokeResponse response = await Task.Run(() => client.InvokeAsync(ir));
            var sr = new StreamReader(response.Payload);
            JsonReader reader = new JsonTextReader(sr);

            var serilizer = new JsonSerializer();
            var op = serilizer.Deserialize(reader);

            Console.WriteLine(op);
            Console.ReadLine();
        }
    }
}