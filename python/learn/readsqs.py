import boto3  
# Get the service resource
sqs = boto3.resource('sqs')

# Get the queue. This returns an SQS.Queue instance
queue = sqs.get_queue_by_name(QueueName='test')

# You can now access identifiers and attributes
print(queue.url)
print(queue.attributes.get('DelaySeconds'))
for queue in sqs.queues.all():
    print(queue.url)

for message in queue.receive_messages():      
    # Print out the body and author (if set)
    print('Hello, {0}!'.format(message.body))
