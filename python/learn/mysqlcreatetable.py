import mysql.connector

mydb = mysql.connector.connect(
  host="evandb.cjiabok62vq8.us-east-1.rds.amazonaws.com",
  user="mysqluser",
  password="mysqlpassword",
  database="pythonlearn"
)

mycursor = mydb.cursor()

mycursor.execute("CREATE TABLE if not exists customers (name VARCHAR(255), address VARCHAR(255))")
mycursor.execute("ALTER TABLE customers ADD COLUMN id INT AUTO_INCREMENT PRIMARY KEY")