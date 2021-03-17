```
Load the Sample Data into DynamoDB Tables
To load the ProductCatalog table with data, enter the following command.

aws dynamodb batch-write-item --request-items file://forumdynamo/ProductCatalog.json

To load the Forum table with data, enter the following command.

aws dynamodb batch-write-item --request-items file://forumdynamo/Forum.json

To load the Thread table with data, enter the following command.

aws dynamodb batch-write-item --request-items file://forumdynamo/Thread.json

To load the Reply table with data, enter the following command.

aws dynamodb batch-write-item --request-items file://forumdynamo/Reply.json
```