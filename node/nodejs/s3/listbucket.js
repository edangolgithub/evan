'use strict';
var AWS = require('aws-sdk');
var s3 = new AWS.S3();  // 1
module.exports.listbuck = (event, context, callback) => {
  var params = {
    Bucket: 'evandangol',  // 2
  };
 // s3.listObjectsV2(params, function(err, data) {  // 3
 s3.listObjectsV2(params, function(err, data) {  // 3
    if (err) {
      console.log(err, err.stack);
    } else {
     // console.log(data["Contents"])
     var folderarr=[];
     data["Contents"].forEach(x=>{
      // console.log(x.Key)
      folderarr.push(x.Key)
     })
      const response = {
        statusCode: 200,
        // body: JSON.stringify({
        //   "bucket_list": data // 4
        // })
        body:folderarr
      };
      callback(null, response);
    }
  });
};