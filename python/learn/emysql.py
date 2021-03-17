import mysql.connector

# mydb = mysql.connector.connect(
#   host="evandb.cjiabok62vq8.us-east-1.rds.amazonaws.com",
#   user="mysqluser",
#   password="mysqlpassword"
# )

# print(mydb)


mydb = mysql.connector.connect(
  host="evandb.cjiabok62vq8.us-east-1.rds.amazonaws.com",
  user="mysqluser",
  password="mysqlpassword"
)

# mycursor = mydb.cursor()
# mycursor.execute("CREATE DATABASE pythonlearn")



mycursor = mydb.cursor()
mycursor.execute("SHOW DATABASES")

for x in mycursor:
  print(x)