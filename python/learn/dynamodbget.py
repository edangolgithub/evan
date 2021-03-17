import boto3
from boto3.dynamodb.conditions import Key

TABLE_NAME = "evantable"

# Creating the DynamoDB Client
dynamodb_client = boto3.client('dynamodb', region_name="us-east-1")

# Creating the DynamoDB Table Resource
dynamodb = boto3.resource('dynamodb', region_name="us-east-1")
table = dynamodb.Table(TABLE_NAME)
# Use the DynamoDB Table resource get item method to get a single item
response = table.get_item(
    Key={
        'evanid': '2001',
        'created': '2021-03-04T13:50:31.709799'
    }
)
print(response['Item'])

# The Table resource's response looks like this:
# {
#  'artist': 'Arturus Ardvarkian',
#  'id': 'dbea9bd8-fe1f-478a-a98a-5b46d481cf57',
#  'priceUsdCents': '161',
#  'publisher': 'MUSICMAN INC',
#  'song': 'Carrot Eton'
# }