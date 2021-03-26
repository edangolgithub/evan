import mysql.connector

mydb = mysql.connector.connect(
  host="evandb.cjiabok62vq8.us-east-1.rds.amazonaws.com",
  user="mysqluser",
  password="mysqlpassword")

print(mydb)