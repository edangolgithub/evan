import boto3           # no error
s3 = boto3.resource('s3')
for bucket in s3.buckets.all():
 print(bucket.name)
exit()

#sudo chmod 2775 /home/ec2-user/evan/nginx && find /home/ec2-user/evan/nginx -type d -exec sudo chmod 2775 {} \;
find /home/ec2-user/evan/nginx -type f -exec sudo chmod 0664 {} \;