using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace emptyservice
{
    public class Function
    {


         public APIGatewayProxyResponse FunctionHandler()
        {
            return new APIGatewayProxyResponse
            {
                Headers = new Dictionary<string, string> {
                     { "Content-Type", "text/plain" },
                      { "Access-Control-Allow-Origin", "*" },
                       { "Access-Control-Allow-Methods", "OPTIONS,POST,GET,PUT" }

                      },
                      Body="hello world",
                      StatusCode = (int)HttpStatusCode.OK,
            };
        }
    }
}
