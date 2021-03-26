const AWS = require('aws-sdk');

AWS.config.update({
    region: "us-east-1"

});

const db = new AWS.DynamoDB.DocumentClient();
const table = "evantable";

async function insertitem() {
    var params = {
        TableName: table,
        Item: {
            "evanid": "2011",
            "created": new Date().toISOString()
        }
    }
    var res = db.put(params, (er, data) => {
        er ? console.log(er) : console.log(data)
    }).promise();
   return res;
}
insertitem();