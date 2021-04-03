using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace console
{
    class EvanSms
    {
        public static async Task RunAsync()
        {
            try
            {
              await SendSms();
            }
            catch (Exception)
            {
                throw;
            }
        }
        static async Task SendSms()
        {
            AmazonSimpleNotificationServiceClient snsClient = new AmazonSimpleNotificationServiceClient(Amazon.RegionEndpoint.USWest2);
            PublishRequest pubRequest = new PublishRequest();
            pubRequest.Message = "My SMS message test by evan";
            pubRequest.PhoneNumber = "+9779849178036";
            // add optional MessageAttributes, for example:
            //   pubRequest.MessageAttributes.Add("AWS.SNS.SMS.SenderID", new MessageAttributeValue
            //      { StringValue = "SenderId", DataType = "String" });
            PublishResponse pubResponse =await snsClient.PublishAsync(pubRequest);
            Console.WriteLine(pubResponse.MessageId);
        }
    }
}