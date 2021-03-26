const AWS = require('aws-sdk');

AWS.config.update({
    region: "us-east-1"

});

const db = new AWS.DynamoDB.DocumentClient();
const table = "evantable";

async function scanitem() {
    var param = {
        TableName: table
    }
    var x = db.scan(param,
        function (error, data) {
            if (error) {
                console.log(error.message);
            }
            else {
                console.log(data);
            }
        }
    ).promise();
}
scanitem();