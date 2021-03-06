const AWS = require('aws-sdk');

AWS.config.update({
    region: "us-east-1"

});

const db = new AWS.DynamoDB.DocumentClient();
const table = "evantable";


async function querydynamodb() {
    let params = {
      TableName: table,
      KeyConditionExpression: 'evanid = :id ',
      ExpressionAttributeValues: { ':id': '2011' },

    }
  
    try {
      let result = await db.query(params).promise()
      console.log(result)
    } catch (error) {
      console.error(error)
    }
  }
  querydynamodb();