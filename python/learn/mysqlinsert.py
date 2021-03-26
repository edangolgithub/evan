import mysql.connector

mydb = mysql.connector.connect(
  host="evandb.cjiabok62vq8.us-east-1.rds.amazonaws.com",
  user="mysqluser",
  password="mysqlpassword",
  database="pythonlearn"
)

mycursor = mydb.cursor()

sql = "INSERT INTO customers (name, address) VALUES (%s, %s)"
val = ("John", "Highway 21")
mycursor.execute(sql, val)

mydb.commit()

print(mycursor.rowcount, "record inserted.")
