a = """Lorem ipsum dolor sit amet,
consectetur adipiscing elit,
sed do eiusmod tempor incididunt
ut labore et dolore magna aliqua."""
#print(a)

txt = "The best things in life are free!"
#print("free" in txt)

txt = "The best things in life are free!"
#print("expensive" not in txt)

b = "Hello, World!"
# print(b[2:5]) # llo
# print(b[:5])  # , World

# print(b[-5:-2]) # orl

a = "Hello, World!"
print(a.upper())
print(a.strip()) # returns "Hello, World!"
print(a.replace("H", "J"))
print(a.split(",")) # returns ['Hello', ' World!']


quantity = 3
itemno = 567
price = 49.95
myorder = "I want {} pieces of item {} for {} dollars."
print(myorder.format(quantity, itemno, price))