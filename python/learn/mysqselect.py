import mysql.connector

mydb = mysql.connector.connect(
  host="evandb.cjiabok62vq8.us-east-1.rds.amazonaws.com",
  user="mysqluser",
  password="mysqlpassword",
  database="pythonlearn"
)

mycursor = mydb.cursor()
mycursor.execute("SELECT * FROM customers")

myresult = mycursor.fetchall()

for x in myresult:
  print(x)
