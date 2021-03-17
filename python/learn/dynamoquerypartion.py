import boto3
from boto3.dynamodb.conditions import Key

TABLE_NAME = "evantable"

# Creating the DynamoDB Client
dynamodb_client = boto3.client('dynamodb', region_name="us-east-1")

# Creating the DynamoDB Table Resource
dynamodb = boto3.resource('dynamodb', region_name="us-east-1")
table = dynamodb.Table(TABLE_NAME)
# Use the DynamoDB Table resource get item method to get a single item
# Use the DynamoDB client to query for all songs by artist Arturus Ardvarkian
response = dynamodb_client.query(
    TableName=TABLE_NAME,
    KeyConditionExpression='evanid = :evanid',
    ExpressionAttributeValues={
        ':evanid': {'S': '2001'}
    }
)
print(response['Items'])