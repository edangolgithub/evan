const AWS = require('aws-sdk');

AWS.config.update({
    region: "us-east-1"

});
const db = new AWS.DynamoDB.DocumentClient();
const table = "evantable";

async function updatedynamodb() {
    const params = {
        TableName: table,
        Key: {
            evanid: '2001',
            created: "2021-03-02T15:50:54.203Z"
        },
        UpdateExpression: 'set #na = :n',
        ExpressionAttributeValues: {
            ':n': "evan"
        },
        ExpressionAttributeNames: {
            "#na": "name"
        },
        ReturnValues: 'UPDATED_NEW'
    }

    try {
        await db.update(params).promise()
    } catch (error) {
        console.error(error)
    }
}

updatedynamodb();