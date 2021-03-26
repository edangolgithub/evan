import pymongo

import urllib.parse
# client = pymongo.MongoClient("mongodb://edangol:97ni@123Ui@<hostname>/myFirstDatabase?ssl=true&replicaSet=atlas-bzvhix-shard-0&authSource=admin&retryWrites=true&w=majority")

client = pymongo.MongoClient("mongodb+srv://edangol:"+urllib.parse.quote_plus('97ni@123Ui')+"@cluster0.xrtz3.mongodb.net/myFirstDatabase?retryWrites=true&w=majority")
db = client.test
mydb = client["myFirstDatabase"]

mycol = mydb["customers"]
mydict = { "name": "John", "address": "Highway 37" }

x = mycol.insert_one(mydict)
y = mycol.find_one()

print(y)