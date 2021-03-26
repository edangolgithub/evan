import boto3           # no error
client = boto3.client('lambda',region_name='us-east-1')
response = client.list_functions()
for fun in response["Functions"]:
 print(fun["FunctionName"])
exit()