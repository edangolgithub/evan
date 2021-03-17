import re

txt = "The rain in Spain"
#Find all lower case characters alphabetically between "a" and "m":
x = re.findall("[a-m]", txt)
print(x)

txt = "That will be 59 dollars"
#Find all digit characters:
x = re.findall("\d", txt)
print(x)
for a in x:
    print(int(a)+1)

txt = "hello world"
#Search for a sequence that starts with "he", followed by two (any) characters, and an "o":
x = re.findall("he..o", txt)
print(x)

try:
 x=5/0
except:
  print("Something went wrong")
finally:
  print("The 'try except' is finished")

